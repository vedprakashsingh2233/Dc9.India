using Dc9.India.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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


    }
}