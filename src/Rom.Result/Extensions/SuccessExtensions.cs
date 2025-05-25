using Rom.Result.Domain;
using Rom.Result.Factories;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rom.Result.Extensions
{
    /// <summary>
    /// Provides extension methods for generating detailed success results.
    /// </summary>
    public static class SuccessExtensions
    {
        /// <summary>
        /// Generates a detailed success result for the specified entity.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailSuccess<TEntity>(this TEntity result)
        {
            return ResultDetailFactory.Create(result, ResultType.Success);
        }

        /// <summary>
        /// Generates a detailed success result for the specified entity with a message.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailSuccess<TEntity>(this TEntity result, string message)
        {
            return ResultDetailFactory.Create(result, ResultType.Success, message);
        }

        /// <summary>
        /// Generates a detailed success result for the specified entity with a message and date.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailSuccess<TEntity>(this TEntity result, string message, string date)
        {
            return ResultDetailFactory.Create(result, ResultType.Success, message, date);
        }

        /// <summary>
        /// Generates a detailed success result for the specified entity with a message, date, and parameters.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailSuccess<TEntity>(this TEntity result, string message, string date, params Expression<Func<object>>[] paramExpressions)
        {
            return ResultDetailFactory.CreateWithParams(result, ResultType.Success, message, date, paramExpressions);
        }

        /// <summary>
        /// Generates a detailed success result for the specified entity with parameters.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailSuccess<TEntity>(this TEntity result, params Expression<Func<object>>[] paramExpressions)
        {
            return ResultDetailFactory.CreateWithParams(result, ResultType.Success, null, null, paramExpressions);
        }

        /// <summary>
        /// Asynchronously generates a detailed success result for the specified entity.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailSuccessAsync<TEntity>(this TEntity result)
        {
            return Task.FromResult(ResultDetailFactory.Create(result, ResultType.Success));
        }

        /// <summary>
        /// Asynchronously generates a detailed success result for the specified entity with a message.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailSuccessAsync<TEntity>(this TEntity result, string message)
        {
            return Task.FromResult(ResultDetailFactory.Create(result, ResultType.Success, message));
        }

        /// <summary>
        /// Asynchronously generates a detailed success result for the specified entity with a message and date.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailSuccessAsync<TEntity>(this TEntity result, string message, string date)
        {
            return Task.FromResult(ResultDetailFactory.Create(result, ResultType.Success, message, date));
        }

        /// <summary>
        /// Asynchronously generates a detailed success result for the specified entity with a message, date, and parameters.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailSuccessAsync<TEntity>(this TEntity result, string message, string date, params Expression<Func<object>>[] paramExpressions)
        {
            return Task.FromResult(ResultDetailFactory.CreateWithParams(result, ResultType.Success, message, date, paramExpressions));
        }

        /// <summary>
        /// Asynchronously generates a detailed success result for the specified entity with parameters.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailSuccessAsync<TEntity>(this TEntity result, params Expression<Func<object>>[] paramExpressions)
        {
            return Task.FromResult(ResultDetailFactory.CreateWithParams(result, ResultType.Success, null, null, paramExpressions));
        }
    }
}
