using System.Text;
using System.Text.RegularExpressions;

namespace TaskIt.Dotnet.Versions
{
    internal static class ContentUtil
    {

        /// <summary>
        /// modifies one digit with the modifier.<br/>
        /// If the modifier is 0, the digit will be set to 0, <br/>
        /// otherwise the modifier will be added.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="modifier"></param>
        static private string ModifyDigit(string source, int? modifier)
        {
            string ret = source;
            if (modifier.HasValue && int.TryParse(source, out int intVersion))
            {
                if (modifier.Value == 0)
                {
                    ret = $"{modifier.Value}";
                }
                else
                {
                    intVersion += modifier.Value;
                    ret = $"{intVersion}";
                }
            }
            return ret;
        }



        /// <summary>
        /// sets all occurances of the tag content to newValue
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="newValue"></param>
        /// <param name="source"></param>
        public static void ReplaceContent(string tag, string newValue, string[] source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (RegexUtil.GetTag(source[i], tag, out var match))
                {
                    if (newValue.Contains('*'))
                    {
                        newValue = HandleWildcards(newValue, match.Groups[1].Value);
                    }
                    source[i] = Regex.Replace(source[i], match.Groups[1].Value, newValue);
                }
            }
        }

        /// <summary>
        /// Constructs a new Version number handling wildcards<br/>
        /// 
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="currentValue"></param>
        /// <returns></returns>
        private static string HandleWildcards(string newValue, string currentValue)
        {
            currentValue = RegexUtil.GetNonSemverVersion(currentValue);

            var newArray = newValue.Split('.');
            var oldArray = currentValue.Split('.');

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                if (newArray[i].Contains("*"))
                {
                    builder.Append(newArray[i].Replace("*", oldArray[i]));
                }
                else
                {
                    builder.Append(newArray[i]);
                }
                builder.Append(".");
            }
            builder.Length--;
            return builder.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="newValue"></param>
        /// <param name="source"></param>
        public static void AdjustContent(string tag, Modifier modifier, string[] source, bool adjustSemver)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (RegexUtil.GetTag(source[i], tag, out var match))
                {
                    source[i] = Regex.Replace(source[i], match.Groups[1].Value, AdjustContent(match.Groups[1].Value, modifier, adjustSemver));
                }
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="modifier"></param>
        /// <param name="adjustSemver"></param>
        /// <returns></returns>
        private static string AdjustContent(string source, Modifier modifier, bool adjustSemver)
        {
            // get the semver matchers
            if (!RegexUtil.GetSemverMatch(source, out var match))
            {
                // nothing matched, return original source
                return source;
            }

            // incremet / set the version numbers
            // major
            string ret = RegexUtil.ReplaceMatch(source, 1, ModifyDigit(match.Groups[1].Value, modifier.Major), match);
            // minor
            RegexUtil.GetSemverMatch(ret, out match);
            ret = RegexUtil.ReplaceMatch(source, 2, ModifyDigit(match.Groups[2].Value, modifier.Minor), match);
            // patch
            RegexUtil.GetSemverMatch(ret, out match);
            ret = RegexUtil.ReplaceMatch(source, 3, ModifyDigit(match.Groups[3].Value, modifier.Patch), match);

            // no semver processing, return the string
            if (!adjustSemver)
            {
                return ret;
            }

            // check if the current version is already a semver version
            if (!RegexUtil.IsSemver(ret))
            {
                // nope, create a new on and return
                return CreateSemver(ret, modifier.SemverPattern, modifier.Semver);
            }

            // semver part
            // iterate over the capture groups > 3 and check if the pattern matches
            for (int i = 4; i < match.Groups.Count; i++)
            {
                // get the matcher value
                var tmpvalue = match.Groups[i].Value;
                if (!string.IsNullOrEmpty(tmpvalue))
                {
                    // check the pattern
                    var semverPatternMatch = Regex.Match(tmpvalue, modifier.SemverPattern);
                    if (semverPatternMatch.Success)
                    {
                        //replace content
                        var replacedInMatch = RegexUtil.ReplaceMatch(tmpvalue, 1, ModifyDigit(semverPatternMatch.Groups[1].Value, modifier.Semver), semverPatternMatch);
                        ret = RegexUtil.ReplaceMatch(ret, i, replacedInMatch, match);

                    }

                }
            }


            return ret;
        }

        /// <summary>
        /// Creates a new Semver Version number from the source, the pattern and the modifier.<br/>
        /// Therefor the first capture group of the pattern will be replaced with the modifier.<br/>
        /// Delimeter between the non semver part and the semver part is "-".<br/>
        /// Example:<br/>
        /// Source: 1.0.1, Pattern: RC(\\d+), Modifier: 1<br/>
        /// --> 1.0.1-RC1
        /// 
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pattern"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        private static string CreateSemver(string source, string pattern, int? version)
        {
            // not a semver version yet, so create a new one from the pattern and the modifier
            var tmp = Regex.Replace(pattern, @"(\(\S+\))", $"{version}");
            var ret = source + $"-{tmp}";
            return ret;
        }

    }
}
