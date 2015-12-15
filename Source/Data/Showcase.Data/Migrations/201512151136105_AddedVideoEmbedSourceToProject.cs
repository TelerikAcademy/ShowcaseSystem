namespace Showcase.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedVideoEmbedSourceToProject : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "VideoEmbedSource", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "VideoEmbedSource", c => c.String());
        }
    }
}
