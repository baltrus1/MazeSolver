using System;

public enum CellType {
	Empty,
	Wall,
	Start
};

class Cell {
	public CellType cellType;
	public int parentX;
	public int parentY;
	public int pathFromStart = -1;
}

class Maze {
	private Cell[,] grid;
	private int height;
	private int length;

	private int startX;
	private int startY;

	public Maze(int height, int length) {
		grid = new Cell[height,length];
		for (int i = 0; i < height; ++i) {
			for (int j = 0; j < height; ++j) {
				grid[i,j] = new Cell();
			}
		}
		this.height = height;
		this.length = length;
	}

	public void findExit() {

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

		grid[startY, startX].cellType = CellType.Empty;
		grid[y, x].cellType = CellType.Start;
		startX = x;
		startY = y;
	}

	public bool gridInit(int[,] cells) {
		if (cells.Length != height * length) {
			Console.WriteLine("Maze created with incorrect dimensions.");
			return false;
		}

		int row = 0;
		int col = 0;
		foreach (int cell in cells) {
			Console.WriteLine(grid[0,0].cellType);
			switch (cell) {
				case 0:
					grid[row,col].cellType = CellType.Empty;
					Console.WriteLine(row + " " + col + " setting as empty: " + grid[row,col].cellType);
					break;
				case 1:
					grid[row,col].cellType = CellType.Wall;
					Console.WriteLine(row + " " + col + " setting as wall: " + grid[row,col].cellType);
					break;
				case 2:
					grid[row,col].cellType = CellType.Start;
					Console.WriteLine(row + " " + col + " setting as start: " + grid[row,col].cellType);
					startX = col;
					startY = row;
					break;
			}

			++col;
			if (col == length) {
				col = 0;
				++row;
			}
		}
		Console.WriteLine(grid[0,0].cellType);
		return true;
	}

	public void gridPrint() {
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
            	}
            }

            Console.WriteLine();
        }
	}
}