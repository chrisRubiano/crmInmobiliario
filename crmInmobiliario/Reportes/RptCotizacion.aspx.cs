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
    public partial class RptCotizacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var idcotizacion = Request.QueryString["IdCotizacion"];
                if (idcotizacion != null)
                {
                    CRMReportes.Cotizacion rpt = new CRMReportes.Cotizacion();
                    rpt.dsCotizacion.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    rpt.dsAmortizaciones.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                    rpt.ReportParameters["IdCotizacion"].Value = idcotizacion;
                    rpt.ReportParameters["IdCotizacion"].Visible = false;

                    InstanceReportSource irs = new InstanceReportSource();
                    irs.ReportDocument = rpt;


                    ReportViewer1.ReportSource = irs;
                }

            }

            

            
        }
    }
}