using FluentAssertions;
using Rom.Result.Domain;
using Rom.Result.Factories;

namespace Rom.Result.Test.Factories
{
    public class ResultDetailFactoryTest
    {
        [Fact]
        public void Create_ShouldReturnResultDetail_WithCorrectProperties()
        {
            // Arrange
            var type = ResultType.Success;
            var message = "Test message";
            var date = DateTimeOffset.UtcNow.ToString("o");
            var parameters = new { Id = 1 };

            // Act
            var result = ResultDetailFactory.Create(type, message, date, parameters);

            // Assert
            result.ResultType.Should().Be(type);
            result.Message.Should().Be(message);
            result.Parameters.Should().Be(parameters);
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19));
        }

        [Fact]
        public void GenericCreate_ShouldReturnResultDetailT_WithCorrectProperties()
        {
            // Arrange
            var data = 123;
            var type = ResultType.Info;
            var message = "Info message";
            var date = DateTimeOffset.UtcNow.ToString("o");
            var parameters = new { Name = "Test" };

            // Act
            var result = ResultDetailFactory.Create(data, type, message, date, parameters);

            // Assert
            result.ResultData.Should().Be(data);
            result.ResultType.Should().Be(type);
            result.Message.Should().Be(message);
            result.Parameters.Should().Be(parameters);
            result.Timestamp.ToString("o").Should().StartWith(date.Substring(0, 19));
        }

        [Fact]
        public void CreateWithParams_ShouldCaptureParameters()
        {
            // Arrange
            int id = 42;
            string name = "Test";

            // Act
            var result = ResultDetailFactory.CreateWithParams(ResultType.Warning, "Params", null, () => id, () => name);

            // Assert
            result.ResultType.Should().Be(ResultType.Warning);
            result.Message.Should().Be("Params");
            result.Parameters.Should().BeOfType<Dictionary<string, object>>();
            var dict = (System.Collections.Generic.Dictionary<string, object>)result.Parameters;
            dict["id"].Should().Be(id);
            dict["name"].Should().Be(name);
        }

        [Fact]
        public void GenericCreateWithParams_ShouldCaptureParameters()
        {
            // Arrange
            var data = "entity";
            double value = 3.14;

            // Act
            var result = ResultDetailFactory.CreateWithParams(data, ResultType.Info, "Params", null, () => value);

            // Assert
            result.ResultData.Should().Be(data);
            result.Parameters.Should().BeOfType<Dictionary<string, object>>();
            var dict = (System.Collections.Generic.Dictionary<string, object>)result.Parameters;
            dict["value"].Should().Be(value);
        }

        [Fact]
        public void CreateFromException_ShouldReturnErrorResult()
        {
            // Arrange
            var ex = new InvalidOperationException("fail");

            // Act
            var result = ResultDetailFactory.CreateFromException(ex);

            // Assert
            result.ResultType.Should().Be(ResultType.Error);
            result.Message.Should().Contain("fail");
            result.Parameters.Should().BeNull();
        }

        [Fact]
        public void CreateFromException_WithParams_ShouldCaptureParameters()
        {
            // Arrange
            var ex = new Exception("outer", new Exception("inner"));
            int code = 500;

            // Act
            var result = ResultDetailFactory.CreateFromException(ex, () => code);

            // Assert
            result.ResultType.Should().Be(ResultType.Error);
            result.Message.Should().Contain("outer");
            result.Message.Should().Contain("inner");
            result.Parameters.Should().BeOfType<Dictionary<string, object>>();
            var dict = (System.Collections.Generic.Dictionary<string, object>)result.Parameters;
            dict["code"].Should().Be(code);
        }

        [Fact]
        public void GenericCreateFromException_ShouldReturnErrorResultWithDefaultData()
        {
            // Arrange
            var ex = new Exception("error");

            // Act
            var result = ResultDetailFactory.CreateFromException<int>(ex);

            // Assert
            result.ResultType.Should().Be(ResultType.Error);
            result.ResultData.Should().Be(default(int));
            result.Message.Should().Contain("error");
        }
    }
}
