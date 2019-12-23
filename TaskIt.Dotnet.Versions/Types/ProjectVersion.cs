using System;
using System.Text.RegularExpressions;

namespace TaskIt.Dotnet.Versions.Types
{
    public class ProjectVersion
    {

        // regex pattern for semantiv versions.
        private const string _semverPattern = @"^(?<major>0|[1-9]\d*)\.(?<minor>0|[1-9]\d*)\.(?<patch>0|[1-9]\d*)(?:-(?<prerelease>(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+(?<buildmetadata>[0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$";
        private const string _filePattern = @"^(?<major>0|[1-9]\d*)\.(?<minor>0|[1-9]\d*)\.(?<patch>0|[1-9]\d*)\.(?<file>0|[1-9]\d*)";

        public int? Major { get; internal set; }
        public int? Minor { get; internal set; }
        public int? Patch { get; internal set; }

        public int? File { get; internal set; }

        public string Prerelease { get; internal set; }
        public string Buildmetadata { get; internal set; }
        public bool IsFileVersion { get; internal set; } = false;

        internal Match _match;

        internal string _source;



        private int? ParseMatch(Match match, string group)
        {
            int? ret = new Nullable<int>();
            if (match.Groups[group].Success && int.TryParse(match.Groups[group].Value, out int num))
            {
                ret = num;
            }
            return ret;
        }



        public string NonSemanticVersion
        {
            get
            {
                var ret = $"{ Major.Value}.{ Minor.Value}.{ Patch.Value}";
                if (IsFileVersion)
                {
                    ret += $".{ File.Value}";
                }
                return ret;
            }
        }

        public string SemanticVersion
        {
            get
            {
                var ret = string.Empty;
                if (!string.IsNullOrEmpty(Prerelease))
                {
                    ret += $"-{Prerelease}";
                }
                if (!string.IsNullOrEmpty(Buildmetadata))
                {
                    ret += $"+{Buildmetadata}";
                }
                return ret;
            }
        }

        public string FullVersion
        {
            get
            {
                var ret = NonSemanticVersion;

                if (!string.IsNullOrEmpty(Prerelease))
                {
                    ret += $"-{Prerelease}";
                }
                if (!string.IsNullOrEmpty(Buildmetadata))
                {
                    ret += $"+{Buildmetadata}";
                }
                return ret;
            }
        }

        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="version"></param>
        public ProjectVersion(string version)
        {
            Init(version);
        }

        internal void Init(String version)
        {
            _source = version;
            var regex = new Regex(_semverPattern);
            _match = regex.Match(_source);

            if (!_match.Success)
            {
                regex = new Regex(_filePattern);
                _match = regex.Match(_source);
                if (!_match.Success)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(version)}, VersionString: {version}");
                }
            }

            Major = ParseMatch(_match, "major");
            Minor = ParseMatch(_match, "minor");
            Patch = ParseMatch(_match, "patch");
            File = ParseMatch(_match, "file");
            if (File.HasValue)
            {
                IsFileVersion = true;
            }

            Prerelease = _match.Groups["prerelease"].Value;
            Buildmetadata = _match.Groups["buildmetadata"].Value;
        }



    }
}

