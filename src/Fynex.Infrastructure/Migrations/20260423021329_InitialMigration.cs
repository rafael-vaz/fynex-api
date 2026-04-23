using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fynex.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserIdentifier = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<int>(type: "int", nullable: false),
                    ExpenseId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role", "UserIdentifier" },
                values: new object[,]
                {
                    { 1L, "alice@email.com", "Alice", "hashedpassword1", "administrator", new Guid("11111111-1111-1111-1111-111111111111") },
                    { 2L, "bob@email.com", "Bob", "hashedpassword2", "teamMember", new Guid("22222222-2222-2222-2222-222222222222") },
                    { 3L, "carol@email.com", "Carol", "hashedpassword3", "teamMember", new Guid("33333333-3333-3333-3333-333333333333") },
                    { 4L, "david@email.com", "David", "hashedpassword4", "teamMember", new Guid("44444444-4444-4444-4444-444444444444") },
                    { 5L, "eve@email.com", "Eve", "hashedpassword5", "teamMember", new Guid("55555555-5555-5555-5555-555555555555") }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Amount", "Date", "Description", "PaymentType", "Title", "UserId" },
                values: new object[,]
                {
                    { 1L, 150.75m, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Notebooks and pens", 1, "Office Supplies", 1L },
                    { 2L, 99.90m, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Monthly plan", 3, "Internet", 2L },
                    { 3L, 350.00m, new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Tech conference 2025", 1, "Conference Ticket", 3L },
                    { 4L, 120.00m, new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Programming books", 2, "Books", 4L },
                    { 5L, 45.50m, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Coffee for the team", 3, "Coffee", 5L }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "ExpenseId", "Value" },
                values: new object[,]
                {
                    { 1L, 1L, 1 },
                    { 2L, 1L, 3 },
                    { 3L, 2L, 1 },
                    { 4L, 2L, 3 },
                    { 5L, 3L, 8 },
                    { 6L, 3L, 6 },
                    { 7L, 4L, 8 },
                    { 8L, 4L, 4 },
                    { 9L, 5L, 1 },
                    { 10L, 5L, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ExpenseId",
                table: "Tags",
                column: "ExpenseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
