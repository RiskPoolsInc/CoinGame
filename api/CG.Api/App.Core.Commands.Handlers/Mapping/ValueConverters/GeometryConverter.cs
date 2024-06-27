using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace App.Core.Commands.Handlers.Mapping.ValueConverters {

internal class GeometryConverter : IValueConverter<double[][], Polygon>
{
    public Polygon Convert(double[][] sourceMember, ResolutionContext context)
    {
        if (sourceMember == null) return null;

        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(4326);
        var polygon = geometryFactory.CreatePolygon(sourceMember.Select(x => new Coordinate(x[0], x[1])).ToArray());

        polygon.Normalize();
        return (Polygon) polygon.Reverse();
    }
}
}