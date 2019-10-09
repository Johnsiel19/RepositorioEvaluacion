<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="rEstudiantes.aspx.cs" Inherits="RegistroEvaluacion.Registros.rEstudiantes" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container">
        <div class="panel panel-primary">
            <div class="panel-heading">Registro de Estudiantes</div>
            <div class="panel-body">
                <div class="form-horizontal col-md-12" role="form">
                    <%--SugerenciaID--%>
                    <div class="form-group">
                        <label for="EstudianteIdTextBox" class="col-md-3 control-label input-sm" style="font-size: small">EstudianteId: </label>
                        <div class="col-md-2">
                            <asp:TextBox CssClass="form-control input-sm" TextMode="Number" ID="EstudianteIdTextBox" Text="0" runat="server"></asp:TextBox>
                        </div>
                         <asp:RegularExpressionValidator ID="ValidaID" runat="server" ErrorMessage='Campo "ID" solo acepta numeros' ControlToValidate="EstudianteIdTextBox" ValidationExpression="^[0-9]*" Text="*" ForeColor="Red" Display="Dynamic" ToolTip="Entrada no valida" ValidationGroup="Guardar"></asp:RegularExpressionValidator>
                        <asp:Button CssClass="col-md-1 btn btn-primary btn-sm" ID="BuscarButton" runat="server" Text="Buscar" OnClick="BuscarButton_Click1" />
                        <%--Fecha--%>
                        <label for="fechaTextBox" class="col-md-1 control-label input-sm">Fecha: </label>
                        <div class="col-md-2">
                            <asp:TextBox CssClass="form-control" ID="fechaTextBox" TextMode="Date" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <%--Descripcion--%>
            

                    <div class="form-group">
                        <label for="NombresTextBox" class="col-md-3 control-label input-sm" style="font-size: small">Descripcion</label>
                        <div class="col-md-5">
                            <asp:TextBox ID="NombresTextBox" runat="server" class="form-control input-sm" Style="font-size: small"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="Valida" runat="server" ErrorMessage="El campo &quot;Nombres&quot; esta vacio" ControlToValidate="NombresTextBox" ForeColor="Red" Display="Dynamic" ToolTip="Campo Descripcion es obligatorio" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                    </div>


                    <%--Puntos Perdidos--%>
                    <div class="form-group">
                        <label for="PuntosPerdidosTextBox" class="col-md-3 control-label input-sm" style="font-size: small">Promedio</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="PuntosPeridosTextBox" ReadOnly ="true" runat="server" class="form-control input-sm" Style="font-size: small"></asp:TextBox>
                        </div>
                    
                    </div>


                </div>
            </div>

        </div>

         <%--botones--%>
         <div class="panel">
             <div class="text-center">
                 <div class="form-group">
                     <asp:Button ID="NuevoButton" runat="server" Text="Nuevo" class="btn btn-primary" OnClick="NuevoButton_Click" />
                     <asp:Button ID="GuardarButton" runat="server" Text="Guardar" class="btn btn-success" ValidationGroup="Guardar" OnClick="GuardarButton_Click1"  />
                     <asp:Button ID="EliminarButton" runat="server" Text="Eliminar" class="btn btn-danger" OnClick="EliminarButton_Click1"  />
                 </div>
             </div>
         </div>
    </div>

</asp:Content>
