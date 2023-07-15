using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prison.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Crime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinimalPunishment = table.Column<int>(type: "int", nullable: false),
                    SecurityLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crime", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReeducationProgram",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReeducationProgram", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReeducationStaff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QualificationsType = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReeducationStaff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warden",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmploymentStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warden", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prisoner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationNumber = table.Column<int>(type: "int", nullable: false),
                    SecurityLevel = table.Column<int>(type: "int", nullable: false),
                    ImprisonmentStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImprisonmentEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CrimeId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prisoner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prisoner_Crime_CrimeId",
                        column: x => x.CrimeId,
                        principalTable: "Crime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReeducationMeeting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaximumNumberOfPrisoners = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReeducationMeeting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReeducationMeeting_ReeducationProgram_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "ReeducationProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReeducationProgramReeducationStaff",
                columns: table => new
                {
                    ProgramsId = table.Column<int>(type: "int", nullable: false),
                    ReeducationStaffId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReeducationProgramReeducationStaff", x => new { x.ProgramsId, x.ReeducationStaffId });
                    table.ForeignKey(
                        name: "FK_ReeducationProgramReeducationStaff_ReeducationProgram_ProgramsId",
                        column: x => x.ProgramsId,
                        principalTable: "ReeducationProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReeducationProgramReeducationStaff_ReeducationStaff_ReeducationStaffId",
                        column: x => x.ReeducationStaffId,
                        principalTable: "ReeducationStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrisonerReeducationMeeting",
                columns: table => new
                {
                    MeetingsId = table.Column<int>(type: "int", nullable: false),
                    PrisonersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrisonerReeducationMeeting", x => new { x.MeetingsId, x.PrisonersId });
                    table.ForeignKey(
                        name: "FK_PrisonerReeducationMeeting_Prisoner_PrisonersId",
                        column: x => x.PrisonersId,
                        principalTable: "Prisoner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrisonerReeducationMeeting_ReeducationMeeting_MeetingsId",
                        column: x => x.MeetingsId,
                        principalTable: "ReeducationMeeting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prisoner_CrimeId",
                table: "Prisoner",
                column: "CrimeId");

            migrationBuilder.CreateIndex(
                name: "IX_PrisonerReeducationMeeting_PrisonersId",
                table: "PrisonerReeducationMeeting",
                column: "PrisonersId");

            migrationBuilder.CreateIndex(
                name: "IX_ReeducationMeeting_ProgramId",
                table: "ReeducationMeeting",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ReeducationProgramReeducationStaff_ReeducationStaffId",
                table: "ReeducationProgramReeducationStaff",
                column: "ReeducationStaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrisonerReeducationMeeting");

            migrationBuilder.DropTable(
                name: "ReeducationProgramReeducationStaff");

            migrationBuilder.DropTable(
                name: "Warden");

            migrationBuilder.DropTable(
                name: "Prisoner");

            migrationBuilder.DropTable(
                name: "ReeducationMeeting");

            migrationBuilder.DropTable(
                name: "ReeducationStaff");

            migrationBuilder.DropTable(
                name: "Crime");

            migrationBuilder.DropTable(
                name: "ReeducationProgram");
        }
    }
}
