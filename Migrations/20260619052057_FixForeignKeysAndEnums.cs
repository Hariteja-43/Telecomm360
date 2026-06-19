using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Telecomm360.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeysAndEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orders_SubscriberID",
                table: "Orders",
                column: "SubscriberID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Subscribers_SubscriberID",
                table: "Orders",
                column: "SubscriberID",
                principalTable: "Subscribers",
                principalColumn: "SubscriberId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Subscribers_SubscriberID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SubscriberID",
                table: "Orders");
        }
    }
}
