using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanBee.Universityportal.backend.Start.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseUsers",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserRole = table.Column<int>(type: "int", nullable: false),
                    UniversityEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMarkedAsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseUsers", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationRequests",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelDataJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorUserRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionStatus = table.Column<int>(type: "int", nullable: false),
                    ActionComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMarkedAsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationRequests", x => x.ItemId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseUsers_Email",
                table: "BaseUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BaseUsers_MobilePhone",
                table: "BaseUsers",
                column: "MobilePhone",
                unique: true,
                filter: "[MobilePhone] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BaseUsers_RegistrationId",
                table: "BaseUsers",
                column: "RegistrationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseUsers");

            migrationBuilder.DropTable(
                name: "RegistrationRequests");
        }
    }
}
