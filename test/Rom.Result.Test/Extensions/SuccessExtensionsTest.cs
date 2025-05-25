using FluentAssertions;
using Rom.Result.Domain;
using Rom.Result.Extensions;
using System.Linq.Expressions;
using System.Text.Json;

namespace Rom.Result.Test.Extensions
{
    public class SuccessExtensionsTest
    {
        [Fact]
        public void GetResultDetailSuccess_ShouldReturnSuccessResult_WithDefaultValues()
        {
            // Arrange
            var entity = "test";

            // Act
            var result = entity.GetResultDetailSuccess();

            // Assert
            result.ResultType.Should().Be(ResultType.Success);
            result.ResultData.Should().Be(entity);
            result.Message.Should().BeNull();
            result.Parameters.Should().BeNull();
        }

        [Fact]
        public void GetResultDetailSuccess_WithMessage_ShouldSetMessage()
        {
            var entity = 123;
            var message = "Operation completed";

            var result = entity.GetResultDetailSuccess(message);

            result.ResultType.Should().Be(ResultType.Success);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Parameters.Should().BeNull();
        }

        [Fact]
        public void GetResultDetailSuccess_WithMessageAndDate_ShouldSetMessageAndDate()
        {
            var entity = new { Id = 1 };
            var message = "Done";
            var date = DateTime.UtcNow.ToString("o");

            var result = entity.GetResultDetailSuccess(message, date);

            result.ResultType.Should().Be(ResultType.Success);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19)); // Compare up to seconds
        }

        [Fact]
        public void GetResultDetailSuccess_WithParams_ShouldSetParameters()
        {
            var entity = 42;
            Expression<Func<object>>[] paramExpressions = { () => entity };

            var result = entity.GetResultDetailSuccess(paramExpressions);

            result.ResultType.Should().Be(ResultType.Success);
            result.ResultData.Should().Be(entity);
            result.Parameters.Should().NotBeNull();
        }

        [Fact]
        public void GetResultDetailSuccess_WithMessageDateAndParams_ShouldSetAllFields()
        {
            var entity = "abc";
            var message = "Success!";
            var date = DateTime.UtcNow.ToString("o");
            int value = 99;
            Expression<Func<object>>[] paramExpressions = { () => value };

            var result = entity.GetResultDetailSuccess(message, date, paramExpressions);

            result.ResultType.Should().Be(ResultType.Success);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Parameters.Should().NotBeNull();
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19));
        }

        [Fact]
        public async Task GetResultDetailSuccessAsync_ShouldReturnSuccessResultAsync()
        {
            var entity = "async";
            var result = await entity.GetResultDetailSuccessAsync();

            result.ResultType.Should().Be(ResultType.Success);
            result.ResultData.Should().Be(entity);
            result.Message.Should().BeNull();
            result.Parameters.Should().BeNull();
        }

        [Fact]
        public async Task GetResultDetailSuccessAsync_WithMessage_ShouldSetMessageAsync()
        {
            var entity = 1.23;
            var message = "Async message";

            var result = await entity.GetResultDetailSuccessAsync(message);

            result.ResultType.Should().Be(ResultType.Success);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Parameters.Should().BeNull();
        }

        [Fact]
        public async Task GetResultDetailSuccessAsync_WithMessageAndDate_ShouldSetMessageAndDateAsync()
        {
            var entity = new { Name = "Test" };
            var message = "Async done";
            var date = DateTime.UtcNow.ToString("o");

            var result = await entity.GetResultDetailSuccessAsync(message, date);

            result.ResultType.Should().Be(ResultType.Success);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19));
        }

        [Fact]
        public async Task GetResultDetailSuccessAsync_WithParams_ShouldSetParametersAsync()
        {
            var entity = 7;
            Expression<Func<object>>[] paramExpressions = { () => entity };

            var result = await entity.GetResultDetailSuccessAsync(paramExpressions);

            result.ResultType.Should().Be(ResultType.Success);
            result.ResultData.Should().Be(entity);
            result.Parameters.Should().NotBeNull();
        }

        [Fact]
        public async Task GetResultDetailSuccessAsync_WithMessageDateAndParams_ShouldSetAllFieldsAsync()
        {
            var entity = "async-all";
            var message = "All async";
            var date = DateTime.UtcNow.ToString("o");
            int value = 123;
            Expression<Func<object>>[] paramExpressions = { () => value };

            var result = await entity.GetResultDetailSuccessAsync(message, date, paramExpressions);

            result.ResultType.Should().Be(ResultType.Success);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Parameters.Should().NotBeNull();
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19));
        }

        [Fact]
        public void GetResultDetailSuccess_SerializeToJson_WithoutParameters_ShouldSerializeCorrectly()
        {
            // Arrange
            var entity = "test-entity";
            var message = "Success message";
            var result = entity.GetResultDetailSuccess(message);

            // Act
            //var json = JsonSerializer.Serialize(result);
            //json.Should().Contain("\"parameters\":null");
            //or

            var json = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

            // Assert
            json.Should().Contain("\"resultType\":\"Success\"");
            json.Should().Contain("\"message\":\"Success message\"");
            json.Should().Contain("\"isSuccess\":true");
            json.Should().Contain("\"timestamp\"");
            json.Should().NotContain("\"parameters\"");
        }

        [Fact]
        public void GetResultDetailSuccess_SerializeToJson_WithParameters_ShouldSerializeCorrectly()
        {
            // Arrange
            var entity = 42;
            int value = 99;
            var result = entity.GetResultDetailSuccess("Success with params", DateTime.UtcNow.ToString("o"), () => value);

            // Act
            var json = JsonSerializer.Serialize(result);

            // Assert
            json.Should().Contain("\"resultType\":\"Success\"");
            json.Should().Contain("\"message\":\"Success with params\"");
            json.Should().Contain("\"isSuccess\":true");
            json.Should().Contain("\"timestamp\"");
            json.Should().Contain("\"parameters\"");
            json.Should().Contain("\"value\"");
            json.Should().Contain("99");
        }
    }
}
