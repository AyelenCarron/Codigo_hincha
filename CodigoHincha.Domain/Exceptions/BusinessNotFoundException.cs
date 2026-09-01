namespace CodigoHincha.Domain.Exceptions;

public class BusinessNotFoundException : BusinessException
{
    public BusinessNotFoundException(string message)
        : base(message)
    {
    }
}
