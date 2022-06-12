using Foundation.Csv.Shared;
using System.Collections.Generic;

namespace Foundation.Csv.Reader
{
    public interface ICsvReader
    {
        void SetHeader(string header);
        void SetFile(string path);
        List<Row> GetRows(int count);
        Row[] GetAllRows();
        Row GetNextRow();
        void SetUseFileColumnOrder(bool set);
        void SetFileHasHeaderRow(bool set);
    }
}