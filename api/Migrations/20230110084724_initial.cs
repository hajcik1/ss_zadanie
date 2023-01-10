using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MojeFilmyAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filmy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Director = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Rate = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Filmy__3214EC07E7DDAE8A", x => x.Id);
                });
            migrationBuilder.Sql("SET IDENTITY_INSERT dbo.Filmy ON");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Filmy");
        }
    }
}
