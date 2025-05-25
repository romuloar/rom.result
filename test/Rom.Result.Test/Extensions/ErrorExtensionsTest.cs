using FluentAssertions;
using Rom.Result.Domain;
using Rom.Result.Extensions;
using System.Linq.Expressions;
using System.Text.Json;

namespace Rom.Result.Test.Extensions
{
    public class ErrorExtensionsTest
    {
        [Fact]
        public void GetResultDetailError_ShouldReturnErrorResult_WithDefaultValues()
        {
            var entity = "test-error";
            var result = entity.GetResultDetailError();

            result.ResultType.Should().Be(ResultType.Error);
            result.ResultData.Should().Be(entity);
            result.Message.Should().BeNull();
            result.Parameters.Should().BeNull();
        }

        [Fact]
        public void GetResultDetailError_WithMessage_ShouldSetMessage()
        {
            var entity = 123;
            var message = "Error message";

            var result = entity.GetResultDetailError(message);

            result.ResultType.Should().Be(ResultType.Error);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Parameters.Should().BeNull();
        }

        [Fact]
        public void GetResultDetailError_WithMessageAndDate_ShouldSetMessageAndDate()
        {
            var entity = new { Id = 4 };
            var message = "Error with date";
            var date = DateTime.UtcNow.ToString("o");

            var result = entity.GetResultDetailError(message, date);

            result.ResultType.Should().Be(ResultType.Error);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19));
        }

        [Fact]
        public void GetResultDetailError_WithParams_ShouldSetParameters()
        {
            var entity = 77;
            Expression<Func<object>>[] paramExpressions = { () => entity };

            var result = entity.GetResultDetailError(paramExpressions);

            result.ResultType.Should().Be(ResultType.Error);
            result.ResultData.Should().Be(entity);
            result.Parameters.Should().NotBeNull();
        }

        [Fact]
        public void GetResultDetailError_WithMessageDateAndParams_ShouldSetAllFields()
        {
            var entity = "error-entity";
            var message = "Error with params";
            var date = DateTime.UtcNow.ToString("o");
            int value = 55;
            Expression<Func<object>>[] paramExpressions = { () => value };

            var result = entity.GetResultDetailError(message, date, paramExpressions);

            result.ResultType.Should().Be(ResultType.Error);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Parameters.Should().NotBeNull();
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19));
        }

        [Fact]
        public async Task GetResultDetailErrorAsync_ShouldReturnErrorResultAsync()
        {
            var entity = "async-error";
            var result = await entity.GetResultDetailErrorAsync();

            result.ResultType.Should().Be(ResultType.Error);
            result.ResultData.Should().Be(entity);
            result.Message.Should().BeNull();
            result.Parameters.Should().BeNull();
        }

        [Fact]
        public async Task GetResultDetailErrorAsync_WithMessage_ShouldSetMessageAsync()
        {
            var entity = 3.14;
            var message = "Async error message";

            var result = await entity.GetResultDetailErrorAsync(message);

            result.ResultType.Should().Be(ResultType.Error);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Parameters.Should().BeNull();
        }

        [Fact]
        public async Task GetResultDetailErrorAsync_WithMessageAndDate_ShouldSetMessageAndDateAsync()
        {
            var entity = new { Name = "AsyncError" };
            var message = "Async error with date";
            var date = DateTime.UtcNow.ToString("o");

            var result = await entity.GetResultDetailErrorAsync(message, date);

            result.ResultType.Should().Be(ResultType.Error);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19));
        }

        [Fact]
        public async Task GetResultDetailErrorAsync_WithParams_ShouldSetParametersAsync()
        {
            var entity = 21;
            Expression<Func<object>>[] paramExpressions = { () => entity };

            var result = await entity.GetResultDetailErrorAsync(paramExpressions);

            result.ResultType.Should().Be(ResultType.Error);
            result.ResultData.Should().Be(entity);
            result.Parameters.Should().NotBeNull();
        }

        [Fact]
        public async Task GetResultDetailErrorAsync_WithMessageDateAndParams_ShouldSetAllFieldsAsync()
        {
            var entity = "async-error-all";
            var message = "All async error";
            var date = DateTime.UtcNow.ToString("o");
            int value = 101;
            Expression<Func<object>>[] paramExpressions = { () => value };

            var result = await entity.GetResultDetailErrorAsync(message, date, paramExpressions);

            result.ResultType.Should().Be(ResultType.Error);
            result.ResultData.Should().Be(entity);
            result.Message.Should().Be(message);
            result.Parameters.Should().NotBeNull();
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19));
        }

        public async Task GetResultDetailErrorAsync_FromException_ShouldSetErrorMessageAsync()
        {
            var exception = new InvalidOperationException("Async exception");
            var result = await exception.GetResultDetailExceptionAsync();

            result.ResultType.Should().Be(ResultType.Error);
            result.Message.Should().Contain("Async exception");
            result.Parameters.Should().BeNull();
        }

        [Fact]
        public async Task GetResultDetailErrorAsync_FromExceptionWithParams_ShouldSetErrorMessageAndParametersAsync()
        {
            var exception = new InvalidOperationException("Async exception with params");
            int value = 99;
            var result = await (exception.GetResultDetailExceptionAsync(() => value));

            result.ResultType.Should().Be(ResultType.Error);
            result.Message.Should().Contain("Async exception with params");
            result.Parameters.Should().NotBeNull();
            result.Parameters.Should().BeAssignableTo<IDictionary<string, object>>();
            var dict = (IDictionary<string, object>)result.Parameters;
            dict.Should().ContainKey("value");
            dict["value"].Should().Be(value);
        }

        [Fact]
        public void GetResultDetailError_FromExceptionGenericWithParams_ShouldSetErrorMessageAndParameters()
        {
            var exception = new InvalidOperationException("Generic exception with params");
            int value = 123;
            var result = exception.GetResultDetailException<string>(() => value);

            result.ResultType.Should().Be(ResultType.Error);
            result.Message.Should().Contain("Generic exception with params");
            result.Parameters.Should().NotBeNull();
            result.Parameters.Should().BeAssignableTo<IDictionary<string, object>>();
            var dict = (IDictionary<string, object>)result.Parameters;
            dict.Should().ContainKey("value");
            dict["value"].Should().Be(value);
        }

        [Fact]
        public async Task GetResultDetailErrorAsync_FromExceptionGenericWithParams_ShouldSetErrorMessageAndParametersAsync()
        {
            var exception = new InvalidOperationException("Async generic exception with params");
            int value = 555;
            var result = await exception.GetResultDetailExceptionAsync<string>(() => value);

            result.ResultType.Should().Be(ResultType.Error);
            result.Message.Should().Contain("Async generic exception with params");
            result.Parameters.Should().NotBeNull();
            result.Parameters.Should().BeAssignableTo<IDictionary<string, object>>();
            var dict = (IDictionary<string, object>)result.Parameters;
            dict.Should().ContainKey("value");
            dict["value"].Should().Be(value);
        }

        [Fact]
        public void GetResultDetailError_FromExceptionGeneric_ShouldSetErrorMessage()
        {
            var exception = new InvalidOperationException("Generic exception");
            var result = exception.GetResultDetailException<string>();

            result.ResultType.Should().Be(ResultType.Error);
            result.Message.Should().Contain("Generic exception");
            result.ResultData.Should().Be(default(string));
        }       

        [Fact]
        public async Task GetResultDetailErrorAsync_FromExceptionGeneric_ShouldSetErrorMessageAsync()
        {
            var exception = new InvalidOperationException("Async generic exception");
            var result = await exception.GetResultDetailExceptionAsync<string>();

            result.ResultType.Should().Be(ResultType.Error);
            result.Message.Should().Contain("Async generic exception");
            result.ResultData.Should().Be(default(string));
        }

      
        [Fact]
        public void GetResultDetailError_SerializeToJson_WithoutParameters_ShouldSerializeCorrectly()
        {
            var entity = "error-entity";
            var message = "Error message";
            var result = entity.GetResultDetailError(message);

            var json = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

            json.Should().Contain("\"resultType\":\"Error\"");
            json.Should().Contain("\"message\":\"Error message\"");
            json.Should().Contain("\"isSuccess\":false");
            json.Should().Contain("\"timestamp\"");
            json.Should().NotContain("\"parameters\"");
        }

        [Fact]
        public void GetResultDetailError_SerializeToJson_WithParameters_ShouldSerializeCorrectly()
        {
            var entity = 42;
            int value = 88;
            var result = entity.GetResultDetailError("Error with params", DateTime.UtcNow.ToString("o"), () => value);

            var json = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

            json.Should().Contain("\"resultType\":\"Error\"");
            json.Should().Contain("\"message\":\"Error with params\"");
            json.Should().Contain("\"isSuccess\":false");
            json.Should().Contain("\"timestamp\"");
            json.Should().Contain("\"parameters\"");
            json.Should().Contain("\"value\"");
            json.Should().Contain("88");
        }
    }
}
