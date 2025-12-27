using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeToSurvive.Migrations
{
    /// <inheritdoc />
    public partial class InitGameDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Outfit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    IsOwned = table.Column<bool>(type: "bit", nullable: false),
                    StyleBonus = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outfit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Money = table.Column<int>(type: "int", nullable: false),
                    Energy = table.Column<int>(type: "int", nullable: false),
                    CodingSkill = table.Column<int>(type: "int", nullable: false),
                    Reputation = table.Column<int>(type: "int", nullable: false),
                    CurrentDay = table.Column<int>(type: "int", nullable: false),
                    CreditScore = table.Column<int>(type: "int", nullable: false),
                    ActiveLoan = table.Column<int>(type: "int", nullable: false),
                    StyleLevel = table.Column<int>(type: "int", nullable: false),
                    JobLevel = table.Column<int>(type: "int", nullable: false),
                    TotalWorkDays = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    CurrentOutfitPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Style = table.Column<int>(type: "int", nullable: false),
                    CurrentOutfitId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Outfit_CurrentOutfitId",
                        column: x => x.CurrentOutfitId,
                        principalTable: "Outfit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OwnedOutfits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    OutfitName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StyleBonus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnedOutfits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnedOutfits_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Outfit_PlayerId",
                table: "Outfit",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnedOutfits_PlayerId",
                table: "OwnedOutfits",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CurrentOutfitId",
                table: "Players",
                column: "CurrentOutfitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Outfit_Players_PlayerId",
                table: "Outfit",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Outfit_Players_PlayerId",
                table: "Outfit");

            migrationBuilder.DropTable(
                name: "OwnedOutfits");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Outfit");
        }
    }
}
