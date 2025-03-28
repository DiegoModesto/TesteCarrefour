namespace DM.SharedKernel.CustomErrors;

public abstract class NotFoundException(string message) : Exception(message);