﻿using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface ISubjectPreferenceDataRepository
    {
        Task UpdateLepsDateSent(IList<int> ids);
    }
}