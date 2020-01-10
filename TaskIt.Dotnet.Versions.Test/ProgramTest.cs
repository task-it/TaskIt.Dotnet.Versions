using TaskIt.Dotnet.Versions.Types;
using Xunit;

namespace TaskIt.Dotnet.Versions.Test
{
    /// <summary>
    /// Unit Test for the main Program <see cref="Program"/>
    /// </summary>
    public class ProgramTest
    {
        /// <summary>
        /// Unit test for <see cref="Program.Main(string[])"/>
        /// </summary>
        /// <param name="version"></param>
        /// <param name="path"></param>
        /// <param name="expectedResult"></param>
        [Theory]
        [InlineData("", "", EExitCode.GENERAL_ERROR)]
        [InlineData("*.*.*.*", "", EExitCode.SUCCESS)]
        public void TestMain_SetVersions(string version, string path, EExitCode expectedResult)
        {
            string[] testArgs = { "set", "-v", version, "-f", path };
            var result = Program.Main(testArgs);

            Assert.True(result == (int)expectedResult);
        }

        /// <summary>
        /// Unit test for <see cref="Program.Main(string[])"/>
        /// </summary>
        /// <param name="version"></param>
        /// <param name="path"></param>
        /// <param name="expectedResult"></param>
        [Theory]
        [InlineData("", "", EExitCode.GENERAL_ERROR)]
        [InlineData("*.*.*.*", "", EExitCode.SUCCESS)]
        public void TestMain_ModVersions(string version, string path, EExitCode expectedResult)
        {
            string[] testArgs = { "mod", "-v", version, "-f", path };
            var result = Program.Main(testArgs);

            Assert.True(result == (int)expectedResult);
        }
    }
}
