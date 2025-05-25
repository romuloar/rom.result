using System;
using System.Text.Json.Serialization;

namespace Rom.Result.Domain
{
    /// <summary>
    /// Represents a detailed result of an operation, 
    /// including the result type, parameters, success status, 
    /// message, and timestamp.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ResultDetail<TEntity> : ResultDetail
    {
        public TEntity ResultData { get; set; }
    }

    /// <summary>
    /// Represents a detailed result of an operation,
    /// </summary>
    public class ResultDetail
    {
        /// <summary>
        /// The type of result, indicating success, error, info, or warning.
        /// </summary>
        [JsonIgnore]
        public ResultType ResultType { get; set; }

        /// <summary>
        /// The name of the result type, serialized as a string.
        /// </summary>
        [JsonPropertyName("resultType")]
        public string ResultTypeName { get { return ResultType.ToString(); } }

        /// <summary>
        /// The parameters associated with the result,
        /// </summary>
        [JsonPropertyName("parameters")]
        public object Parameters { get; set; }

        /// <summary>
        /// Indicates whether the result is successful.
        /// </summary>
        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get { return ResultType != ResultType.Error; } }

        /// <summary>
        /// A message providing additional information about the result.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// The timestamp of when the result was created, defaulting to the current UTC time.
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// The timestamp of when the result was created, formatted as a string in ISO 8601 format.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public string TimestampString => Timestamp.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
    }
}
