using FluentResults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
