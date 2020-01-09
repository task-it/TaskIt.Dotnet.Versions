using TaskIt.Dotnet.Versions.Types;
using Xunit;

namespace TaskIt.Dotnet.Versions.Test
{
    /// <summary>
    /// Unit Test for the main Program
    /// </summary>
    public class ProgramTest
    {

        [Theory]
        [InlineData("", "", EExitCode.GENERAL_ERROR)]
        [InlineData("*.*.*.*", "", EExitCode.INVALID_FILE)]
        [InlineData("*.*.*.*", "../../../", EExitCode.INVALID_FILE)]
        public void TestMain_SetVersions(string version, string path, EExitCode expectedResult)
        {
            string[] testArgs = { "set", "-v", version, "-s", path };
            var result = Program.Main(testArgs);

            Assert.True(result == (int)expectedResult);
        }
    }
}
