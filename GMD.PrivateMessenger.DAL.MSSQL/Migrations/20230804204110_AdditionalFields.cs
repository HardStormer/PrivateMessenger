using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMD.PrivateMessenger.DAL.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomDTOUserDTO_Rooms_RoomsId",
                table: "RoomDTOUserDTO");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomDTOUserDTO_Users_UsersId",
                table: "RoomDTOUserDTO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomDTOUserDTO",
                table: "RoomDTOUserDTO");

            migrationBuilder.RenameTable(
                name: "RoomDTOUserDTO",
                newName: "RoomDtoUserDto");

            migrationBuilder.RenameIndex(
                name: "IX_RoomDTOUserDTO_UsersId",
                table: "RoomDtoUserDto",
                newName: "IX_RoomDtoUserDto_UsersId");

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomDtoUserDto",
                table: "RoomDtoUserDto",
                columns: new[] { "RoomsId", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RoomDtoUserDto_Rooms_RoomsId",
                table: "RoomDtoUserDto",
                column: "RoomsId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomDtoUserDto_Users_UsersId",
                table: "RoomDtoUserDto",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomDtoUserDto_Rooms_RoomsId",
                table: "RoomDtoUserDto");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomDtoUserDto_Users_UsersId",
                table: "RoomDtoUserDto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomDtoUserDto",
                table: "RoomDtoUserDto");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "RoomDtoUserDto",
                newName: "RoomDTOUserDTO");

            migrationBuilder.RenameIndex(
                name: "IX_RoomDtoUserDto_UsersId",
                table: "RoomDTOUserDTO",
                newName: "IX_RoomDTOUserDTO_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomDTOUserDTO",
                table: "RoomDTOUserDTO",
                columns: new[] { "RoomsId", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RoomDTOUserDTO_Rooms_RoomsId",
                table: "RoomDTOUserDTO",
                column: "RoomsId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomDTOUserDTO_Users_UsersId",
                table: "RoomDTOUserDTO",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
