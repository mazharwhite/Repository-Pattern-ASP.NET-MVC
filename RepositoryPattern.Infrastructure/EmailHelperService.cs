using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RepositoryPattern.Infrastructure
{
    public class EmailHelperService
    {
        internal bool SendEmail(string Subject, string Body, List<MailAddress> Receivers, List<MailAddress> BCC)
        {
            try
            {

                string SmtpServer = ConfigurationManager.AppSettings["EmailSMTPServer"];
                int SmtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["EmailSMTPPort"]);
                string SmtpUser = ConfigurationManager.AppSettings["EmailSMTPUser"];
                string SmtpPassword = ConfigurationManager.AppSettings["EmailSMTPPassword"];

                string FromEmail = ConfigurationManager.AppSettings["EmailFrom"];
                string FromEmailTitle = ConfigurationManager.AppSettings["FromEmailTitle"];

                SmtpClient smtp = new SmtpClient(SmtpServer, SmtpPort);
                smtp.EnableSsl = false;
                smtp.Credentials = new NetworkCredential(SmtpUser, SmtpPassword);

                MailMessage message = new MailMessage();
                message.From = new MailAddress(FromEmail, FromEmailTitle);

                if (Receivers != null)
                {
                    foreach (var rec in Receivers)
                    {
                        message.To.Add(rec);
                    }
                }

                if (BCC != null)
                {
                    foreach (var bcc in BCC)
                    {
                        message.Bcc.Add(bcc);
                    }
                }

                message.Subject = Subject;

                message.Body = HtmlEmailBody(Subject, Body);

                message.IsBodyHtml = true;

                try
                {
                    smtp.Send(message);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        internal string HtmlEmailBody(string preHeaderText, string middleMessageBody)
        {
            string emailHtml = @"<!doctype html>
<html>

<head>
  <meta name='viewport' content='width=device-width'>
  <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
  <title>" + preHeaderText + @"</title>
  <style>
    /* -------------------------------------
          Fonts: Poppins
      ----------------------------------------*/
    /* devanagari */

    @font-face {
      font-family: 'Poppins';
      font-style: normal;
      font-weight: 300;
      src: local('Poppins Light'), local('Poppins-Light'), url(https://fonts.gstatic.com/s/poppins/v5/pxiByp8kv8JHgFVrLDz8Z11lFc-K.woff2) format('woff2');
      unicode-range: U+0900-097F, U+1CD0-1CF6, U+1CF8-1CF9, U+200C-200D, U+20A8, U+20B9, U+25CC, U+A830-A839, U+A8E0-A8FB;
    }
    /* latin-ext */

    @font-face {
      font-family: 'Poppins';
      font-style: normal;
      font-weight: 300;
      src: local('Poppins Light'), local('Poppins-Light'), url(https://fonts.gstatic.com/s/poppins/v5/pxiByp8kv8JHgFVrLDz8Z1JlFc-K.woff2) format('woff2');
      unicode-range: U+0100-024F, U+0259, U+1E00-1EFF, U+2020, U+20A0-20AB, U+20AD-20CF, U+2113, U+2C60-2C7F, U+A720-A7FF;
    }
    /* latin */

    @font-face {
      font-family: 'Poppins';
      font-style: normal;
      font-weight: 300;
      src: local('Poppins Light'), local('Poppins-Light'), url(https://fonts.gstatic.com/s/poppins/v5/pxiByp8kv8JHgFVrLDz8Z1xlFQ.woff2) format('woff2');
      unicode-range: U+0000-00FF, U+0131, U+0152-0153, U+02BB-02BC, U+02C6, U+02DA, U+02DC, U+2000-206F, U+2074, U+20AC, U+2122, U+2191, U+2193, U+2212, U+2215, U+FEFF, U+FFFD;
    }
    /* devanagari */

    @font-face {
      font-family: 'Poppins';
      font-style: normal;
      font-weight: 400;
      src: local('Poppins Regular'), local('Poppins-Regular'), url(https://fonts.gstatic.com/s/poppins/v5/pxiEyp8kv8JHgFVrJJbecmNE.woff2) format('woff2');
      unicode-range: U+0900-097F, U+1CD0-1CF6, U+1CF8-1CF9, U+200C-200D, U+20A8, U+20B9, U+25CC, U+A830-A839, U+A8E0-A8FB;
    }
    /* latin-ext */

    @font-face {
      font-family: 'Poppins';
      font-style: normal;
      font-weight: 400;
      src: local('Poppins Regular'), local('Poppins-Regular'), url(https://fonts.gstatic.com/s/poppins/v5/pxiEyp8kv8JHgFVrJJnecmNE.woff2) format('woff2');
      unicode-range: U+0100-024F, U+0259, U+1E00-1EFF, U+2020, U+20A0-20AB, U+20AD-20CF, U+2113, U+2C60-2C7F, U+A720-A7FF;
    }
    /* latin */

    @font-face {
      font-family: 'Poppins';
      font-style: normal;
      font-weight: 400;
      src: local('Poppins Regular'), local('Poppins-Regular'), url(https://fonts.gstatic.com/s/poppins/v5/pxiEyp8kv8JHgFVrJJfecg.woff2) format('woff2');
      unicode-range: U+0000-00FF, U+0131, U+0152-0153, U+02BB-02BC, U+02C6, U+02DA, U+02DC, U+2000-206F, U+2074, U+20AC, U+2122, U+2191, U+2193, U+2212, U+2215, U+FEFF, U+FFFD;
    }
    /* -------------------------------------
          GLOBAL RESETS
      ------------------------------------- */

    img {
      border: none;
      -ms-interpolation-mode: bicubic;
      max-width: 100%;
    }

    body {
      background-color: #f6f6f6;
      font-family: sans-serif;
      font-family: 'Poppins', sans-serif;
      font-weight: 300;
      -webkit-font-smoothing: antialiased;
      font-size: 14px;
      margin: 0;
      padding: 0;
      -ms-text-size-adjust: 100%;
      -webkit-text-size-adjust: 100%;
    }

    table {
      border-collapse: separate;
      mso-table-lspace: 0pt;
      mso-table-rspace: 0pt;
      width: 100%;
    }

    table td {
      font-size: 14px;
      vertical-align: top;
    }
    /* -------------------------------------
          BODY & CONTAINER
      ------------------------------------- */

    .body {
      background-color: #f6f6f6;
      width: 100%;
    }
    /* Set a max-width, and make it display as block so it will automatically stretch to that width, but will also shrink down on a phone or something */

    .container {
      display: block;
      Margin: 0 auto !important;
      /* makes it centered */
      max-width: 580px;
      padding: 10px;
      width: 580px;
    }
    /* This should also be a block element, so that it will fill 100% of the .container */

    .content {
      box-sizing: border-box;
      display: block;
      Margin: 0 auto;
      max-width: 580px;
      padding: 10px;
    }
    /* -------------------------------------
          HEADER, FOOTER, MAIN
      ------------------------------------- */

    .main {
      background: #fff;
      border-radius: 3px;
      width: 100%;
    }

    .wrapper {
      box-sizing: border-box;
      padding: 30px 20px;
    }

    .footer {
      clear: both;
      text-align: center;
      width: 100%;
      background-color: #f0f8ff;
      margin-top: 10px;
      padding: 10px 0;
    }
    .footer td,
    .footer p,
    .footer span,
    .footer a {
      color: #999999;
      font-size: 10px;
      text-align: justify;
    }
    .footer .content-block {
      padding: 0 10px;
    }    
    .text-center {
      text-align: center !important;
    }
    /* -------------------------------------
          TYPOGRAPHY
      ------------------------------------- */

    h1,
    h2,
    h3,
    h4 {
      color: #000000;
      font-family: sans-serif;
      font-family: 'Poppins', sans-serif;
      font-weight: 400;
      line-height: 1.4;
      margin: 0;
      Margin-bottom: 30px;
    }

    h1 {
      font-size: 35px;
      font-weight: 300;
      text-align: center;
      text-transform: capitalize;
    }

    p,
    ul,
    ol {
      font-family: sans-serif;
      font-family: 'Poppins', sans-serif;
      font-size: 14px;
      line-height: 26.8px;
      font-weight: normal;
      font-weight: 300;
      margin: 0;
      margin-bottom: 15px;
    }

    p li,
    ul li,
    ol li {
      list-style-position: inside;
      margin-left: 5px;
    }

    a {
      color: #3498db;
      text-decoration: underline;
    }
    /* -------------------------------------
          BUTTONS
      ------------------------------------- */

    .btn {
      box-sizing: border-box;
      width: 100%;
    }

    .btn>tbody>tr>td {
      padding-bottom: 15px;
    }

    .btn table {
      width: auto;
    }

    .btn table td {
      background-color: #ffffff;
      border-radius: 5px;
      text-align: center;
    }

    .btn a {
      background-color: #ffffff;
      border: solid 1px #3498db;
      border-radius: 5px;
      box-sizing: border-box;
      color: #3498db;
      cursor: pointer;
      display: inline-block;
      font-size: 14px;
      font-weight: bold;
      margin: 0;
      padding: 12px 25px;
      text-decoration: none;
      text-transform: capitalize;
    }

    .btn-primary table td {
      background-color: #3498db;
    }

    .btn-primary a {
      background-color: #3498db;
      border-color: #3498db;
      color: #ffffff;
    }
    /* -------------------------------------
          OTHER STYLES THAT MIGHT BE USEFUL
      ------------------------------------- */

    .last {
      margin-bottom: 0;
    }

    .first {
      margin-top: 0;
    }

    .align-center {
      text-align: center;
    }

    .align-right {
      text-align: right;
    }

    .align-left {
      text-align: left;
    }

    .clear {
      clear: both;
    }

    .mt0 {
      margin-top: 0;
    }

    .mb0 {
      margin-bottom: 0;
    }

    .preheader {
      color: transparent;
      display: none;
      height: 0;
      max-height: 0;
      max-width: 0;
      opacity: 0;
      overflow: hidden;
      mso-hide: all;
      visibility: hidden;
      width: 0;
    }

    .powered-by a {
      text-decoration: none;
    }

    hr {
      border: 0;
      border-bottom: 1px solid #f6f6f6;
      Margin: 20px 0;
    }
    /* -------------------------------------
          RESPONSIVE AND MOBILE FRIENDLY STYLES
      ------------------------------------- */

    @media only screen and (max-width: 620px) {
      table[class=body] h1 {
        font-size: 28px !important;
        margin-bottom: 10px !important;
      }
      table[class=body] p,
      table[class=body] ul,
      table[class=body] ol,
      table[class=body] td,
      table[class=body] span,
      table[class=body] a {
        font-size: 16px !important;
      }
      table[class=body] .wrapper,
      table[class=body] .article {
        padding: 10px !important;
      }
      table[class=body] .content {
        padding: 0 !important;
      }
      table[class=body] .container {
        padding: 0 !important;
        width: 100% !important;
      }
      table[class=body] .main {
        border-left-width: 0 !important;
        border-radius: 0 !important;
        border-right-width: 0 !important;
      }
      table[class=body] .btn table {
        width: 100% !important;
      }
      table[class=body] .btn a {
        width: 100% !important;
      }
      table[class=body] .img-responsive {
        height: auto !important;
        max-width: 100% !important;
        width: auto !important;
      }
    }
    /* -------------------------------------
          PRESERVE THESE STYLES IN THE HEAD
      ------------------------------------- */

    @media all {
      .ExternalClass {
        width: 100%;
      }
      .ExternalClass,
      .ExternalClass p,
      .ExternalClass span,
      .ExternalClass font,
      .ExternalClass td,
      .ExternalClass div {
        line-height: 100%;
      }
      .apple-link a {
        color: inherit !important;
        font-family: inherit !important;
        font-size: inherit !important;
        font-weight: inherit !important;
        line-height: inherit !important;
        text-decoration: none !important;
      }
      .btn-primary table td:hover {
        background-color: #34495e !important;
      }
      .btn-primary a:hover {
        background-color: #34495e !important;
        border-color: #34495e !important;
      }
    }
  </style>
</head>

<body class='' style='background-color: #f6f6f6;font-family: 'Poppins', sans-serif;font-weight: 300;-webkit-font-smoothing: antialiased;font-size: 14px;margin: 0;padding: 0;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;'>
  <table border='0' cellpadding='0' cellspacing='0' class='body' style='border-collapse: separate;mso-table-lspace: 0pt;mso-table-rspace: 0pt;width: 100%;background-color: #f6f6f6;'>
    <tr>
      <td style='font-size: 14px;vertical-align: top;'>&nbsp;</td>
      <td class='container' style='font-size: 14px;vertical-align: top;display: block;max-width: 580px;padding: 10px;width: 580px;margin: 0 auto !important;'>
        <div class='content' style='box-sizing: border-box;display: block;margin: 0 auto;max-width: 580px;padding: 10px;'>

          <!-- START CENTERED WHITE CONTAINER -->
          <span class='preheader' style='color: transparent;display: none;height: 0;max-height: 0;max-width: 0;opacity: 0;overflow: hidden;mso-hide: all;visibility: hidden;width: 0;'>" + preHeaderText + @" - </span>
          <table class='main' style='border-collapse: separate;mso-table-lspace: 0pt;mso-table-rspace: 0pt;width: 100%;background: #fff;border-radius: 3px;'>

            <!-- START MAIN CONTENT AREA -->
            <tr>
              <td class='wrapper' style='font-size: 14px;vertical-align: top;box-sizing: border-box;padding: 30px 20px;'>
                <table border='0' cellpadding='0' cellspacing='0' style='border-collapse: separate;mso-table-lspace: 0pt;mso-table-rspace: 0pt;width: 100%;'>
                  <tr>
                    <td style='font-size: 14px;vertical-align: top;'>
                      " + middleMessageBody + @"
                    </td>
                  </tr>

                  <tr>
                    <td style='font-size: 14px;vertical-align: top;'>
                      <hr style='border: 0;border-bottom: 1px solid #f6f6f6;margin: 20px 0;'>
                      <div>
                        <a href='https://find2me.org' style='color: #3498db;text-decoration: underline;'>
                          <img src=""https://find2me.org/images/find2me-logo.png"" alt='Find2Me' title='Find2Me' style='border: none;-ms-interpolation-mode: bicubic;max-width: 100%;'>
                        </a>
                      </div>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>

            <!-- END MAIN CONTENT AREA -->
          </table>

          <!-- START FOOTER -->
          <div class='footer' style='clear: both;text-align: center;width: 100%;background-color: #f0f8ff;margin-top: 10px;padding: 10px 0;'>
            <table border='0' cellpadding='0' cellspacing='0' style='border-collapse: separate;mso-table-lspace: 0pt;mso-table-rspace: 0pt;width: 100%;'>
              <tr>
                <td class='content-block' style='font-size: 10px;vertical-align: top;color: #999999;text-align: justify;padding: 0 10px;'>
                    This email and any files transmitted with it are confidential and intended solely for the use of the individual or entity to whom they are addressed. If you have received this email in error please notify the system manager. This message contains confidential information and is intended only for the individual named. If you are not the named addressee you should not disseminate, distribute or copy this e-mail. Please notify the sender immediately by e-mail if you have received this e-mail by mistake and delete this e-mail from your system. If you are not the intended recipient you are notified that disclosing, copying, distributing or taking any action in reliance on the contents of this information is strictly prohibited.
                </td>
              </tr>
              <tr>
                <td class='content-block text-center' style='font-size: 10px;vertical-align: top;color: #999999;text-align: center !important;padding: 0 10px;'>
                  <br>
                  Powered By: <a href='https://find2me.org' style='color: #999999;text-decoration: underline;font-size: 10px;text-align: justify;'>Find2Me</a>
                </td>
              </tr>
            </table>
          </div>
          <!-- END FOOTER -->

          <!-- END CENTERED WHITE CONTAINER -->
        </div>
      </td>
      <td style='font-size: 14px;vertical-align: top;'>&nbsp;</td>
    </tr>
  </table>
</body>

</html>";

            return emailHtml;
        }

        public bool SendEmailConfirmationTokenMail(string email, string callbackUrl, string confirmationEmailSubject)
        {
            throw new NotImplementedException();
        }

        /*************************************************************************************/
        /*************************************************************************************/

        public bool SendEmailConfirmationTokenMail(string _RecEmail, string _CallbackUrl, string subject, string langCode)
        {            
            StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath("~/Templates/Email/ConfirmationEmail-" + langCode + ".html"));
            string bodyText = sr.ReadToEnd();
            sr.Close();
            bodyText = bodyText.Replace("###_CallbackUrl###", _CallbackUrl);

            /*        string bodyText = @"
                        <p style='font-family: 'Poppins', sans-serif;font-size: 14px;line-height: 26.8px;font-weight: 300;margin: 0;margin-bottom: 15px;'>
                            Hello,
                        </p>
                        <br>
                        <p style='font-family: 'Poppins', sans-serif;font-size: 14px;line-height: 26.8px;font-weight: 300;margin: 0;margin-bottom: 15px;'>
                            Please click the link below or click <a href=" + _CallbackUrl + @">here</a> to confirm your email address:
                            <br />
                            <br />
                            <a href='" + _CallbackUrl + "'>" + _CallbackUrl + @"</a>
                        </p>
                        <br>
                        <p style='font-family: 'Poppins', sans-serif;font-size: 14px;line-height: 26.8px;font-weight: 300;margin: 0;margin-bottom: 15px;'>
                            Thank you, <br> 
                            Find2Me
                        </p>
                        ";
                        string subject = "Confirm your Email Address - Find2Me";
            */           

            return SendEmail(subject, bodyText, new List<MailAddress>
            {
                new MailAddress(_RecEmail),
            }, null);
            //throw new NotImplementedException();
        }
    }
}
