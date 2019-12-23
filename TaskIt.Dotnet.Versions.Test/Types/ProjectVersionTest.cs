using System;
using TaskIt.Dotnet.Versions.Types;
using Xunit;

namespace TaskIt.Dotnet.Versions.Test.Types
{
    public class ProjectVersionTest
    {
        [Theory]
        [MemberData(nameof(TestdataVersionNumbers.GetValidVersionWithExpectations), MemberType = typeof(TestdataVersionNumbers))]
        public void TestValidConstruction(string version, bool expectVersion, bool expectPrerelease, bool expectMetadata)
        {
            var testdata = new ProjectVersion(version);
            Assert.True(expectVersion == testdata.Major.HasValue, $"{version} - Version Expectation not matched");
            Assert.True(expectVersion == testdata.Minor.HasValue, $"{version} - Version Expectation not matched");
            Assert.True(expectVersion == testdata.Patch.HasValue, $"{version} - Version Expectation not matched");
            Assert.False(testdata.File.HasValue);

            Assert.True(expectPrerelease != string.IsNullOrEmpty(testdata.Prerelease), $"{version} - Prerelease Expectation not matched");
            Assert.True(expectMetadata != string.IsNullOrEmpty(testdata.Buildmetadata), $"{version} - Metadata Expectation not matched");
        }

        [Theory]
        [MemberData(nameof(TestdataVersionNumbers.GetInvalidVersion), MemberType = typeof(TestdataVersionNumbers))]
        public void TestInvalidConstruction(string version)
        {

            Assert.Throws<ArgumentOutOfRangeException>(() => new ProjectVersion(version));

        }



        [Theory]
        [InlineData("0.0.1.2")]
        [InlineData("0.0.1.0-RC1")]
        [InlineData("1.2.31.2.3----RC-SNAPSHOT.12.09.1--..12+788")]
        public void TestConstructionFileVersion(string version)
        {
            var testdata = new ProjectVersion(version);
            Assert.True(testdata.Major.HasValue);
            Assert.True(testdata.Minor.HasValue);
            Assert.True(testdata.Patch.HasValue);
            Assert.True(testdata.File.HasValue);
            Assert.True(testdata.IsFileVersion);


            Assert.True(string.IsNullOrEmpty(testdata.Buildmetadata));
            Assert.True(string.IsNullOrEmpty(testdata.Prerelease));
        }




    }
}
