using Rom.Result.Domain;
using Rom.Result.Factories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rom.Result.Extensions
{
    /// <summary>
    /// Provides static methods to create error ResultDetail objects without requiring an instance.
    /// </summary>
    public static class ResultDetailExtensions
    {
        /// <summary>
        /// Creates a ResultDetail of type T with error result type and message.
        /// </summary>
        public static ResultDetail<T> GetError<T>(string message)
        {
            return ResultDetailFactory.Create(default(T), ResultType.Error, message);
        }

        /// <summary>
        /// Creates a ResultDetail of type T with error result type, message, and date.
        /// </summary>
        public static ResultDetail<T> GetError<T>(string message, string date)
        {
            return ResultDetailFactory.Create(default(T), ResultType.Error, message, date);
        }

        /// <summary>
        /// Creates a ResultDetail of type T with error result type, message, date, and parameters.
        /// </summary>
        public static ResultDetail<T> GetError<T>(string message, string date, params Expression<Func<object>>[] paramExpressions)
        {
            return ResultDetailFactory.CreateWithParams(default(T), ResultType.Error, message, date, paramExpressions);
        }

        /// <summary>
        /// Creates a ResultDetail of type T with error result type, message, and parameters.
        /// </summary>
        public static ResultDetail<T> GetError<T>(string message, params Expression<Func<object>>[] paramExpressions)
        {
            return ResultDetailFactory.CreateWithParams(default(T), ResultType.Error, message, null, paramExpressions);
        }

        /// <summary>
        /// Asynchronously creates a ResultDetail of type T with error result type and message.
        /// </summary>
        public static Task<ResultDetail<T>> GetErrorAsync<T>(string message)
        {
            return Task.FromResult(GetError<T>(message));
        }

        /// <summary>
        /// Asynchronously creates a ResultDetail of type T with error result type, message, and date.
        /// </summary>
        public static Task<ResultDetail<T>> GetErrorAsync<T>(string message, string date)
        {
            return Task.FromResult(GetError<T>(message, date));
        }

        /// <summary>
        /// Asynchronously creates a ResultDetail of type T with error result type, message, date, and parameters.
        /// </summary>
        public static Task<ResultDetail<T>> GetErrorAsync<T>(string message, string date, params Expression<Func<object>>[] paramExpressions)
        {
            return Task.FromResult(GetError<T>(message, date, paramExpressions));
        }

        /// <summary>
        /// Asynchronously creates a ResultDetail of type T with error result type, message, and parameters.
        /// </summary>
        public static Task<ResultDetail<T>> GetErrorAsync<T>(string message, params Expression<Func<object>>[] paramExpressions)
        {
            return Task.FromResult(GetError<T>(message, paramExpressions));
        }
    }
}
