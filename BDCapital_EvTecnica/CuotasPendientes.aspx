<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CuotasPendientes.aspx.cs" Inherits="EvaluacionTecnica.Web.CuotasPendientes" %>
<%@ Register Assembly="CrystalDecisions.Web" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager runat="server" ID="rm1" EnableDirectEvents="true" />
        <ext:Panel runat="server" Title="Cuotas Pendientes/Vencidas" Width="800" Padding="10">

            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:DateField 
                            runat="server" 
                            FieldLabel="Fecha Referencia" 
                            ID="dtFechaRef" 
                            AutoDataBind="true"  
                            Value="<%# DateTime.Today %>"/>

                        <ext:ToolbarFill />

                        <ext:Button 
                            ID="btnImprimir" 
                            runat="server" 
                            Text="Imprimir" 
                            Icon="Printer">
                            <Listeners>
                                <Click Handler="window.open('/ReportePrestamos.aspx', '_blank');" />
                            </Listeners>
                        </ext:Button>



                    </Items>
                </ext:Toolbar>
            </TopBar>

            <Items>
                <ext:GridPanel runat="server" ID="gridCronograma" Title="Listado de Prestamos" Height="300" Scrollable="Both"
                        ResponsiveConfig='{"width<768": {"Width":"100%"}, "width>=768": {"Width":780}}'>
                    <Store>
                        <ext:Store ID="storePrestamos" runat="server" AutoLoad="true">
                            <Model>
                                <ext:Model runat="server" ID="ModelPrestamos">
                                    <Fields>
                                        <ext:ModelField Name="PrestamoId" Type="Int" />
                                        <ext:ModelField Name="Cliente" Type="String" />
                                        <ext:ModelField Name="Monto" Type="Float" />
                                        <ext:ModelField Name="PlazoMeses" Type="Int" />
                                        <ext:ModelField Name="TasaInteres" Type="Float" />
                                        <ext:ModelField Name="FechaPrestamo" Type="Date" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>

                    <SelectionModel>
                        <ext:RowSelectionModel Mode="Single" />
                    </SelectionModel>

                    <ColumnModel>
                        <Columns>
                            <ext:Column runat="server" Text="N°" DataIndex="PrestamoId" Width="60" Align="Center" />
                            <ext:Column runat="server" Text="Cliente" DataIndex="Cliente" Width="150" />
                            <ext:NumberColumn runat="server" Text="Monto" DataIndex="Monto" Format="0.00" Width="100" Align="Center"/>
                            <ext:Column runat="server" Text="Plazo (meses)" DataIndex="PlazoMeses" Width="120" Align="Center"/>
                            <ext:NumberColumn runat="server" Text="Tasa (%)" DataIndex="TasaInteres" Format="0.00" Width="100" Align="Center"/>
                            <ext:DateColumn runat="server" Text="Fecha Préstamo" DataIndex="FechaPrestamo" Format="dd/MM/yyyy" Width="140" Align="Center"/>
                        </Columns>
                    </ColumnModel>
                    <Listeners>
                        <SelectionChange Handler="
                            if (selected.length > 0) {
                                var prestamoId = selected[0].data.PrestamoId;
                                Ext.defer(function () {
                                    App.direct.BuscarCuotas(prestamoId);
                                }, 10);
                            }
                        " />
                    </Listeners>
                </ext:GridPanel>


                <ext:GridPanel runat="server" ID="gridCuotas" Title="Cuotas Pendientes/Vencidas" Height="300" Width="780">
                    <Store>
                        <ext:Store ID="storeCuotas" runat="server" AutoLoad="false">
                            <Model>
                                <ext:Model ID="ModelCuota" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="PagoId" Type="Int" />
                                        <ext:ModelField Name="NombreCompleto" />
                                        <ext:ModelField Name="PrestamoId" Type="Int" />
                                        <ext:ModelField Name="NumeroCuota" Type="Int" />
                                        <ext:ModelField Name="FechaVencimiento" Type="Date" />
                                        <ext:ModelField Name="MontoCuota" Type="Float" />
                                        <ext:ModelField Name="Estado" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel>
                        <Columns>
                            <ext:Column runat="server" Text="Cliente" DataIndex="NombreCompleto" Width="150" />
                            <ext:Column runat="server" Text="Préstamo" DataIndex="PrestamoId" Width="80" />
                            <ext:Column runat="server" Text="Cuota" DataIndex="NumeroCuota" Width="80" />
                            <ext:DateColumn runat="server" Text="Vencimiento" DataIndex="FechaVencimiento" Format="dd/MM/yyyy" Width="120" />
                            <ext:NumberColumn runat="server" Text="Monto" DataIndex="MontoCuota" Format="0.00" Width="100" />

                            <ext:Column runat="server" Text="Estado" DataIndex="Estado" Width="120">
                                <Renderer Handler="
                                    if(value === 'Pendiente'){ 
                                        return '<span style=\'color:orange;font-weight:bold;\'>⚠️ ' + value + '</span>'; 
                                    }
                                    if(value === 'Pagado'){ 
                                        return '<span style=\'color:green;font-weight:bold;\'>✅ ' + value + '</span>'; 
                                    }
                                    if(value === 'Vencido'){ 
                                        return '<span style=\'color:red;font-weight:bold;\'>❌ ' + value + '</span>'; 
                                    }
                                    return value;
                                " />
                            </ext:Column>

                            <ext:CommandColumn runat="server" Text="Acción" Width="100">
                                <Commands>
                                    <ext:GridCommand CommandName="Pagar" Text="Pagar" Icon="Money" />
                                </Commands>
                                <Listeners>
                                    <Command Handler="App.direct.PagarCuota(record.data.PagoId, record.data.NumeroCuota, record.data.PrestamoId);" />
                                </Listeners>
                            </ext:CommandColumn>

                        </Columns>
                    </ColumnModel>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
    </form>
</body>
</html>
