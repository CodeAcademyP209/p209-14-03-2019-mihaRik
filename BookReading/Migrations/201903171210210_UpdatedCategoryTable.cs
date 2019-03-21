namespace BookReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedCategoryTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String());
        }
    }
}
