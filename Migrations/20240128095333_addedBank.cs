using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoaEasyPay.Migrations
{
    /// <inheritdoc />
    public partial class addedBank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bank",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bank",
                table: "Accounts");
        }
    }
}
