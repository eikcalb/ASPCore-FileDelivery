using Microsoft.EntityFrameworkCore.Migrations;

namespace FileDelivery.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entry",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    TransactionID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entry", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Upload",
                columns: table => new
                {
                    UploadID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntryID = table.Column<int>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Upload", x => x.UploadID);
                    table.ForeignKey(
                        name: "FK_Upload_Entry_EntryID",
                        column: x => x.EntryID,
                        principalTable: "Entry",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entry_EmailAddress_TransactionID",
                table: "Entry",
                columns: new[] { "EmailAddress", "TransactionID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Upload_EntryID",
                table: "Upload",
                column: "EntryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Upload");

            migrationBuilder.DropTable(
                name: "Entry");
        }
    }
}
