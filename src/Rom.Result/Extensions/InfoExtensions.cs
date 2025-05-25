using Rom.Result.Domain;
using Rom.Result.Factories;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;

namespace Rom.Result.Extensions
{
    /// <summary>
    /// Provides extension methods for generating detailed informational results.
    /// </summary>
    public static class InfoExtensions
    {
        /// <summary>
        /// Generates a detailed informational result for the specified entity.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailInfo<TEntity>(this TEntity result)
        {
            return ResultDetailFactory.Create(result, ResultType.Info);
        }

        /// <summary>
        /// Generates a detailed informational result for the specified entity with a message.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailInfo<TEntity>(this TEntity result, string message)
        {
            return ResultDetailFactory.Create(result, ResultType.Info, message);
        }

        /// <summary>
        /// Generates a detailed informational result for the specified entity with a message and date.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailInfo<TEntity>(this TEntity result, string message, string date)
        {
            return ResultDetailFactory.Create(result, ResultType.Info, message, date);
        }

        /// <summary>
        /// Generates a detailed informational result for the specified entity with a message, date, and parameters.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailInfo<TEntity>(this TEntity result, string message, string date, params Expression<Func<object>>[] paramExpressions)
        {
            return ResultDetailFactory.CreateWithParams(result, ResultType.Info, message, date, paramExpressions);
        }

        /// <summary>
        /// Generates a detailed informational result for the specified entity with parameters.
        /// </summary>
        public static ResultDetail<TEntity> GetResultDetailInfo<TEntity>(this TEntity result, params Expression<Func<object>>[] paramExpressions)
        {
            return ResultDetailFactory.CreateWithParams(result, ResultType.Info, null, null, paramExpressions);
        }

        /// <summary>
        /// Asynchronously generates a detailed informational result for the specified entity.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailInfoAsync<TEntity>(this TEntity result)
        {
            return Task.FromResult(ResultDetailFactory.Create(result, ResultType.Info));
        }

        /// <summary>
        /// Asynchronously generates a detailed informational result for the specified entity with a message.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailInfoAsync<TEntity>(this TEntity result, string message)
        {
            return Task.FromResult(ResultDetailFactory.Create(result, ResultType.Info, message));
        }

        /// <summary>
        /// Asynchronously generates a detailed informational result for the specified entity with a message and date.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailInfoAsync<TEntity>(this TEntity result, string message, string date)
        {
            return Task.FromResult(ResultDetailFactory.Create(result, ResultType.Info, message, date));
        }

        /// <summary>
        /// Asynchronously generates a detailed informational result for the specified entity with a message, date, and parameters.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailInfoAsync<TEntity>(this TEntity result, string message, string date, params Expression<Func<object>>[] paramExpressions)
        {
            return Task.FromResult(ResultDetailFactory.CreateWithParams(result, ResultType.Info, message, date, paramExpressions));
        }

        /// <summary>
        /// Asynchronously generates a detailed informational result for the specified entity with parameters.
        /// </summary>
        public static Task<ResultDetail<TEntity>> GetResultDetailInfoAsync<TEntity>(this TEntity result, params Expression<Func<object>>[] paramExpressions)
        {
            return Task.FromResult(ResultDetailFactory.CreateWithParams(result, ResultType.Info, null, null, paramExpressions));
        }
    }
}
