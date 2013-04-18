<%@ Page Language="VB" AutoEventWireup="false" CodeFile="program5.aspx.vb" Inherits="program5" Debug="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Pick a card by Anthony Neace</title>
  
</head>
<body style="background-color:#B1B1B1;">

    <form runat="server">

    <div style="width:80%;
          padding:5px 25px 5px 25px;
          border:1px solid black;
          margin-left:auto;
          margin-right:auto;  
          background-color:#FFB1B2;"> 
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/pickacard.png" 
            style="display: block; margin-left: auto; margin-right: auto;" />
    </div>
    <br /><br />
    <div style="width:80%;
          padding:5px 25px 5px 25px;
          border:1px solid black;
          margin-left:auto;
          margin-right:auto;  
          background-color:#B1B1FF;">  
    <p><asp:Label ID="pageloaded" runat="server" Text=""></asp:Label> </p>

        Name:
        <asp:TextBox ID="username" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
            ControlToValidate="username" runat="server" ErrorMessage="name required" 
            style="color: #FF0000"></asp:RequiredFieldValidator>
        <br />
        Stake:
        <asp:TextBox ID="stake" runat="server"></asp:TextBox>
        <asp:Label  ForeColor="Red" ID="stakeError" runat="server" Text=""></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
            ControlToValidate="stake" runat="server" 
            ErrorMessage="Stake must be between 10 and 500" style="color: #FF0000"></asp:RequiredFieldValidator>
        <br />
        <asp:Button ID="Button1" onClick="nameEntered" runat="server" Text="Submit" />
        
        </div>

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

         <br />
        <br />

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="false">


        <ContentTemplate>
        <div style="width:80%;
          padding:5px 25px 5px 25px;
          border:1px solid black;
          margin-left:auto;
          margin-right:auto;  
          background-color:#B1FFB2;"> 
            Current Stake:
            <asp:Label ID="currentStake" runat="server" Text="0"></asp:Label>

            <br />Wager
            <asp:TextBox ID="wagerBox" runat="server" AutoPostBack=true></asp:TextBox>
            <asp:Label  ForeColor="Red" ID="wagerError" runat="server" Text=""></asp:Label>

            <br />Select card (A for ace F for face card E for even card 2-10 O for odd card 
            3-9)
            <asp:TextBox ID="cardBox" runat="server" AutoPostBack=true></asp:TextBox>
            <asp:Label  ForeColor="Red" ID="cardError" runat="server" Text=""></asp:Label>
            <br /> Won/lost:
            <asp:Label ID="winState" runat="server" Text="?"></asp:Label>
            <br />
            <div style="width:10%;margin-left:auto; margin-right:auto;">  
            <asp:ImageButton  ImageUrl="~/Images/b1fv.png" OnClick=userplay 
                ID="ImageButton1" runat="server" />
            </div>

        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
  
    </form>
</body>
</html>