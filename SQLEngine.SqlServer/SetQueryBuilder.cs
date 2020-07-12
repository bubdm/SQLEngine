﻿namespace SQLEngine.SqlServer
{
    internal class SetQueryBuilder : AbstractQueryBuilder, ISetNeedSetQueryBuilder, ISetNeedToQueryBuilder, 
        ISetNoSetNoToQueryBuilder
    {
        private AbstractSqlVariable _variable;
        private ISqlExpression _value;
        protected override void ValidateAndThrow()
        {
            base.ValidateAndThrow();
            //TODO
        }
        public ISetNeedToQueryBuilder Set(AbstractSqlVariable variable)
        {
            _variable = variable;
            return this;
        }

        public ISetNoSetNoToQueryBuilder To(ISqlExpression value)
        {
            _value = value;
            return this;
        }

        public override string Build()
        {
            Writer.Write(SQLKeywords.SET);
            Writer.Write2();
            Writer.Write(_variable.ToSqlString());
            Writer.Write(SQLKeywords.SPACE);
            Writer.Write2(SQLKeywords.EQUALS);
            Writer.Write(_value.ToSqlString());
            Writer.Write(SQLKeywords.SEMICOLON);
            return base.Build();
        }
    }
}