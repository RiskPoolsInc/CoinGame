namespace App.Web.Core.HAL;

public interface IHalFactory {
    IHalSpecificationBuilder CreateSpecification(string title);
}