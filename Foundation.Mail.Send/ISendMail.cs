namespace Foundation.Mail.Send
{
    public interface ISendMail
    {
        void SetSmtpServer(string server);
        void Send(string content, string to, string? cc = null, string? bcc = null);
    }
}