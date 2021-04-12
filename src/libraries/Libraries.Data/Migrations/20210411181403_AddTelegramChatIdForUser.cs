using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevQuiz.Libraries.Data.Migrations
{
    public partial class AddTelegramChatIdForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_time",
                table: "users");

            migrationBuilder.DropColumn(
                name: "created_time",
                table: "questions");

            migrationBuilder.RenameColumn(
                name: "updated_time",
                table: "users",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "updated_time",
                table: "questions",
                newName: "created_date");

            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "telegram_chat_id",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "telegram_id",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_date",
                table: "users",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "questions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_date",
                table: "questions",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_telegram_chat_id",
                table: "users",
                column: "telegram_chat_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_users_telegram_chat_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "users");

            migrationBuilder.DropColumn(
                name: "telegram_chat_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "telegram_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "updated_date",
                table: "users");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "updated_date",
                table: "questions");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "users",
                newName: "updated_time");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "questions",
                newName: "updated_time");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_time",
                table: "users",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_time",
                table: "questions",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
