using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HW_09._09._2026.Migrations;

public partial class AddUserAddresses : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "UserAddresses",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                Label = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                RecipientName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                HouseNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                Apartment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserAddresses", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserAddresses_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_UserAddresses_UserId",
            table: "UserAddresses",
            column: "UserId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "UserAddresses");
    }
}
