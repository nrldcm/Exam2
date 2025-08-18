For Traxion Tech - Technical Exam EXAM #2 - Windows Services

Instructions (Installation)
1. Navigate to Microsoft .NET Framework 
> C:\Windows\Microsoft.NET\Framework\v4.0.30319
use: cd * using cmd prompt/terminal
2. Build the Project
- Copy the Directory and File Name where Exe file is located.
Ex. C:\Project\Exam2\Bin\Debug\Exam2.exe
3. Install the Service using InstallUtil.exe
- Use this command to install the Service.
> InstallUtil.exe C:\Project\Exam2\Bin\Debug\Exam2.exe 
- Press Enter

-- Simplified: Update

1. Open CMD (Administrator)
2. Run this command 

Commands for
sc create Exam2 binPath= "..\Exam2\Bin\Debug\Exam2.exe" start= auto -- Creating Service
sc start Exam2 -- Starting Service
sc stop Exam2 -- Stopping Service
sc delete Exam2 -- Deleting Service

Monitoring Logs
1. Open Windows Event Viewer
- On the Left side of the Event Viewer Panel Click and Expand "Windows Logs"
- Select Application
2. Filtering The Logs
- On the Right side of the Event Viewer Panel (action), Click Filter Current Logs
- There will be a dialog form named "Filter Current Log" appear
3. Selecting Filter
- Select all the Event Level: Critical, Warning, Verbose, Error and Information
- Choose/Select Event Source: *Type or Search* Exam2
- Then Press Okay

Additional Note: You can also create a custom filter with the same process mentioned above, but clicked the "Create Custom View" instead of "Filter Current Log".






