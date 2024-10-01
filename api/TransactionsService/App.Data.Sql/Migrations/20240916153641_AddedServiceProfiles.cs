using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Sql.Migrations
{
    public partial class AddedServiceProfiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
insert into ""ServiceProfiles"" (""Id"", ""CreatedOn"", ""UpdatedOn"", ""DateDeleted"", ""IsDeleted"", ""Name"", ""ApiKey"", ""Description"")
VALUES ('a205da10-f94d-4439-b392-86465ee6a20d', current_timestamp,current_timestamp,null, false, 'CoinGame', 'a205da10-f94d-4439-b392-86465ee6a20d',null)");
            migrationBuilder.Sql(@"
insert into ""ServiceProfiles"" (""Id"", ""CreatedOn"", ""UpdatedOn"", ""DateDeleted"", ""IsDeleted"", ""Name"", ""ApiKey"", ""Description"")
VALUES ('d048684e-a176-4578-97cd-6761017c7574', current_timestamp,current_timestamp,null, false, 'Crowdfeeding', 'd048684e-a176-4578-97cd-6761017c7574',null)");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
