using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReventTask.Migrations
{
    public partial class additionalProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Teachers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<double>(
                name: "Salary",
                table: "Teachers",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Teachers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Students",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Students",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Students",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Classrooms",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Classrooms",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Classrooms",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Classrooms");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Teachers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Classrooms",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
