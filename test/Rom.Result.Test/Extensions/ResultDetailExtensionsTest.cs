using FluentAssertions;
using Rom.Result.Domain;
using Rom.Result.Extensions;

namespace Rom.Result.Test.Extensions
{
    public class ResultDetailExtensionsTest
    {
        [Fact]
        public void GetError_ShouldReturnErrorResult_WithMessage()
        {
            var result = ResultDetailExtensions.GetError<string>("Error occurred");

            result.ResultType.Should().Be(ResultType.Error);
            result.ResultData.Should().BeNull();
            result.Message.Should().Be("Error occurred");
            result.Parameters.Should().BeNull();
        }

        [Fact]
        public void GetError_WithDate_ShouldSetDate()
        {
            var date = DateTime.UtcNow.ToString("o");
            var result = ResultDetailExtensions.GetError<int>("Error with date", date);

            result.ResultType.Should().Be(ResultType.Error);
            result.Message.Should().Be("Error with date");
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19));
        }

        [Fact]
        public void GetError_WithParams_ShouldSetParameters()
        {
            int code = 404;
            var result = ResultDetailExtensions.GetError<object>("Error with params", () => code);

            result.ResultType.Should().Be(ResultType.Error);
            result.Message.Should().Be("Error with params");
            result.Parameters.Should().NotBeNull();
            var dict = (System.Collections.Generic.IDictionary<string, object>)result.Parameters;
            dict.Should().ContainKey("code");
            dict["code"].Should().Be(code);
        }

        [Fact]
        public void GetError_WithDateAndParams_ShouldSetAllFields()
        {
            var date = DateTime.UtcNow.ToString("o");
            string reason = "NotFound";
            var result = ResultDetailExtensions.GetError<string>("Error with all", date, () => reason);

            result.ResultType.Should().Be(ResultType.Error);
            result.Message.Should().Be("Error with all");
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19));
            var dict = (System.Collections.Generic.IDictionary<string, object>)result.Parameters;
            dict.Should().ContainKey("reason");
            dict["reason"].Should().Be(reason);
        }

        [Fact]
        public async Task GetErrorAsync_ShouldReturnErrorResultAsync()
        {
            var result = await ResultDetailExtensions.GetErrorAsync<string>("Async error");

            result.ResultType.Should().Be(ResultType.Error);
            result.ResultData.Should().BeNull();
            result.Message.Should().Be("Async error");
            result.Parameters.Should().BeNull();
        }

        [Fact]
        public async Task GetErrorAsync_WithDateAndParams_ShouldSetAllFieldsAsync()
        {
            var date = DateTime.UtcNow.ToString("o");
            int value = 123;
            var result = await ResultDetailExtensions.GetErrorAsync<int>("Async error with params", date, () => value);

            result.ResultType.Should().Be(ResultType.Error);
            result.Message.Should().Be("Async error with params");
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19));
            var dict = (System.Collections.Generic.IDictionary<string, object>)result.Parameters;
            dict.Should().ContainKey("value");
            dict["value"].Should().Be(value);
        }
    }
}
