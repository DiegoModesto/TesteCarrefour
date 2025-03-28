namespace DM.SharedKernel.Common;

public sealed class Result<T> where T : class
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? ResultSet { get; init; }
    public Error Error { get; } 

    private Result(bool isSuccess, Error error, T? resultSet = default)
    {
        if (
            isSuccess && error != Error.None
            ||
            !isSuccess && error == Error.None
        )
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
        ResultSet = resultSet;
    }

    public static Result<T> Success() => new(true, Error.None);
    public static Result<T> Success(T resultSet) => new(true, Error.None, resultSet);
    public static Result<T> Failure(Error error) => new(false, error);
}