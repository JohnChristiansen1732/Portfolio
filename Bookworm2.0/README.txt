This is more of a proof of concept to find a more efficient solution to find loan folders on my company's production server.

The user types in the deal number and loan number and that loan folder would pop up on their screen.

The original solution, Bookworm, created a listing of every folder and file and then search for the loan number in that listing.  For some of the larger deals with over 1 million files, this process may take up to 10 minutes.

This solution, Bookworm 2.0, leverages the standard folder structure of dealNumber/caseNumber/loanNumber to get a list of each caseNumber for a given dealNumber (typically a handful at most) and see if the loanNumber folder is in that caseNumber folder.  Even for the largest deal, this process only takes a few seconds.

Posted with permission from company.