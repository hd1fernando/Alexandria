using FluentMigrator;

namespace Alexandria.Migrations.Migrations
{
    [AlexandriaMigration(20220803204842, "Criação da tabela BOOOK_INSTANCE")]
    public class M001CreateTableBookInstance : Migration
    {
        private const string TABLE_NAME = "BOOK_INSTANCE";

        public override void Down()
        {
            Delete.Table(TABLE_NAME);
        }

        public override void Up()
        {
            Create.Table(TABLE_NAME)
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("CirculationType").AsString().NotNullable()
                .WithColumn("BookId").AsInt64().NotNullable().ForeignKey();

            Create.ForeignKey()
                .FromTable(TABLE_NAME).ForeignColumn("BookId")
                .ToTable("BOOK").PrimaryColumn("Id");
        }
    }
}
//  dotnet-fm migrate -a .\Alexandria.Migrations.dll -p SqlServer2016 -o -t local -c "Server=localhost,1433;DataBase=Alexandria;User Id=sa;Password=P@ssword42;Trusted_Connection=false"     --allowDirtyAssemblies
//  dotnet-fm rollback -a .\Alexandria.Migrations.dll -p SqlServer2016 -o -t local -c "Server=localhost,1433;DataBase=Alexandria;User Id=sa;Password=P@ssword42;Trusted_Connection=false"     --allowDirtyAssemblies
