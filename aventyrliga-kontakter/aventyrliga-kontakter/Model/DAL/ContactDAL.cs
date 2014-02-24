using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace aventyrliga_kontakter.Model.DAL
{
    public class ContactDAL : DALBase
    {
        /// <summary>
        /// Tar bort kontakt
        /// </summary>
        /// <param name="contactId">kontaktid</param>
        public void DeleteContact(int contactId)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Person.uspRemoveContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactId;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        /// <summary>
        /// Hämtar kontakt baserat på kontaktid
        /// </summary>
        /// <param name="contactId">kontaktid</param>
        /// <returns>enskild kontakt</returns>
        public Contact GetContactById(int contactId)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Person.uspGetContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactId;

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var contactIdIndex = reader.GetOrdinal("ContactID");
                            var firstNameIndex = reader.GetOrdinal("FirstName");
                            var lastNameIndex = reader.GetOrdinal("LastName");
                            var emailAdressIndex = reader.GetOrdinal("EmailAddress");

                            return new Contact
                            {
                                ContactId = reader.GetInt32(contactIdIndex),
                                FirstName = reader.GetString(firstNameIndex),
                                LastName = reader.GetString(lastNameIndex),
                                EmailAdress = reader.GetString(emailAdressIndex)
                            };
                        }
                    }

                    return null;
                }
                catch
                {

                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        /// <summary>
        /// Hämtar kontaktlista
        /// </summary>
        /// <returns>Kontaktlista</returns>
        public IEnumerable<Contact> GetContacts()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var contacts = new List<Contact>(100);
                    var cmd = new SqlCommand("Person.uspGetContacts", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var contactIdIndex = reader.GetOrdinal("ContactID");
                        var firstNameIndex = reader.GetOrdinal("FirstName");
                        var lastNameIndex = reader.GetOrdinal("LastName");
                        var emailAdressIndex = reader.GetOrdinal("EmailAddress");

                        while (reader.Read())
                        {
                            contacts.Add(new Contact
                            {
                                ContactId = reader.GetInt32(contactIdIndex),
                                FirstName = reader.GetString(firstNameIndex),
                                LastName = reader.GetString(lastNameIndex),
                                EmailAdress = reader.GetString(emailAdressIndex)
                            });
                        }
                    }

                    contacts.TrimExcess();
                    if (contacts.Count > 1)
                    {
                        return contacts;                        
                    }
                    else
                    {
                        throw new ApplicationException("An error occured while getting contacts from the database.");
                    }
                }
                catch
                {
                    throw new ApplicationException("An error occured while getting contacts from the database.");
                }
            }
        }

        /// <summary>
        /// Hämtar del av kontaktlista (för paging)
        /// </summary>
        /// <param name="maximumRows">Antal rader att hämta</param>
        /// <param name="startRowIndex">Första rad att hämta</param>
        /// <param name="totalRowCount">Totalt antal rader</param>
        /// <returns>del av kontaktlista</returns>
        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    var contacts = new List<Contact>(100);
                    SqlCommand cmd = new SqlCommand("Person.uspGetContactsPageWise", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = (startRowIndex / maximumRows) + 1;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var contactIdIndex = reader.GetOrdinal("ContactID");
                        var firstNameIndex = reader.GetOrdinal("FirstName");
                        var lastNameIndex = reader.GetOrdinal("LastName");
                        var emailAdressIndex = reader.GetOrdinal("EmailAddress");

                        while (reader.Read())
                        {
                            contacts.Add(new Contact
                            {
                                ContactId = reader.GetInt32(contactIdIndex),
                                FirstName = reader.GetString(firstNameIndex),
                                LastName = reader.GetString(lastNameIndex),
                                EmailAdress = reader.GetString(emailAdressIndex)
                            });
                        }
                    }
                    contacts.TrimExcess();
                    totalRowCount = (int)cmd.Parameters["@RecordCount"].Value;
                    if (contacts.Count > 1)
                    {
                        return contacts;
                    }
                    else
                    {
                        throw new ApplicationException("An error occured while getting contacts from the database.");
                    }
                }

                catch (Exception)
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        /// <summary>
        /// Lägg till kontakt
        /// </summary>
        /// <param name="contact">Kontakt (objekt)</param>
        public void InsertContact(Contact contact)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Person.uspAddContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 50).Value = contact.EmailAdress;

                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    contact.ContactId = (int)cmd.Parameters["@ContactID"].Value;
                }
                catch (Exception)
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        /// <summary>
        /// Uppdatera kontakt
        /// </summary>
        /// <param name="contact">kontakt (objekt)</param>
        public void UpdateContact(Contact contact)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Person.uspUpdateContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contact.ContactId;
                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 100).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 100).Value = contact.EmailAdress;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }
    }
}