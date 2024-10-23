using App.Web.Core.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.Web.Core.Filters;

public class ValidationModelFilter : ActionFilterAttribute {
    public override void OnActionExecuting(ActionExecutingContext context) {
        if (!context.ModelState.IsValid)
            context.Result = new BadRequestObjectResult(new ValidationResultModel(context.ModelState));
        base.OnActionExecuting(context);
    }
}