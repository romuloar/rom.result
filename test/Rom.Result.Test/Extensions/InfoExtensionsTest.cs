using FluentAssertions;
using Rom.Result.Domain;
using Rom.Result.Extensions;
using System.Linq.Expressions;
using System.Text.Json;

namespace Rom.Result.Test.Extensions
{
    public class InfoExtensionsTest
    {
        [Fact]
        public void GetResultDetailInfo_ShouldReturnInfoResult_WithDefaultValues()
        {
            var entity = "test-info";
            var result = entity.GetResultDetailInfo();

            result.ResultType.Should().Be(ResultType.Info);
            result.ResultData.Should().Be(entity);
            result.Message.Should().BeNull();
            result.Parameters.Should().BeNull();
        }

        [Fact]
        public void GetResultDetailInfo_WithMessage_ShouldSetMessage()
        {
            var entity = 123;
            var message = "Info message";

            var result = entity.GetResultDetailInfo(message);

            result.ResultType.Should().Be(ResultType.Info);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Parameters.Should().BeNull();
        }

        [Fact]
        public void GetResultDetailInfo_WithMessageAndDate_ShouldSetMessageAndDate()
        {
            var entity = new { Id = 2 };
            var message = "Info with date";
            var date = DateTime.UtcNow.ToString("o");

            var result = entity.GetResultDetailInfo(message, date);

            result.ResultType.Should().Be(ResultType.Info);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19));
        }

        [Fact]
        public void GetResultDetailInfo_WithParams_ShouldSetParameters()
        {
            var entity = 77;
            Expression<Func<object>>[] paramExpressions = { () => entity };

            var result = entity.GetResultDetailInfo(paramExpressions);

            result.ResultType.Should().Be(ResultType.Info);
            result.ResultData.Should().Be(entity);
            result.Parameters.Should().NotBeNull();
        }

        [Fact]
        public void GetResultDetailInfo_WithMessageDateAndParams_ShouldSetAllFields()
        {
            var entity = "info-entity";
            var message = "Info with params";
            var date = DateTime.UtcNow.ToString("o");
            int value = 55;
            Expression<Func<object>>[] paramExpressions = { () => value };

            var result = entity.GetResultDetailInfo(message, date, paramExpressions);

            result.ResultType.Should().Be(ResultType.Info);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Parameters.Should().NotBeNull();
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19));
        }

        [Fact]
        public async Task GetResultDetailInfoAsync_ShouldReturnInfoResultAsync()
        {
            var entity = "async-info";
            var result = await entity.GetResultDetailInfoAsync();

            result.ResultType.Should().Be(ResultType.Info);
            result.ResultData.Should().Be(entity);
            result.Message.Should().BeNull();
            result.Parameters.Should().BeNull();
        }

        [Fact]
        public async Task GetResultDetailInfoAsync_WithMessage_ShouldSetMessageAsync()
        {
            var entity = 3.14;
            var message = "Async info message";

            var result = await entity.GetResultDetailInfoAsync(message);

            result.ResultType.Should().Be(ResultType.Info);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Parameters.Should().BeNull();
        }

        [Fact]
        public async Task GetResultDetailInfoAsync_WithMessageAndDate_ShouldSetMessageAndDateAsync()
        {
            var entity = new { Name = "AsyncInfo" };
            var message = "Async info with date";
            var date = DateTime.UtcNow.ToString("o");

            var result = await entity.GetResultDetailInfoAsync(message, date);

            result.ResultType.Should().Be(ResultType.Info);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19));
        }

        [Fact]
        public async Task GetResultDetailInfoAsync_WithParams_ShouldSetParametersAsync()
        {
            var entity = 21;
            Expression<Func<object>>[] paramExpressions = { () => entity };

            var result = await entity.GetResultDetailInfoAsync(paramExpressions);

            result.ResultType.Should().Be(ResultType.Info);
            result.ResultData.Should().Be(entity);
            result.Parameters.Should().NotBeNull();
        }

        [Fact]
        public async Task GetResultDetailInfoAsync_WithMessageDateAndParams_ShouldSetAllFieldsAsync()
        {
            var entity = "async-info-all";
            var message = "All async info";
            var date = DateTime.UtcNow.ToString("o");
            int value = 101;
            Expression<Func<object>>[] paramExpressions = { () => value };

            var result = await entity.GetResultDetailInfoAsync(message, date, paramExpressions);

            result.ResultType.Should().Be(ResultType.Info);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Parameters.Should().NotBeNull();
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19));
        }

        [Fact]
        public void GetResultDetailInfo_SerializeToJson_WithoutParameters_ShouldSerializeCorrectly()
        {
            var entity = "info-entity";
            var message = "Info message";
            var result = entity.GetResultDetailInfo(message);

            var json = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

            json.Should().Contain("\"resultType\":\"Info\"");
            json.Should().Contain("\"message\":\"Info message\"");
            json.Should().Contain("\"isSuccess\":true");
            json.Should().Contain("\"timestamp\"");
            json.Should().NotContain("\"parameters\"");
        }

        [Fact]
        public void GetResultDetailInfo_SerializeToJson_WithParameters_ShouldSerializeCorrectly()
        {
            var entity = 42;
            int value = 88;
            var result = entity.GetResultDetailInfo("Info with params", DateTime.UtcNow.ToString("o"), () => value);

            var json = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

            json.Should().Contain("\"resultType\":\"Info\"");
            json.Should().Contain("\"message\":\"Info with params\"");
            json.Should().Contain("\"isSuccess\":true");
            json.Should().Contain("\"timestamp\"");
            json.Should().Contain("\"parameters\"");
            json.Should().Contain("\"value\"");
            json.Should().Contain("88");
        }
    }
}
