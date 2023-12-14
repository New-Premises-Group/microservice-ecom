using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IW.Migrations
{
    /// <inheritdoc />
    public partial class AddPointDeductionAmountToOrderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "PointDeductionAmount",
                table: "Orders",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PointDeductionAmount",
                table: "Orders");
        }
    }
}
