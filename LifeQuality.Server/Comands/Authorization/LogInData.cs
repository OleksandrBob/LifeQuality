using CSharpFunctionalExtensions;
using LifeQuality.Core.Dto;
using LifeQuality.Core.Services.Interfaces;
using MediatR;

namespace LifeQuality.Server.Comands.Authorization;

public class LogInData : IRequest<Result<LoginResultDto, string>>
{
    public string Login { get; set; }

    public string Password { get; set; }

    public class Handler : IRequestHandler<LogInData, Result<LoginResultDto, string>>
    {
        private readonly IAuthorizationService _authorizationService;

        public Handler(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public async Task<Result<LoginResultDto, string>> Handle(LogInData request,
            CancellationToken cancellationToken)
        {
            var result = await _authorizationService.LogIn(request.Login, request.Password);

            if (result == null)
            {
                return Result.Failure<LoginResultDto, string>("User not found");
            }

            return result;
        }
    }
}


