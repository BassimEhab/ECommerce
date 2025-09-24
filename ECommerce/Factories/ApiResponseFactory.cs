using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace ECommerce.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorResponse(ActionContext context)
        {
            var Errors = context.ModelState.Where(x => x.Value.Errors.Any())
                .Select(x => new ValidationError()
                {
                    Field = x.Key,
                    Errors = x.Value.Errors.Select(e => e.ErrorMessage)
                });
            var Response = new ValidationErrorToReturn()
            {
                StatusCode = 400,
                ValidationErrors = Errors
            };
            return new BadRequestObjectResult(Response);
        }
    }
}
