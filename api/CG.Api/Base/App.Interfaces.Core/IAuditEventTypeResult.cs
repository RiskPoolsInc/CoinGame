namespace App.Interfaces.Core;

public interface IAuditEventTypeResult {
    public int AuditEventTypeSuccessfulId { get; }
    public int AuditEventTypeErrorId { get; }
}