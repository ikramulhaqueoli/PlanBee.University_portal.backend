using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Enums;
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
        var token = await _jwtManagerRepository.Authenticate(
            query.UniversityId,
            query.Password);

        var response = new QueryResponse();
        switch (token)
        {
            case null:
                response.SetQueryError(
                    ResponseErrorType.BusinessException,
                    "Invalid Registration ID or Password");
                break;
            default:
                response.QueryData = new { Token = token };
                break;
        }

        return response;
    }
}