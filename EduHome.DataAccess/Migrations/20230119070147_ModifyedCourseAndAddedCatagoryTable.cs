using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHome.DataAccess.Migrations
{
    public partial class ModifyedCourseAndAddedCatagoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatagoryId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Catagories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catagories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CatagoryId",
                table: "Courses",
                column: "CatagoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Catagories_CatagoryId",
                table: "Courses",
                column: "CatagoryId",
                principalTable: "Catagories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Catagories_CatagoryId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "Catagories");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CatagoryId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CatagoryId",
                table: "Courses");
        }
    }
}
