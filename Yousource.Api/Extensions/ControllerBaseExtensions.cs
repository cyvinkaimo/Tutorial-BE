namespace Yousource.Api.Extensions
{
    using System;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Yousource.Api.Messages;
    using Yousource.Api.Models.User;

    public static class ControllerBaseExtensions
    {
        public static IActionResult CreateResponse<T>(this ControllerBase controller, T value) where T : WebResponse
        {
            var result = default(IActionResult);
            result = controller.StatusCode(value.StatusCode, value);
            return result;
        }

        public static UserWebModel GetCurrentUser(this ControllerBase controller)
        {
            var result = new UserWebModel();

            if (controller.User != default(ClaimsPrincipal))
            {
                result.UserId = new Guid(controller.User.FindFirstValue(ClaimTypes.NameIdentifier));
                result.FirstName = controller.User.FindFirstValue(ClaimTypes.GivenName);
                result.LastName = controller.User.FindFirstValue(ClaimTypes.Surname);
                result.UserName = controller.User.FindFirstValue(ClaimTypes.Name);
            }

            return result;
        }
    }
}
