using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests
{
    public class DeliveryUpdateRequest
    {
        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Invalid Source")]
        public string Source { get; set; }
        public List<int> Ids { get; set; }
    }
}
