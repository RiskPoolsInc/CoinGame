using Newtonsoft.Json.Schema;

namespace App.Web.Core.HAL;

public interface IHalSpecification : IHalBuilder {
    IHalSpecification AddLink<TController>(string methodName, JSchema schema = null);
}