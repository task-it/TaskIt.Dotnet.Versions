namespace TaskIt.Dotnet.Versions
{
    internal class Modifier
    {
        /// <summary>
        /// Major Version Modifier
        /// </summary>
        public int? Major { get; set; }

        /// <summary>
        /// Minor Version Modifier
        /// </summary>
        public int? Minor { get; set; }

        /// <summary>
        /// Patch Version Modifier
        /// </summary>
        public int? Patch { get; set; }

        /// <summary>
        /// Semver Pattern
        /// </summary>
        public string SemverPattern { get; set; }

        /// <summary>
        /// Semver Pattern
        /// </summary>
        public int? Semver { get; set; }
    }
}
