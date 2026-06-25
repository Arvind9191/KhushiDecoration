using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace Shubhdecoration.DataAccess.Dapper
{
    public interface IConnecctions
    {
        public NpgsqlConnection GetConnection();
    }
}
