using Foundation.Csv.Shared;

namespace Foundation.Csv.Writer
{
    public interface ICsvWriter
    {
        void SetHeader(string header);
        void SetOutputFile(string path);
        Row NewRow();
        void CommitRow(Row r);
        void CommitFile();
    }
}