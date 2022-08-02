using FluentMigrator;

namespace Alexandria.Migrations.Migrations
{
    [AlexandriaMigrationAttribute(20220802201342, "Criação da tabela BOOK")]
    public class M000CreateTableBook : Migration
    {
        private const string TABLE_NAME = "BOOK";

        public override void Down()
        {
            Delete.Table(TABLE_NAME);
        }

        public override void Up()
        {
            Create.Table(TABLE_NAME)
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("Price").AsDecimal().NotNullable()
                .WithColumn("ISBN").AsString().NotNullable().Unique();
        }
    }
}
//dotnet-fm migrate -a .\Alexandria.Migrations.dll -p SqlServer2016 -o -t local -c "Server=localhost,1433;DataBase=Alexandria;User Id=sa;Password=P@ssword42;Trusted_Connection=false"     --allowDirtyAssemblies
