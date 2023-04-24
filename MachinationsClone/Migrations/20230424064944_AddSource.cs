using Microsoft.EntityFrameworkCore.Migrations;

namespace MachinationsClone.Migrations
{
    public partial class AddSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NodeTypes",
                columns: new[] { "Name", "Description", "Exportable", "Symbol" },
                values: new object[] { "source", "A source can be thought of as a pool with an infinite amount of resources, and therefore always pushes all resources or all resources are pulled from it.", false, "source" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NodeTypes",
                keyColumn: "Name",
                keyValue: "source");
        }
    }
}
