using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMD.PrivateMessenger.DAL.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class Dependencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Rooms_RoomDTOId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Rooms_RoomDTOId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoomDTOId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Messages_RoomDTOId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "RoomDTOId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoomDTOId",
                table: "Messages");

            migrationBuilder.CreateTable(
                name: "RoomDTOUserDTO",
                columns: table => new
                {
                    RoomsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomDTOUserDTO", x => new { x.RoomsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoomDTOUserDTO_Rooms_RoomsId",
                        column: x => x.RoomsId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomDTOUserDTO_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RoomId",
                table: "Messages",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomDTOUserDTO_UsersId",
                table: "RoomDTOUserDTO",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Rooms_RoomId",
                table: "Messages",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Rooms_RoomId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "RoomDTOUserDTO");

            migrationBuilder.DropIndex(
                name: "IX_Messages_RoomId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserId",
                table: "Messages");

            migrationBuilder.AddColumn<Guid>(
                name: "RoomDTOId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoomDTOId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoomDTOId",
                table: "Users",
                column: "RoomDTOId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RoomDTOId",
                table: "Messages",
                column: "RoomDTOId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Rooms_RoomDTOId",
                table: "Messages",
                column: "RoomDTOId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Rooms_RoomDTOId",
                table: "Users",
                column: "RoomDTOId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }
    }
}
