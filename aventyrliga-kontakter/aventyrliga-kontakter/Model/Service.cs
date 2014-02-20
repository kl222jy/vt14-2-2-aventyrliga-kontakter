using aventyrliga_kontakter.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aventyrliga_kontakter.Model
{
    public class Service
    {
        /// <summary>
        /// Sparar databasobjekt för tabellen kontakt
        /// </summary>
        private ContactDAL _contactDAL;

        /// <summary>
        /// Hanterar databaslager för kontakt (skapar objektet om det inte redan finns)
        /// </summary>
        private ContactDAL ContactDAL { 
            get
            {
                try
                {
                    return _contactDAL ?? (_contactDAL = new ContactDAL());
                }
                catch (Exception)
                {

                    throw new ApplicationException("Ett oväntat fel inträffade då data skulle hämtas.");
                }
            }
        }

        /// <summary>
        /// Ta bort kontakt (objekt)
        /// </summary>
        /// <param name="contact">Kontakt (objekt)</param>
        public void DeleteContact(Contact contact)
        {
            ContactDAL.DeleteContact(contact.ContactId);
        }

        /// <summary>
        /// Ta bort kontakt (id)
        /// </summary>
        /// <param name="contactId">Kontaktid</param>
        public void DeleteContact(int contactId)
        {
            ContactDAL.DeleteContact(contactId);
        }

        /// <summary>
        /// Hämta enskild kontakt (id)
        /// </summary>
        /// <param name="contactId">Kontaktid</param>
        /// <returns>enskild kontakt</returns>
        public Contact GetContact(int contactId)
        {
            return ContactDAL.GetContactById(contactId);
        }

        /// <summary>
        /// Hämta kontaktlista (hela)
        /// </summary>
        /// <returns>hela kontaktlistan</returns>
        public IEnumerable<Contact> GetContacts()
        {
            return ContactDAL.GetContacts();
        }

        /// <summary>
        /// Hämta kontaktlista (del, för paging)
        /// </summary>
        /// <param name="maximumRows">Antal rader att hämta</param>
        /// <param name="startRowIndex">Rad att börja på</param>
        /// <param name="totalRowCount">Totalt antal rader</param>
        /// <returns>Kontaktlista (del)</returns>
        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return ContactDAL.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        /// <summary>
        /// Spara kontakt (objekt)
        /// </summary>
        /// <param name="contact">kontakt (objekt)</param>
        public void SaveContact(Contact contact)
        {
            if (contact.ContactId == 0)
            {
                ContactDAL.InsertContact(contact);
            }
            else
            {
                ContactDAL.UpdateContact(contact);
            }
        }
    }
}