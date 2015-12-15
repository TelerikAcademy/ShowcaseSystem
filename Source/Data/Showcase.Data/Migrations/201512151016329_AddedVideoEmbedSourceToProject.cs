namespace Showcase.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedVideoEmbedSourceToProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "VideoEmbedSource", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "VideoEmbedSource");
        }
    }
}
