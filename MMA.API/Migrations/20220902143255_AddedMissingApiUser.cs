using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMA.API.Migrations
{
    public partial class AddedMissingApiUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8343074e-8623-4e1a-b0c1-84fb8678c8f3",
                column: "ConcurrencyStamp",
                value: "48cf01a6-c5a4-47cd-b92e-7a14f39ca41c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7ac6cfe-1f10-4baf-b604-cde350db9554",
                column: "ConcurrencyStamp",
                value: "6f75055d-5543-4823-8011-e7f9cb890ae0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30a24107-d279-4e37-96fd-01af5b38cb27",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5aa92697-6a8e-422b-9508-77d880c07e22", "AQAAAAEAACcQAAAAEKLRc0vYyhcRac6JbqgS38PJ4lM5czKqHdrnjSm17a2YUEM/FojKrIakZubVjV2NhA==", "c3338dc7-db63-4632-89a3-8fe86662f586" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e448afa-f008-446e-a52f-13c449803c2e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2821a028-3ad2-4792-a391-2f75f70f2b73", "AQAAAAEAACcQAAAAEAZQlgxhiV6P+0HwMPV4zMQ5CFe9R4KJ4HROIs4r0xcRAsRLSiTHRaJ2qoe8i2YyKA==", "6ba06b4c-b4f8-476b-bbd4-857c88d95bb1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30a24107-d279-4e37-96fd-01af5b38cb27");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e448afa-f008-446e-a52f-13c449803c2e");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8343074e-8623-4e1a-b0c1-84fb8678c8f3",
                column: "ConcurrencyStamp",
                value: "97cc4555-b1e9-4ea0-8ae8-6634f503a91d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7ac6cfe-1f10-4baf-b604-cde350db9554",
                column: "ConcurrencyStamp",
                value: "76b1881d-9107-4ba7-bb36-1744e415ed3f");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "30a24107-d279-4e37-96fd-01af5b38cb27", 0, "78c6e26a-5330-4480-aad9-46d6a13db487", "ApiUser", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAEAACcQAAAAELh+4cmCio1NS6MfQ0KuANPOlZrLBaDvKr7MENz8V1zburljo/+zsuJNChI1JLS5qw==", null, false, "2aa447a1-a0a9-4134-8da9-24a476766fa8", false, "user@bookstore.com" },
                    { "8e448afa-f008-446e-a52f-13c449803c2e", 0, "78da4472-0905-453d-8d05-c82583422520", "ApiUser", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEF9/lL9/1Je4OtClq9rf5FEmCTl627Zj6Yb2jghMOn07i2qrkbjexo90uEuY/fpO8g==", null, false, "208d568b-a7d4-42fe-93f3-f17bdacf73b8", false, "admin@bookstore.com" }
                });
        }
    }
}
