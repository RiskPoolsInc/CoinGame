using App.Data.Entities.Tasks;

namespace CF.Tests;

public class Tests {
    [SetUp]
    public void Setup() {
    }

    [Test]
    public void UpdateTaskHistoryEntity() {
        var entity = new TaskEntity {
            Award = 200
        };

        var updated = new TaskEntity {
            Award = 100
        };


        var type = entity.GetType();
        var typeProperties = type.GetProperties();

        var properties = typeProperties.Where(propertyInfo => propertyInfo is {
                                            CanWrite: true, PropertyType.IsValueType: true, PropertyType.IsEnum: false
                                        })
                                       .ToList();

        foreach (var propertyInfo in properties) {
            var propertyTypeName = propertyInfo.PropertyType.Name;

            switch (propertyTypeName.ToLower()) {
                case "int32":
                    break;
                case "guid":
                    break;
                case "string":
                    break;
                case "long":
                    break;
                case "decimal":
                    break;
                case "boolean":
                    break;
            }


            var currentValue = propertyInfo.GetValue(entity);
            var updatedValue = propertyInfo.GetValue(updated);
        }

        Assert.Pass();
    }
}