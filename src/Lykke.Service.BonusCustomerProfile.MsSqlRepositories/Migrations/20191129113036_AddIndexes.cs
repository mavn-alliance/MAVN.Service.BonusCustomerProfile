using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.BonusCustomerProfile.MsSqlRepositories.Migrations
{
    public partial class AddIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_campaigns_contribution_customer_id_campaign_id",
                schema: "bonus_customer_profile",
                table: "campaigns_contribution",
                columns: new[] { "customer_id", "campaign_id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_campaigns_contribution_customer_id_campaign_id",
                schema: "bonus_customer_profile",
                table: "campaigns_contribution");
        }
    }
}
