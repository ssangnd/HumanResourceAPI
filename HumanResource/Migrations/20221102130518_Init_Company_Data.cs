using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResource.Migrations
{
    public partial class Init_Company_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "Country", "Name" },
                values: new object[] { new Guid("79e6e443-850f-433c-bb54-9975354a20d1"), "Thu Duc, HCM, VN", "Viet Nam", "FPT Software" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "Country", "Name" },
                values: new object[] { new Guid("f111ee9c-b0dc-4c73-8c0c-d8b78c6b52f1"), "Ha Noi, VN", "Viet Nam", "VinGroup" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("79e6e443-850f-433c-bb54-9975354a20d1"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("f111ee9c-b0dc-4c73-8c0c-d8b78c6b52f1"));
        }
    }
}
