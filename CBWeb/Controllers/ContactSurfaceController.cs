using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.Mvc;
using System.Web.Mvc;
using System.Net.Mail;

namespace CBWeb.Controllers
{
    public class ContactSurfaceController : SurfaceController
    {
        public const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/Contact/";
        public ActionResult RenderForm()
        {
            //Finds partial view
            return PartialView(PARTIAL_VIEW_FOLDER +  "_Contact.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //stops posting from the form unless within the site

        public ActionResult submitForm( Models.ContactModel model)
        {
            //check model is valid when submitted
            if (ModelState.IsValid)
            {
                SendEmail(model);
                return RedirectToCurrentUmbracoPage();
            }
            return CurrentUmbracoPage();
        }

       
        private void SendEmail(Models.ContactModel model)
        {
            //send as from user email to user email address
            MailMessage message = new MailMessage(model.EmailAddress, model.EmailAddress);

            //using model values to create email
            message.Subject = string.Format("Enquiry from {0} {1} - Email: {2}", model.FirstName, model.LastName, model.EmailAddress);
            message.Body = model.Message;

            //used to send email
            SmtpClient client = new SmtpClient("127.0.0.1", 25);
            client.Send(message);
        }

    }
}