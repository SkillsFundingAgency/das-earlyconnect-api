using Microsoft.EntityFrameworkCore;
using SFA.DAS.EarlyConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.EarlyConnect.Data
{
    public interface IEarlyConnectDataContext
    {
        DbSet<StudentData> StudentData { get; set; }
    }
}
