using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drones.Migrations
{
    public partial class Fixentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Drones_DroneId",
                table: "Medications");

            migrationBuilder.AlterColumn<int>(
                name: "DroneId",
                table: "Medications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Drones_DroneId",
                table: "Medications",
                column: "DroneId",
                principalTable: "Drones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Drones_DroneId",
                table: "Medications");

            migrationBuilder.AlterColumn<int>(
                name: "DroneId",
                table: "Medications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Drones_DroneId",
                table: "Medications",
                column: "DroneId",
                principalTable: "Drones",
                principalColumn: "Id");
        }
    }
}
