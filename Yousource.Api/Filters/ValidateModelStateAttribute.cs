namespace Yousource.Api.Filters
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Yousource.Api.Constants;
    using Yousource.Api.Messages;

    public class ValidateModelStateAttribute : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Keys.SelectMany(key => context.ModelState[key].Errors.Select(x => new { key, x.ErrorMessage }))
                        .Select(a => a.ErrorMessage)
                        .ToList();

                var response = new WebResponse
                {
                    Message = string.Join(string.Empty, errors),
                    ErrorCode = Errors.ModelState
                };

                context.Result = new BadRequestObjectResult(response);
            }
        }
    }
}
