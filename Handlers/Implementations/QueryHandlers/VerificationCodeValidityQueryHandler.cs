using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Entities.UserVerificationDomain;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.QueryHandlers
{
    public class VerificationCodeValidityQueryHandler : AbstractQueryHandler<VerificationCodeValidityQuery>
    {
        private readonly IUserVerificationReadRepository _userVerificationReadRepository;

        public VerificationCodeValidityQueryHandler(
            ILogger<VerificationCodeValidityQueryHandler> logger, 
            IUserVerificationReadRepository userVerificationReadRepository)
            : base(logger)
        {
            _userVerificationReadRepository = userVerificationReadRepository;
        }

        public override async Task<QueryResponse> HandleAsync(VerificationCodeValidityQuery query)
        {
            var verification = await _userVerificationReadRepository.GetByVerificationCodeAsync(query.VerificationCode);
            var isValid = verification != null && verification.IsStillValid();

            return new QueryResponse { QueryData = new { IsVerificationCodeValid = isValid } };
        }
    }
}
