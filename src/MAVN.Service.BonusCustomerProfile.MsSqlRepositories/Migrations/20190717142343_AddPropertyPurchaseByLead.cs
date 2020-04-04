using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.BonusCustomerProfile.MsSqlRepositories.Migrations
{
    public partial class AddPropertyPurchaseByLead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "total_property_purchases_by_lead_count",
                schema: "bonus_customer_profile",
                table: "customer_profile",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total_property_purchases_by_lead_count",
                schema: "bonus_customer_profile",
                table: "customer_profile");
        }
    }
}
