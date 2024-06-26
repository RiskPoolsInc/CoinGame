namespace CF.WebApi.Models; 

public class TaskEntityExternal {
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid WalletId { get; set; }
}