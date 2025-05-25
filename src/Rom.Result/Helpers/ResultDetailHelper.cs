using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Rom.Result.Helpers
{
    internal static class ResultDetailHelper
    {
        public static Dictionary<string, object> CaptureParams(params Expression<Func<object>>[] paramExpressions)
        {
            var dict = new Dictionary<string, object>();
            if (paramExpressions == null)
                return dict;

            foreach (var expr in paramExpressions)
            {
                if (expr.Body is MemberExpression member)
                {
                    var name = member.Member.Name;
                    var value = expr.Compile().Invoke();
                    dict[name] = value ?? throw new ArgumentNullException(nameof(value));
                }
                else if (expr.Body is UnaryExpression unary && unary.Operand is MemberExpression innerMember)
                {
                    var name = innerMember.Member.Name;
                    var value = expr.Compile().Invoke();
                    dict[name] = value ?? throw new ArgumentNullException(nameof(value));
                }
            }
            return dict;
        }

        public static string GetExceptionMessage(Exception exception)
        {
            var erroBuilder = new StringBuilder();
            erroBuilder.AppendLine(exception.Message);

            if (exception.InnerException != null)
            {
                erroBuilder.AppendLine("\n ");
                erroBuilder.AppendLine(exception.InnerException.Message);
            }

            if (exception.InnerException != null && exception.InnerException.InnerException != null)
            {
                erroBuilder.AppendLine("\n ");
                erroBuilder.AppendLine(exception.InnerException.InnerException.Message);
            }

            return erroBuilder.ToString();
        }
    }
}
