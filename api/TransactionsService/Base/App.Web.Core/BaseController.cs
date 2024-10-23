using System.Reflection;

using App.Web.Core.Models;

using Microsoft.AspNetCore.Mvc;

namespace App.Web.Core;

public abstract class BaseController : ControllerBase {
    protected IActionResult Version() {
        var executingAssembly = Assembly.GetExecutingAssembly();
        var version = executingAssembly.GetName().Version.ToString();

        return Ok(new VersionInfo {
            Version = version
        });
    }
}