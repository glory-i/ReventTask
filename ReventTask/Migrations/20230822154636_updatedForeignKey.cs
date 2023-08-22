using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReventTask.Migrations
{
    public partial class updatedForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classrooms_ClassromId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Teachers_TeacherId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Students",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ClassromId",
                table: "Students",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classrooms_ClassromId",
                table: "Students",
                column: "ClassromId",
                principalTable: "Classrooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Teachers_TeacherId",
                table: "Students",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classrooms_ClassromId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Teachers_TeacherId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClassromId",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classrooms_ClassromId",
                table: "Students",
                column: "ClassromId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Teachers_TeacherId",
                table: "Students",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
