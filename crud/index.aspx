<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="crud.index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CRUD Application</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.3/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
</head>

<body>
    <form id="form1" runat="server">
        <div class="full-box">
            <div class="first">
                <div class="input_box">
                    <label for="namebox">Name</label>
                    <asp:TextBox ID="TextBox" runat="server" placeholder="Enter Username" ValidationGroup="LoginFrame" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextBox" ValidationGroup="LoginFrame" runat="server" ErrorMessage="please fill the name*" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="input_box">
                <label for="radiobtn">Gender</label> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Please select a gender *" ControlToValidate="radiobtn" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="LoginFrame" />
                <asp:RadioButtonList ID="radiobtn" runat="server" CssClass="inline-rb" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Male">Male</asp:ListItem>
                    <asp:ListItem Value="Female">Female</asp:ListItem>
                </asp:RadioButtonList>   
            </div>

            <div class="input_box">
                Date:
                <asp:TextBox ID="datepicker" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server"  ID="RequiredFieldValidator3" ErrorMessage="Please select the date*" ControlToValidate="datepicker" ForeColor="Red" ValidationGroup="LoginFrame"></asp:RequiredFieldValidator>
            </div>
             <div class="input_box">
     <asp:DropDownList ID="DropDownaList" runat="server">
         <asp:ListItem Value="">Please Select</asp:ListItem>
         <asp:ListItem Value="Kolkata">Kolkata</asp:ListItem>
         <asp:ListItem Value="Gurgaon">Gurgaon</asp:ListItem>
         <asp:ListItem Value="Chicago">Chicago</asp:ListItem>
         <asp:ListItem Value="Milan">Milan</asp:ListItem>
         <asp:ListItem Value="Nevada">Nevada</asp:ListItem>
     </asp:DropDownList>
      <asp:RequiredFieldValidator runat="server"  ID="RequiredFieldValidator6" ErrorMessage="Please select any one opations*" ControlToValidate="DropDownaList" ForeColor="Red" ValidationGroup="LoginFrame"></asp:RequiredFieldValidator>
 </div>
            
            <label>File Upload</label>
            <asp:FileUpload runat="server" ID="fileUpload" />
           <%-- <asp:RequiredFieldValidator runat="server"  ID="RequiredFieldValidator4" ErrorMessage="Please select the File*" ControlToValidate="fileUpload" ForeColor="Red" ValidationGroup="LoginFrame"></asp:RequiredFieldValidator>--%>
             <asp:Image ID="image" runat="server" Width="100" Height="100" Visible="false" />
            <asp:Label runat="server" ID="hidla" Visible="false"></asp:Label>
           

            <div class="input_box">
                <asp:TextBox ID="Message_Box" runat="server" TextMode="MultiLine" CssClass="Contact_Input"
                    MaxLength="1200" Rows="5" Columns="40" Wrap="true" />
                 <asp:RequiredFieldValidator runat="server"  ID="RequiredFieldValidator5" ErrorMessage="Please fill the area*" ControlToValidate="Message_Box" ForeColor="Red" ValidationGroup="LoginFrame"></asp:RequiredFieldValidator>
            </div>
            checkBox
            <div class="input_box">
                   <asp:CheckBoxList ID="CheckBox" runat="server">
                    <asp:ListItem Value="Right">Right</asp:ListItem> 
                    <asp:ListItem Value="Left">Left</asp:ListItem>
                </asp:CheckBoxList>

            </div>

            <div class="submit">
                <asp:Button ID="btnsubmit" runat="server" Text="submit" OnClick="btnSubmit_Click" ValidationGroup="LoginFrame" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
            </div>
        </div>
        <div class="two">
            <table class="table_full " border="1">
                <asp:HiddenField ID="hdfid" runat="server" />
                <asp:HiddenField ID="hidimg" runat="server" />
                <thead>
                    <tr>
                        <th width="0%">S.No</th>
                        <th width="0%">Name</th>
                        <th width="0%">Gender</th>
                        <th width="0%">Calendar date</th>
                        <th width="0%">Img</th>
                        <th width="0%">Dropdown</th>
                        <th width="0%">Content</th>
                        <th width="0%">Check box</th>
                        <th width="0%">Action</th>
                    </tr>
                </thead>
                <tbody class="grid-table">
                    <asp:Repeater ID="reptproductlist" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <p><%# Container.ItemIndex + 1 %></p>
                                </td>
                                <td>
                                    <p><%# Eval("Name") %></p>
                                </td>
                                <td>
                                    <p><%# Eval("Gender") %></p>
                                </td>
                                <td>
                                    <p><%# Eval("CalendarDate") %></p>
                                </td>
                                <td>
                                    <asp:Image ID="img" runat="server" ImageUrl='<%# Eval("img", "~/img/{0}") %>' Width="100" Height="100" />
                                    <asp:Image ID="image" runat="server" Visible="false" />
                                </td>
                                <td>
                                    <p><%# Eval("Dropdown") %></p>
                                </td>
                                <td>
                                    <p><%# Eval("Content") %></p>
                                </td>
                                <td>
                                    <p><%# Eval("Checkbox") %></p>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandArgument='<%# Eval("ID") %>' OnClick="lnkbtnEdit_Click" ToolTip="Edit" Text="Edit"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" CommandArgument='<%# Eval("ID") %>' OnClick="lnkbtnDelete_Click" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');" Text="Delete"></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <script src="https://code.jquery.com/jquery-3.7.1.js"></script>
        <script src="https://code.jquery.com/ui/1.13.3/jquery-ui.js"></script>
        <script>
            //$(function () {
            //    $("#datepicker").datepicker({
            //        changeMonth: true,
            //        changeYear: true
            //    });
            //});
            $(function () {
                $("#datepicker").datepicker(
                    {
                        dateFormat:  "dd/mm/yy"
                    });
                
              
                
            });
        </script>
    </form>

</body>
</html>
