using Microsoft.AspNetCore.Mvc.Controllers;

namespace App.Web.Core.Metadata;

public class ControllerInfo : Dictionary<string, ControllerActions> {
    private const string ASYNC_POSTFIX = "Async";

    public ControllerInfo(string contollerName, ControllerActions actions) {
        ControllerName = contollerName ?? throw new ArgumentNullException(nameof(contollerName));
        Actions = actions ?? throw new ArgumentNullException(nameof(actions));
    }

    public string ControllerName { get; }
    public ControllerActions Actions { get; }

    public ControllerActionDescriptor[] GetDescriptors(string actionName) {
        if (string.IsNullOrWhiteSpace(actionName))
            throw new ArgumentNullException(nameof(actionName));

        if (actionName.EndsWith(ASYNC_POSTFIX))
            actionName = actionName.Substring(0, actionName.Length - ASYNC_POSTFIX.Length);
        return Actions.Where(a => a.ActionName == actionName).ToArray();
    }
}