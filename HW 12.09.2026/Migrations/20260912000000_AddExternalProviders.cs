using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HW_12._09._2026.Migrations;

public partial class AddExternalProviders : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Providers",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Providers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "UserProviders",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                user_id = table.Column<int>(type: "int", nullable: false),
                provider_id = table.Column<int>(type: "int", nullable: false),
                number_provider = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserProviders", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserProviders_Providers_provider_id",
                    column: x => x.provider_id,
                    principalTable: "Providers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_UserProviders_Users_user_id",
                    column: x => x.user_id,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Providers_Name",
            table: "Providers",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_UserProviders_provider_id_number_provider",
            table: "UserProviders",
            columns: new[] { "provider_id", "number_provider" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_UserProviders_user_id_provider_id",
            table: "UserProviders",
            columns: new[] { "user_id", "provider_id" },
            unique: true);

        migrationBuilder.InsertData(
            table: "Providers",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "google" },
                { 2, "fb" },
                { 3, "apple" }
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "UserProviders");
        migrationBuilder.DropTable(name: "Providers");
    }
}
