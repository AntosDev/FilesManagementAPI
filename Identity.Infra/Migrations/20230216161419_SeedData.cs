using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Infra.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "fm_User",
                columns: new[] { "id", "usr_FirstName", "usr_LastName", "usr_Password", "usr_ID", "usr_UserName" },
                values: new object[] { 1L, "Antoine", "Hachem", "10|Jf2tWxMCxFxb/UPkHnxZqA==|NsYGhfw4NYyiWLsSxrTISNAB1SYmrinumZTbaBT7wms=", "7d274d1c-1c4c-4ecf-b9aa-c205e749f74d", "AntoineH" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "fm_User",
                keyColumn: "id",
                keyValue: 1L);
        }
    }
}
