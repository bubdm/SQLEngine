﻿using System;

namespace SQLEngine
{
    public interface IInsertNoIntoWithColumns : IAbstractInsertQueryBuilder
    {
        IInsertNoValuesQueryBuilder Values(params ISqlExpression[] values);
        IInsertNoValuesQueryBuilder Values(params AbstractSqlLiteral[] values);

        IInsertNoValuesQueryBuilder Values(Action<ISelectQueryBuilder> builder);
    }
}