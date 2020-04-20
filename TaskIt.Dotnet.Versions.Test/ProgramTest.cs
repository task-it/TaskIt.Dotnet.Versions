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
        /// tests without any Parameters
        /// </summary>
        [Fact]
        public void Test_Empty()
        {
            var result = Program.Main(null);
            Assert.True(result == (int)EExitCode.INVALID_PARAMS);
        }

        /// <summary>
        /// tests without nonsense parametrs
        /// </summary>
        [Fact]
        public void Test_noSense()
        {
            string[] testArgs = { "lalala", "bla", ",tra", "blu", "bli" };
            var result = Program.Main(testArgs);
            Assert.Equal((int)EExitCode.PARSE_ERROR, result);
        }

        /// <summary>
        /// Unit test for <see cref="Program.Main(string[])"/>
        /// </summary>
        /// <param name="version"></param>
        /// <param name="path"></param>
        /// <param name="expectedResult"></param>
        [Theory]
        [InlineData("", "", EExitCode.INVALID_PARAMS)]
        [InlineData("*.*.*.*", "", EExitCode.SUCCESS)]
        public void TestMain_SetVersions(string version, string path, EExitCode expectedResult)
        {
            string[] testArgs = { "set", "-v", version, "-f", path };
            var result = Program.Main(testArgs);

            Assert.Equal((int)expectedResult, result);
        }

        /// <summary>
        /// Unit test for <see cref="Program.Main(string[])"/>
        /// </summary>
        /// <param name="version"></param>
        /// <param name="path"></param>
        /// <param name="expectedResult"></param>
        [Theory]
        [InlineData("", "", EExitCode.INVALID_PARAMS)]
        [InlineData("*.*.*.*", "", EExitCode.SUCCESS)]
        public void TestMain_ModVersions(string version, string path, EExitCode expectedResult)
        {
            string[] testArgs = { "mod", "-v", version, "-f", path };
            var result = Program.Main(testArgs);

            Assert.Equal((int)expectedResult, result);
        }
    }
}
