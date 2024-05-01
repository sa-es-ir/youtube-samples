using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFQueryOptimization.Migrations
{
    /// <inheritdoc />
    public partial class InitAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(System.IO.File.ReadAllText("script.sql"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
