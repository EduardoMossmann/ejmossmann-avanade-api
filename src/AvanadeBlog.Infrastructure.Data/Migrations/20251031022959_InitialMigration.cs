using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvanadeBlog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommentEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    PostEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentEntity_Blog_PostEntityId",
                        column: x => x.PostEntityId,
                        principalTable: "Blog",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Title",
                table: "Blog",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentEntity_PostEntityId",
                table: "CommentEntity",
                column: "PostEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentEntity");

            migrationBuilder.DropTable(
                name: "Blog");
        }
    }
}
