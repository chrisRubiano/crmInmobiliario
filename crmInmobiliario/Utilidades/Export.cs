using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace crmInmobiliario.Utilidades
{
    public class Export
    {
        public void ToExcel(HttpResponseBase Response, object datasource, string fileName)
        {
            var grid = new System.Web.UI.WebControls.GridView();
            grid.DataSource = datasource;
            grid.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename="+fileName+".xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

    }
}