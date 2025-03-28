﻿using Dc9.India.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dc9.India.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        #region Login
        public ActionResult Login()
        {
            return View();
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
                            {"@UserCode",UserCode.Trim()},
                            {"@Passwrd",Password.Trim()},
                            {"@Action","Login" },
                        };
                DtLoginInfo = CommonMethod.ExecuteProc("USP_GetLoginDetail", param);
                if (DtLoginInfo.Rows.Count > 0)
                {
                    if (DtLoginInfo.Rows[0]["Status"].ToString() == "3")
                    {
                        Session["UserCode"] = UserCode.ToString();
                        Session["UserId"] = DtLoginInfo.Rows[0]["UserId"].ToString();
                        Session["UserType"] = DtLoginInfo.Rows[0]["UserType"].ToString();
                    }
                    else if (DtLoginInfo.Rows[0]["Status"].ToString() == "1")
                    {
                        Dic["Result"] = "Your Account is not active please contact to administrator.!";
                    }
                    else
                    {
                        Dic["Result"] = "Invalid User Code Or Password.!";
                    }

                }
            }
            catch (Exception ex)
            {
                Dic["Result"] = ex.Message;
            }
            return Json(Dic);
        }

        #endregion Login

        #region Category Mater
        public ActionResult CategoryMaster()
        {
            if (Session["UserId"] == null)
            {
                return Redirect("~/Admin/Login");
            }
            else
            {
                return View();
            }
        }
        public JsonResult InsertUpdateCategoryMaster(int Id, string CategoryName, string IsActive)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            string Action = "";
            if (Id != 0)
            {
                Action = "Update";
            }
            else
            {
                Action = "Insert";
            }
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action",Action },
                    {"@Id",Id.ToString().Trim() },
                    {"@CategoryName",CategoryName.Trim() },
                    {"@IsActive",IsActive.Trim() },
                };
                DataTable dt = CommonMethod.ExecuteProc("USP_InsertUpdateDelCategoryMaster", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Result"] = dt.Rows[0]["Message"].ToString();
                }
            }
            catch (Exception ex)
            {

                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        public JsonResult ShowCategoryList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action","Select" },
                };
                DataTable dt = CommonMethod.ExecuteProc("USP_InsertUpdateDelCategoryMaster", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Data"] = JsonConvert.SerializeObject(dt);
                }
            }
            catch (Exception ex)
            {

                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        public JsonResult EditRecord(string Id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action","Edit" },
                    {"@Id",Id},
                };
                DataTable dt = CommonMethod.ExecuteProc("USP_InsertUpdateDelCategoryMaster", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Record"] = JsonConvert.SerializeObject(dt);
                }
            }
            catch (Exception ex)
            {

                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        public JsonResult DeleteRecord(string Id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action","Delete" },
                    {"@Id",Id},
                };
                DataTable dt = CommonMethod.ExecuteProc("USP_InsertUpdateDelCategoryMaster", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Result"] = dt.Rows[0]["Message"].ToString();
                }
            }
            catch (Exception ex)
            {

                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        #endregion Category Master

        #region Sub Category Mater
        public ActionResult SubCategoryMaster()
        {
            if (Session["UserId"] == null)
            {
                return Redirect("~/Admin/Login");
            }
            else
            {
                return View();
            }
        }
        public JsonResult InsertUpdateSubCategoryMaster(int Id, string SubCategoryName,string Heading1,string Heading2,string Heading3,string Heading4,
            int Category_Id_FK ,string IsActive)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            string Action = "";
            if (Id != 0)
            {
                Action = "Update";
            }
            else
            {
                Action = "Insert";
            }
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action",Action },
                    {"@Id",Id.ToString().Trim() },
                    {"@SubCategoryName",SubCategoryName.Trim() },
                    {"@Heading1",Heading1.Trim() },
                    {"@Heading2",Heading2.Trim() },
                    {"@Heading3",Heading3.Trim() },
                    {"@Heading4",Heading4.Trim() },
                    {"@Category_Id_FK",Category_Id_FK.ToString() },
                    {"@IsActive",IsActive.Trim() },
                };
                DataTable dt = CommonMethod.ExecuteProc("USP_InsertUpdateDelSubCategoryMaster", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Result"] = dt.Rows[0]["Message"].ToString();
                }
            }
            catch (Exception ex)
            {

                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        public JsonResult ShowSubCategoryList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action","Select" },
                };
                DataTable dt = CommonMethod.ExecuteProc("USP_InsertUpdateDelSubCategoryMaster", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Data"] = JsonConvert.SerializeObject(dt);
                }
            }
            catch (Exception ex)
            {

                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        public JsonResult EditSubCategory(string Id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action","Edit" },
                    {"@Id",Id},
                };
                DataTable dt = CommonMethod.ExecuteProc("USP_InsertUpdateDelSubCategoryMaster", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Record"] = JsonConvert.SerializeObject(dt);
                }
            }
            catch (Exception ex)
            {

                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        public JsonResult DeleteSubCategory(string Id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action","Delete" },
                    {"@Id",Id},
                };
                DataTable dt = CommonMethod.ExecuteProc("USP_InsertUpdateDelSubCategoryMaster", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Result"] = dt.Rows[0]["Message"].ToString();
                }
            }
            catch (Exception ex)
            {

                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        #endregion Sub Category Master

        #region Plan Mater
        public ActionResult PlanMaster()
        {
            if (Session["UserId"] == null)
            {
                return Redirect("~/Admin/Login");
            }
            else
            {
                return View();
            }
        }
        public JsonResult InsertUpdatePlanMaster(int Id, string PlanName, string PriceYearly,string Price,string PriceType,string PlanType,string Ram, string vCPU, string SSD, string HDD, 
            string Memory,  string Bandwidth,   string Sub_Cat_Id_Fk, string DedicatedIP, string OSChoice, string Remark, string ServerLocation, 
            string NVMe, string Bonus, string Migration, string SSL, string Security, string Monitoring, string Prevention, string Service_Support,
            string Support, string Guarantee, string IsActive,string IsFreeSSL,string IsFirewallMonitory,string Isprevention,string Is27Support,
            string IsUpTimeGuarntee, string IsAPIIntegration, string IsFreeBonuses, string IsFreeMigration, string IsFullRootAccess, string IsFullStackDevelopment
            , string IsMalwareScans, string IsMultiplePHPVersion, string IsOSChoice, string IsPageSpeed, string IsPHPVulCheck, string IsProtectiveFirewall, string IsVul_Scanner,
            string IsWebsiteOptimization, string IsWorldwideDataCenters, string IsPowerfulControlPanel, string IsCSSJSOptimizers, string IsOneClickIns
)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            string Action = "";
            if (Id != 0)
            {
                Action = "Update";
            }
            else
            {
                Action = "Insert";
            }
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action",Action },
                    {"@Id",Id.ToString().Trim() },
                    {"@PlanName",PlanName.ToString() },
                    {"@Price",Price },
                    {"@PriceYearly",PriceYearly },
                    {"@PriceType",PriceType },
                    {"@PlanType",PlanType },
                    {"@Ram",Ram },
                    {"@vCPU",vCPU },
                    {"@SSD",SSD },
                    {"@HDD",HDD },
                    {"@Memory",Memory },
                    {"@Bandwidth",Bandwidth },
                    {"@Sub_Cat_Id_Fk",Sub_Cat_Id_Fk },
                    {"@IsActive",IsActive },
                    {"@DedicatedIP",DedicatedIP },
                    {"@OSChoice",OSChoice },
                    {"@Remark",Remark },
                    {"@ServerLocation",ServerLocation },
                    {"@NVMe",NVMe },
                    {"@Bonus",Bonus },
                    {"@Migration",Migration },
                    {"@SSL",SSL },
                    {"@Security",Security },
                    {"@Monitoring",Monitoring },
                    {"@Prevention",Prevention },
                    {"@Service_Support",Service_Support },
                    {"@Support",Support },
                    {"@Guarantee",Guarantee },
                    {"@IsFreeSSL", IsFreeSSL },
                    {"@IsFirewallMonitory", IsFirewallMonitory },
                    {"@Isprevention", Isprevention },
                    {"@Is27Support", Is27Support },
                    {"@IsUpTimeGuarntee", IsUpTimeGuarntee },
                    {"@IsAPIIntegration", IsAPIIntegration },
                    {"@IsFreeBonuses", IsFreeBonuses },
                    {"@IsFreeMigration", IsFreeMigration },
                    {"@IsFullRootAccess", IsFullRootAccess },
                    {"@IsFullStackDevelopment", IsFullStackDevelopment },
                    {"@IsMalwareScans", IsMalwareScans },
                    {"@IsMultiplePHPVersion", IsMultiplePHPVersion },
                    {"@IsOSChoice", IsOSChoice },
                    {"@IsPageSpeed", IsPageSpeed },
                    {"@IsPHPVulCheck", IsPHPVulCheck },
                    {"@IsProtectiveFirewall", IsProtectiveFirewall },
                    {"@IsVul_Scanner", IsVul_Scanner },
                    {"@IsWebsiteOptimization", IsWebsiteOptimization },
                    {"@IsWorldwideDataCenters", IsWorldwideDataCenters },
                    {"@IsPowerfulControlPanel", IsPowerfulControlPanel },
                    {"@IsCSSJSOptimizers", IsCSSJSOptimizers },
                    {"@IsOneClickIns", IsOneClickIns },
                };
                DataTable dt = CommonMethod.ExecuteProc("USP_InsertUpdateDelPlanMaster", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Result"] = dt.Rows[0]["Message"].ToString();
                }
            }
            catch (Exception ex)
            {
                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        public JsonResult ShowPlanList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action","Select" },
                };
                DataTable dt = CommonMethod.ExecuteProc("USP_InsertUpdateDelPlanMaster", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Data"] = JsonConvert.SerializeObject(dt);
                }
            }
            catch (Exception ex)
            {
                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        public JsonResult EditPlan(string Id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action","Edit" },
                    {"@Id",Id},
                };
                DataTable dt = CommonMethod.ExecuteProc("USP_InsertUpdateDelPlanMaster", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Record"] = JsonConvert.SerializeObject(dt);
                }
            }
            catch (Exception ex)
            {
                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        public JsonResult DeletePlan(string Id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action","Delete" },
                    {"@Id",Id},
                };
                DataTable dt = CommonMethod.ExecuteProc("USP_InsertUpdateDelPlanMaster", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Result"] = dt.Rows[0]["Message"].ToString();
                }
            }
            catch (Exception ex)
            {
                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        #endregion Plan Master

        #region Additional Item Mater
        public ActionResult AdditionalItem()
        {
            if (Session["UserId"] == null)
            {
                return Redirect("~/Admin/Login");
            }
            else
            {
                return View();
            }
        }
        public JsonResult InsertUpdateAdditionalItem(int Id, string ItemName,string ItemPrice, string IsActive)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            string Action = "";
            if (Id != 0)
            {
                Action = "Update";
            }
            else
            {
                Action = "Insert";
            }
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action",Action },
                    {"@Id",Id.ToString().Trim() },
                    {"@ItemName",ItemName.Trim() },
                    {"@ItemPrice",ItemPrice },
                    {"@IsActive",IsActive.Trim() },
                };
                DataTable dt = CommonMethod.ExecuteProc("USP_InsertUpdateDelAdditionItem", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Result"] = dt.Rows[0]["Message"].ToString();
                }
            }
            catch (Exception ex)
            {

                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        public JsonResult ShowAdditionalItem()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action","Select" },
                };
                DataTable dt = CommonMethod.ExecuteProc("USP_InsertUpdateDelAdditionItem", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Data"] = JsonConvert.SerializeObject(dt);
                }
            }
            catch (Exception ex)
            {

                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        public JsonResult EditAdditionalItem(string Id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action","Edit" },
                    {"@Id",Id},
                };
                DataTable dt = CommonMethod.ExecuteProc("USP_InsertUpdateDelAdditionItem", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Record"] = JsonConvert.SerializeObject(dt);
                }
            }
            catch (Exception ex)
            {

                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        public JsonResult DeleteAdditionalItem(string Id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action","Delete" },
                    {"@Id",Id},
                };
                DataTable dt = CommonMethod.ExecuteProc("USP_InsertUpdateDelAdditionItem", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Result"] = dt.Rows[0]["Message"].ToString();
                }
            }
            catch (Exception ex)
            {

                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        #endregion Category Master

        #region Discount Mater
        public ActionResult DiscountMaster()
        {
            if (Session["UserId"] == null)
            {
                return Redirect("~/Admin/Login");
            }
            else
            {
                return View();
            }
        }
        public JsonResult InsertUpdateDiscountMaster(int Id, string DiscountName,string TenureText,string PercentageAmount, string IsActive)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            string Action = "";
            if (Id != 0)
            {
                Action = "Update";
            }
            else
            {
                Action = "Insert";
            }
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action",Action },
                    {"@Id",Id.ToString().Trim() },
                    {"@PercentageAmount",PercentageAmount },
                    {"@DiscountName",DiscountName.Trim() },
                    {"@TenureText",TenureText.Trim() },
                    {"@IsActive",IsActive.Trim() },
                };
                DataTable dt = CommonMethod.ExecuteProc("USP_InsertUpdateDelDiscountMaster", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Result"] = dt.Rows[0]["Message"].ToString();
                }
            }
            catch (Exception ex)
            {

                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        public JsonResult ShowDiscountList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action","Select" },
                };
                DataTable dt = CommonMethod.ExecuteProc("USP_InsertUpdateDelDiscountMaster", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Data"] = JsonConvert.SerializeObject(dt);
                }
            }
            catch (Exception ex)
            {

                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        public JsonResult EditDiscount(string Id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action","Edit" },
                    {"@Id",Id},
                };
                DataTable dt = CommonMethod.ExecuteProc("USP_InsertUpdateDelDiscountMaster", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Record"] = JsonConvert.SerializeObject(dt);
                }
            }
            catch (Exception ex)
            {

                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        public JsonResult DeleteDiscount(string Id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action","Delete" },
                    {"@Id",Id},
                };
                DataTable dt = CommonMethod.ExecuteProc("USP_InsertUpdateDelDiscountMaster", Param);
                if (dt.Rows.Count > 0)
                {
                    dic["Result"] = dt.Rows[0]["Message"].ToString();
                }
            }
            catch (Exception ex)
            {

                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        #endregion Discount Master
    }
}