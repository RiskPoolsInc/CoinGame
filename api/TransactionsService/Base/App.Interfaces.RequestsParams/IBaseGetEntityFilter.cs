namespace App.Interfaces.RequestsParams;

public interface IBaseGetEntityFilter<TKey> {
    public TKey TaskId { get; set; }
}