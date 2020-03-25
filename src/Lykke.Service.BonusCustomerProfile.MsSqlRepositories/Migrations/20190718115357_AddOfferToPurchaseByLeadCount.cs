using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.BonusCustomerProfile.MsSqlRepositories.Migrations
{
    public partial class AddOfferToPurchaseByLeadCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "total_offer_to_purchases_by_lead_count",
                schema: "bonus_customer_profile",
                table: "customer_profile",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total_offer_to_purchases_by_lead_count",
                schema: "bonus_customer_profile",
                table: "customer_profile");
        }
    }
}
