using Application.Common.Behaviors;
using Application.Common.Errors;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Common.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        [NonAction]
        protected virtual IActionResult HandleErrors(IError error)
        {
            if (error is ValidationError)
                return HandleValidationErrors(error);

            if (error is not IStatusCodeError)
                return Problem(title: error.Message);

            return HandleSimpleStatusCodeErrors(error);
        }

        private IActionResult HandleSimpleStatusCodeErrors(IError error)
        {
            IStatusCodeError statusCodeError = (IStatusCodeError)error;

            return Problem(statusCode: (int)statusCodeError.Code, title: statusCodeError.Message);
        }

        private IActionResult HandleValidationErrors(IError error)
        {
            ValidationError validationError = (ValidationError)error;

            ModelStateDictionary errorMap = new ModelStateDictionary();

            foreach (IError property in validationError.Reasons)
            {
                foreach (IError propertyError in property.Reasons)
                {
                    errorMap.AddModelError(property.Message, propertyError.Message);
                }
            }

            return ValidationProblem(errorMap);
        }
    }
}
