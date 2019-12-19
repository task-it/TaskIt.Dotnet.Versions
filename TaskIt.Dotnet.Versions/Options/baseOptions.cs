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
        [Option('s', "source", Required = true, HelpText = "solution or project file")]
        public string Filename { get; set; }

        /// <summary>
        /// Flag indicating a solution or project File<br/>
        /// computed
        /// </summary>
        public bool isSolution { get { return Filename.EndsWith(".sln"); } }
    }
}
