﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using System.Text.Json.Serialization;

namespace PlanBee.University_portal.backend.Domain.Entities.EmployeeDesignationDomain
{
    public class EmployeeDesignation : EntityBase
    {
        public string Title { get; set; } = null!;

        [BsonRepresentation(BsonType.String)]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DesignationType DesignationType { get; set; }
    }
}
