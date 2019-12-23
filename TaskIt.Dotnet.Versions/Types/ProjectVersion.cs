using System;
using System.Text.RegularExpressions;

namespace TaskIt.Dotnet.Versions.Types
{
    public class ProjectVersion
    {

        // regex pattern for semantiv versions.
        private const string _semverPattern = @"^(?<major>0|[1-9]\d*)\.(?<minor>0|[1-9]\d*)\.(?<patch>0|[1-9]\d*)(?:-(?<prerelease>(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+(?<buildmetadata>[0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$";
        private const string _filePattern = @"^(?<major>0|[1-9]\d*)\.(?<minor>0|[1-9]\d*)\.(?<patch>0|[1-9]\d*)\.(?<file>0|[1-9]\d*)?$";

        public int? Major { get; private set; }
        public int? Minor { get; private set; }
        public int? Patch { get; private set; }

        public int? File { get; private set; }

        public string Prerelease { get; private set; }
        public string Buildmetadata { get; private set; }
        public bool IsFileVersion { get; private set; } = false;

        private Match _match;

        private readonly string _source;



        private int? ParseMatch(Match match, string group)
        {
            int? ret = new Nullable<int>();
            if (match.Groups[group].Success && int.TryParse(match.Groups[group].Value, out int num))
            {
                ret = num;
            }
            return ret;
        }

        private int? ApplyModifier(int? source, int? modifier)
        {
            if (modifier.HasValue)
            {
                if (modifier.Value == 0)
                {
                    return modifier.Value;
                }
                else
                {
                    return source.Value + modifier.Value;
                }
            }
            return source.Value;
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
            _source = version;
            var regex = new Regex(_semverPattern);
            _match = regex.Match(_source);

            if (!_match.Success)
            {
                regex = new Regex(_filePattern);
                _match = regex.Match(_source);
                if (!_match.Success)
                {
                    throw new ArgumentOutOfRangeException(nameof(version));
                }
                IsFileVersion = true;
            }

            Major = ParseMatch(_match, "major");
            Minor = ParseMatch(_match, "minor");
            Patch = ParseMatch(_match, "patch");
            File = ParseMatch(_match, "file");

            Prerelease = _match.Groups["prerelease"].Value;
            Buildmetadata = _match.Groups["buildmetadata"].Value;
        }

        /// <summary>
        /// Sets the modifier as the current version - overwrite
        /// </summary>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public string Set(Modifier modifier)
        {

            if (modifier.Major.HasValue)
            {
                Major = modifier.Major.Value;
            }
            if (modifier.Minor.HasValue)
            {
                Minor = modifier.Minor.Value;
            }
            if (modifier.Patch.HasValue)
            {
                Minor = modifier.Patch.Value;
            }

            return FullVersion;
        }

        /// <summary>
        /// Sets the modifier as the current version - overwrite
        /// </summary>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public string Modify(Modifier modifier)
        {

            Major = ApplyModifier(Major, modifier.Major);
            Minor = ApplyModifier(Minor, modifier.Minor);
            Patch = ApplyModifier(Patch, modifier.Patch);


            return FullVersion;
        }
    }
}

