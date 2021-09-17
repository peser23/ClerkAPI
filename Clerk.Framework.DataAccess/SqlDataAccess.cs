using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Clerk.Framework.DataAccess
{
    public class SqlDataAccess : DataAccessBase
    {
        public SqlDataAccess()
        {
            dbProvider = SqlClientFactory.Instance;
        }
        public SqlDataAccess(string connectionString)
        : base(connectionString, SqlClientFactory.Instance)
        { }
        public SqlDataAccess(string connectionString, DbProviderFactory provider)
            : base(connectionString, provider)
        { }
       
        public List<DbParameter> CreateParams()
        {
            return new List<DbParameter>();
        }

        public SqlParameter CreateParam(string parameterName, string value, SqlDbType sqlDbType, ParameterDirection direction)
        {
            return new SqlParameter { ParameterName = parameterName, SqlDbType = sqlDbType, Direction = direction, Value = value };
        }
    }

}

