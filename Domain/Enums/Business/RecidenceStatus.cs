using System.Runtime.Serialization;

namespace PlanBee.University_portal.backend.Domain.Enums.Business
{
    public enum RecidenceStatus
    {
        [EnumMember(Value = "recident")] Recident,
        [EnumMember(Value = "nonrecident")] NonRecident
    }
}
