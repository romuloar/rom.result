using FluentAssertions;
using Rom.Result.Helpers;

namespace Rom.Result.Test.Helpers
{
    public class ResultDetailHelperTest
    {
        [Fact]
        public void CaptureParams_ShouldReturnEmptyDictionary_WhenNoParams()
        {
            // Act
            var result = ResultDetailHelper.CaptureParams();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public void CaptureParams_ShouldReturnDictionaryWithValues()
        {
            // Arrange
            int id = 10;
            string name = "Test";

            // Act
            var result = ResultDetailHelper.CaptureParams(() => id, () => name);

            // Assert
            result.Should().ContainKey("id");
            result["id"].Should().Be(id);

            result.Should().ContainKey("name");
            result["name"].Should().Be(name);
        }

        [Fact]
        public void CaptureParams_ShouldThrowArgumentNullException_WhenValueIsNull()
        {
            // Arrange
            string value = null;

            // Act
            Action act = () => ResultDetailHelper.CaptureParams(() => value);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetExceptionMessage_ShouldReturnMessage_ForSingleException()
        {
            // Arrange
            var ex = new Exception("error message");

            // Act
            var result = ResultDetailHelper.GetExceptionMessage(ex);

            // Assert
            result.Should().Contain("error message");
        }

        [Fact]
        public void GetExceptionMessage_ShouldReturnMessages_ForNestedExceptions()
        {
            // Arrange
            var ex = new Exception("outer", new Exception("inner", new Exception("deepest")));

            // Act
            var result = ResultDetailHelper.GetExceptionMessage(ex);

            // Assert
            result.Should().Contain("outer");
            result.Should().Contain("inner");
            result.Should().Contain("deepest");
        }
    }
}
