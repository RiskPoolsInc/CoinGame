namespace App.Security.Annotation;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class AccessAttribute : Attribute {
}