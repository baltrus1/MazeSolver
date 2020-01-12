using System;

class MazeSolver {
	public static void Main(string[] args) {
		if (args.Length != 1) {
			Console.WriteLine("Usage: \"MazeCalculator.exe <maze input file>\"");
			return;
		}

		var maze = Reader.readMaze(args[0]);
		if (maze.Equals(null)) {
			Console.WriteLine("Failed to read maze.");
			return;
		}
		
                Maze.printGrid(maze.grid, maze.length, maze.height);

                printHelp();

                while(true) {
                	Console.Write("(MazeSolver) ");
                	string line = Console.ReadLine();
                	string[] input = line.Split(' ');
                	if (input[0].Equals("q")) {
        			break;
                	}

                	if (input[0].Equals("s")) {
                		if (input.Length != 3) {
                			Console.WriteLine("specify x and y coordinates from top right. E.g. \"s 4 3\"");
                			continue;
                		}

                		maze.setStartPositions(Int32.Parse(input[1]), Int32.Parse(input[2]));
                	} else if (input[0].Equals("p")) {
                		Maze.printGrid(maze.grid, maze.length, maze.height);
                	} else if(input[0].Equals("e")) {
                		maze.findExit();
                	} else {
                		printHelp();
                	}
                }

	}

	private static void printHelp() {
		Console.WriteLine(@"Options:
    e - explore maze. Adds path to closest exist if such exists, prints trail into file.
    p - print current maze
    s <x> <y> - set up new start position
    q - quit
    h - print this message
");
	}
}