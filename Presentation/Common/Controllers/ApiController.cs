using Application.Common.Behaviors;
using Domain.Common.Errors;
using Domain.Errors;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Presentation.Common.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        [NonAction]
        protected virtual IActionResult HandleErrors(IError error)
        {
            if (error is ObjectInInvalidState)
                return HandleObjectErrors(error);

            if (error is ValidationError)
                return HandleValidationErrors(error);

            if (error is not IStatusCodeError)
                return Problem(title: error.Message);

            return HandleSimpleStatusCodeErrors(error);
        }

        private IActionResult HandleObjectErrors(IError error)
        {
            ObjectInInvalidState validationError = (ObjectInInvalidState)error;

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
