using FluentResults;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common
{
    public abstract class ValidationObject
    {
        [NotMapped]
        public Result ValidationResult { get; private set; } = new Result();

        public bool IsValid()
        {
            return ValidationResult.IsSuccess;
        }
    }
}
