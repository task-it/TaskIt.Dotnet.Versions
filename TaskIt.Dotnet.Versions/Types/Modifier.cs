using System;
using System.Text.RegularExpressions;

namespace TaskIt.Dotnet.Versions.Types

{
    public class Modifier
    {
        /// <summary>
        /// Major Version Modifier
        /// </summary>
        public int? Major { get; private set; }

        /// <summary>
        /// Minor Version Modifier
        /// </summary>
        public int? Minor { get; private set; }

        /// <summary>
        /// Patch Version Modifier
        /// </summary>
        public int? Patch { get; private set; }

        /// <summary>
        /// Semver Pattern
        /// </summary>
        public string SemverPattern { get; private set; }

        /// <summary>
        /// Semver Pattern
        /// </summary>
        public int? Semver { get; private set; }

        /// <summary>
        /// Creates the Modifier from a version String<br/>
        /// Wildcards ('*' are allowed)
        /// </summary>
        /// <param name="version"></param>
        public Modifier(string version)
        {
            // 1 - eliminate wildcards and set major, minor, patch
            try
            {
                var tempArray = version.Split('.', '-', '+');
                int num;
                if (int.TryParse(tempArray[0], out num))
                {
                    Major = num;
                }

                if (int.TryParse(tempArray[1], out num))
                {
                    Minor = num;
                }

                if (int.TryParse(tempArray[2], out num))
                {
                    Patch = num;
                }
            }
            catch (Exception)
            {
                throw new ArgumentOutOfRangeException($"{nameof(version)}, VersionString: {version}");

            }

            // 2 - create dummy version to extract semver part
            version = version.Replace('*', '1');
            var temp = new ProjectVersion(version);
            SemverPattern = temp.SemanticVersion;
        }
        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="version"></param>
        /// <param name="pattern"></param>
        /// <param name="semvermodifier"></param>
        public Modifier(string version, string pattern, int? semvermodifier) : this(version)
        {
            SemverPattern = pattern;
            if (semvermodifier.HasValue)
            {
                Semver = semvermodifier.Value;
            }
        }

        /// <summary>
        /// Sets the modifier as the current version - overwrite
        /// </summary>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public string Overwrite(ProjectVersion source, bool isSemantic)
        {
            int major;
            int minor;
            int patch;
            major = Major.HasValue ? Major.Value : source.Major.Value;
            minor = Minor.HasValue ? Minor.Value : source.Minor.Value;
            patch = Patch.HasValue ? Patch.Value : source.Patch.Value;

            string versionString = $"{major}.{minor}.{patch}";
            if (source.IsFileVersion)
            {
                versionString += $".{source.File}";
            }
            if (isSemantic)
            {
                if (!string.IsNullOrWhiteSpace(SemverPattern))
                {
                    versionString += $"{SemverPattern}";
                }
            }
            source.Init(versionString);
            return source.FullVersion;
        }

        /// <summary>
        /// Sets the modifier as the current version - overwrite
        /// </summary>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public string Modify(ProjectVersion source, bool isSemantic)
        {
            int major;
            int minor;
            int patch;

            major = ApplyModifier(source.Major, Major);
            minor = ApplyModifier(source.Minor, Minor);
            patch = ApplyModifier(source.Patch, Patch);


            string versionString = $"{major}.{minor}.{patch}";
            if (source.IsFileVersion)
            {
                versionString += $".{source.File}";
            }
            if (isSemantic)
            {
                string semver = ApplyModifier(source.SemanticVersion);
                versionString += $"{semver}";
            }

            source.Init(versionString);
            return source.FullVersion;
        }

        private int ApplyModifier(int? source, int? modifier)
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

        private string ApplyModifier(string source)
        {
            var regex = new Regex(this.SemverPattern);
            var match = regex.Match(source);
            if (!match.Success)
            {
                return source;
            }

            int.TryParse(match.Groups[1].Value, out int num);
            var digit = ApplyModifier(num, Semver);
            var replaced = RegexUtil.ReplaceMatch(source, 1, $"{digit}", match);

            return Regex.Replace(source, match.Groups[0].Value, replaced);
        }
    }
}
