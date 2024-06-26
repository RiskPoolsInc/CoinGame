namespace App.Core.Commands.Telegram;

public class SendTgmTaskPublishedCommand : IRequest<string>
{
    public string Index { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }
    public DateTime TaskDate { get; set; }
    public decimal Reward { get; set; }
    public Guid TaskId { get; set; }
    public string ExternalLink { get; set; }
    public string? Location { get; set; }
    
    public long ReceiverId { get; set; }
}