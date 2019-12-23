using CommandLine;
using System;
using System.Reflection;
using TaskIt.Dotnet.Versions.Options;
using TaskIt.Dotnet.Versions.Types;

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
            Result ret = null;
            if (options.isSolution)
            {
                // get all csproj file paths and iterate
                var paths = FileUtil.GetCsprojFilepaths(options.Filename);
                foreach (var item in paths)
                {
                    ret = SetVersion(item, options);
                    if (EExitCode.SUCCESS != ret?.Code)
                    {
                        break;
                    }
                }

            }
            else
            {
                ret = SetVersion(options.Filename, options);
            }
            return ret;
        }

        /// <summary>
        /// sets the new version in one file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        static private Result SetVersion(string path, SetOptions options)
        {
            // read file
            var ret = FileUtil.ReadFile(path, out var content);
            if (ret.Code != EExitCode.SUCCESS)
            {
                return ret;
            }

            // set versions            
            var modifier = new Modifier(options.Version);

            ContentUtil.ReplaceContent(content, "Version", modifier);
            ContentUtil.ReplaceContent(content, "AssemblyVersion", modifier);
            ContentUtil.ReplaceContent(content, "FileVersion", modifier);

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
        static private Result ModifyVersion(string path, Modifier options)
        {
            // read file
            var ret = FileUtil.ReadFile(path, out var content);
            if (EExitCode.SUCCESS != ret?.Code)
            {
                return ret;
            }

            // modify content
            ContentUtil.AdjustContent("Version", options, content, true);
            ContentUtil.AdjustContent("AssemblyVersion", options, content, false);
            ContentUtil.AdjustContent("FileVersion", options, content, false);

            // save file
            ret = FileUtil.WriteFile(path, content);

            return ret;
        }





    }
}
