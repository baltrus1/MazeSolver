﻿using System;
using System.IO;
using System.Text;

class Reader {
    private static void printError(string message) {
        Console.WriteLine("Incorrect input: " + message);
    }

    public static Maze readMaze(string path) {
        var file = new System.IO.StreamReader(path);
        string header = file.ReadLine();
        string[] dimentions = header.Split(' ');

        if (dimentions.Length != 2) {
            printError("Header length should be 2, was: " + header.Length);
            return null;
        }

        int height = Int32.Parse(dimentions[0]);
        int length = Int32.Parse(dimentions[1]);

        int[,] cells = new int[height, length];

        for (int i = 0; i < height; ++i) {
            String line = file.ReadLine();
            if (string.IsNullOrEmpty(line)) {
                printError("Height of the maze doesn't match the height specified in header");
            }
            
            String[] cellsInLine = line.Split(' ');
            if (cellsInLine.Length != length) {
                printError("Incorrect number of cells in line " + i);
                return null;
            }

            for(int j = 0; j < length; ++j) {
                cells[i, j] = Int32.Parse(cellsInLine[j]);
            }
        }

        var maze = new Maze(height, length);
        if (!maze.gridInit(cells)) {
            return null;
        }

        return maze;
    }
}