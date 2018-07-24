using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace App.Data.Repositories
{
    internal class RepositoryBase
    {
        private const string CONNECTIONSTRING_KEY = "ConnectionString";

        protected SqlConnection connection;

        public RepositoryBase(IConfiguration config)
        {
            var connectionString = config.GetSection(CONNECTIONSTRING_KEY);

            if (string.IsNullOrEmpty(connectionString.Value))
                throw new ArgumentNullException("Connection string not found");

            connection = new SqlConnection(connectionString.Value);
        }
    }
}
