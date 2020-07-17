﻿namespace SQLEngine.SqlServer
{
    internal class TruncateQueryBuilder : AbstractQueryBuilder, ITruncateQueryBuilder, ITruncateNoTableQueryBuilder
    {
        private string _tableName;
        public ITruncateNoTableQueryBuilder Table(string tableName)
        {
            _tableName = tableName;
            return this;
        }

        public override string Build()
        {
            Writer.Write(C.TRUNCATE);
            Writer.Write2(C.TABLE);
            Writer.Write(I(_tableName));
            return base.Build();
        }
    }
}