using AutoMapper;
using MediatR;
using UserManagement.Application.Contracts.Repositories;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.CommandHandlers.User
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool MfaEnabled { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        // TODO:
        // Map Roles and other information of user
        // Map all other user profiles
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<CreateUserCommand, ApplicationUser>(request);
            
            // TODO: authorization, validation and other stuffs

            (bool success, string userId) = await _userRepository.CreateUserAsync(user);

            // query to db to make sure if user 
            var applicationUsher = await _userRepository.GetUserAsync(userId);

            return _mapper.Map<ApplicationUser, UserDto>(applicationUsher);
        }
    }
}
