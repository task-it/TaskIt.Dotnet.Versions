using TaskIt.Dotnet.Versions.Types;
using Xunit;

namespace TaskIt.Dotnet.Versions.Test.Types
{
    public class ProjectVersionTest
    {
        [Fact]
        public void TestConstruction()
        {
            var testdata = new ProjectVersion(TestdataVersionNumbers.Valid[0]);
            Assert.True(testdata.Major.HasValue);
            Assert.True(testdata.Minor.HasValue);
            Assert.True(testdata.Patch.HasValue);
            Assert.True(string.IsNullOrEmpty(testdata.Buildmetadata));
            Assert.True(string.IsNullOrEmpty(testdata.Prerelease));
        }

        [Fact]
        public void TestConstruction2()
        {
            var testdata = new ProjectVersion(TestdataVersionNumbers.Valid[10]);
            Assert.True(testdata.Major.HasValue);
            Assert.True(testdata.Minor.HasValue);
            Assert.True(testdata.Patch.HasValue);
            Assert.True(string.IsNullOrEmpty(testdata.Buildmetadata));
            Assert.False(string.IsNullOrEmpty(testdata.Prerelease));
        }

        [Fact]
        public void TestConstruction3()
        {
            var testdata = new ProjectVersion(TestdataVersionNumbers.Valid[15]);
            Assert.True(testdata.Major.HasValue);
            Assert.True(testdata.Minor.HasValue);
            Assert.True(testdata.Patch.HasValue);
            Assert.False(string.IsNullOrEmpty(testdata.Buildmetadata));
            Assert.False(string.IsNullOrEmpty(testdata.Prerelease));
        }

        [Fact]
        public void TestConstruction4()
        {
            var testdata = new ProjectVersion(TestdataVersionNumbers.Invalid[3]);
            Assert.True(testdata.Major.HasValue);
            Assert.True(testdata.Minor.HasValue);
            Assert.True(testdata.Patch.HasValue);
            Assert.False(string.IsNullOrEmpty(testdata.Buildmetadata));
            Assert.False(string.IsNullOrEmpty(testdata.Prerelease));
        }
    }
}
