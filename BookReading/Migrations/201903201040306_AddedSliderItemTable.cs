namespace BookReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSliderItemTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SliderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 500),
                        Button = c.String(nullable: false, maxLength: 50),
                        Image = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SliderItems");
        }
    }
}
