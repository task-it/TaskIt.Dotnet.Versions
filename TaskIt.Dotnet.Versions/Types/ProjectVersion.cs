using System;
using System.Text.RegularExpressions;

namespace TaskIt.Dotnet.Versions.Types
{
    public class ProjectVersion
    {

        // regex pattern for semantiv versions.
        private const string _pattern = @"^(?<major>0|[1-9]\d*)\.(?<minor>0|[1-9]\d*)\.(?<patch>0|[1-9]\d*)(?:-(?<prerelease>(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+(?<buildmetadata>[0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$";


        public int? Major { get; private set; }
        public int? Minor { get; private set; }
        public int? Patch { get; private set; }
        public string Prerelease { get; private set; }
        public string Buildmetadata { get; private set; }

        private Match _match;

        private readonly string _source;

        public string NonSemanticVersion
        {
            get
            {
                return $"{ Major.Value}.{ Minor.Value}.{ Patch.Value}";
            }
        }


        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="version"></param>
        public ProjectVersion(string version)
        {
            _source = version;
            var regex = new Regex(_pattern);
            _match = regex.Match(_source);

            if (!_match.Success)
            {
                throw new ArgumentOutOfRangeException(nameof(version));
            }

            int.TryParse(_match.Groups["major"].Value, out int num);
            Major = num;

            int.TryParse(_match.Groups["minor"].Value, out num);
            Minor = num;

            int.TryParse(_match.Groups["patch"].Value, out num);
            Patch = num;

            Prerelease = _match.Groups["prerelease"].Value;
            Buildmetadata = _match.Groups["buildmetadata"].Value;
        }

    }
}

