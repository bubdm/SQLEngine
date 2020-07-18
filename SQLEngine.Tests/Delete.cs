﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.SqlServer;

namespace SQLEngine.Tests
{
    public partial class Test_Query_Builder_Sql_Server
    {
        [TestMethod]
        public void Test_Delete_Table_1()
        {
            using (var b = Query.New)
            {
                var id = b.Column("Id");
                
                var queryThat = b
                    .Delete
                    .Table("Users")
                    .Where(id == 111)
                    .ToString();

                var query = @"
DELETE from Users WHERE Id = 111
";
                QueryAssert.AreEqual(queryThat, query);
            }
        }
        [TestMethod]
        public void Test_Delete_Table_2()
        {
            using (var b = Query.New)
            {
                var id = b.Column("Id");

                var queryThat=b
                    .Delete
                    .Table<UserTable>()
                    .Where(id == 111)
                    .ToString();

                const string query = @"
DELETE from Users WHERE Id = 111
";
                QueryAssert.AreEqual(queryThat, query);
            }
        }
        
        [TestMethod]
        public void Test_Delete_Table_3()
        {
            using (var b = Query.New)
            {
                var id = b.Column("Id");

                var queryThat = b.Delete
                    .Top(10)
                    .Table<UserTable>()
                    .Where(id == 111)
                    .ToString();

                var query = @"
DELETE TOP(10) from Users WHERE Id = 111
";
                QueryAssert.AreEqual(queryThat, query);
            }
        }

        [TestMethod]
        public void Test_Delete_Table_4()
        {
            using (var q = Query.New)
            {
                var id = q.Column("Id");
                var isBlocked = q.Column("IsBlocked");


                void BlockedUserIdList(ISelectQueryBuilder _) =>
                    _.Select("UserId")
                        .From("Attachments")
                        .Where(isBlocked == true);

                var deleteQuery = q
                    .Delete
                    .Top(10)
                    .Table<UserTable>()
                    .Where(id.In(BlockedUserIdList))
                    .ToString();

                var query = @"
DELETE TOP(10)   FROM Users 
WHERE 
(
    Id  IN (SELECT UserId
        FROM Attachments
        WHERE IsBlocked = 1)
)
";
                QueryAssert.AreEqual(deleteQuery, query);
            }
        }
    }
}