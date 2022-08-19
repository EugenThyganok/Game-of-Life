using System;
using System.Threading;

namespace game_of_life
{
	internal enum State { Dead, Alive };

	internal class Board
	{
		private readonly int RowsCount = 15;
		private readonly int ColsCount = 15;

		public Board(int rowsCount, int colsCount) 
		{
			RowsCount = rowsCount;
			ColsCount = colsCount;
		}

		public State[][] NextGeneration(State[][] currentBoard)
		{
			State[][] newBoard = new State[RowsCount][];
			for (int row = 0; row < RowsCount; row++)
			{
				newBoard[row] = new State[ColsCount];
				for (int col = 0; col < ColsCount; col++)
				{
					int livingNeighboursCount = GetLivingNeighbours(currentBoard, row, col);
					State currentCellState = GetCellState(currentBoard, row, col, livingNeighboursCount);
					newBoard[row][col] = currentCellState;
				}
			}

			return newBoard;
		}

		private int GetLivingNeighbours(State[][] grid, int rows, int cols)
		{
			int livingCell = 0;
			for (int i = 0; i < 3; i++)
			{
				int rowCoordStart = GetCellIndex(rows + i - 1, RowsCount);
				for (int j = 0; j < 3; j++)
				{
					int colCorodStart = GetCellIndex(cols + j - 1, ColsCount);

					if (rows == rowCoordStart && cols == colCorodStart) { continue; }

					if (grid[rowCoordStart][colCorodStart] == State.Alive) { livingCell++; }

				}
			}

			return livingCell;
		}

		private State GetCellState(State[][] grid, int rows, int cols, int livingCell)
		{
			State currentCellState = grid[rows][cols];
			if (currentCellState == State.Alive)
			{
				return (livingCell == 2 || livingCell == 3) ? State.Alive : State.Dead;
			}

			return livingCell == 3 ? State.Alive : State.Dead;
		}

		private int GetCellIndex(int row, int max)
		{
			if (row < 0)
			{
				return (row + max) % max;
			}
			else if (row > max - 1)
			{
				return row % max;
			}

			return row;
		}
	}

	internal class Program
	{
		static void Main(string[] args)
		{
			Console.CursorVisible = false;

			Board board = new Board(15, 15);
			var grid = GetGrid();
			while (true)
			{
				DrawBoard(grid);
				grid = board.NextGeneration(grid);

				Thread.Sleep(500);
			}
		}

		private static void DrawBoard(State[][] rows)
		{
			foreach (var row in rows)
			{
				foreach (var col in row)
				{
					switch (col)
					{
						case State.Alive:
							Console.Write("#");
							break;
						case State.Dead:
							Console.Write(".");
							break;
					}
				}

				Console.WriteLine();
			}

			Console.SetCursorPosition(0, 0);
		}

		private static State[][] GetGrid()
		{
			return new State[][]
			{
				new State[] {State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead },

				new State[] {State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead },

				new State[] {State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead },

				new State[] {State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead },

				new State[] {State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead },

				new State[] {State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead },

				new State[] {State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Alive, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead },

				new State[] {State.Dead, State.Dead, State.Dead, State.Dead, State.Alive, State.Alive, State.Alive, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead },

				new State[] {State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead },

				new State[] {State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead },

				new State[] {State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead },

				new State[] {State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead },

				new State[] {State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead },

				new State[] {State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead },

				new State[] {State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead, State.Dead }
			};
		}
	}
}