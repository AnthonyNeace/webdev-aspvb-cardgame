' program5.aspx.vb
' asp.net vb.net example
' CS316 fall 2010  
' Anthony Neace
' see assignment 5

' Prolog: This script handles calls made by the browser relating to the 'Pick a Card' web game.
'         These calls are made when:
'         * User begins a game by pressing the submit button.
'         * User begins a round by pressing the card image.
'         The user will win or lose a stake depending on their wager and the win multiplier.
'         Every time the stake changes (beginning of a game, end of a round) a cookie containing
'         the users name and stake will be set for 24 hours.

' imports are similar to C++ includes
' Not all these imports needed for this simple example
' Extra import statements do not hurt
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
' code behind is in a class like this
' the name program5 must match the name in the inherits
' attribute of the page directive at the top of the aspx file
Partial Class program5
    Inherits System.Web.UI.Page

    ' Page_Load executed whenever page is loaded by user
    Function Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Get the time that the game started.
        Dim currenttime As String ' current time kept here
        currenttime = DateTime.Now.ToString()
        pageloaded.Text = "Game started at " + currenttime

    End Function

    ' Userplay executed whenever user clicks a card.
    Function userplay(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim storedStake As Integer ' previous stake kept here
        Dim winnings As Integer ' winnings value kept here
        Dim filename As String ' Card filename kept here
        Dim randomCard As String ' Randomly generated card kept here
        Dim selectCard As String ' User selected card kept here
        Dim I As Integer  ' iterator for cookie search
        'Error Check on Wager -- checks for valid wager and wager < stake
        If wagerBox.Text = "" Then
            wagerError.Text = "Please enter a valid bet"
            Return 1
        ElseIf Not IsNumeric(wagerBox.Text) Then
            wagerError.Text = "Please enter a valid bet (Non-negative Integer)"
            Return 1
        ElseIf IsNumeric(wagerBox.Text) And CInt(wagerBox.Text) > CInt(currentStake.Text) Then
            wagerError.Text = "Bet is greater than current stake"
            Return 2
        ElseIf IsNumeric(wagerBox.Text) And CInt(wagerBox.Text) < 0 Then
            wagerError.Text = "Please enter a valid bet (Non-negative Integer)"
            Return 3
        End If

        'Card Error Checking
        If cardBox.Text.Equals("A") Or cardBox.Text.Equals("a") Or
           cardBox.Text.Equals("F") Or cardBox.Text.Equals("f") Or
           cardBox.Text.Equals("E") Or cardBox.Text.Equals("e") Or
           cardBox.Text.Equals("O") Or cardBox.Text.Equals("o") Then
            'All is Well
            cardError.Text = ""
            wagerError.Text = ""
            ' Assign Selected Card a string value for later comparison
            If cardBox.Text.Equals("a") Then
                selectCard = "A"
            ElseIf cardBox.Text.Equals("f") Then
                selectCard = "F"
            ElseIf cardBox.Text.Equals("e") Then
                selectCard = "E"
            ElseIf cardBox.Text.Equals("o") Then
                selectCard = "O"
            Else
                selectCard = cardBox.Text
            End If
        Else
            ' Card Error Checking Failed
            cardError.Text = "Please enter A, F, E or O"
            Return 1
        End If

        ' Random Number Generator
        ' Uses Random Object method 'Next' to generate in the [MinValue, MaxValue) range.
        Dim rndnumber As Random
        Dim randomvalue As Integer
        rndnumber = New Random
        randomvalue = rndnumber.Next(1, 53)
        filename = "~/Images/" + Convert.ToString(randomvalue) + ".png"
        ImageButton1.ImageUrl = filename

        'determine randomvalue card type
        randomCard = cardType(randomvalue)

        'win logic
        If String.Equals(selectCard, randomCard) Then
            If randomCard = "A" Then
                winnings = CInt(wagerBox.Text) * 12
                storedStake = CInt(currentStake.Text) + winnings
            ElseIf randomCard = "F" Then
                winnings = CInt(wagerBox.Text) * 4
                storedStake = CInt(currentStake.Text) + winnings
            ElseIf randomCard = "E" Then
                winnings = CInt(wagerBox.Text) * 2
                storedStake = CInt(currentStake.Text) + winnings
            ElseIf randomCard = "O" Then
                winnings = CInt(wagerBox.Text) * 3
                storedStake = CInt(currentStake.Text) + winnings
            End If
        Else
            winnings = -1 * CInt(wagerBox.Text)
            storedStake = CInt(currentStake.Text) + winnings
        End If

        'Update Win State and other text fields
        winState.Text = Convert.ToString(winnings)
        currentStake.Text = Convert.ToString(storedStake)

        'update Cookie
        setCookie(storedStake)

    End Function

    ' Called when cookie needs to be updated after each round
    ' Updates current stake value in cookie
    Function setCookie(ByVal newStake As Integer)
        ' Save current stake in a cookie named afer user
        Dim myCookie As New HttpCookie(username.Text)
        ' cookie value
        myCookie.Value = newStake
        ' cookie expires in 24 hours
        myCookie.Expires = DateTime.Now.AddHours(24)
        ' now add the cookie (name in myCookie: LastAccess)
        Response.Cookies.Add(myCookie)
    End Function

    ' Called when random card type needs to be identified
    ' Returns a string value for the card type
    Function cardType(ByVal randomvalue As Integer)
        If randomvalue >= 1 And randomvalue <= 4 Then
            Return "A"
        ElseIf randomvalue >= 5 And randomvalue <= 16 Then
            Return "F"
        Else
            If randomvalue >= 17 And randomvalue <= 20 Or
               randomvalue >= 25 And randomvalue <= 28 Or
               randomvalue >= 33 And randomvalue <= 36 Or
               randomvalue >= 41 And randomvalue <= 44 Or
               randomvalue >= 49 And randomvalue <= 52 Then
                Return "E"
            Else
                Return "O"
            End If
        End If
    End Function

    ' Called when a game is begin and user presses submit
    ' Handles input error checking for range,
    ' cookie setting,
    ' showing the game display field.
    Function nameEntered(ByVal sender As Object, ByVal e As System.EventArgs)
        'Stake Error Checking
        If CInt(stake.Text) > 9 And CInt(stake.Text) < 501 Then
            'All is Well
            stakeError.Text = ""
        Else
            stakeError.Text = "Stake must be between 10 and 500"
            Return 1
        End If
        Button1.Text = "Clicked"
        'Update cookie
        ' Page.IsPostBack is used to check if this is
        ' the first load of the page (not an Ajax update)
        Dim storedStake As Integer ' previous int kept here
        Dim cookiename As String ' current time kept here
        cookiename = username.Text
        storedStake = 5
        'If Not Page.IsPostBack Then
        ' first load - not Ajax update of page
        ' Cookie Check
        Dim I As Integer  ' iterator for cookie search
        Dim foundcookie As Boolean = False ' flag if cookie found
        ' Running on your local PC cookies kept
        ' under "localhost". Iterate thru all local cookies. 
        For I = 0 To Request.Cookies.Count - 1
            ' If not first execution within 24 hours
            ' the stake was put in a cookie named after the user
            If Request.Cookies.Item(I).Name = cookiename Then
                foundcookie = True
                storedStake = CInt(stake.Text) + CInt(Request.Cookies.Item(I).Value)
            End If
        Next
        If Not foundcookie Then
            storedStake = CInt(stake.Text)
            'pageloaded.Text = "First load of page at: " + currenttime
        End If
        ' Save current stake in a cookie named afer user
        Dim myCookie As New HttpCookie(cookiename)
        ' cookie value
        myCookie.Value = storedStake
        ' cookie expires in 24 hours
        myCookie.Expires = DateTime.Now.AddHours(24)
        ' now add the cookie (name in myCookie: LastAccess)
        Response.Cookies.Add(myCookie)
        'End If ' if not first load of page


        'Get Current Stake
        currentStake.Text = storedStake

        UpdatePanel1.Visible = True
    End Function
End Class