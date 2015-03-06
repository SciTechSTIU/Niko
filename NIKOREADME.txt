Basic Instructions to get the app up and running:

1.	Download the project as zip and extract / clone to your PC using GitHub client.

2.	Open the solution in Visual Studio (any version over 2013 should work, I use VS Community 2013)

3.	In the solution explorer navigate to Views -> Home -> Index.cshtml.This is the application's index page.

4.	Run the application (CTRL + F5 to start without debugging). Visual Studio should now start downloading all the required NuGet packages for the project.

5.	The app should now be running smoothly in your browser. Go to Student to create a new student or edit an existing student and update their enrollments. Go to Course to view a list of courses, sort by students who haven't taken the course and details about a selected course.

Note: The apps local database will be re-created every time the application is run (for development purposes), this means any change made (create, update) will be reset.

Niko Kallio and Maaya Matsumoto
