using System.Runtime.Serialization;

namespace PlanBee.University_portal.backend.Domain.Enums.Business
{
    public enum DesignationType
    {
        [EnumMember(Value = "academic")] Academic,
        [EnumMember(Value = "nonacademic")] NonAcademic
    }
}
