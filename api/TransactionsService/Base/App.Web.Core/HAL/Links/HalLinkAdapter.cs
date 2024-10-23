using System.Reflection;

using App.Common;
using App.Web.Core.HAL.Attributes;
using App.Web.Core.HAL.Generators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Routing;

namespace App.Web.Core.HAL.Links;

internal class HalLinkAdapter {
    private readonly ControllerActionDescriptor _actionDescriptor;
    private readonly AttributeLazy<HalAttribute> _halLinkAttr;
    private readonly AttributeLazy<HttpMethodAttribute> _httpMethodAttr;
    private readonly AttributeLazy<ProducesAttribute> _producesAttr;
    private readonly ISchemaGenerator _schemaGenerator;
    private IHalLinkBuilder _halLinkBuilder;

    public HalLinkAdapter(ISchemaGenerator schemaGenerator, MethodInfo methodInfo, ControllerActionDescriptor actionDescriptor) {
        _schemaGenerator = schemaGenerator ?? throw new ArgumentNullException(nameof(schemaGenerator));
        _actionDescriptor = actionDescriptor ?? throw new ArgumentNullException(nameof(actionDescriptor));
        _httpMethodAttr = new AttributeLazy<HttpMethodAttribute>(methodInfo);
        _halLinkAttr = new AttributeLazy<HalAttribute>(methodInfo);
        _producesAttr = new AttributeLazy<ProducesAttribute>(methodInfo);
    }

    public bool IsOptions() {
        return _httpMethodAttr.Value.HttpMethods.FirstOrDefault() == HttpMethods.Options;
    }

    public IHalLinkBuilder Make() {
        _halLinkBuilder = new HalLinkBuilder(_halLinkAttr.Value?.Rel, _schemaGenerator);
        AddLinkAttributes();
        AddLinkMethod();
        AddLinkMediaType();
        AddLinkPropertyBody();
        return _halLinkBuilder;
    }

    private void AddLinkAttributes() {
        _halLinkBuilder.AddRel(_halLinkAttr.Value?.Rel)
                       .AddTitle(_halLinkAttr.Value?.Title);
    }

    private void AddLinkMethod() {
        _halLinkBuilder.AddMethod(_httpMethodAttr.Value?.HttpMethods.FirstOrDefault());
    }

    private void AddLinkMediaType() {
        _halLinkBuilder.AddMediaType(_producesAttr.Value?.ContentTypes.FirstOrDefault());
    }

    private void AddLinkPropertyBody() {
        if (_halLinkAttr.Value?.TypeBody == null)
            AddLinkPropertyFromBodyParameter();
        else
            _halLinkBuilder.AddProperties(_halLinkAttr.Value?.TypeBody);
    }

    private void AddLinkPropertyFromBodyParameter() {
        var bodyParameter = _actionDescriptor.Parameters.FirstOrDefault(p => p.BindingInfo?.BindingSource.Id == HalConstants.Body);

        if (bodyParameter != null)
            _halLinkBuilder.AddProperties(bodyParameter.ParameterType);
    }
}