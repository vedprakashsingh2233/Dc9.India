﻿using Dc9.India.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Dc9.India.Controllers
{
    public class VpshostingController : Controller
    {
        // GET: Vpshosting
        public ActionResult ManagedServer()
        {
            return View();
        }
        public ActionResult SelfManagedServer()
        {
            return View();
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult checkout()
        {
            //if (Session["UserId"] == null)
            //{
            //    return Redirect("~/UserLogin/Login");
            //}
            //else
            //{
            //    return View();
            //}
            return View();
        }
        public ActionResult LinuxDedicatedSelfManagedSP()
        {
            return View();
        }
        public ActionResult WindowDedicatedSelfManagedSP()
        {
            return View();
        }
        public ActionResult LinuxVPSSelfManagedSP()
        {
            return View();
        }
        public ActionResult WindowVPSSelfManagedSP()
        {
            return View();
        }
        public JsonResult BindPlanDetail(string Id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Result"] = "";
            dic["Plan"] = "";
            try
            {
                string[,] Param = new string[,]
                {
                    {"@Action","Plan" },
                    {"@Id",Id},
                };
                DataTable dt = CommonMethod.ExecuteProc("getPlanDetailBySubCatId", Param);
                if (dt.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("<table>");
                    sb.AppendLine("<thead>");
                    sb.AppendLine("<tr>");
                    sb.AppendLine("<th>Plans</th>");
                    sb.AppendLine("<th>Price</th>");
                    sb.AppendLine("<th>NVMe DISK Space</th>");
                    sb.AppendLine("<th>RAM</th>");
                    sb.AppendLine("<th>CPU</th>");
                    sb.AppendLine("<th>Dedicated IP's</th>");
                    sb.AppendLine("<th>Buy Plan</th>");
                    sb.AppendLine("<th>Detail</th>");
                    sb.AppendLine("</tr>");
                    sb.AppendLine("</thead>");
                    sb.AppendLine("<tbody>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.AppendLine("<tr>");
                        sb.AppendLine("<td>" + dt.Rows[i]["PlanName"] + "</td>");
                        sb.AppendLine("<td>" + dt.Rows[i]["Price"] + "/Month</td>");
                        //if (Convert.ToDecimal(dt.Rows[i]["Price"])>0)
                        //{
                        //sb.AppendLine("<td>" + dt.Rows[i]["Price"] + "/Month</td>");

                        //}
                        //else
                        //{
                        //    sb.AppendLine("<td>" + dt.Rows[i]["PriceYearly"] + "</td>");
                        //}
                        sb.AppendLine("<td>" + dt.Rows[i]["NVMe"] + "</td>");
                        sb.AppendLine("<td>" + dt.Rows[i]["Ram"] + "</td>");
                        sb.AppendLine("<td>" + dt.Rows[i]["vCPU"] + "</td>");
                        sb.AppendLine("<td>" + dt.Rows[i]["DedicatedIP"] + "</td>");
                        sb.AppendLine("<td><span class=\"price-linux\"></span> <button onclick=\"BuyNow('" + dt.Rows[i]["Id"] + "')\" class=\"btn btn-transparent-black btn-small text-extra-small\">Buy Now</button></td>");
                        sb.AppendLine("<td>");
                        sb.AppendLine("<a data-toggle=\"collapse\" href=\"#accordion-one-link" + dt.Rows[i]["Id"] + "\" aria-expanded=\"false\">");
                        sb.AppendLine("<div class=\"panel-title font-weight-500 text-uppercase position-relative padding-20px-right\">");
                        sb.AppendLine("<span>");
                        sb.AppendLine("<i class=\"ti-plus\" style=\"font-weight: 900;color: red;\"></i>");
                        sb.AppendLine("</span>");
                        sb.AppendLine("</div>");
                        sb.AppendLine("</a>");
                        sb.AppendLine("</td>");
                        sb.AppendLine("</tr>");
                        sb.AppendLine("<tr id=\"accordion-one-link" + dt.Rows[i]["Id"] + "\" class=\"panel-collapse collapse\">");
                        sb.AppendLine("<td colspan=\"8\">");
                        sb.AppendLine("<div class=\"panel-body bg-light-gray\">");
                        sb.AppendLine("<ul class=\"list-style-11 bg-light-gray\" style=\"display: flex; flex-wrap: wrap; list-style: none; padding: 0; margin-left: 100px;text-align: left;\">");
                        //if (Convert.ToDecimal(dt.Rows[i]["Price"]) > 0)
                        //{
                        //    sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\">" + dt.Rows[i]["Price"] + "</li>");
                        //}
                        //else
                        //{
                        //    sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\">" + dt.Rows[i]["PriceYearly"] + "</li>");
                        //}
                        if (dt.Rows[i]["Remark"].ToString()!="")
                        {
                        sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["Remark"] + "</li>");
                        }
                        if (dt.Rows[i]["ServerLocation"].ToString() != "")
                        { 
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">Server Location : " + dt.Rows[i]["ServerLocation"] + "</li>");
                        }
                        //if (dt.Rows[i]["vCPU"].ToString() != "")
                        //{ 
                        //    sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\">" + dt.Rows[i]["vCPU"] + "</li>");
                        //}
                        //if (dt.Rows[i]["Ram"].ToString() != "")
                        //{ 
                        //    sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"> " + dt.Rows[i]["Ram"] + "</li>");
                        //}
                        //if (dt.Rows[i]["NVMe"].ToString() != "")
                        //{
                        //sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\">" + dt.Rows[i]["NVMe"] + "</li>");
                        //}
                        if (dt.Rows[i]["PlanType"].ToString() != "")
                        {
                        sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">Server : " + dt.Rows[i]["PlanType"] + "</li>");
                        }
                        if (dt.Rows[i]["SSD"].ToString() != "")
                        {
                        sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["SSD"] + "</li>");
                        }
                        if (dt.Rows[i]["HDD"].ToString() != "")
                        {
                        sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["HDD"] + "</li>");
                        }
                        if (dt.Rows[i]["Memory"].ToString() != "")
                        {
                        sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["Memory"] + "</li>");
                        }
                        if (dt.Rows[i]["Bandwidth"].ToString() != "")
                        {
                        sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">Bandwidth : " + dt.Rows[i]["Bandwidth"] + "</li>");
                        }
                        //if (dt.Rows[i]["DedicatedIP"].ToString() != "")
                        //{
                        //sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\">" + dt.Rows[i]["DedicatedIP"] + "</li>");
                        //}
                        if (dt.Rows[i]["OSChoice"].ToString() != "")
                        {
                        sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["OSChoice"] + "</li>");
                        }
                        if (dt.Rows[i]["Bonus"].ToString() != "")
                        {
                        sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["Bonus"] + "</li>");
                        }
                        if (dt.Rows[i]["Migration"].ToString() != "")
                        {
                        sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["Migration"] + "</li>");
                        }
                        if (dt.Rows[i]["SSL"].ToString() != "")
                        {
                        sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["SSL"] + "</li>");
                        }
                        if (dt.Rows[i]["Security"].ToString() != "")
                        {
                        sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["Security"] + "</li>");
                        }
                        if (dt.Rows[i]["Monitoring"].ToString() != "")
                        {
                        sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["Monitoring"] + "</li>");
                        }
                        if (dt.Rows[i]["Service_Support"].ToString() != "")
                        {
                        sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["Service_Support"] + "</li>");
                        }
                        if (dt.Rows[i]["Support"].ToString() != "")
                        {
                        sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["Support"] + "</li>");
                        }
                        if (dt.Rows[i]["Guarantee"].ToString() != "")
                        {
                        sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["Guarantee"] + "</li>");
                        }
                        if (dt.Rows[i]["Isprevention"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["Isprevention"] + "</li>");
                        }
                        if (dt.Rows[i]["IsFreeSSL"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["IsFreeSSL"] + "</li>");
                        }
                        if (dt.Rows[i]["IsFirewallMonitory"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\">" + dt.Rows[i]["IsFirewallMonitory"] + "</li>");
                        }
                        if (dt.Rows[i]["Is27Support"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["Is27Support"] + "</li>");
                        }
                        if (dt.Rows[i]["IsUpTimeGuarntee"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["IsUpTimeGuarntee"] + "</li>");
                        }
                        if (dt.Rows[i]["IsFreeMigration"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["IsFreeMigration"] + "</li>");
                        }
                        if (dt.Rows[i]["IsFreeBonuses"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["IsFreeBonuses"] + "</li>");
                        }
                        if (dt.Rows[i]["IsOSChoice"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\">" + dt.Rows[i]["IsOSChoice"] + "</li>");
                        }
                        if (dt.Rows[i]["IsPowerfulControlPanel"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["IsPowerfulControlPanel"] + "</li>");
                        }
                        if (dt.Rows[i]["IsFullRootAccess"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["IsFullRootAccess"] + "</li>");
                        }
                        if (dt.Rows[i]["IsOneClickIns"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["IsOneClickIns"] + "</li>");
                        }
                        if (dt.Rows[i]["IsCSSJSOptimizers"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["IsCSSJSOptimizers"] + "</li>");
                        }
                        if (dt.Rows[i]["IsWorldwideDataCenters"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["IsWorldwideDataCenters"] + "</li>");
                        }
                        if (dt.Rows[i]["IsAPIIntegration"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["IsAPIIntegration"] + "</li>");
                        }
                        if (dt.Rows[i]["IsVul_Scanner"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["IsVul_Scanner"] + "</li>");
                        }
                        if (dt.Rows[i]["IsFullStackDevelopment"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["IsFullStackDevelopment"] + "</li>");
                        }
                        if (dt.Rows[i]["IsMultiplePHPVersion"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["IsMultiplePHPVersion"] + "</li>");
                        }
                        if (dt.Rows[i]["IsPageSpeed"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["IsPageSpeed"] + "</li>");
                        }
                        if (dt.Rows[i]["IsWebsiteOptimization"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["IsWebsiteOptimization"] + "</li>");
                        }
                        if (dt.Rows[i]["IsPHPVulCheck"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["IsPHPVulCheck"] + "</li>");
                        }
                        if (dt.Rows[i]["IsProtectiveFirewall"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["IsProtectiveFirewall"] + "</li>");
                        } 
                        if (dt.Rows[i]["IsMalwareScans"].ToString() != "")
                        {
                            sb.AppendLine("<li style=\"flex: 1 0 33%; margin-bottom: 10px;\"class=\"col-sm-4\">" + dt.Rows[i]["IsMalwareScans"] + "</li>");
                        }
                        sb.AppendLine("</ul>");
                        sb.AppendLine("</div>");
                        sb.AppendLine("</td>");
                        sb.AppendLine("</tr>");
                    }
                    sb.AppendLine("</tbody>");
                    sb.AppendLine("</table>");                   
                    dic["Plan"] = sb.ToString();
                }
                DataTable dtDetail = GetPlanContent(Id);
                if (dtDetail.Rows.Count > 0)
                {
                    dic["SubCategoryName"] = dtDetail.Rows[0]["SubCategoryName"].ToString();
                    dic["Heading1"] = dtDetail.Rows[0]["Heading1"].ToString();
                    dic["Heading2"] = dtDetail.Rows[0]["Heading2"].ToString();
                    dic["Heading3"] = dtDetail.Rows[0]["Heading3"].ToString();
                    dic["Heading4"] = dtDetail.Rows[0]["Heading4"].ToString();
                }
            }
            catch (Exception ex)
            {
                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }

        public DataTable GetPlanContent(string Id)
        {
            DataTable dt = new DataTable();
            try
            {
                string[,] Param = new string[,]
                   {
                    {"@Action","Plandetail" },
                    {"@Id",Id},
                   };
                 dt = CommonMethod.ExecuteProc("getPlanDetailBySubCatId", Param);
            }
            catch (Exception ec)
            {

            }
            return dt;
        }

        public JsonResult BindAdditionaltem()
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
                    StringBuilder htmlString = new StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {                    
                        htmlString.AppendLine("<div class=\"col-sm-6 item\">");
                        htmlString.AppendLine("<label class=\"col-sm-8\">" + dt.Rows[i]["ItemName"] + "</label>");
                        htmlString.AppendLine("<input type=\"checkbox\" class=\"form-check col-sm-3 chkAddtional\" value=\""+ dt.Rows[i]["ItemName"] + "\" />");
                        htmlString.AppendLine("<input type=\"hidden\" class=\"chkAddtionalID\" value=\""+ dt.Rows[i]["Id"] + "\" />");
                        htmlString.AppendLine("<span class=\"text-extra-dark-gray alt-font font-weight-600 mb-0\">₹<span class=\"AddtionalPrice\">" + dt.Rows[i]["ItemPrice"] + "</span>/month</span>");
                        htmlString.AppendLine("</div>");
                    }
                    dic["Data"] = htmlString.ToString();
                }
            }
            catch (Exception ex)
            {

                dic["Result"] = ex.Message;
            }
            return Json(dic);
        }
        public ActionResult CustomServer()
        {
            return View();
        }
    }
}