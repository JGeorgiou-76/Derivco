# Derivco
Jason Georgiou - Derivco Assessment 

First I would like to thank Derivco for the opportunity given to me.

Please find attached to this repository my relating code to the technical assessment.

For my solution to Step 1 - Stored Procedure:

Please find in the file named Derivco, a folder named scripts, this folder holds a script called Northwind Database Creation Script.
This script is used to create the Northwind database.

Step 1:
Open SSMS. (Microsoft SQL Server Management Studio).

Step 2:
Connect to the target SQL Server.

Step 3:
Open the script (Northwind Database Creation Script) in a new query window.

Step 4:
Run the script.
  
My stored procedure is called Derivco Stored Procedure.

Comment:
  My only comment during this first test, was that the TotalFreightCost, I didn't understand how to calculate the total freight cost from the 
  freight column in the orders table.
  
  
For my solution to Step 2 - Web API:
  
Please make a clone of this repository to either Visual Studio or Visula Studio Code.

Please confirm that Dotnet 6 Runtime is installed.

Build the API on Visual Studio, or from the terminal, first cd into the API folder and run with "dotnet watch run". 
If the Database is not seen, Run the Command "dotnet ef database update".

I have added a postman collection called Derivco.postman_collection. 
Please open Postman and import the collection to gain access to the API calls created.

Using the API calls with Postman:

Get All Bets:
This call will return a list of all the bets created.

Get All Bets for Roll ID:
  This call will return a list of all bets for a spacific Roll via the Roll ID, to change the Roll ID, you change the number at the end of the URL.

Get All Previous Rolls:
  This call returns all the previous rolls that have been made, also returned in Decending order so that we see the latest Roll first.

Get a Previous Roll via ID:
  This call return a Roll Result via the given ID, to change the Roll ID, you change the number at the end of the URL.

Place your Bet:
  This call allows a user to place a bet which accepts a BetNumber and a BetAmount, inputs for both are seen in the body of the API request, a there is a check 
  to see if the BetNumber is between 0 and 36.

Create New Spin:
  This call requires no input and generates a random spin/number between 0 and 36, and produces the output, at the same time that API call checks to see if there 
  are any winning numbers.

Get Payout:
  This call will return the winning Bets details and display the total winnings, it will also display a message if there are no winning bets available to Payout.

Comments:
  My only issue I faced with the technical assessment was implementing Unit Tests as I have never done them before and wasted too much time trying to learn while 
  doing the assessment, I thus have made the choice to not implement Unit Tests and I apologies for leaving them out.

This concludes my Derivco Assessment.

Thank you for this opportunity.

Jason Georgiou
