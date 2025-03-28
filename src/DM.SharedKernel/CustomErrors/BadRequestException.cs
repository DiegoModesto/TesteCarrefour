namespace DM.SharedKernel.CustomErrors;

public abstract class BadRequestException(string message) : Exception(message);