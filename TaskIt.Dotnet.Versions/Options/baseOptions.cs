using CommandLine;
using System;

namespace TaskIt.Dotnet.Versions.Options
{
    /// <summary>
    /// Base Class with the common options
    /// </summary>
    public class BaseOptions
    {
        /// <summary>
        /// Source File name
        /// </summary>
        [Option('f', "folder", Required = false, HelpText = "path to the solution or project directory")]
        public string Filename { get; set; } = Environment.CurrentDirectory;

        /// <summary>
        /// Flag indicating a solution or project File - computed
        /// </summary>
        [Option('b', "backup", Required = false, HelpText = "create backup file")]
        public bool Backup { get; set; } = false;

    }
}
