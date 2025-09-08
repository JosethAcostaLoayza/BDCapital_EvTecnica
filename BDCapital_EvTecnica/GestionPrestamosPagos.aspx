<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestionPrestamosPagos.aspx.cs" Inherits="EvaluacionTecnica.Web.Default" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Evaluación Técnica - Sistema Financiero</title>
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />

        <ext:Viewport runat="server" Layout="BorderLayout">
            <Items>
                <ext:TabPanel 
                    ID="MainTabs" 
                    runat="server" 
                    Region="Center" 
                    DeferredRender="false"
                    ActiveTabIndex="0">

                    <Items>
                        <ext:Panel 
                            runat="server" 
                            Title="Registro de Préstamos"
                            Closable="false"
                            Layout="FitLayout">
                            <Loader 
                                Url="RegistroPrestamo.aspx"
                                Mode="Frame"
                                LoadMask="true" />
                        </ext:Panel>

                        <ext:Panel 
                            runat="server" 
                            Title="Cuotas Pendientes"
                            Closable="false"
                            Layout="FitLayout">
                            <Loader 
                                Url="CuotasPendientes.aspx"
                                Mode="Frame"
                                LoadMask="true" />
                        </ext:Panel>
                    </Items>
                </ext:TabPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
