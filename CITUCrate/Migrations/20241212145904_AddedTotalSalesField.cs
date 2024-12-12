using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CITUCrate.Migrations
{
    /// <inheritdoc />
    public partial class AddedTotalSalesField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalSales",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalSales",
                table: "Products");
        }
    }
}
