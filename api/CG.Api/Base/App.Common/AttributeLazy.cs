using System.Reflection;

namespace App.Common;

public class AttributeLazy<T>
    where T : Attribute {
    private readonly MethodInfo _methodInfo;
    private T _attribute;
    private bool _attributeInitialized;

    public AttributeLazy(MethodInfo methodInfo) {
        _methodInfo = methodInfo;
    }

    public T Value {
        get {
            if (!_attributeInitialized) {
                _attributeInitialized = true;
                _attribute = _methodInfo.GetCustomAttributes<T>().FirstOrDefault();
            }

            return _attribute;
        }
    }
}