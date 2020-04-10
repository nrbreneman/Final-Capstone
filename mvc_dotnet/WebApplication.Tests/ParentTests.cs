using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using System.Data.SqlClient;

namespace WebApplication.Tests
{
    class ParentTests
    {
        private TransactionScope trans;

        protected string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=DemoDB;Integrated Security=True";


        [TestInitialize]
        public void Setup()
        {
            trans = new TransactionScope();
        }

        [TestCleanup]
        public void Reset()
        {
            trans.Dispose();
        }

        public int GetRowCount(string table)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table}", conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count;
            }
        }
    }
}
