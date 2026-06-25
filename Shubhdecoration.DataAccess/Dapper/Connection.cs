using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shubhdecoration.DataAccess.Dapper
{
    public class Connection : IConnecctions
    {
       public   ConectionModel con;
        public Connection(IOptions<ConectionModel> _con) {
            con = _con.Value;
        } 
        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(con.DefaultConnection);
        }
    }
}
