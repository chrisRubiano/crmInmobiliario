using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Reporting;

namespace crmInmobiliario.Reportes
{
    public partial class RptReciboPago : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var idcotizacion = Request.QueryString["IdAmortizacion"];
                if (idcotizacion != null)
                {
                    CRMReportes.ReciboPago rpt = new CRMReportes.ReciboPago();
                    rpt.dsPago.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
//                    rpt.dsAmortizaciones.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                    rpt.ReportParameters["IdAmortizacion"].Value = idcotizacion;
                    rpt.ReportParameters["IdAmortizacion"].Visible = false;

                    InstanceReportSource irs = new InstanceReportSource();
                    irs.ReportDocument = rpt;


                    ReportViewer1.ReportSource = irs;
                }

            }
        }
    }
}