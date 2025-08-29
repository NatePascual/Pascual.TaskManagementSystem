

using System.ComponentModel.DataAnnotations;

namespace Pascual.TaskManagementSystem.Domain.Exceptions;

public class ValidateException: Exception
{
    public Dictionary<string, string[]> Errors { get; } = new();

    public ValidateException() : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidateException(string message) : base(message)
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidateException(string message, Exception innerException) : base(message,innerException)
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidateException(IDictionary<string, string[]> errors) : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>(errors);
    }
}
