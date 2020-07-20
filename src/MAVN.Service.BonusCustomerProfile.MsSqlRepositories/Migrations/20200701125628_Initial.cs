using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.BonusCustomerProfile.MsSqlRepositories.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bonus_customer_profile");

            migrationBuilder.CreateTable(
                name: "campaigns_contribution",
                schema: "bonus_customer_profile",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    customer_id = table.Column<Guid>(nullable: false),
                    campaign_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campaigns_contribution", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customer_profile",
                schema: "bonus_customer_profile",
                columns: table => new
                {
                    customer_id = table.Column<Guid>(nullable: false),
                    total_campaigns_contributed_count = table.Column<int>(nullable: false),
                    total_referred_friend_count = table.Column<int>(nullable: false),
                    total_purchased_amount = table.Column<decimal>(nullable: false),
                    total_referred_purchase_count = table.Column<int>(nullable: false),
                    total_referred_purchase_amount = table.Column<decimal>(nullable: false),
                    total_referred_estate_leads_count = table.Column<int>(nullable: false),
                    total_referred_estate_purchases_count = table.Column<int>(nullable: false),
                    total_referred_estate_purchases_amount = table.Column<decimal>(nullable: false),
                    total_property_purchases_by_lead_count = table.Column<int>(nullable: false),
                    total_property_purchases_by_lead_amount = table.Column<decimal>(nullable: false),
                    total_offer_to_purchases_by_lead_count = table.Column<int>(nullable: false),
                    total_hotel_stay_count = table.Column<int>(nullable: false),
                    total_hotel_stay_amount = table.Column<decimal>(nullable: false),
                    total_hotel_referral_stay = table.Column<int>(nullable: false),
                    total_hotel_referral_stay_amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_profile", x => x.customer_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_campaigns_contribution_customer_id_campaign_id",
                schema: "bonus_customer_profile",
                table: "campaigns_contribution",
                columns: new[] { "customer_id", "campaign_id" });

            migrationBuilder.CreateIndex(
                name: "IX_customer_profile_customer_id",
                schema: "bonus_customer_profile",
                table: "customer_profile",
                column: "customer_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "campaigns_contribution",
                schema: "bonus_customer_profile");

            migrationBuilder.DropTable(
                name: "customer_profile",
                schema: "bonus_customer_profile");
        }
    }
}
