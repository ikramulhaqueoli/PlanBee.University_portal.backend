using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Responses;
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
        var token = await _jwtManagerRepository.GetAuthTokenAsync(
            query.EmailOrUniversityId,
            query.Password);

        var response = new QueryResponse
        {
            QueryData = token switch
            {
                null => throw new GeneralBusinessException("Invalid University ID or Password"),
                _ => new { Token = token },
            }
        };
        return response;
    }
}