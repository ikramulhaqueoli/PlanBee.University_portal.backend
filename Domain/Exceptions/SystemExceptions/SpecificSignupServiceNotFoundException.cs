using PlanBee.University_portal.backend.Domain.Enums.Business;

namespace PlanBee.University_portal.backend.Domain.Exceptions.SystemExceptions;

public class SpecificSignupServiceNotFoundException : AbstractSystemException
{
    public SpecificSignupServiceNotFoundException(UserType userType)
        : base($"No suitable Signup Service implementation found for UserType: {userType}") { }
}