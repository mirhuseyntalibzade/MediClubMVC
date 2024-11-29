using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediPlusMVC.Migrations
{
    /// <inheritdoc />
    public partial class isActivecolumnadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "SliderItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "HospitalFacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Doctors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Contacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "SliderItems");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "HospitalFacts");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Blogs");
        }
    }
}
