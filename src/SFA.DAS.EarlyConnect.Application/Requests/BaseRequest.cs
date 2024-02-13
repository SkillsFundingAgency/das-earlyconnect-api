using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.EarlyConnect.Application.Requests
{
    public abstract class BaseRequest
    {

    }

    public enum StatusUpdate
    {
        CommunicationSent,
        ReplyAwaiting,
        ReplyReceived,
        ActivelyWorking,
        HelpNoLongerRequired,
        OfferMade,
        ContactLost
    }
}
