# designtech View Extensions
https://github.com/MarkThorley/designtechViewExtension is the WIP source code for the designtech view extensions for DynamoBIM.





## TESTED:
Currently tested against Dynamo 2.0.1

Please Note: Not all new features will not be added to Dynamo 1.3.1 branch




## CONTRIBUTION:
Feel free to fork and submit pull requests




## DYNAMO 1.3 INSTALL INSTRUCTIONS:
Build Dynamo from Source in Visual Studio from Dynamo_1.3 branch

Build the designtech View Extension from Source in Visual Studio from 1.3 branch

Copy src/bin/release/designtech_ViewExtensionDefinition.xml to C:\Program Files\Dynamo\Dynamo Core\1.3\viewExtensions

Copy src/bin/release/designtech.dll to C:\Program Files\Dynamo\Dynamo Core\1.3

Access the View Extension from the drop down menu designtech in Dynamo




## DYNAMO 2.0 INSTALL INSTRUCTIONS:
Build Dynamo from Source in Visual Studio from Dynamo_2.0 branch

Build the designtech View Extension from Source in Visual Studio from 2.0_release branch

Copy src/bin/release/designtech_ViewExtensionDefinition.xml to C:\Program Files\Dynamo\Dynamo Core\2\viewExtensions

Copy src/bin/release/designtech.dll to C:\Program Files\Dynamo\Dynamo Core\2

Access the View Extension from the drop down menu designtech in Dynamo




## CURRENT FEATURES:
- Node / Connector Count
  Counts all the nodes and connectors live in the model

- Toggle Freeze
  Allows the user to freeze and unfreeze selected/multiple nodes

- Group Navigation
  Navigate round the graph easily by Group names
  
- Error Nodes
  Highlights all the nodes in the graph with an error associated and allows an easy way to navigate to them
  
- Favourite Nodes
  Setup user defined custom nodes that can be placed on the canvas easily




## VISUAL STUDIO POST-BUILD EVENTS:
copy "$(ProjectDir)bin\Release\*.xml" "C:\Users\markt\Documents\GitHub\Dynamo\bin\AnyCPU\Debug\viewExtensions"

copy "$(ProjectDir)bin\Release\*.dll" "C:\Users\markt\Documents\GitHub\Dynamo\bin\AnyCPU\Debug\"




## NOTES:
All code is WIP, please ensure you save work and create backups
