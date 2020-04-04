using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.BonusCustomerProfile.MsSqlRepositories.Migrations
{
    public partial class HotelReferral : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "total_hotel_referral_stay_amount",
                schema: "bonus_customer_profile",
                table: "customer_profile",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "total_hotel_referral_stay",
                schema: "bonus_customer_profile",
                table: "customer_profile",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total_hotel_referral_stay_amount",
                schema: "bonus_customer_profile",
                table: "customer_profile");

            migrationBuilder.DropColumn(
                name: "total_hotel_referral_stay",
                schema: "bonus_customer_profile",
                table: "customer_profile");
        }
    }
}
