using Microsoft.EntityFrameworkCore.Migrations;

namespace FileDelivery.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Upload_Entry_EntryID",
                table: "Upload");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Upload",
                table: "Upload");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entry",
                table: "Entry");

            migrationBuilder.RenameTable(
                name: "Upload",
                newName: "Uploads");

            migrationBuilder.RenameTable(
                name: "Entry",
                newName: "Entries");

            migrationBuilder.RenameIndex(
                name: "IX_Upload_EntryID",
                table: "Uploads",
                newName: "IX_Uploads_EntryID");

            migrationBuilder.RenameIndex(
                name: "IX_Entry_EmailAddress_TransactionID",
                table: "Entries",
                newName: "IX_Entries_EmailAddress_TransactionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Uploads",
                table: "Uploads",
                column: "UploadID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entries",
                table: "Entries",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Uploads_Entries_EntryID",
                table: "Uploads",
                column: "EntryID",
                principalTable: "Entries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uploads_Entries_EntryID",
                table: "Uploads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Uploads",
                table: "Uploads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entries",
                table: "Entries");

            migrationBuilder.RenameTable(
                name: "Uploads",
                newName: "Upload");

            migrationBuilder.RenameTable(
                name: "Entries",
                newName: "Entry");

            migrationBuilder.RenameIndex(
                name: "IX_Uploads_EntryID",
                table: "Upload",
                newName: "IX_Upload_EntryID");

            migrationBuilder.RenameIndex(
                name: "IX_Entries_EmailAddress_TransactionID",
                table: "Entry",
                newName: "IX_Entry_EmailAddress_TransactionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Upload",
                table: "Upload",
                column: "UploadID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entry",
                table: "Entry",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Upload_Entry_EntryID",
                table: "Upload",
                column: "EntryID",
                principalTable: "Entry",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
