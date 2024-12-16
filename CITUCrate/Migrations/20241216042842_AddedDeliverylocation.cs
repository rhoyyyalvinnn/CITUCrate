using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CITUCrate.Migrations
{
    /// <inheritdoc />
    public partial class AddedDeliverylocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "deliveryLocation",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deliveryLocation",
                table: "Orders");
        }
    }
}
