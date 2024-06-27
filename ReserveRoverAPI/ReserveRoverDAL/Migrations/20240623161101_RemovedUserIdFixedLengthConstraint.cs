using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReserveRoverDAL.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUserIdFixedLengthConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "mark",
                table: "reviews",
                type: "numeric(1)",
                precision: 1,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(1,0)",
                oldPrecision: 1);

            migrationBuilder.AlterColumn<string>(
                name: "author_id",
                table: "reviews",
                type: "character varying(28)",
                maxLength: 28,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(28)",
                oldFixedLength: true,
                oldMaxLength: 28);

            migrationBuilder.AlterColumn<string>(
                name: "manager_id",
                table: "places",
                type: "character varying(28)",
                maxLength: 28,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(28)",
                oldFixedLength: true,
                oldMaxLength: 28);

            migrationBuilder.AlterColumn<string>(
                name: "moderator_id",
                table: "moderation",
                type: "character varying(28)",
                maxLength: 28,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(28)",
                oldFixedLength: true,
                oldMaxLength: 28);

            migrationBuilder.AlterColumn<string>(
                name: "from_user_id",
                table: "chats_messages",
                type: "character varying(28)",
                maxLength: 28,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(28)",
                oldFixedLength: true,
                oldMaxLength: 28);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "mark",
                table: "reviews",
                type: "numeric(1,0)",
                precision: 1,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(1)",
                oldPrecision: 1);

            migrationBuilder.AlterColumn<string>(
                name: "author_id",
                table: "reviews",
                type: "character(28)",
                fixedLength: true,
                maxLength: 28,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(28)",
                oldMaxLength: 28);

            migrationBuilder.AlterColumn<string>(
                name: "manager_id",
                table: "places",
                type: "character(28)",
                fixedLength: true,
                maxLength: 28,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(28)",
                oldMaxLength: 28);

            migrationBuilder.AlterColumn<string>(
                name: "moderator_id",
                table: "moderation",
                type: "character(28)",
                fixedLength: true,
                maxLength: 28,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(28)",
                oldMaxLength: 28);

            migrationBuilder.AlterColumn<string>(
                name: "from_user_id",
                table: "chats_messages",
                type: "character(28)",
                fixedLength: true,
                maxLength: 28,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(28)",
                oldMaxLength: 28);
        }
    }
}
