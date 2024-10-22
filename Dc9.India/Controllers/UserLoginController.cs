using Dc9.India.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Dc9.India.Controllers
{
    public class UserLoginController : Controller
    {
        // GET: UserLogin
       [HttpPost]
        public JsonResult InsertUserDetails( string UserName, string EmailId, string Password, string MobileNo)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";           
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action","Registration" },
                    {"@UserName",UserName.ToString().Trim() },
                    {"@EmailId",EmailId.Trim() },
                    {"@Password", CommonMethod.Encrypt(CommonMethod.SecureHashSHA256(Password.Trim()),"") },
                    {"@MobileNo",MobileNo.Trim() },
                };
                DataTable dt = CommonMethod.ExecuteProc("UserDetail_Regisration_Login_SP", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Result"] = dt.Rows[0]["Msg"].ToString();
                }
            }
            catch (Exception ex)
            {

                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        [HttpPost]
        public JsonResult LoginDetail(string UserCode, string Password)
        {
            DataTable DtLoginInfo = new DataTable();
            Dictionary<string, string> Dic = new Dictionary<string, string>();
            Dic["Result"] = "";
            try
            {
                string[,] param = new string[,]{
                            {"@EmailId",UserCode.Trim()},
                            {"@Password",CommonMethod.Encrypt(CommonMethod.SecureHashSHA256(Password.Trim()),"")},
                            {"@Action","Login" },
                        };
                DtLoginInfo = CommonMethod.ExecuteProc("UserDetail_Regisration_Login_SP", param);
                if (DtLoginInfo.Rows.Count > 0)
                {
                    if (DtLoginInfo.Rows[0]["Msg"].ToString() == "Success") 
                    { 
                                    
                        Session["UserId"] = DtLoginInfo.Rows[0]["UserId"].ToString();
                        Session["UserEmailId"] = DtLoginInfo.Rows[0]["Emailid"].ToString();
                        Dic["Result"] = DtLoginInfo.Rows[0]["Msg"].ToString();
                    }
                    else
                    {
                        Dic["Result"] = DtLoginInfo.Rows[0]["Msg"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Dic["Result"] = ex.Message;
            }
            return Json(Dic);
        }

        [HttpPost]
        public JsonResult SendContactUs(string Name, string EmailID, string Message)
        {
            Hashtable data = new Hashtable();
            data["Error"] = "";
            data["Focus"] = "";
            try
            {

                if (Name.Trim() == "")
                {
                   // data["Error"] = CommonMethod.PleaseEnterValidateMessage("Your Name", "3");
                    data["Focus"] = "txtYourName";
                }
                else if (EmailID.Trim() == "")
                {
                    //data["Error"] = CommonMethod.PleaseEnterValidateMessage("Your Email Address", "3");
                    data["Focus"] = "txtEmailAddress";
                }
                //else if (CommonMethod.SIPLGF.IsEmail(EmailID) == false)
                //{
                //    //data["Error"] = CommonMethod.PleaseEnterValidateMessage("Your Email Address", "2");
                //    data["Focus"] = "txtEmailAddress";
                //}
                else if (Message.Trim() == "")
                {
                    //data["Error"] = CommonMethod.PleaseEnterValidateMessage("Your Message", "3");
                    data["Focus"] = "txtMessages";
                }
                else
                {
                    MailMessage mm = new MailMessage();
                    string EmailFrom = ConfigurationManager.AppSettings["EmailFrom"].ToString();
                    string EMailTo = ConfigurationManager.AppSettings["EMailTo"].ToString();
                    string EMailToCC = ConfigurationManager.AppSettings["EMailToCC"].ToString();
                    string EMailToBCC = ConfigurationManager.AppSettings["EMailToBCC"].ToString();
                    string EMailSubject = "DC9India : Contact Us";
                    string EMailBody = CommonMethod.SendMailForContact(Name, EmailID, Message);
                    MailAddress From = new MailAddress(EmailFrom);
                    mm.From = From;
                    if (EMailTo.Trim() != "")
                    {
                        string[] MailTo = EMailTo.Split(',');
                        foreach (string Mail in MailTo)
                        {
                            mm.To.Add(Mail);
                        }
                    }
                    if (EMailToCC.Trim() != "")
                    {
                        string[] CCMail = EMailToCC.Split(',');
                        foreach (string MailCC in CCMail)
                        {
                            mm.CC.Add(MailCC);
                        }
                    }
                    if (EMailToBCC.Trim() != "")
                    {
                        string[] BCC = EMailToBCC.Split(',');
                        foreach (string MailBCC in BCC)
                        {
                            mm.Bcc.Add(MailBCC);
                        }
                    }
                    mm.Subject = EMailSubject;
                    mm.Body = EMailBody;
                    mm.IsBodyHtml = true;
                    string Result = CommonMethod.Send(mm, EmailFrom);
                    if (Result == "" || Result == null)
                    {
                        data["Error"] = "Success";
                        data["Focus"] = "txtYourName";
                    }
                    else
                    {
                        data["Error"] = "Something Went wrong...";
                    }
                }
            }
            catch (Exception ex)
            {
                data["Error"] = ex.Message;
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}