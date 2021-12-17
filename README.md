# PathFinder
An A* pathfinder assignment made with C# and WPF which reads a text file and shows the best path in the UI

This is a Visual Studio 2022 C# WPF project. 

The folder PenttiMaze/executable contains the executable PenttiMaze.exe and the text files given with the assignment.
When the executable is run, it looks for a maze file in the same folder with the name 'maze.txt'.
If another file should be used, it should be renamed to 'maze.txt' and put into the same folder.

The cost of each movement in the finding the path is 1 (since no diagonal movements were possible)

NOTE! If the project is run in Visual studio, it fails to find the 'maze.txt'. Put the desired 'maze.txt' in to the folder:
PathFinder\PenttiMaze\bin\Debug\net6.0-windows or similar since that is where the build is and the executable looks for the file locally.
