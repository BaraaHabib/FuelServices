using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelServices.Api.Helpers.Attributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                                                .Where(e => e.Value.Errors.Count > 0)
                                                .Select(e =>
                                                e.Value.Errors.First().ErrorMessage
                                                //Error
                                                //{
                                                //    Name = e.Key,
                                                //    Message = e.Value.Errors.First().ErrorMessage
                                                //}
                                                ).ToArray();
                var response = new Response<object>(Constants.SOMETHING_WRONG_CODE, errors, "Validation Errors");
                context.Result = new BadRequestObjectResult(response);
            }
        }
    }
}
