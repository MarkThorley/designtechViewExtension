# desingtech View Extensions
(https://github.com/MarkThorley/designtechViewExtension) is the WIP source code for the designtech view extensions for DynamoBIM.



## TESTED:
Currently tested against Dynamo 1.3


## CONTRIBUTION:
Feel free to fork and submit pull requests


## DYNAMO 1.3 INSTALL INSTRUCTIONS:
Build Dynamo from Source in Visual Studio from Dynamo_1.3 branch
Copy src/bin/release/designtech_ViewExtensionDefinition.xml to C:\Program Files\Dynamo\Dynamo Core\1.3\viewExtensions
Copy src/bin/release/designtech.dll to C:\Program Files\Dynamo\Dynamo Core\1.3
Access the View Extension from the drop down menu designtech in Dynamo


## VISUAL STUDIO POST-BUILD EVENTS:
copy "$(ProjectDir)bin\Release\*.xml" "C:\Users\markt\Documents\GitHub\Dynamo\bin\AnyCPU\Debug\viewExtensions"

copy "$(ProjectDir)bin\Release\*.dll" "C:\Users\markt\Documents\GitHub\Dynamo\bin\AnyCPU\Debug\"


## NOTES:
All code is WIP, please ensure you save work and create backups
