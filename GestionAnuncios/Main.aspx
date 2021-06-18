<%@Page Title="" Language="C#" Async="true"  MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="GestionAnuncios.Main"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContentplaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LateralContentplaceholder" runat="server">
     <p style="text-align:center" >
        <H5>MI BUSCADOR DE ANUNCIOS EMPRESARIAL ENTRE TRABAJADORES</H5>
    </p>
    <br />
    <table border="0" width="100%" cellpadding="0" cellspacing="0" >
        <tr>
            <td>
                Anuncio
                <br />
                <asp:TextBox runat="server" ID="txtAnuncio" OnTextChanged="txtAnuncio_TextChanged" AutoPostBack="True"></asp:TextBox>                
             </td>        
        </tr>        
                <tr>
            <td>
                <br />
            </td>
        </tr>

        <tr>
            <td>
                Tipo
                <br />
                <asp:DropDownList runat="server" ID="ddlTipo" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Text="Seleccione" Value="-1"></asp:ListItem>
                </asp:DropDownList>                
             </td>        
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                Precio
                <br />
                <asp:TextBox runat="server" ID="txtPrecio" type="number" min="0" max="999999999999999999" Width="150px" OnTextChanged="txtPrecio_TextChanged" AutoPostBack="True"></asp:TextBox>                
             </td>        
        </tr>
        <tr>
            <td>
                <br /><br /><br />
            </td>
        </tr>
        <tr>
            <td style="text-align:center">
                <asp:Button ID="btnBuscar" runat="server" Text="BUSCAR" OnClick="btnBuscar_Click" CausesValidation="false"/>
            </td>
        </tr>
    </table>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <table border="0" width="100%" cellpadding="0" cellspacing="0"  >
        <tr>
            <td>  
                ---                
             </td>        
            <td style="text-align:right">
                <asp:Image runat="server" ID="btnEnviarEmail"  ImageUrl="~/Images/imgEmail.png" Width="40px" Height="30px"/>
            </td>
        </tr>        
        <tr>
            <td colspan="2" >
                 

 <asp:ListView ID="lvAnuncios" runat="server" DataKeyNames="Titulo,Precio"   GroupItemCount="4" OnPagePropertiesChanging="lvAnuncios_PagePropertiesChanging">
        <ItemTemplate>
            <td style="text-align:center"  width:100%;">
                <asp:Image runat="server" ImageUrl ='<%# "data:image/jpg;base64,"  + Eval("Imagen") %>' width="200px" hight="200px" />
                <br />
                <asp:Label runat="server" Text='<%# Eval("Titulo") %>' />
                <br />
                <asp:Label runat="server" Text='<%# "$ " + Eval("Precio") + " MN" %>' />
            </td>
        </ItemTemplate>
        <GroupTemplate>
            <tr>
                <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
            </tr>
        </GroupTemplate>
        <EmptyItemTemplate>    
            <td>
                &nbsp;
            </td>
        </EmptyItemTemplate>
        <LayoutTemplate>
            <table border="1" width="100%" cellpadding="0" cellspacing="0" >
                <asp:PlaceHolder runat="server" ID="groupPlaceHolder" />
            </table>
        </LayoutTemplate>        
    </asp:ListView>
    <br /><br />
     <asp:DataPager ID="dtPaginacion" runat="server" PageSize="10" PagedControlID="lvAnuncios">
        <Fields>
            <asp:NextPreviousPagerField />
            <asp:NumericPagerField />
            <asp:TemplatePagerField>

               <PagerTemplate>
                        Página
                        <asp:Label runat="server" ID="labelCurrentPage" Text="<%# Container.TotalRowCount > 0 ? (Container.StartRowIndex / Container.PageSize) + 1 : 0 %>" />
                        de
                        <asp:Label runat="server" ID="labelTotalPages" Text="<%#  Math.Ceiling ((double)Container.TotalRowCount / Container.PageSize) %>" />
                    </PagerTemplate>
             </asp:TemplatePagerField>
        </Fields>
    </asp:DataPager>
                       
            </td>
        </tr>
        </table>


     

            
            <asp:Panel ID="pnlCorreo" runat="server" Style="display: none" CssClass="modalPopup">
                <asp:Panel ID="Panel3" runat="server" BorderWidth="0" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black">
                <div>
                    
                    <table border="0" width="100%" cellpadding="0" cellspacing="0" style="padding:50px; width:400px; border:darkgray 1px solid; background-color:whitesmoke; text-align:left; " >
                        <tr>
                            <td>
                                 <p style="text-align:left" >
                                        Contáctanos y dejanos tus comentarios
                                     
                            </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr />
                            </td>
                        </tr>
        <tr>
            <td>
                Nombre
                <br />
                <asp:TextBox runat="server" ID="txtNombre" MaxLength="50"></asp:TextBox>                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre" SetFocusOnError="true" ValidationGroup="vgMensaje" >*</asp:RequiredFieldValidator>                            
             </td>        
        </tr>        
                <tr>
            <td>
                <br />
            </td>
        </tr>

        <tr>
            <td>
                Correo
                <br />
                <asp:TextBox runat="server" ID="txtCorreo"></asp:TextBox>              
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCorreo" SetFocusOnError="true" ValidationGroup="vgMensaje" >*</asp:RequiredFieldValidator>                            
               <asp:RegularExpressionValidator ID="revValidarEmail" runat="server" ControlToValidate="txtCorreo" SetFocusOnError="true" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" ValidationGroup="vgMensaje">Correo Erróneo</asp:RegularExpressionValidator>
             </td>        
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                Mensaje
                <br />
                <asp:TextBox runat="server" ID="txtMensaje" TextMode="MultiLine" MaxLength="100"></asp:TextBox>                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMensaje" SetFocusOnError="true" ValidationGroup="vgMensaje" >*</asp:RequiredFieldValidator>                            
             </td>        
        </tr>
        <tr>
            <td>
            <br />
            </td>
        </tr>        
    </table>

                </div>
                </asp:Panel>
                <p style="text-align: center;">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="false" />                    
                    <asp:Button ID="btnOk" runat="server" Text="Enviar" CausesValidation="true"  ValidationGroup="vgMensaje"  OnClick="btnOk_Click"  OnClientClick = "if(Page_ClientValidate('vgMensaje')) CloseDialog();"  />                                                 
                </p>
            </asp:Panel>            
        
            <div>
                <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
                    TargetControlID="btnEnviarEmail"
                    PopupControlID="pnlCorreo"                
                    OnOkScript="onOk()" 
                    CancelControlID="btnCancel"
                    DropShadow="true"
                    BackgroundCssClass="modalBackground"
                    >
                </ajax:ModalPopupExtender>
            
            </div>

</asp:Content>

