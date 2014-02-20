using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace aventyrliga_kontakter.Model.DAL
{
    public abstract class DALBase
    {
        /// <summary>
        /// Sparar anslutningssträng
        /// </summary>
        private static string _connectionString;

        /// <summary>
        /// Skapar anslutning
        /// </summary>
        /// <returns>anslutningsobjekt</returns>
        protected static SqlConnection CreateConnection()
        {
            try
            {
                return new SqlConnection(_connectionString);
            }
            catch (Exception)
            {
                throw new ApplicationException("Ett fel inträffade då uppgifter hämtades.");
            }
        }

        /// <summary>
        /// Hämtar anslutningssträng från webconfig och sparar
        /// </summary>
        static DALBase()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["AdventureWorksConnectionString"].ConnectionString;
        }
    }
}