using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetLEPSData
{
    public class GetLEPSDataByRegionQuery : IRequest<int>
    {
        public string Region { get; set; }
    }
}
