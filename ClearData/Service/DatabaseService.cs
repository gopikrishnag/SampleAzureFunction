using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ClearData.Service;
using Microsoft.Extensions.Logging;
namespace ClearData.Service
{
    
    public class DatabaseService : IDatabaseService
    {
        private readonly string _connStr = Environment.GetEnvironmentVariable("SQLConnectionString");
        
        public async Task<byte> CleanDatabaseTables(string email)
        {
            byte result = 0;

            try
            {
                using (var conn = new SqlConnection(_connStr))
                {
                   
                    using (var cmd = new SqlCommand("ClearData", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email", email + " : " + DateTime.Now.ToLocalTime().ToShortTimeString());
                        cmd.Parameters.Add(new SqlParameter("@result", SqlDbType.TinyInt)).Direction = ParameterDirection.Output;
                        await conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                      //  _logger.LogInformation("SQL Info - Email id :", email  );
                        result = (byte) cmd.Parameters["@result"].Value;
                    }

                   
                }
            }
            catch (Exception ex)
            {
                //_logger.LogInformation("SQL Exception :", ex.ToString());
            }

            return result;
        }
    }
}


 
