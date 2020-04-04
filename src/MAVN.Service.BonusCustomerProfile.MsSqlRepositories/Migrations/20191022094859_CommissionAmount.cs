using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.BonusCustomerProfile.MsSqlRepositories.Migrations
{
    public partial class CommissionAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "total_property_purchases_by_lead_amount",
                schema: "bonus_customer_profile",
                table: "customer_profile",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "total_referred_estate_purchases_amount",
                schema: "bonus_customer_profile",
                table: "customer_profile",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total_property_purchases_by_lead_amount",
                schema: "bonus_customer_profile",
                table: "customer_profile");

            migrationBuilder.DropColumn(
                name: "total_referred_estate_purchases_amount",
                schema: "bonus_customer_profile",
                table: "customer_profile");
        }
    }
}
