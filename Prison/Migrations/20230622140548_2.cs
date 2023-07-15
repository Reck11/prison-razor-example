using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prison.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecurityLevel",
                table: "Prisoner",
                newName: "CellId");

            migrationBuilder.CreateTable(
                name: "CellBlock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CellBlock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobType = table.Column<int>(type: "int", nullable: false),
                    DailyWage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Visitor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cell",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaximumCapacity = table.Column<int>(type: "int", nullable: false),
                    CellBlockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cell", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cell_CellBlock_CellBlockId",
                        column: x => x.CellBlockId,
                        principalTable: "CellBlock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Guard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weapon = table.Column<int>(type: "int", nullable: false),
                    CellBlockId = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guard_CellBlock_CellBlockId",
                        column: x => x.CellBlockId,
                        principalTable: "CellBlock",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobPrisoner",
                columns: table => new
                {
                    JobsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrisonersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPrisoner", x => new { x.JobsId, x.PrisonersId });
                    table.ForeignKey(
                        name: "FK_JobPrisoner_Job_JobsId",
                        column: x => x.JobsId,
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobPrisoner_Prisoner_PrisonersId",
                        column: x => x.PrisonersId,
                        principalTable: "Prisoner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrisonerVisitorRelation",
                columns: table => new
                {
                    PrisonerId = table.Column<int>(type: "int", nullable: false),
                    VisitorId = table.Column<int>(type: "int", nullable: false),
                    Relationship = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrisonerVisitorRelation", x => new { x.PrisonerId, x.VisitorId });
                    table.ForeignKey(
                        name: "FK_PrisonerVisitorRelation_Prisoner_PrisonerId",
                        column: x => x.PrisonerId,
                        principalTable: "Prisoner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrisonerVisitorRelation_Visitor_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prisoner_CellId",
                table: "Prisoner",
                column: "CellId");

            migrationBuilder.CreateIndex(
                name: "IX_Cell_CellBlockId",
                table: "Cell",
                column: "CellBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Guard_CellBlockId",
                table: "Guard",
                column: "CellBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPrisoner_PrisonersId",
                table: "JobPrisoner",
                column: "PrisonersId");

            migrationBuilder.CreateIndex(
                name: "IX_PrisonerVisitorRelation_VisitorId",
                table: "PrisonerVisitorRelation",
                column: "VisitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prisoner_Cell_CellId",
                table: "Prisoner",
                column: "CellId",
                principalTable: "Cell",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prisoner_Cell_CellId",
                table: "Prisoner");

            migrationBuilder.DropTable(
                name: "Cell");

            migrationBuilder.DropTable(
                name: "Guard");

            migrationBuilder.DropTable(
                name: "JobPrisoner");

            migrationBuilder.DropTable(
                name: "PrisonerVisitorRelation");

            migrationBuilder.DropTable(
                name: "CellBlock");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Prisoner_CellId",
                table: "Prisoner");

            migrationBuilder.RenameColumn(
                name: "CellId",
                table: "Prisoner",
                newName: "SecurityLevel");
        }
    }
}
