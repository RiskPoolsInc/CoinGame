using App.Web.Core.HAL.Builders;
using App.Web.Core.HAL.Generators;
using App.Web.Core.Metadata;

namespace App.Web.Core.HAL;

public class HalFactory : IHalFactory {
    private readonly Catalog _catalog;
    private readonly IPathGenerator _pathGenerator;
    private readonly ISchemaGenerator _schemaGenerator;

    public HalFactory(Catalog appCatalog, ISchemaGenerator schemaGenerator, IPathGenerator pathGenerator) {
        _schemaGenerator = schemaGenerator ?? throw new ArgumentNullException(nameof(schemaGenerator));
        _catalog = appCatalog ?? throw new ArgumentNullException(nameof(appCatalog));
        _pathGenerator = pathGenerator ?? throw new ArgumentNullException(nameof(pathGenerator));
    }

    public IHalSpecificationBuilder CreateSpecification(string title) {
        return new HalSpecification(title, _catalog, _pathGenerator, _schemaGenerator);
    }
}