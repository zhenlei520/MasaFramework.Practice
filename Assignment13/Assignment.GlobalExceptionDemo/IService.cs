using Masa.Utils.Exceptions;

namespace Assignment.GlobalExceptionDemo;

public interface IService
{

}

public class ExceptionHandler : IMasaExceptionHandler
{
    public void OnException(MasaExceptionContext context)
    {
        throw new NotImplementedException();
    }
}
