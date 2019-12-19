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
                errs => EExitCode.INVALID_PARAMS);



            Console.WriteLine($"Dotnet.Versions {versionString} finished");
            return (int)result;
        }

        private static EExitCode CreateModifier(ModOptions source, out Modifier result)
        {
            result = new Modifier();
            var ret = EExitCode.SUCCESS;

            result.Semver = source.Semver;
            result.SemverPattern = source.SemverPattern;

            if (source.Version == null)
            {
                return ret;
            }
            try
            {
                var temp = source.Version.Split('.');
                int num;
                if (int.TryParse(temp[0], out num))
                {
                    result.Major = num;
                }

                if (int.TryParse(temp[1], out num))
                {
                    result.Minor = num;
                }

                if (int.TryParse(temp[2], out num))
                {
                    result.Patch = num;
                }
            }
            catch (Exception)
            {
                return EExitCode.INVALID_PARAMS;
            }

            return ret;


        }

        /// <summary>
        /// sets the new versions in all files
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        static private EExitCode SetVersions(SetOptions options)
        {
            EExitCode ret = EExitCode.INVALID_FILE;
            if (options.isSolution)
            {
                // get all csproj file paths and iterate
                var paths = FileUtil.GetCsprojFilepaths(options.Filename);
                foreach (var item in paths)
                {
                    ret = SetVersion(item, options);
                    if (EExitCode.SUCCESS != ret)
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
        static private EExitCode SetVersion(string path, SetOptions options)
        {
            // read file
            EExitCode ret = FileUtil.ReadFile(path, out var content);
            if (ret != EExitCode.SUCCESS)
            {
                return ret;
            }

            // set versions            
            var fileVersion = RegexUtil.GetNonSemverVersion(options.Version);

            // special treatment: version = 0.0.0
            ContentUtil.ReplaceContent("Version", options.Version, content);
            ContentUtil.ReplaceContent("AssemblyVersion", fileVersion, content);
            ContentUtil.ReplaceContent("FileVersion", fileVersion + ".0", content);

            // write file
            ret = FileUtil.WriteFile(path, content);

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        static private EExitCode ModifyVersions(ModOptions options)
        {
            var ret = CreateModifier(options, out var modifier);
            if (EExitCode.SUCCESS != ret)
            {
                return ret;
            }

            if (options.isSolution)
            {
                var paths = FileUtil.GetCsprojFilepaths(options.Filename);
                foreach (var item in paths)
                {
                    ret = ModifyVersion(item, modifier);
                    if (EExitCode.SUCCESS != ret)
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
        static private EExitCode ModifyVersion(string path, Modifier options)
        {
            // read file
            EExitCode ret = FileUtil.ReadFile(path, out var content);
            if (EExitCode.SUCCESS != ret)
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
