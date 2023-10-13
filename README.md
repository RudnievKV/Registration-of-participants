# Asp.Net-Framework-Test
Test application "Registration of conference participants" ASP.Net MVC (.NET framework 4.5.2) application "Registration of conference participants"

First, the interface has a form for registration of a participant or login of an already registered one.

Participant registration for the conference takes place according to the following fields:
Full name
age
Regional Center
email
phone
password All fields are mandatory except "phone". The "regional center" field is a drop-down list of already existing ones in Ukraine (5 for example). After clicking on the button, the data is sent and stored on the server, the storage must work autonomously, using a database server (SQL Server).
Log in to view the list of all registered conference participants by email + password. The table of participants is made by all fields (except password) with the possibility of sorting by columns - full name, age, regional center. Highlighting of the user himself in the table, who is viewing it, is also implemented.
