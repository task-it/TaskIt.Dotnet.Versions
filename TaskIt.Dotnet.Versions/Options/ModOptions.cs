using CommandLine;

namespace TaskIt.Dotnet.Versions.Options
{
    [Verb("mod", HelpText = "increments versions by pattern")]
    class ModOptions : BaseOptions
    {
        [Option('a', "major", Required = false, HelpText = "modification of the major revision")]
        public int? Major { get; set; }

        [Option('i', "minor", Required = false, HelpText = "modification of the minor revision")]
        public int? Minor { get; set; }

        [Option('t', "patch", Required = false, HelpText = "modification of the patch revision")]
        public int? Patch { get; set; }

        [Option('r', "semverpattern", Required = false, HelpText = "pattern of the semver revision")]
        public string SemverPattern { get; set; }

        [Option('v', "semver", Required = false, HelpText = "pattern of the sermver revision")]
        public int? semver { get; set; }

    }
}
