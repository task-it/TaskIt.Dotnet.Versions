using CommandLine;

namespace TaskIt.Dotnet.Versions.Options
{
    [Verb("mod", HelpText = "increments versions by pattern")]
    class ModOptions : BaseOptions
    {
        [Option('v', "newVersion", Required = false, HelpText = "New Version to set. Wildcards ('*') are possible.")]
        public string Version { get; set; }

        [Option('p', "semverpattern", Required = false, HelpText = "pattern of the semver revision to modify")]
        public string SemverPattern { get; set; }

        [Option('m', "semvermodifier", Required = false, HelpText = "modifier for the semver Part of the version")]
        public int? Semver { get; set; }

    }
}
