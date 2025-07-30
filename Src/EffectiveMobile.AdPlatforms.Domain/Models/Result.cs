namespace EffectiveMobile.AdPlatforms.Domain.Models;

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; private set; }
    public T Response { get; private set; }
    
    public Result(T response, Error? error = null)
    {
        Response = response;
        Error = error;
        IsSuccess = error is null;
    }
}

public class Result : Result<bool>
{
    public Result(Error? error = null) : base(error is null, error)
    {
    }
}