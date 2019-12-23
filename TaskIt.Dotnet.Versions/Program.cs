using CommandLine;
using System;
using System.Reflection;
using TaskIt.Dotnet.Versions.Options;
using TaskIt.Dotnet.Versions.Types;
using TaskIt.Dotnet.Versions.Util;

namespace TaskIt.Dotnet.Versions
{
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
                errs => new Result(EExitCode.INVALID_PARAMS, ""));


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
            var modifier = new Modifier(options.Version);
            Result ret = null;

            if (options.isSolution)
            {
                // get all csproj file paths and iterate
                var paths = FileUtil.GetCsprojFilepaths(options.Filename);
                foreach (var item in paths)
                {
                    ret = SetVersion(item, modifier);
                    if (ret != null)
                    {
                        break;
                    }
                }

            }
            else
            {
                ret = SetVersion(options.Filename, modifier);
            }
            return ret;
        }

        /// <summary>
        /// sets the new version in one file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        static private Result SetVersion(string path, Modifier modifier)
        {
            // read file
            var ret = FileUtil.ReadFile(path, out var content);
            if (ret != null)
            {
                return ret;
            }

            // set versions            
            ContentUtil.ReplaceTag(content, "Version", modifier, true);
            ContentUtil.ReplaceTag(content, "AssemblyVersion", modifier, false);
            ContentUtil.ReplaceTag(content, "FileVersion", modifier, false);

            // write file
            ret = FileUtil.WriteFile(path, content);

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        static private Result ModifyVersions(ModOptions options)
        {
            Result ret = null;
            var modifier = new Modifier(options.Version, options.SemverPattern, options.Semver);
            if (options.isSolution)
            {
                var paths = FileUtil.GetCsprojFilepaths(options.Filename);
                foreach (var item in paths)
                {
                    ret = ModifyVersion(item, modifier);
                    if (EExitCode.SUCCESS != ret?.Code)
                    {
                        break;
                    }
                }
            }
            else
            {
                ret = ModifyVersion(options.Filename, modifier);
            }
            return ret;
        }

        /// <summary>
        /// modifies the Version in one file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        static private Result ModifyVersion(string path, Modifier modifier)
        {
            // read file
            var ret = FileUtil.ReadFile(path, out var content);
            if (ret != null)
            {
                return ret;
            }

            // modify content
            ContentUtil.ModifyTag(content, "Version", modifier, true);
            ContentUtil.ModifyTag(content, "AssemblyVersion", modifier, false);
            ContentUtil.ModifyTag(content, "FileVersion", modifier, false);

            // save file
            ret = FileUtil.WriteFile(path, content);

            return ret;
        }





    }
}
