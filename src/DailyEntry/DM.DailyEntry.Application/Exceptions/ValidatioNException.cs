using DM.SharedKernel.CustomErrors;

namespace DM.Application.Exceptions;

public sealed class ValidatioNException : BadRequestException
{
    public ValidatioNException(Dictionary<string, string[]> errors)
        : base("Validation erros occurred") => Errors = errors;
    
    public Dictionary<string, string[]> Errors { get; }
}