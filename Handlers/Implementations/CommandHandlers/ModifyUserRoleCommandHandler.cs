using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers
{
    public class ModifyUserRoleCommandHandler
        : AbstractCommandHandler<ModifyUserRoleCommand>
    {
        private readonly IBaseUserWriteRepository _baseUserWriteRepository;
        private readonly IBaseUserReadRepository _baseUserReadRepository;

        public ModifyUserRoleCommandHandler(
            ILogger<ModifyUserRoleCommandHandler> logger,
            IBaseUserWriteRepository baseUserWriteRepository,
            IBaseUserReadRepository baseUserReadRepository)
            : base(logger)
        {
            _baseUserWriteRepository = baseUserWriteRepository;
            _baseUserReadRepository = baseUserReadRepository;
        }

        public override async Task<CommandResponse> HandleAsync(ModifyUserRoleCommand command)
        {
            var baseUser = await _baseUserReadRepository.GetAsync(command.TargetBaseUserId)
                ?? throw new ItemNotFoundException($"BaseUser for with Id: {command.TargetBaseUserId} not found in the database.");

            baseUser.AddRole(command.RolesToBeAdded);
            baseUser.RemoveRole(command.RolesToBeRemoved);

            await _baseUserWriteRepository.UpdateAsync(baseUser);

            return new CommandResponse();
        }
    }
}
