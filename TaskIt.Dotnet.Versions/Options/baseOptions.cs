using CommandLine;

namespace TaskIt.Dotnet.Versions.Options
{
    /// <summary>
    /// Base Class with the common options
    /// </summary>
    class BaseOptions
    {
        /// <summary>
        /// Source File name
        /// </summary>
        [Option('f', "folder", Required = false, HelpText = "path to the solution or project directory")]
        public string Filename { get; set; }

        /// <summary>
        /// Flag indicating a solution or project File - computed
        /// </summary>
        [Option('b', "backup", Required = false, HelpText = "create backup file")]
        public bool Backup { get; set; } = false;

    }
}
