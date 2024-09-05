using Microsoft.AspNetCore.Mvc.Controllers;

namespace App.Web.Core.Metadata;

public class ControllerActions : List<ControllerActionDescriptor> {
    public ControllerActions(ControllerActionDescriptor[] actions) : base(actions) {
    }
}