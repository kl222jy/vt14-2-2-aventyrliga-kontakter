<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="aventyrliga_kontakter._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="SuccessPanel" runat="server" Visible="False">
        <div class="alert alert-success successbox">
            <a class="close" data-dismiss="alert">x</a>
            <asp:Literal ID="SuccessLiteral" runat="server" />
        </div>
    </asp:Panel>
    <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowModelStateErrors="true" CssClass="validation-summary-errors alert alert-danger" />
    <asp:ValidationSummary ID="InsertSummary" ShowModelStateErrors="false" runat="server" ValidationGroup="insert" CssClass="validation-summary-errors alert alert-danger" />
    <asp:ListView EnableModelValidation="true" ID="ContactListView" runat="server" ItemType="aventyrliga_kontakter.Model.Contact"
        SelectMethod="ContactListView_GetData"
        InsertMethod="ContactListView_InsertItem"
        UpdateMethod="ContactListView_UpdateItem"
        DeleteMethod="ContactListView_DeleteItem"
        DataKeyNames="ContactId"
        InsertItemPosition="FirstItem">
        <LayoutTemplate>
            <table class="table table-hover table-responsive">
                <tr>
                    <th>Förnamn</th>
                    <th>Efternamn</th>
                    <th>E-post</th>
                    <th>Redigering</th>
                </tr>
                <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td><%#: Item.FirstName %></td>
                <td><%#: Item.LastName %></td>
                <td><%#: Item.EmailAdress %></td>
                <td>
                    <asp:LinkButton runat="server" CommandName="Edit" Text="Redigera" CausesValidation="false" />
                    <asp:LinkButton runat="server" CommandName="Delete" Text="Ta bort" CausesValidation="false" class="deleteButton" OnClientClick="return confirm('Är du säker på att du vill ta bort kontakten?');" />
                </td>
            </tr>
        </ItemTemplate>
        <InsertItemTemplate>
            <tr>
                <td>
                    <%--<asp:TextBox ID="FirstName" runat="server" Text="<%# BindItem.FirstName %>" />--%>
                    <asp:DynamicControl ID="FirstName" runat="server" DataField="FirstName" Mode="Insert" ValidationGroup="insert" />
                </td>
                <td>
                    <%--<asp:TextBox ID="LastName" runat="server" Text="<%# BindItem.LastName %>" />--%>
                    <asp:DynamicControl ID="LastName" runat="server" DataField="LastName" Mode="Insert" ValidationGroup="insert" />
                </td>
                <td>
                    <%--<asp:TextBox ID="TextBox1" runat="server" Text="<%# BindItem.EmailAdress %>" />--%>
                    <asp:DynamicControl ID="EmailAdress" runat="server" DataField="EmailAdress" Mode="Insert" ValidationGroup="insert" />
                </td>
                <td>
                    <asp:LinkButton runat="server" CommandName="Insert" Text="Lägg till" ValidationGroup="insert" />
                    <asp:LinkButton runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                </td>
            </tr>
        </InsertItemTemplate>
        <EditItemTemplate>
            <tr>
                <td>
                    <%--<asp:TextBox ID="FirstName" runat="server" Text="<%# BindItem.FirstName %>" />--%>
                    <asp:DynamicControl ID="FirstName" runat="server" DataField="FirstName" Mode="Edit" />
                </td>
                <td>
                    <%--<asp:TextBox ID="LastName" runat="server" Text="<%# BindItem.LastName %>" />--%>
                    <asp:DynamicControl ID="LastName" runat="server" DataField="LastName" Mode="Edit" />
                </td>
                <td>
                    <%--<asp:TextBox ID="EmailAdress" runat="server" Text="<%# BindItem.EmailAdress %>" />--%>
                    <asp:DynamicControl ID="EmailAdress" runat="server" DataField="EmailAdress" Mode="Edit" />
                </td>
                <td>
                    <asp:LinkButton runat="server" CommandName="Update" Text="Spara" />
                    <asp:LinkButton runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                </td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            Kunduppgifter saknas.
        </EmptyDataTemplate>
    </asp:ListView>
    <asp:DataPager ID="ContactPager" runat="server" PagedControlID="ContactListview" PageSize="20">
        <Fields>
            <%--<asp:NumericPagerField ButtonCount="10" NextPageText="--->" PreviousPageText="<---" />--%>
            <asp:NextPreviousPagerField FirstPageText="<<--Första" PreviousPageText="<-Föregående" NextPageText="Nästa->" LastPageText="Sista-->>" ShowFirstPageButton="true" ShowLastPageButton="true" RenderDisabledButtonsAsLabels="True" />
        </Fields>
    </asp:DataPager>
</asp:Content>
