using AutoMapper;
using IdentitySecurityTokenService.Application.Common.Exceptions;
using MediatR;
using UserManagement.Application.Contracts.Repositories;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.QueryHandlers.User
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public string UserId { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IMapper mapper, IUserRepository identityRepository)
        {
            _mapper = mapper;
            _userRepository = identityRepository;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var userInfo = await _userRepository.GetUserAsync(request.UserId);
            if (userInfo is null)
            {
                throw new NotFoundException("Invalid User Information");
            }
            return _mapper.Map<ApplicationUser, UserDto>(userInfo);
        }
    }
}
