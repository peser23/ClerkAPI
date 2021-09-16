using Microsoft.Data.SqlClient;
using System;
using System.Data.Common;

namespace Clerk.Framework.DataAccess
{
    public class ConnectionStringInfo
    {
        public static string DefaultProviderName = "System.Data.SqlClient";
        public string ConnectionString { get; set; }
        public DbProviderFactory Provider { get; set; }
        public static ConnectionStringInfo GetConnectionStringInfo(string connectionString, string providerName = null, DbProviderFactory factory = null)
        {
            var info = new ConnectionStringInfo();

            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("AConnectionStringMustBePassedToTheConstructor");

            if (!connectionString.Contains("="))
            {
                throw new ArgumentException("Connection string names are not supported with .NET Standard. Please use a full connectionstring.");
            }
            else
            {
                info.Provider = factory;

                if (factory == null)
                {
                    if (providerName == null)
                        providerName = DefaultProviderName;

                    info.Provider = SqlClientFactory.Instance;
                }
            }

            info.ConnectionString = connectionString;

            return info;
        }
    }



}