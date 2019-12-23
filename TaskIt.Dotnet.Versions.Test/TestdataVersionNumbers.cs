using System.Collections.Generic;

namespace TaskIt.Dotnet.Versions.Test
{
    class TestdataVersionNumbers
    {
        static public readonly string[] Valid = new string[]
        {
            "0.0.4",
            "1.2.3",
            "10.20.30",
            "1.1.2-prerelease+meta",
            "1.1.2+meta",
            "1.1.2+meta-valid",
            "1.0.0-alpha",
            "1.0.0-beta",
            "1.0.0-alpha.beta",
            "1.0.0-alpha.beta.1",
            "1.0.0-alpha.1",
            "1.0.0-alpha0.valid",
            "1.0.0-alpha.0valid",
            "1.0.0-alpha-a.b-c-somethinglong+build.1-aef.1-its-okay",
            "1.0.0-rc.1+build.1",
            "2.0.0-rc.1+build.123",
            "1.2.3-beta",
            "10.2.3-DEV-SNAPSHOT",
            "1.2.3-SNAPSHOT-123",
            "1.0.0",
            "2.0.0",
            "1.1.7",
            "2.0.0+build.1848",
            "2.0.1-alpha.1227",
            "1.0.0-alpha+beta",
            "1.2.3----RC-SNAPSHOT.12.9.1--.12+788",
            "1.2.3----R-S.12.9.1--.12+meta",
            "1.2.3----RC-SNAPSHOT.12.9.1--.12",
            "1.0.0+0.build.1-rc.10000aaa-kk-0.1",
            $"{int.MaxValue}.{int.MaxValue}.{int.MaxValue}",
            "1.0.0-0A.is.legal"
        };

        static public string[] Invalid = new string[]
        {
            "1",
            "1.2",
            "1.2.3-0123",
            "1.2.3-0123.0123",
            "1.1.2+.123",
            "+invalid",
            "-invalid",
            "-invalid+invalid",
            "-invalid.01",
            "alpha",
            "alpha.beta",
            "alpha.beta.1",
            "alpha.1",
            "alpha+beta",
            "alpha_beta",
            "alpha.",
            "alpha..",
            "beta",
            "1.0.0-alpha_beta",
            "-alpha.",
            "1.0.0-alpha..",
            "1.0.0-alpha..1",
            "1.0.0-alpha...1",
            "1.0.0-alpha....1",
            "1.0.0-alpha.....1",
            "1.0.0-alpha......1",
            "1.0.0-alpha.......1",
            "01.1.1",
            "1.01.1",
            "1.1.01",
            "1.2",
            "1.2.3.DEV",
            "1.2-SNAPSHOT",
            "1.2-RC-SNAPSHOT",
            "-1.0.3-gamma+b7718",
            "+justmeta",
            "9.8.7+meta+meta",
            "9.8.7-whatever+meta+meta",
            $"{int.MaxValue}.{int.MaxValue}.{int.MaxValue}----RC-SNAPSHOT.12.09.1--------------------------------..12",
        };

        /// <summary>
        /// Returns a valid sematic (semver 2.0) Version number
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> GetValidVersion()
        {
            yield return new object[] { Valid[0] };
            yield return new object[] { Valid[1] };
            yield return new object[] { Valid[2] };
            yield return new object[] { Valid[3] };
            yield return new object[] { Valid[4] };
            yield return new object[] { Valid[5] };
            yield return new object[] { Valid[6] };
            yield return new object[] { Valid[7] };
            yield return new object[] { Valid[8] };
            yield return new object[] { Valid[9] };
            yield return new object[] { Valid[10] };
            yield return new object[] { Valid[11] };
            yield return new object[] { Valid[12] };
            yield return new object[] { Valid[13] };
            yield return new object[] { Valid[14] };
            yield return new object[] { Valid[15] };
            yield return new object[] { Valid[16] };
            yield return new object[] { Valid[17] };
            yield return new object[] { Valid[18] };
            yield return new object[] { Valid[19] };
            yield return new object[] { Valid[20] };
            yield return new object[] { Valid[21] };
            yield return new object[] { Valid[22] };
            yield return new object[] { Valid[23] };
            yield return new object[] { Valid[24] };
            yield return new object[] { Valid[25] };
            yield return new object[] { Valid[26] };
            yield return new object[] { Valid[26] };
            yield return new object[] { Valid[27] };
            yield return new object[] { Valid[28] };
            yield return new object[] { Valid[29] };
            yield return new object[] { Valid[30] };
            yield return new object[] { Valid[31] };
        }

