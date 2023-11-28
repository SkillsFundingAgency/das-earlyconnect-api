using MediatR;
using SFA.DAS.EarlyConnect.Application.Models;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSUserByLepsId;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataWithUsers
{
    public class GetLEPSDataWithUsersQueryHandler : IRequestHandler<GetLEPSDataWithUsersQuery, GetLEPDataWithUsersResult>
    {
        private readonly ILEPSDataRepository _lepsDataRepository;
        private readonly IMediator _mediator;

        public GetLEPSDataWithUsersQueryHandler(ILEPSDataRepository lepsDataRepository, IMediator mediator)
        {
            _lepsDataRepository = lepsDataRepository;
            _mediator = mediator;
        }

        public async Task<GetLEPDataWithUsersResult> Handle(GetLEPSDataWithUsersQuery request, CancellationToken cancellationToken)
        {
            var lepsDataDtos = new List<LEPSDataDto>();

            var lepsData = await _lepsDataRepository.GetAllLepsDataAsync();

            foreach (var leps in lepsData) 
            {
                var lepsUsersResult = await _mediator.Send(new GetLEPSUserByLepsIdQuery
                {
                    LepsId = leps.Id
                });

                var lepsDto = new LEPSDataDto
                {
                    Id = leps.Id,
                    LepCode = leps.LepCode,
                    Region = leps.Region,
                    LepName = leps.LepName,
                    EntityEmail = leps.EntityEmail,
                    Postcode = leps.Postcode,
                    DateAdded = leps.DateAdded,
                    LEPSUsers = lepsUsersResult.LEPSUsers,
                };

                lepsDataDtos.Add(lepsDto);
            }

            return new GetLEPDataWithUsersResult
            {
                LEPSData = lepsDataDtos
            };
        }
    }
}
