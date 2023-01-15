using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Handlers.Responses;
using PlanBee.University_portal.backend.Services;

namespace PlanBee.University_portal.backend.Handlers.Implementations.QueryHandlers;

public class GetAuthTokenQueryHandler
    : AbstractQueryHandler<GetAuthTokenQuery>
{
    private readonly IJwtAuthenticationService _jwtManagerRepository;

    public GetAuthTokenQueryHandler(
        ILogger<GetAuthTokenQueryHandler> logger,
        IJwtAuthenticationService jwtManagerRepository) : base(logger)
    {
        _jwtManagerRepository = jwtManagerRepository;
    }

    public override async Task<QueryResponse> HandleAsync(GetAuthTokenQuery query)
    {
        var token = await _jwtManagerRepository.Authenticate(
            query.RegistrationId,
            query.Password,
            query.Email);

        return new QueryResponse
        {
            QueryData = new { Token = token }
        };
    }
}