        /// <summary>
        /// Returns a valid sematic (semver 2.0) Version number
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> GetValidVersionWithExpectations =>
            new List<object[]>()
            {
                new object[] { Valid[0], true, false, false },
                new object[] { Valid[1], true, false, false },
                new object[] { Valid[2], true, false, false },
                new object[] { Valid[3], true, true, true },
                new object[] { Valid[4], true, false, true },
                new object[] { Valid[5], true, false, true },
                new object[] { Valid[6], true, true, false },
                new object[] { Valid[7], true, true, false },
                new object[] { Valid[8], true, true, false },
                new object[] { Valid[9], true, true, false },
                new object[] { Valid[10], true, true, false },
                new object[] { Valid[11], true, true, false },
                new object[] { Valid[12], true, true, false },
                new object[] { Valid[13], true, true, true },
                new object[] { Valid[14], true, true, true },
                new object[] { Valid[15], true, true, true },
                new object[] { Valid[16], true, true, false },
                new object[] { Valid[17], true, true, false },
                new object[] { Valid[18], true, true, false },
                new object[] { Valid[19], true, false, false },
                new object[] { Valid[20], true, false, false },
                new object[] { Valid[21], true, false, false },
                new object[] { Valid[22], true, false, true },
                new object[] { Valid[23], true, true, false },
                new object[] { Valid[24], true, true, true },
                new object[] { Valid[25], true, true, true },
                new object[] { Valid[26], true, true, true },
                new object[] { Valid[27], true, true, false },
                new object[] { Valid[28], true, false, true },
                new object[] { Valid[29], true, false, false },
                new object[] { Valid[30], true, true, false },
            };

        /// <summary>
        /// Returns a valid sematic (semver 2.0) Version number
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> GetInvalidVersion =>
            new List<object[]>()
            {
               new object[] { Invalid[0] },
               new object[] { Invalid[0] },
                new object[] { Invalid[1] },
                new object[] { Invalid[2] },
                new object[] { Invalid[3] },
                new object[] { Invalid[4] },
                new object[] { Invalid[5] },
                new object[] { Invalid[6] },
                new object[] { Invalid[7] },
                new object[] { Invalid[8] },
                new object[] { Invalid[9] },
                new object[] { Invalid[10] },
                new object[] { Invalid[11] },
                new object[] { Invalid[12] },
                new object[] { Invalid[13] },
                new object[] { Invalid[14] },
                new object[] { Invalid[15] },
                new object[] { Invalid[16] },
                new object[] { Invalid[17] },
                new object[] { Invalid[18] },
                new object[] { Invalid[19] },
                new object[] { Invalid[20] },
                new object[] { Invalid[21] },
                new object[] { Invalid[22] },
                new object[] { Invalid[23] },
                new object[] { Invalid[24] },
                new object[] { Invalid[25] },
                new object[] { Invalid[26] },
                new object[] { Invalid[27] },
                new object[] { Invalid[28] },
                new object[] { Invalid[29] },
                new object[] { Invalid[30] },
                new object[] { Invalid[31] },
                new object[] { Invalid[32] },
                new object[] { Invalid[33] },
                new object[] { Invalid[34] },
                new object[] { Invalid[35] },
                new object[] { Invalid[36] },
                new object[] { Invalid[37] },
                new object[] { Invalid[38] },
            };
    }
}
