using System;

class Maze {
	enum CellType {
		Empty,
		Wall,
		Start
	};

	private CellType[,] grid;
	private int height;
	private int length;

	public Maze(int height, int length) {
		grid = new CellType[height,length];
		this.height = height;
		this.length = length;
	}

	public void gridInit(int[,] cells) {
		if (cells.Length != height * length) {
			Console.WriteLine("Maze created with incorrect dimensions.");
			return;
		}

		int row = 0;
		int col = 0;
		foreach (int cell in cells) {
			switch (cell) {
				case 0:
					grid[row,col] = CellType.Empty;
					break;
				case 1:
					grid[row,col] = CellType.Wall;
					break;
				case 2:
					grid[row,col] = CellType.Start;
					// Do something else. Add start to visited nodes queue etc.
					break;
			}

			++col;
			if (col == length) {
				col = 0;
				++row;
			}
		}
	}

	public void gridPrint() {
		for (int i = 0; i < height; ++i) {
            for (int j = 0; j < length; ++j) {
            	switch(grid[i, j]) {
            		case CellType.Empty :
            			Console.Write("E ");
            			break;

            		case CellType.Wall:
            			Console.Write("W ");
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