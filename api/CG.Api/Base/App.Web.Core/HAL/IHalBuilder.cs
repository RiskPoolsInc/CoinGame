using Newtonsoft.Json.Linq;

namespace App.Web.Core.HAL;

public interface IHalBuilder {
    JObject Build();
}