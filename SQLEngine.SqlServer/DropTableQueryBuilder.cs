﻿namespace SQLEngine.SqlServer
{
    internal class DropTableQueryBuilder : AbstractQueryBuilder, 
        IDropTableQueryBuilder, IDropTableNoNameQueryBuilder,
        IDropTableNoNameNoSchemaQueryBuilder,
        IDropTableNoNameNoSchemaNoDBQueryBuilder
    {
        private string _databaseName;
        private string _tableName;
        private string _schemaName;

        public IDropTableNoNameQueryBuilder Table(string tableName)
        {
            _tableName = tableName;
            return this;
        }


        public IDropTableNoNameNoSchemaQueryBuilder FromSchema(string schemaName)
        {
            _schemaName = schemaName;
            return this;
        }
        public IDropTableNoNameNoSchemaNoDBQueryBuilder FromDB(string databaseName)
        {
            _databaseName = databaseName;
            return this;
        }
        public DropTableQueryBuilder DropTable(string tableName, string schemaName, string databaseName = null)
        {
            _schemaName = schemaName;
            _tableName = tableName;
            _databaseName = databaseName;
            return this;
        }

        protected override void ValidateAndThrow()
        {
            if (string.IsNullOrEmpty(_tableName))
            {
                Bomb();
            }
            base.ValidateAndThrow();
        }

        public override string Build()
        {
            Writer.Write(C.DROP);
            Writer.Write2(C.TABLE);
            if (!string.IsNullOrEmpty(_databaseName))
            {
                Writer.Write(I(_databaseName));
                Writer.Write(C.DOT);
            }
            if (!string.IsNullOrEmpty(_schemaName))
            {
                Writer.Write(I(_schemaName));
                Writer.Write(C.DOT);
            }
            Writer.Write(I(_tableName));
            Writer.Write(C.SEMICOLON);

            return base.Build();
        }
    }
}