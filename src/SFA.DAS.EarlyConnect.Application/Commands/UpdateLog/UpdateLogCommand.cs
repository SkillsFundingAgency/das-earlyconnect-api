namespace SFA.DAS.EarlyConnect.Application.Commands.UpdateLog
{
    public class UpdateLogCommand
    {
        public int LogId { get; set; }
        public string Status { get; set; }
        public string Error { get; set; }
    }
}
