﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="rEvaluacion.aspx.cs" Inherits="RegistroEvaluacion.Registros.rEvaluacion" %>
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
                        <label for="EstudianteIdlabel" class="col-md-3 control-label input-sm" style="font-size: small">EstudianteId: </label>
                        <div class="col-md-2">
                            <asp:TextBox CssClass="form-control input-sm" TextMode="Number" ID="EvaluacionIdTextBox" Text="0" runat="server"></asp:TextBox>
                        </div>
                         <asp:RegularExpressionValidator ID="ValidaID" runat="server" ErrorMessage='Campo "ID" solo acepta numeros' ControlToValidate="EvaluacionIdTextBox" ValidationExpression="^[0-9]*" Text="*" ForeColor="Red" Display="Dynamic" ToolTip="Entrada no valida" ValidationGroup="Guardar"></asp:RegularExpressionValidator>
                        <asp:Button CssClass="col-md-1 btn btn-primary btn-sm" ID="BuscarButton" runat="server" Text="Buscar" OnClick="BuscarButton_Click" />
                        <%--Fecha--%>
                        <label for="fechaTextBox" class="col-md-1 control-label input-sm">Fecha: </label>
                        <div class="col-md-3">
                            <asp:TextBox CssClass="form-control" ID="fechaTextBox" TextMode="Date" runat="server"></asp:TextBox>
                        </div>
                    </div>

            
                                 <%--Estudiante--%>
                    <div class="form-group">
                        <label for="Estudiantelabel" class="col-md-3  control-label input-sm" style="font-size: small">Estudiante</label>
                        <div class="col-md-3">
                            <asp:DropDownList runat="server" ID="EstudianteDropDownList" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>

                    <%--Valores al grid--%>
                   
                    <div class="form-group">


                        <div class="col-md-3 col-md-offset-2">
                            <asp:Label ID="CategoriaDropDownListlabel" Text="Categoria" Style="font-size: small" runat="server" />
                            <asp:DropDownList runat="server" ID="CategoriaDropDownList" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RFVId" ValidationGroup="Buscar" ControlToValidate="CategoriaDropDownList" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>

                        <div class="col-md-2 ">
                            <asp:Label ID="ValorLabel" Text="Valor" runat="server" />
                            <asp:TextBox ID="ValorTextBox" class="form-control input-sm" TextMode="Number" runat="server" placeholder="0"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Buscar" ControlToValidate="VAlorTextBox" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>

                        <div class="col-md-2 ">
                            <asp:Label ID="LogradosLabel" Text="Logrado" runat="server" />
                            <asp:TextBox ID="LogradosTextBox" class="form-control input-sm" TextMode="Number" runat="server" placeholder="0"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Buscar" ControlToValidate="LogradosTextBox" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>

                        <div class="col-sm-1">
                             <asp:Button ID="AgregarButton" runat="server" Text="Agregar" class="btn btn-success" ValidationGroup="Agregar" OnClick="AgregarButton_Click"  />
                        </div>
                    </div>



         <div class="form-horizontal col-md-12" role="form">
                    
            <%--GRID--%>
                <asp:GridView ID="Grid" CssClass=" col-md-offset-2 col-md-offset-2" runat="server" AllowPaging="true" PageSize="10" ShowHeaderWhenEmpty="false" AutoGenerateDeleteButton="true" CellPadding="4" ForeColor="#333333" GridLines="None" Width="767px" AutoGenerateColumns="false" OnRowDeleting="Grid_RowDeleting" >                         
                    <Columns>
                        <asp:BoundField DataField="EvaluacionId" HeaderText="EvaluacionId" /><asp:BoundField />
                        <asp:BoundField DataField="CategoriaId" HeaderText="CategoriaId" /><asp:BoundField />
                        <asp:BoundField DataField="Valor" HeaderText="Valor" /><asp:BoundField />
                        <asp:BoundField DataField="Logrados" HeaderText="Logrados" /><asp:BoundField />
                        <asp:BoundField DataField="Perdidos" HeaderText="Peridos" /><asp:BoundField />

                       


                       
                    </Columns>     
                    <EmptyDataTemplate><div style="text-align:center">No hay datos en el Grid.</div></EmptyDataTemplate>
                         <AlternatingRowStyle BackColor="White" />

                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>

             

                    <%--Puntos Perdidos--%>
                    <div class="form-group">
                        <label for="PuntosPerdidosTextBox" class="col-md-8 control-label input-sm" style="font-size: small">Promedio</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="PuntosPeridosTextBox" ReadOnly ="true" runat="server" class="form-control input-sm" Style="font-size: small"></asp:TextBox>
                        </div>
                    
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
                     <asp:Button ID="GuardarButton" runat="server" Text="Guardar" class="btn btn-success" ValidationGroup="Guardar" OnClick="GuardarButton_Click"  />
                     <asp:Button ID="EliminarButton" runat="server" Text="Eliminar" class="btn btn-danger" OnClick="EliminarButton_Click"  />
                 </div>
             </div>
         </div>
    </div>
</asp:Content>
