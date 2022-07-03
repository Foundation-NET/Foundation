namespace Foundation
{
    public interface IAuditRecord
    {
        string GetOriginal();
        string GetNew();
        string GetField();
    }
}