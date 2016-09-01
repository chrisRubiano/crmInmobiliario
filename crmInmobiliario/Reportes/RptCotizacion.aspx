<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptCotizacion.aspx.cs" Inherits="crmInmobiliario.Reportes.RptCotizacion" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=8.2.14.1027, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <telerik:ReportViewer ID="ReportViewer1" runat="server" ProgressText="Generando cotización..." ViewMode="PrintPreview" Height="700px" Width="1024px"></telerik:ReportViewer>
    </form>
</body>
</html>
