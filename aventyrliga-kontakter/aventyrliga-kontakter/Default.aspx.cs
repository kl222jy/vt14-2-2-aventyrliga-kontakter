using aventyrliga_kontakter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aventyrliga_kontakter
{
    public partial class _Default : Page
    {
        /// <summary>
        /// Sparat service objekt
        /// </summary>
        private Service _service;

        /// <summary>
        /// Hämtar serviceobjekt(skapar det om det inte redan finns)
        /// </summary>
        private Service Service
        {
            get
            {
                try
                {
                    return _service ?? (_service = new Service());
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Ett oväntat fel inträffade då data skulle hämtas.");
                }
                return null;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Hämtar kontaktlista
        /// </summary>
        /// <param name="maximumRows">antal rader per sida</param>
        /// <param name="startRowIndex">vilken rad listan ska börja på</param>
        /// <param name="totalRowCount">totalt antal rader</param>
        /// <returns>kontaktlista</returns>
        public IEnumerable<Contact> ContactListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            startRowIndex += 1;
            try
            {
                return Service.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Ett oväntat fel inträffade då data skulle hämtas.");
                totalRowCount = 100;
                return null;
            }
        }

        /// <summary>
        /// Lägg till kontakt
        /// </summary>
        /// <param name="contact">Kontakt</param>
        public void ContactListView_InsertItem(Contact contact)
        {
            try
            {
                Service.SaveContact(contact);
                SuccessLiteral.Text = "Kontakten lades till.";
                SuccessPanel.Visible = true;
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ett oväntat fel inträffade då kontakten skulle läggas till.");
            }
        }

        /// <summary>
        /// Uppdatera post
        /// </summary>
        /// <param name="id">kontaktid</param>
        public void ContactListView_UpdateItem(int ContactId)
        {
            try
            {
                var contact = Service.GetContact(ContactId);
                if (contact == null)
                {
                    ModelState.AddModelError(string.Empty, "Kontakten hittades inte.");
                    return;
                }
                if (TryUpdateModel(contact))
                {
                    Service.SaveContact(contact);
                }
                SuccessLiteral.Text = "Uppdateringen lyckades.";
                SuccessPanel.Visible = true;
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ett oväntat fel inträffade då kontakten skulle uppdateras.");
            }
        }

        /// <summary>
        /// Ta bort post
        /// </summary>
        /// <param name="id">kontaktid</param>
        public void ContactListView_DeleteItem(int ContactId)
        {
            try
            {
                Service.DeleteContact(ContactId);
                SuccessLiteral.Text = "Kontakten togs bort.";
                SuccessPanel.Visible = true;
            }
            catch (Exception ex)
            {
                //ModelState.AddModelError(string.Empty, ex.Message);
                ModelState.AddModelError(string.Empty, "Ett oväntat fel inträffade då kontakten skulle tas bort.");
            }
        }
    }
}