using CommandLine;
using System;
using System.Reflection;
using TaskIt.Dotnet.Versions.Options;
using TaskIt.Dotnet.Versions.Types;
using TaskIt.Dotnet.Versions.Util;

namespace TaskIt.Dotnet.Versions
{
    /// <summary>
    /// Executable
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Constructor
        /// </summary>
        private Program()
        {
            // do nothing, just hide Construction

        }

        /// <summary>
        /// Main Method
        /// </summary>
        /// <param name="args"></param>
        static public int Main(string[] args)
        {
            var versionString = Assembly.GetEntryAssembly()
                                        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                        .InformationalVersion
                                        .ToString();

            Console.WriteLine($"Dotnet.Versions {versionString} start...");

            var result = Parser.Default.ParseArguments<SetOptions, ModOptions>(args).MapResult(
                (SetOptions opts) => SetVersions(opts),
                (ModOptions opts) => ModifyVersions(opts),
                errs => new Result(EExitCode.SUCCESS, ""));


            if (result != null)
            {
                Console.WriteLine($"ERROR: {result.Code} {result.Message}");
            }

            Console.WriteLine($"Dotnet.Versions {versionString} finished");
            return result == null ? (int)EExitCode.SUCCESS : (int)result.Code;
        }



        /// <summary>
        /// sets the new versions in all files
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        static private Result SetVersions(SetOptions options)
        {
            Result ret = null;
            CheckPath(options);
            var modifier = new Modifier(options.Version);

            if (options.IsSolution)
            {
                // get all csproj file paths and iterate
                var paths = FileUtil.GetCsprojFilepaths(options.Filename);
                foreach (var item in paths)
                {
                    ret = SetVersion(item, modifier, options);
                    if (ret != null)
                    {
                        break;
                    }
                }

            }
            else
            {
                ret = SetVersion(options.Filename, modifier, options);
            }
            return ret;
        }

        /// <summary>
        /// sets the new version in one file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="modifier"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        static private Result SetVersion(string path, Modifier modifier, BaseOptions options)
        {
            // read file
            var ret = FileUtil.ReadFile(path, out var content);
            if (ret != null)
            {
                return ret;
            }

            // set versions            
            var version = ContentUtil.ReplaceTag(content, "Version", modifier, true);
            var assembly = ContentUtil.ReplaceTag(content, "AssemblyVersion", modifier, false);
            var file = ContentUtil.ReplaceTag(content, "FileVersion", modifier, false);

            // if something was modified
            if (version || assembly || file)
            {
                // create backup - if specified
                if (options.Backup)
                {
                    FileUtil.CreateBackup(path);
                }
                // write file
                ret = FileUtil.WriteFile(path, content);
            }
            return ret;
        }

        /// <summary>
        /// modifies all files
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        static private Result ModifyVersions(ModOptions options)
        {
            Result ret = null;
            CheckPath(options);
            var modifier = new Modifier(options.Version, options.SemverPattern, options.Semver);
            if (options.IsSolution)
            {
                var paths = FileUtil.GetCsprojFilepaths(options.Filename);
                foreach (var item in paths)
                {
                    ret = ModifyVersion(item, modifier, options);
                    if (EExitCode.SUCCESS != ret?.Code)
                    {
                        break;
                    }
                }
            }
            else
            {
                ret = ModifyVersion(options.Filename, modifier, options);
            }
            return ret;
        }

        /// <summary>
        /// modifies the Version in one file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="modifier"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        static private Result ModifyVersion(string path, Modifier modifier, BaseOptions options)
        {
            // read file
            var ret = FileUtil.ReadFile(path, out var content);
            if (ret != null)
            {
                return ret;
            }

            // modify content
            var version = ContentUtil.ModifyTag(content, "Version", modifier, true);
            var assembly = ContentUtil.ModifyTag(content, "AssemblyVersion", modifier, false);
            var file = ContentUtil.ModifyTag(content, "FileVersion", modifier, false);

            // if something was modified
            if (version || assembly || file)
            {
                // create backup - if specified
                if (options.Backup)
                {
                    FileUtil.CreateBackup(path);
                }
                // write file
                ret = FileUtil.WriteFile(path, content);
            }


            return ret;
        }

        private static void CheckPath(BaseOptions options)
        {
            if (string.IsNullOrEmpty(options.Filename))
            {
                options.Filename = FileUtil.GetPath();

            }
        }



    }
}
