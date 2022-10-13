using Masa.Utils.Exceptions;

namespace Assignment.GlobalExceptionDemo;

public class ExceptionHandler : IMasaExceptionHandler
{
    public void OnException(MasaExceptionContext context)
    {
        if (context.Exception is ArgumentNullException ex)
        {
            context.ToResult($"{ex.ParamName}不能为空");
        }
    }
}
