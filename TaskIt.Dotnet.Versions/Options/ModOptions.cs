using CommandLine;

namespace TaskIt.Dotnet.Versions.Options
{
    [Verb("mod", HelpText = "increments versions by pattern")]
    class ModOptions : BaseOptions
    {
        [Option('v', "version", Required = true, HelpText = "the pattern to modify the original version. Wildcards ('*') are possible and will nnot modify the original version.")]
        public string Version { get; set; }

        [Option('p', "semverpattern", Required = false, HelpText = "regex of the semver revision to modify")]
        public string SemverPattern { get; set; }

        [Option('m', "semvermodifier", Required = false, HelpText = "modifier for the semver Part of the version")]
        public int? Semver { get; set; }

    }
}
