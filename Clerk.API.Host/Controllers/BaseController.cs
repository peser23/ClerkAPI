using System;
using Clerk.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Clerk.API.Controllers
{
    public class BaseController : ControllerBase
    {
        public BaseController()
        {

        }

        public string CurrentUserId
        {
            get
            {
                return User.Identity.Name;
            }
        }
        protected void LogException(Exception ex)
        {
            object actionArguments = null, userId = null;

            HttpContext.Items.TryGetValue("ActionRquestData", out actionArguments);
            HttpContext.Items.TryGetValue("ControllerName", out object controllerName);
            HttpContext.Items.TryGetValue("ActionName", out object actionName);
            throw (ex);
        }

        private string ErrorCode(Exception ex)
        {
            switch (ex)
            {
                case GenericCustomException genericException:
                    return genericException.ErrorCode;
                case NotFoundCustomException notFoundException:
                    return notFoundException.ErrorCode;
                case UnauthorizedCustomException unauthorizedCustomException:
                    return unauthorizedCustomException.ErrorCode;
                case Exception systemException:
                    return "B0x005";
                default:
                    break;
            }
            return "";
        }
    }
}
