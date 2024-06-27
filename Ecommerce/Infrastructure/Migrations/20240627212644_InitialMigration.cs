using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sneakers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Brand = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sneakers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    State = table.Column<string>(type: "TEXT", nullable: false),
                    IdUser = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationSneakers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReservationId = table.Column<int>(type: "INTEGER", nullable: false),
                    SneakerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationSneakers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationSneakers_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationSneakers_sneakers_SneakerId",
                        column: x => x.SneakerId,
                        principalTable: "sneakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "sneakers",
                columns: new[] { "Id", "Brand", "Category", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "Nike", "Casual", "Air Max", 120, 50 },
                    { 2, "Adidas", "Casual", "Classic", 100, 30 },
                    { 3, "Nike", "Running", "ZoomX", 150, 20 },
                    { 4, "Adidas", "Running", "Superstar", 80, 40 },
                    { 5, "Adidas", "Sports", "Gel-Kayano", 140, 25 },
                    { 6, "Converse", "Casual", "Chuck Taylor", 60, 35 },
                    { 7, "Adidas", "Sports", "Ultraboost", 180, 15 },
                    { 8, "Nike", "Running", "Pegasus", 110, 45 },
                    { 9, "Adidas", "Running", "Pegaboot", 110, 55 }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "EmailAddress", "Name", "Password", "Type" },
                values: new object[,]
                {
                    { 1, "Ana@example.com", "Ana", "Pass1", "Admin" },
                    { 2, "delfina@example.com", "Delfina", "Pass2", "Admin" },
                    { 3, "juan.doe@example.com", "Juan", "Pass3", "Client" },
                    { 4, "vicky.sosa@example.com", "Victoria", "Pass4", "Client" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_IdUser",
                table: "Reservations",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationSneakers_ReservationId",
                table: "ReservationSneakers",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationSneakers_SneakerId",
                table: "ReservationSneakers",
                column: "SneakerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationSneakers");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "sneakers");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
