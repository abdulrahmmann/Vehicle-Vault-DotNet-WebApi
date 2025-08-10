using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleVault.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColorDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Colors_ColorCode",
                table: "Colors");

            migrationBuilder.AlterColumn<string>(
                name: "ColorCode",
                table: "Colors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "ColorName",
                table: "Colors",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FinishType",
                table: "Colors",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Colors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Colors_ColorName",
                table: "Colors",
                column: "ColorName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Colors_ColorName",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "ColorName",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "FinishType",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Colors");

            migrationBuilder.AlterColumn<string>(
                name: "ColorCode",
                table: "Colors",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_Colors_ColorCode",
                table: "Colors",
                column: "ColorCode");
        }
    }
}
