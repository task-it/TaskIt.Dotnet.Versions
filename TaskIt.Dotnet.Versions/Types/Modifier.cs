using System;

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

        public Modifier(string version)
        {
            try
            {
                var temp = version.Split('.');
                int num;
                if (int.TryParse(temp[0], out num))
                {
                    Major = num;
                }

                if (int.TryParse(temp[1], out num))
                {
                    Minor = num;
                }

                if (int.TryParse(temp[2], out num))
                {
                    Patch = num;
                }
            }
            catch (Exception)
            {
                throw new ArgumentOutOfRangeException(nameof(version));
            }
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
    }
}
