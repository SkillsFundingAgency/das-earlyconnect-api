using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests
{
    public class DeliveryUpdateRequest
    {
        public int LogId { get; set; }
        public string Source { get; set; }
        public List<int> Ids { get; set; }
    }
}
