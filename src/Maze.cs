using System;
using System.Collections.Generic;

public enum CellType {
	Empty,
	Wall,
	Start,
	Visited
};

public enum Direction {
	Right, 
	Down, 
	Left, 
	Up
}

struct Point {
	public Point(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public static Point operator+(Point first, Point second) {
		return new Point(first.x + second.x, first.y + second.y);
	}

	public static Point operator-(Point first, Point second) {
		return new Point(first.x - second.x, first.y - second.y);
	}

	public int x;
	public int y;
}

class Cell {
	public CellType cellType;
	public Point parent;
	public int pathFromStart = -1;

	public Cell() { }

	public Cell(CellType type) {
        cellType = type;
    }
}

class Maze {
	public Cell[,] grid;
	public int height;
	public int length;

	private Point start;
	private  Point[] directions;

	public Maze(int height, int length) {
		grid = new Cell[height,length];
		for (int i = 0; i < height; ++i) {
			for (int j = 0; j < height; ++j) {
				grid[i,j] = new Cell();
			}
		}
		this.height = height;
		this.length = length;

		directions = new Point[] {new Point(0, 1), new Point(1, 0), new Point(0, -1), new Point(-1, 0)};
	}

	public void findExit() {
		var localGrid = new Cell[height,length];
		for (int i = 0; i < height; ++i) {
			for (int j = 0; j < height; ++j) {
				localGrid[i,j] = new Cell(grid[i,j].cellType);
			}
		}

		Queue<Point> pointsQueue = new Queue<Point>();
		pointsQueue.Enqueue(start);
		grid[start.y, start.x].pathFromStart = 0;

		bool found = false;
		var exitPoint = new Point();
		var exitDirection = new Direction();

		while (pointsQueue.Count != 0 && !found) {
			Point current = pointsQueue.Dequeue();

			foreach (var dir in directions) {
				Point next = current + dir;
				if (isOutOfBounds(next)) {
					found = true;
					exitPoint = current;
					exitDirection = getDirectionFromPoint(next - current);
					break;
				}

				if (localGrid[next.y,next.x].cellType != CellType.Empty) {
					continue;
				}

				localGrid[next.y,next.x].cellType = CellType.Visited;
				grid[next.y,next.x].parent = current;
				grid[next.y,next.x].pathFromStart = grid[current.y,current.x].pathFromStart + 1;

				pointsQueue.Enqueue(next);
			}
		}

		printPathInGrid(grid, exitPoint, exitDirection);
	}

	private Direction getDirectionFromPoint(Point p) {
		if (p.x == 0 && p.y == 1) {
			return Direction.Down;
		} else if (p.x == 0 && p.y == -1) {
			return Direction.Up;
		} else if (p.x == 1 && p.y == 0) {
			return Direction.Right;
		} else if (p.x == -1 && p.y == 0) {
			return Direction.Left;
		} else {
			Console.WriteLine("Can't get direction, point invalid.");
			return 0;
		}
	}

	private void printPathInGrid(Cell[,] localGrid, Point exit, Direction exitDirection) {
		var dirStack = new Stack<Direction>();
		dirStack.Push(exitDirection);

		var parent = new Point();
		parent = localGrid[exit.y,exit.x].parent;
		
		localGrid[exit.y,exit.x].cellType = CellType.Visited;

		while(parent.x != start.x || parent.y != start.y) {
			localGrid[parent.y,parent.x].cellType = CellType.Visited;
			dirStack.Push(getDirectionFromPoint(exit - parent));
			exit.x = parent.x;
			exit.y = parent.y;

			parent = localGrid[parent.y,parent.x].parent;
		}

		printGrid(localGrid, length, height);
	}

	private bool isOutOfBounds(Point p) {
		return p.x < 0 || p.y < 0 || p.x > length || p.y > height;
	}

	public void setStartPositions(int x, int y) {
		if (x < 0 || x >= length || y < 0 || y >= height) {
			Console.WriteLine("Invalid positions given.");
			return;
		}

		if (grid[y, x].cellType == CellType.Wall) {
			Console.WriteLine("Specified cell is a wall.");
			return;
		}

		grid[start.y, start.x].cellType = CellType.Empty;
		grid[y, x].cellType = CellType.Start;
		start.x = x;
		start.y = y;
	}

	public bool gridInit(int[,] cells) {
		if (cells.Length != height * length) {
			Console.WriteLine("Maze created with incorrect dimensions.");
			return false;
		}

		int row = 0;
		int col = 0;
		foreach (int cell in cells) {
			switch (cell) {
				case 0:
					grid[row,col].cellType = CellType.Empty;
					break;
				case 1:
					grid[row,col].cellType = CellType.Wall;
					break;
				case 2:
					grid[row,col].cellType = CellType.Start;	
					start = new Point(col, row);
					break;
			}

			++col;
			if (col == length) {
				col = 0;
				++row;
			}
		}
		return true;
	}

	public static void printGrid(Cell[,] grid, int length, int height) {
		for (int i = 0; i < height; ++i) {
            for (int j = 0; j < length; ++j) {
            	switch(grid[i, j].cellType) {
            		case CellType.Empty :
            			Console.Write(". ");
            			break;

            		case CellType.Wall:
            			Console.Write("X ");
            			break;

            		case CellType.Start:
            			Console.Write("S ");
            			break;
        			case CellType.Visited:
            			Console.Write("+ ");
            			break;
            	}
            }

            Console.WriteLine();
        }
        Console.WriteLine();
	}
}