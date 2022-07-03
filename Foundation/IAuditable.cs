using System.Collections.Generic;

namespace Foundation
{
    public interface IAuditable
    {
        List<IAuditRecord> GetAuditRecords();
    }
}