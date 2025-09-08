<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroPrestamo.aspx.cs" Inherits="EvaluacionTecnica.Web.RegistroPrestamo" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Registro de Cliente y Préstamo</title>
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />

        <%-- Stores para ComboBox --%>
        <ext:Store ID="storeTipoDoc" runat="server" AutoLoad="false">
            <Model>
                <ext:Model ID="ModelTipoDoc" runat="server">
                    <Fields>
                        <ext:ModelField Name="Valor" />
                        <ext:ModelField Name="Nombre" />
                    </Fields>
                </ext:Model>
            </Model>
        </ext:Store>

        <ext:Store ID="storeTipoCliente" runat="server" AutoLoad="false">
            <Model>
                <ext:Model ID="ModelTipoCliente" runat="server">
                    <Fields>
                        <ext:ModelField Name="Valor" />
                        <ext:ModelField Name="Nombre" />
                    </Fields>
                </ext:Model>
            </Model>
        </ext:Store>

        <%-- Panel principal --%>
        <ext:Panel runat="server" Title="Registro de Cliente y Préstamo" Layout="VBoxLayout"
            Padding="15" Width="800px" Defaults="{ Anchor='100%' }">

            <Items>
                <%-- Contenedor HBox: Cliente + Préstamo --%>
                <ext:Container runat="server" Layout="HBoxLayout" Style="margin-bottom:10px;">
                    <Items>
                        <%-- Datos Cliente --%>
                        <ext:FieldSet runat="server" Title="Datos del Cliente" Collapsible="true" Flex="1" Style="margin-right:10px;">
                            <Items>
                                <ext:TextField runat="server" FieldLabel="Nombre" ID="txtNombre" AllowBlank="false" MaskRe="^[a-zA-Z\s]+$" EmptyText="Ingrese solo letras" />
                                <ext:ComboBox runat="server" FieldLabel="Tipo Documento" ID="cmbTipoDoc" StoreID="storeTipoDoc" DisplayField="Nombre" ValueField="Valor" Editable="false" />
                                <ext:TextField runat="server" FieldLabel="Número Documento" ID="txtNumDoc" AllowBlank="false" MaskRe="^\d+$" EmptyText="Ingrese solo números"/>
                                <ext:TextField runat="server" FieldLabel="Dirección" ID="txtDireccion" />
                                <ext:TextField runat="server" FieldLabel="Teléfono" ID="txtTelefono" MaskRe="^\d+$" EmptyText="Ingrese solo números"/>
                                <ext:TextField runat="server" FieldLabel="Email" ID="txtEmail" />
                                <ext:ComboBox runat="server" FieldLabel="Tipo Cliente" ID="cmbTipoCliente" StoreID="storeTipoCliente" DisplayField="Nombre" ValueField="Valor" Editable="false" />
                            </Items>
                        </ext:FieldSet>

                        <%-- Datos Préstamo --%>
                        <ext:FieldSet runat="server" Title="Datos del Préstamo" Collapsible="true" Flex="1">
                            <Items>
                                <ext:TextField runat="server" ID="txtMonto" FieldLabel="Monto" EmptyText="0.00" AllowBlank="false">
                                    <Listeners>
                                        <Render Fn="function(field){
                                            field.inputEl.on('keypress', function(e){
                                                var charCode = e.getCharCode();
                                                if((charCode < 48 || charCode > 57) && charCode != 46){
                                                    e.stopEvent();
                                                }
                                            });
                                            field.inputEl.on('blur', function(){
                                                var val = field.getValue();
                                                if(val !== ''){
                                                    val = parseFloat(val).toFixed(2);
                                                    field.setRawValue(val);
                                                }
                                            });
                                        }" />
                                    </Listeners>
                                </ext:TextField>

                                <ext:NumberField runat="server" FieldLabel="Plazo (meses)" ID="txtPlazo" AllowBlank="false" MinValue="1" />
                                <ext:NumberField runat="server" FieldLabel="Tasa de Interés (%)" ID="txtTasa" AllowBlank="false" MinValue="0" />
                            </Items>
                        </ext:FieldSet>
                    </Items>
                </ext:Container>

                <%-- Botón Registrar --%>
                <ext:Button 
                    runat="server"
                    ID="btnRegistrar"
                    Text="Registrar"
                    Icon="Add"
                    Width="150px"
                    MarginSpec="10 0 20 0"
                    Scale="Medium"
                    Style="font-weight:bold; font-size:14px; color:white; background-color:#28a745; border-color:#28a745;">
                    <Listeners>
                        <Click Handler="App.direct.RegistrarPrestamo();" />
                    </Listeners>
                </ext:Button>

                <%-- Grid Cronograma de Pagos --%>
                <ext:GridPanel runat="server" ID="gridCronograma" Title="Cronograma de Pagos" Height="300px" Width="780px">
                    <Store>
                        <ext:Store ID="storeCronograma" runat="server" AutoLoad="false">
                            <Model>
                                <ext:Model ID="ModelCronograma" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="NumeroCuota" />
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
                            <ext:Column runat="server" Text="Cuota" DataIndex="NumeroCuota" Width="100px" />
                            <ext:DateColumn runat="server" Text="Fecha Vencimiento" DataIndex="FechaVencimiento" Format="dd/MM/yyyy" Width="150px" />
                            <ext:NumberColumn runat="server" Text="Monto" DataIndex="MontoCuota" Format="0.00" Width="120px" />
                            <ext:Column runat="server" Text="Estado" DataIndex="Estado" Width="150px" />
                        </Columns>
                    </ColumnModel>
                </ext:GridPanel>

            </Items>
        </ext:Panel>
    </form>
</body>
</html>
