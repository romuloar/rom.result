using Rom.Result.Domain;
using Rom.Result.Helpers;
using System;
using System.Linq.Expressions;

namespace Rom.Result.Factories
{
    internal static class ResultDetailFactory
    {
        public static ResultDetail Create(
            ResultType resultType,
            string message = null,
            string date = null,
            object parameters = null)
        {
            return new ResultDetail
            {
                ResultType = resultType,
                Message = message,
                Parameters = parameters,
                Timestamp = ParseDateOrNow(date)
            };
        }

        public static ResultDetail<TEntity> Create<TEntity>(
            TEntity resultData,
            ResultType resultType,
            string message = null,
            string date = null,
            object parameters = null)
        {
            return new ResultDetail<TEntity>
            {
                ResultData = resultData,
                ResultType = resultType,
                Message = message,
                Parameters = parameters,
                Timestamp = ParseDateOrNow(date)
            };
        }

        public static ResultDetail CreateWithParams(
            ResultType resultType,
            string message = null,
            string date = null,
            params Expression<Func<object>>[] paramExpressions)
        {
            var parameters = ResultDetailHelper.CaptureParams(paramExpressions ?? Array.Empty<Expression<Func<object>>>());
            return Create(
                resultType,
                message,
                date,
                parameters.Count > 0 ? (object)parameters : null
            );
        }

        public static ResultDetail<TEntity> CreateWithParams<TEntity>(
            TEntity resultData,
            ResultType resultType,
            string message = null,
            string date = null,
            params Expression<Func<object>>[] paramExpressions)
        {
            var parameters = ResultDetailHelper.CaptureParams(paramExpressions ?? Array.Empty<Expression<Func<object>>>());
            return Create(
                resultData,
                resultType,
                message,
                date,
                parameters.Count > 0 ? (object)parameters : null
            );
        }

        public static ResultDetail CreateFromException(Exception exception, params Expression<Func<object>>[] paramExpressions)
        {
            return CreateWithParams(
                ResultType.Error,
                ResultDetailHelper.GetExceptionMessage(exception),
                null,
                paramExpressions ?? Array.Empty<Expression<Func<object>>>()
            );
        }

        public static ResultDetail<TEntity> CreateFromException<TEntity>(Exception exception, params Expression<Func<object>>[] paramExpressions)
        {
            return CreateWithParams(
                default(TEntity),
                ResultType.Error,
                ResultDetailHelper.GetExceptionMessage(exception),
                null,
                paramExpressions ?? Array.Empty<Expression<Func<object>>>()
            );
        }

        private static DateTimeOffset ParseDateOrNow(string date)
        {
            if (!string.IsNullOrWhiteSpace(date) && DateTimeOffset.TryParse(date, out var parsed))
                return parsed;
            return DateTimeOffset.UtcNow;
        }
    }
}
