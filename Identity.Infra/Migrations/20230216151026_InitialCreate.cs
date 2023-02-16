using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fm_User",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usr_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usr_UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usr_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usr_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usr_Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fm_User", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fm_User");
        }
    }
}
