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
