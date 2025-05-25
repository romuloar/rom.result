using FluentAssertions;
using Rom.Result.Domain;
using Rom.Result.Extensions;
using System.Linq.Expressions;
using System.Text.Json;

namespace Rom.Result.Test.Extensions
{
    public class WarningExtensionsTest
    {
        [Fact]
        public void GetResultDetailWarning_ShouldReturnWarningResult_WithDefaultValues()
        {
            var entity = "test-warning";
            var result = entity.GetResultDetailWarning();

            result.ResultType.Should().Be(ResultType.Warning);
            result.ResultData.Should().Be(entity);
            result.Message.Should().BeNull();
            result.Parameters.Should().BeNull();
        }

        [Fact]
        public void GetResultDetailWarning_WithMessage_ShouldSetMessage()
        {
            var entity = 123;
            var message = "Warning message";

            var result = entity.GetResultDetailWarning(message);

            result.ResultType.Should().Be(ResultType.Warning);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Parameters.Should().BeNull();
        }

        [Fact]
        public void GetResultDetailWarning_WithMessageAndDate_ShouldSetMessageAndDate()
        {
            var entity = new { Id = 3 };
            var message = "Warning with date";
            var date = DateTime.UtcNow.ToString("o");

            var result = entity.GetResultDetailWarning(message, date);

            result.ResultType.Should().Be(ResultType.Warning);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19));
        }

        [Fact]
        public void GetResultDetailWarning_WithParams_ShouldSetParameters()
        {
            var entity = 77;
            Expression<Func<object>>[] paramExpressions = { () => entity };

            var result = entity.GetResultDetailWarning(paramExpressions);

            result.ResultType.Should().Be(ResultType.Warning);
            result.ResultData.Should().Be(entity);
            result.Parameters.Should().NotBeNull();
        }

        [Fact]
        public void GetResultDetailWarning_WithMessageDateAndParams_ShouldSetAllFields()
        {
            var entity = "warning-entity";
            var message = "Warning with params";
            var date = DateTime.UtcNow.ToString("o");
            int value = 55;
            Expression<Func<object>>[] paramExpressions = { () => value };

            var result = entity.GetResultDetailWarning(message, date, paramExpressions);

            result.ResultType.Should().Be(ResultType.Warning);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Parameters.Should().NotBeNull();
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19));
        }

        [Fact]
        public async Task GetResultDetailWarningAsync_ShouldReturnWarningResultAsync()
        {
            var entity = "async-warning";
            var result = await entity.GetResultDetailWarningAsync();

            result.ResultType.Should().Be(ResultType.Warning);
            result.ResultData.Should().Be(entity);
            result.Message.Should().BeNull();
            result.Parameters.Should().BeNull();
        }

        [Fact]
        public async Task GetResultDetailWarningAsync_WithMessage_ShouldSetMessageAsync()
        {
            var entity = 3.14;
            var message = "Async warning message";

            var result = await entity.GetResultDetailWarningAsync(message);

            result.ResultType.Should().Be(ResultType.Warning);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Parameters.Should().BeNull();
        }

        [Fact]
        public async Task GetResultDetailWarningAsync_WithMessageAndDate_ShouldSetMessageAndDateAsync()
        {
            var entity = new { Name = "AsyncWarning" };
            var message = "Async warning with date";
            var date = DateTime.UtcNow.ToString("o");

            var result = await entity.GetResultDetailWarningAsync(message, date);

            result.ResultType.Should().Be(ResultType.Warning);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19));
        }

        [Fact]
        public async Task GetResultDetailWarningAsync_WithParams_ShouldSetParametersAsync()
        {
            var entity = 21;
            Expression<Func<object>>[] paramExpressions = { () => entity };

            var result = await entity.GetResultDetailWarningAsync(paramExpressions);

            result.ResultType.Should().Be(ResultType.Warning);
            result.ResultData.Should().Be(entity);
            result.Parameters.Should().NotBeNull();
        }

        [Fact]
        public async Task GetResultDetailWarningAsync_WithMessageDateAndParams_ShouldSetAllFieldsAsync()
        {
            var entity = "async-warning-all";
            var message = "All async warning";
            var date = DateTime.UtcNow.ToString("o");
            int value = 101;
            Expression<Func<object>>[] paramExpressions = { () => value };

            var result = await entity.GetResultDetailWarningAsync(message, date, paramExpressions);

            result.ResultType.Should().Be(ResultType.Warning);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Parameters.Should().NotBeNull();
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19));
        }

        [Fact]
        public void GetResultDetailWarning_SerializeToJson_WithoutParameters_ShouldSerializeCorrectly()
        {
            var entity = "warning-entity";
            var message = "Warning message";
            var result = entity.GetResultDetailWarning(message);

            var json = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

            json.Should().Contain("\"resultType\":\"Warning\"");
            json.Should().Contain("\"message\":\"Warning message\"");
            json.Should().Contain("\"isSuccess\":true");
            json.Should().Contain("\"timestamp\"");
            json.Should().NotContain("\"parameters\"");
        }

        [Fact]
        public void GetResultDetailWarning_SerializeToJson_WithParameters_ShouldSerializeCorrectly()
        {
            var entity = 42;
            int value = 88;
            var result = entity.GetResultDetailWarning("Warning with params", DateTime.UtcNow.ToString("o"), () => value);

            var json = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

            json.Should().Contain("\"resultType\":\"Warning\"");
            json.Should().Contain("\"message\":\"Warning with params\"");
            json.Should().Contain("\"isSuccess\":true");
            json.Should().Contain("\"timestamp\"");
            json.Should().Contain("\"parameters\"");
            json.Should().Contain("\"value\"");
            json.Should().Contain("88");
        }
    }
}
