using MediatR;
using SFA.DAS.EarlyConnect.Application.Models;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetLEPSUserByLepsId
{
    public class GetLEPSUserByLepsIdQueryHandler : IRequestHandler<GetLEPSUserByLepsIdQuery, GetLEPSUsersByLepsIdResult>
    {
        private readonly ILEPSUserRepository _lepsUserRepository;

        public GetLEPSUserByLepsIdQueryHandler(ILEPSUserRepository lepsDataRepository)
        {
            _lepsUserRepository = lepsDataRepository;
        }

        public async Task<GetLEPSUsersByLepsIdResult> Handle(GetLEPSUserByLepsIdQuery request, CancellationToken cancellationToken)
        {
            var lepsUsers = await _lepsUserRepository.GetLepsUsersByLepsIdAsync(request.LepsId);

            return new GetLEPSUsersByLepsIdResult
            {
                LEPSUsers = MapToLEPSUsersDto(lepsUsers)
            };
        }

        private ICollection<LEPSUserDto> MapToLEPSUsersDto(ICollection<LEPSUser> lepsUsers)
        {
            var usersDto = new List<LEPSUserDto>();

            foreach (LEPSUser user in lepsUsers) 
            {
                var userDto = new LEPSUserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DateAdded = user.DateAdded
                };

                usersDto.Add(userDto);
            }

            return usersDto;
        }
    }
}
