using System.Runtime.Serialization;

namespace PlanBee.University_portal.backend.Domain.Enums.Business
{
    public enum WorkplaceType
    {
        [EnumMember(Value = "academic")] Academic,
        [EnumMember(Value = "nonacademic")] NonAcademic
    }
}
