using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.Mvc;
using System.Web.Mvc;
using System.Net.Mail;

namespace CBWeb.Controllers
{
    public class ContractSurfaceController : SurfaceController
    {
        public const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/Contact";
        public ActionResult RenderForm()
        {
            //change this probably
            return PartialView(PARTIAL_VIEW_FOLDER +  "_Contact.cshtml");

        }

        [HttpPost]
         
        [ValidateAntiForgeryToken]
        public ActionResult submitForm(Models.ContactModel model)
        {
            if (ModelState.IsValid)
            {
                SendEmail(model);

                return RedirectToCurrentUmbracoPage();
            }

            return CurrentUmbracoPage();
        }


        private void SendEmail(Models.ContactModel model)
        {
            MailMessage message = new MailMessage(model.EmailAddress, "henryjohnpeters@gmail.com");
            message.Subject = string.Format("Enquiry from {0} {1} - Email: {2}", model.FirstName, model.LastName, model.EmailAddress);
            message.Body = model.Message;
            SmtpClient client = new SmtpClient("127.0.0.1", 25);
            client.Send(message);
        }

    }
}