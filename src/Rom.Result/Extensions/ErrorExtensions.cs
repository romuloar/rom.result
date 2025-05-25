using Rom.Result.Domain;
using Rom.Result.Factories;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;

namespace Rom.Result.Extensions
{
    /// <summary>
    /// Provides extension methods for generating detailed error results.
    /// </summary>
    public static class ErrorExtensions
    {
        /// <summary>
        /// Generates a detailed error result for the specified entity.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailError<TEntity>(this TEntity result)
        {
            return ResultDetailFactory.Create(result, ResultType.Error);
        }

        /// <summary>
        /// Generates a detailed error result for the specified entity with a message.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailError<TEntity>(this TEntity result, string message)
        {
            return ResultDetailFactory.Create(result, ResultType.Error, message);
        }

        /// <summary>
        /// Generates a detailed error result for the specified entity with a message and date.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailError<TEntity>(this TEntity result, string message, string date)
        {
            return ResultDetailFactory.Create(result, ResultType.Error, message, date);
        }

        /// <summary>
        /// Generates a detailed error result for the specified entity with a message, date, and parameters.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailError<TEntity>(this TEntity result, string message, string date, params Expression<Func<object>>[] paramExpressions)
        {
            return ResultDetailFactory.CreateWithParams(result, ResultType.Error, message, date, paramExpressions);
        }

        /// <summary>
        /// Generates a detailed error result for the specified entity with parameters.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailError<TEntity>(this TEntity result, params Expression<Func<object>>[] paramExpressions)
        {
            return ResultDetailFactory.CreateWithParams(result, ResultType.Error, null, null, paramExpressions);
        }

        /// <summary>
        /// Asynchronously generates a detailed error result for the specified entity.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailErrorAsync<TEntity>(this TEntity result)
        {
            return Task.FromResult(ResultDetailFactory.Create(result, ResultType.Error));
        }

        /// <summary>
        /// Asynchronously generates a detailed error result for the specified entity with a message.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailErrorAsync<TEntity>(this TEntity result, string message)
        {
            return Task.FromResult(ResultDetailFactory.Create(result, ResultType.Error, message));
        }

        /// <summary>
        /// Asynchronously generates a detailed error result for the specified entity with a message and date.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailErrorAsync<TEntity>(this TEntity result, string message, string date)
        {
            return Task.FromResult(ResultDetailFactory.Create(result, ResultType.Error, message, date));
        }

        /// <summary>
        /// Asynchronously generates a detailed error result for the specified entity with a message, date, and parameters.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailErrorAsync<TEntity>(this TEntity result, string message, string date, params Expression<Func<object>>[] paramExpressions)
        {
            return Task.FromResult(ResultDetailFactory.CreateWithParams(result, ResultType.Error, message, date, paramExpressions));
        }

        /// <summary>
        /// Asynchronously generates a detailed error result for the specified entity with parameters.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailErrorAsync<TEntity>(this TEntity result, params Expression<Func<object>>[] paramExpressions)
        {
            return Task.FromResult(ResultDetailFactory.CreateWithParams(result, ResultType.Error, null, null, paramExpressions));
        }

        /// <summary>
        /// Generates a detailed error result from an exception.
        /// </summary>
        public static ResultDetail GetResultDetailException(this Exception exception)
        {
            return ResultDetailFactory.CreateFromException(exception);
        }

        /// <summary>
        /// Generates a detailed error result from an exception with parameters.
        /// </summary>
        public static ResultDetail GetResultDetailException(this Exception exception, params Expression<Func<object>>[] paramExpressions)
        {
            return ResultDetailFactory.CreateFromException(exception, paramExpressions);
        }

        /// <summary>
        /// Asynchronously generates a detailed error result from an exception.
        /// </summary>
        public static Task<ResultDetail> GetResultDetailExceptionAsync(this Exception exception)
        {
            return Task.FromResult(ResultDetailFactory.CreateFromException(exception));
        }

        /// <summary>
        /// Asynchronously generates a detailed error result from an exception with parameters.
        /// </summary>
        public static Task<ResultDetail> GetResultDetailExceptionAsync(this Exception exception, params Expression<Func<object>>[] paramExpressions)
        {
            return Task.FromResult(ResultDetailFactory.CreateFromException(exception, paramExpressions));
        }

        /// <summary>
        /// Generates a detailed error result from an exception for the specified entity type.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailException<TEntity>(this Exception exception)
        {
            return ResultDetailFactory.CreateFromException<TEntity>(exception);
        }

        /// <summary>
        /// Generates a detailed error result from an exception for the specified entity type with parameters.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailException<TEntity>(this Exception exception, params Expression<Func<object>>[] paramExpressions)
        {
            return ResultDetailFactory.CreateFromException<TEntity>(exception, paramExpressions);
        }

        /// <summary>
        /// Asynchronously generates a detailed error result from an exception for the specified entity type.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailExceptionAsync<TEntity>(this Exception exception)
        {
            return Task.FromResult(ResultDetailFactory.CreateFromException<TEntity>(exception));
        }

        /// <summary>
        /// Asynchronously generates a detailed error result from an exception for the specified entity type with parameters.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailExceptionAsync<TEntity>(this Exception exception, params Expression<Func<object>>[] paramExpressions)
        {
            return Task.FromResult(ResultDetailFactory.CreateFromException<TEntity>(exception, paramExpressions));
        }
    }
}
