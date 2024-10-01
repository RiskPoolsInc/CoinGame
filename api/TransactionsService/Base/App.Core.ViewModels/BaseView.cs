namespace App.Core.ViewModels;

public abstract class BaseView<TKey> {
    public TKey Id { get; set; }

    public DateTime CreatedOn { get; set; }
}

public abstract class BaseView : BaseView<Guid> {
}