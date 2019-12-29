using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Airline.DataProvider.Migrations
{
    public partial class dailyNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //var sp = @"CREATE PROCEDURE [dbo].[GetStudents]
            //        @FirstName varchar(50)
            //    AS
            //    BEGIN
            //        SET NOCOUNT ON;
            //        select * from Students where FirstName like @FirstName +'%'
            //    END";

            //migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
