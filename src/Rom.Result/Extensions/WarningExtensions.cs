using Rom.Result.Domain;
using Rom.Result.Factories;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rom.Result.Extensions
{
    /// <summary>
    /// Provides extension methods for generating detailed warning results.
    /// </summary>
    public static class WarningExtensions
    {
        /// <summary>
        /// Generates a detailed warning result for the specified entity.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailWarning<TEntity>(this TEntity result)
        {
            return ResultDetailFactory.Create(result, ResultType.Warning);
        }

        /// <summary>
        /// Generates a detailed warning result for the specified entity with a message.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailWarning<TEntity>(this TEntity result, string message)
        {
            return ResultDetailFactory.Create(result, ResultType.Warning, message);
        }

        /// <summary>
        /// Generates a detailed warning result for the specified entity with a message and date.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailWarning<TEntity>(this TEntity result, string message, string date)
        {
            return ResultDetailFactory.Create(result, ResultType.Warning, message, date);
        }

        /// <summary>
        /// Generates a detailed warning result for the specified entity with a message, date, and parameters.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailWarning<TEntity>(this TEntity result, string message, string date, params Expression<Func<object>>[] paramExpressions)
        {
            return ResultDetailFactory.CreateWithParams(result, ResultType.Warning, message, date, paramExpressions);
        }

        /// <summary>
        /// Generates a detailed warning result for the specified entity with parameters.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailWarning<TEntity>(this TEntity result, params Expression<Func<object>>[] paramExpressions)
        {
            return ResultDetailFactory.CreateWithParams(result, ResultType.Warning, null, null, paramExpressions);
        }

        /// <summary>
        /// Asynchronously generates a detailed warning result for the specified entity.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailWarningAsync<TEntity>(this TEntity result)
        {
            return Task.FromResult(ResultDetailFactory.Create(result, ResultType.Warning));
        }

        /// <summary>
        /// Asynchronously generates a detailed warning result for the specified entity with a message.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailWarningAsync<TEntity>(this TEntity result, string message)
        {
            return Task.FromResult(ResultDetailFactory.Create(result, ResultType.Warning, message));
        }

        /// <summary>
        /// Asynchronously generates a detailed warning result for the specified entity with a message and date.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailWarningAsync<TEntity>(this TEntity result, string message, string date)
        {
            return Task.FromResult(ResultDetailFactory.Create(result, ResultType.Warning, message, date));
        }

        /// <summary>
        /// Asynchronously generates a detailed warning result for the specified entity with a message, date, and parameters.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailWarningAsync<TEntity>(this TEntity result, string message, string date, params Expression<Func<object>>[] paramExpressions)
        {
            return Task.FromResult(ResultDetailFactory.CreateWithParams(result, ResultType.Warning, message, date, paramExpressions));
        }

        /// <summary>
        /// Asynchronously generates a detailed warning result for the specified entity with parameters.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailWarningAsync<TEntity>(this TEntity result, params Expression<Func<object>>[] paramExpressions)
        {
            return Task.FromResult(ResultDetailFactory.CreateWithParams(result, ResultType.Warning, null, null, paramExpressions));
        }
    }
}
