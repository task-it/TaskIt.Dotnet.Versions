using CommandLine;

namespace TaskIt.Dotnet.Versions.Options
{
    /// <summary>
    /// holds the Parameters, needed for the tool to operate
    /// </summary>
    [Verb("set", HelpText = "sets versions to fixed values")]
    class SetOptions : BaseOptions
    {

        [Option('v', "newVersion", Required = true, HelpText = "New Version to set. Wildcards ('*') are possible.")]
        public string Version { get; set; }


    }
}
