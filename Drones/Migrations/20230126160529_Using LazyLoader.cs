using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drones.Migrations
{
    public partial class UsingLazyLoader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Drones_DroneId",
                table: "Medications");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Drones_DroneId",
                table: "Medications",
                column: "DroneId",
                principalTable: "Drones",
                principalColumn: "Id");
        }
    }
}
