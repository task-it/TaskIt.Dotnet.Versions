using System.Linq;
using System.Text.RegularExpressions;

namespace TaskIt.Dotnet.Versions
{
    internal static class RegexUtil
    {
        // regex for valid semver matches
        public const string RegexSemver = @"^(?P<major>0|[1-9]\d*)\.(?P<minor>0|[1-9]\d*)\.(?P<patch>0|[1-9]\d*)(?:-(?P<prerelease>(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+(?P<buildmetadata>[0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$";
        public static string ReplaceMatch(string input, int index, string replacement, Match match)
        {
            if (string.IsNullOrEmpty(replacement))
            {
                return input;
            }
            string result = match.Value;
            Capture capture = match.Groups[index].Captures.FirstOrDefault();
            result = result.Remove(capture.Index - match.Index, capture.Length);
            result = result.Insert(capture.Index - match.Index, replacement);

            return result;
        }

        public static bool GetSemverMatch(string input, out Match match)
        {
            var generalRegex = new Regex(RegexSemver);
            match = generalRegex.Match(input);

            return match.Success;
        }

        public static bool GetTag(string input, string tag, out Match match)
        {
            string pattern = $"<{tag}>(.*)<\\/{tag}>";
            Regex regex = new Regex(pattern);
            match = regex.Match(input);
            return match.Success;
        }

        /// <summary>
        /// Checks if the version string is a semantiv version
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsSemver(string source)
        {
            bool ret = false;
            GetSemverMatch(source, out var match);

            // match.groups 1-3 are major, minor, patch, others are semver part if they have a value
            for (int i = 4; i < match.Groups.Count; i++)
            {
                if (!string.IsNullOrEmpty(match.Groups[i].Value))
                {
                    ret = true;
                }
            }

            return ret;
        }

        /// <summary>
        /// Gets the non Semver Part of the Version
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetNonSemverVersion(string source)
        {
            var temp = source.Split('-', '+');
            return temp[0];
        }
    }
}
