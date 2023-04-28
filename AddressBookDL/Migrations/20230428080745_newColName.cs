using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AddressBookDL.Migrations
{
    /// <inheritdoc />
    public partial class newColName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NeigbbourhoodId",
                table: "UserAddresses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NeigbbourhoodId",
                table: "UserAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
