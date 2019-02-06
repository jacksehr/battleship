using System;

namespace battleship
{
    public class StateManager
    {
        private CellType[,] board;
        private bool gameStarted;

        /// <summary>
        /// Manages the state of a single player's board in battleship
        /// </summary>
        public StateManager()
        {
            board = null;
            gameStarted = false;
        }

        /// <summary>
        /// Initialises the game board and sets each cell to blank
        /// </summary>
        public void InitBoard()
        {
            AssertGameNotStarted();
            board = new CellType[Consts.BoardSize, Consts.BoardSize];
            for (int row = 0; row < Consts.BoardSize; row++)
            {
                fillBoardInOneDirection(row, 0, row, Consts.BoardEnd, CellType.Blank);
            }
        }
       
        /// <summary>
        /// Needs to be called before any attacks can be received
        /// </summary>
        public void StartGame()
        {
            AssertBoardInit();
            AssertGameNotStarted();
            gameStarted = true;
        }

        /// <summary>
        /// Provides a way to add a ship by giving coordinates of its two extremities
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="startCol"></param>
        /// <param name="endRow"></param>
        /// <param name="endCol"></param>
        /// <returns>Whether or not the ship could be added</returns>
        public bool AddShip(int startRow, int startCol, int endRow, int endCol)
        {
            AssertBoardInit();
            AssertValidCoordinates(startRow, startCol, endRow, endCol);
            for (int row = startRow; row < endRow + 1; row++)
            {
                for (int col = startCol; col < endCol + 1; col++)
                {
                    if (board[col, row] != CellType.Blank)
                    {
                        return false;
                    }
                }
            }

            fillBoardInOneDirection(startRow, startCol, endRow, endCol, CellType.Ship);

            return true;
        }

        /// <summary>
        /// Provides a way to process an incoming attack from an opponent
        /// </summary>
        /// <param name="attackRow">Row of cell being attacked</param>
        /// <param name="attackCol">Column of cell being attacked</param>
        /// <returns>Whether or not the attack hit</returns>
        public bool ReceiveAttack(int attackRow, int attackCol)
        {
            AssertGameStarted();
            AssertValidCoordinates(attackRow, attackCol);
            CellType attackedCell = board[attackCol, attackRow];
            if (attackedCell == CellType.Ship)
            {
                board[attackCol, attackRow] = CellType.Hit;
                return true;
            }
            if (attackedCell != CellType.Hit)
            {
                board[attackCol, attackRow] = CellType.Miss;
            }

            return false;
        }

        /// <summary>
        /// Provides a way to determine if this player has lost the game
        /// </summary>
        /// <returns>Whether or not the player has lost</returns>
        public bool hasLost()
        {
            AssertGameStarted();
            for (int row = 0; row < Consts.BoardSize; row++)
            {
                for (int col = 0; col < Consts.BoardSize; col++)
                {
                    if (board[col, row] == CellType.Ship)
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        /// <summary>
        /// Fills all cells within the extremities given, inclusive
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="startCol"></param>
        /// <param name="endRow"></param>
        /// <param name="endCol"></param>
        /// <param name="filling">That which will go in the cells being filled</param>
        private void fillBoardInOneDirection(int startRow, int startCol, int endRow, int endCol, CellType filling)
        {
            AssertBoardInit();
            Direction direction = startCol == endCol ? Direction.Vertical : Direction.Horizontal;
            if (direction == Direction.Vertical)
            {
                for (int row = startRow; row < endRow + 1; row++)
                {
                    board[startCol, row] = filling;
                }
            }
            else
            {
                for (int col = startCol; col < endCol + 1; col++)
                {
                    board[col, startRow] = filling;
                }
            }
        }


        /// <summary>
        /// Helper to print out the current game board
        /// </summary>
        public void PrintBoard()
        {
            AssertBoardInit();
            Console.Write('\n');
            CellMarker toWrite;
            for (int row = 0; row < Consts.BoardSize; row++)
            {
                for (int col = 0; col < Consts.BoardSize; col++)
                {
                    switch (board[col, row])
                    {
                        case CellType.Ship:
                            toWrite = CellMarker.Ship;
                            break;
                        case CellType.Hit:
                            toWrite = CellMarker.Hit;
                            break;
                        case CellType.Miss:
                            toWrite = CellMarker.Miss;
                            break;
                        default:
                            toWrite = CellMarker.Blank;
                            break;
                    }

                    Console.Write($"{(char)toWrite} ");
                }

                Console.Write('\n');
            }

            Console.Write('\n');
        }

        /// <summary>
        /// Throws if board hasn't been initialised
        /// </summary>
        /// <exception cref="BoardNotInitialisedException"></exception>
        private void AssertBoardInit()
        {
            if (board == null)
            {
                throw new BoardNotInitialisedException();
            }
        }

        /// <summary>
        /// Throws if given extremities can't represent a valid ship
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="startCol"></param>
        /// <param name="endRow"></param>
        /// <param name="endCol"></param>
        /// <exception cref="InvalidCoordinatesException"></exception>
        private void AssertValidCoordinates(int startRow, int startCol, int endRow, int endCol)
        {
            AssertValidCoordinates(startRow, startCol);
            AssertValidCoordinates(endRow, endCol);
            if ((startRow != endRow && startCol != endCol) ||
                startRow > endRow ||
                startCol > endCol)
            {
                throw new InvalidCoordinatesException(startRow, startCol, endRow, endCol);
            }
        }

        /// <summary>
        /// Throws if given coordinates aren't on the game board
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <exception cref="InvalidCoordinatesException"></exception>
        private void AssertValidCoordinates(int row, int col)
        {
            if (row < 0 || col < 0 || row > Consts.BoardEnd || col > Consts.BoardEnd)
            {
                throw new InvalidCoordinatesException(row, col);
            }
        }

        /// <summary>
        /// Throws if game hasn't been started yet
        /// </summary>
        /// <exception cref="GameNotStartedException"></exception>
        private void AssertGameStarted()
        {
            if (!gameStarted)
            {
                throw new GameNotStartedException();
            }
        }

        /// <summary>
        /// Throws if game has already been started
        /// </summary>
        /// <exception cref="GameAlreadyStartedException"></exception>
        private void AssertGameNotStarted()
        {
            if (gameStarted)
            {
                throw new GameAlreadyStartedException();
            }
        }
    }
}