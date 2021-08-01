Hopefully this intro readme file will be helpful along your Xamarin journey. Here we go...

# ADB0010 error thrown while compiling Android mobile apps in Visual Studio 2019
This error is caused by the "Enable Fast Deployment (debug mode only)" setting that may be selected by default.

SOLUTION
1. Right-click the Android project in Solution Explorer
2. Select "Android Options" from the left menu bar
3. If "Enable Fast Deployment (debug mode only)" is checked, de-select it
4. Re-try compiling your project

This error occured several times while I was working on a project. 
Referring to the Microsoft docs for an explanation was not very helpful. After much frustration, I discovered this solution online and thought I would share it.

No autographs, please. 
Just throw paper money :)

# General MOOVPAD FAQ's
Q. Why the repository?
A. This repository is all things Xamarin-related from the MOOVPAD project.

Q. What will be shared?
A. Code examples, as well as solutions to potential issues you might experience.

Q. What is the status of the MOOVPAD project?
A. The project includes several versions of the main app with some differences between them in terms of content and target users, and all of these are designed to be cross-platform. At this stage, the business-logic is progressing well, and work is continuing on the UI aspects too for each platform.

Stay awesome.

Ehsan Hamdy
