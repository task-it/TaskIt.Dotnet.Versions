using System;
using System.Text.RegularExpressions;

namespace TaskIt.Dotnet.Versions.Types
{
    /// <summary>
    /// POCO for a semantic version / file version
    /// </summary>
    public class ProjectVersion
    {

        // regex pattern for semantiv versions.
        private const string _semverPattern = @"^(?<major>0|[1-9]\d*)\.(?<minor>0|[1-9]\d*)\.(?<patch>0|[1-9]\d*)(?:-(?<prerelease>(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+(?<buildmetadata>[0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$";
        // regex pattern for a file revision
        private const string _filePattern = @"^(?<major>0|[1-9]\d*)\.(?<minor>0|[1-9]\d*)\.(?<patch>0|[1-9]\d*)\.(?<file>0|[1-9]\d*)";

        /// <summary>
        /// Major version
        /// </summary>
        public int? Major { get; internal set; }

        /// <summary>
        /// Minor version
        /// </summary>
        public int? Minor { get; internal set; }

        /// <summary>
        /// Patch version
        /// </summary>
        public int? Patch { get; internal set; }

        /// <summary>
        /// File version / revision
        /// </summary>
        public int? File { get; internal set; }

        /// <summary>
        /// Prerelease part of the semantiv version
        /// </summary>
        public string Prerelease { get; internal set; }

        /// <summary>
        /// Build Metadata for a semantic version
        /// </summary>
        public string Buildmetadata { get; internal set; }

        /// <summary>
        /// flag indicating whether ist a semantiv version or a file revision
        /// </summary>
        public bool IsFileVersion { get; internal set; } = false;

        /// <summary>
        /// internal property for keeping the regex match
        /// </summary>
        internal Match _match;

        /// <summary>
        /// keeps the origial version string
        /// </summary>
        internal string _source;


        /// <summary>
        /// transforms a regex match to an int?
        /// </summary>
        /// <param name="match"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        private int? ParseMatch(Match match, string group)
        {
            int? ret = new Nullable<int>();
            if (match.Groups[group].Success && int.TryParse(match.Groups[group].Value, out int num))
            {
                ret = num;
            }
            return ret;
        }


        /// <summary>
        /// the non semantiv part of the version - computed
        /// </summary>
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

        /// <summary>
        /// the semantic part of the version (prerelease+metadata) - computed
        /// </summary>
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

        /// <summary>
        /// full version as string - computed
        /// </summary>
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

        /// <summary>
        /// internal initializsation
        /// </summary>
        /// <param name="version"></param>
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

