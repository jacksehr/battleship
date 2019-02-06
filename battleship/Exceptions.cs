using System;


namespace battleship
{
    public class BoardNotInitialisedException : Exception
    {
        public BoardNotInitialisedException()
        {
            Console.Error.WriteLine("Board needs to be initialised first!");
        }
    }
    
    public class GameNotStartedException : Exception
    {
        public GameNotStartedException()
        {
            Console.Error.WriteLine("Game hasn't started yet!");
        }
    }
    
    public class GameAlreadyStartedException: Exception
    {
        public GameAlreadyStartedException()
        {
            Console.Error.WriteLine("Game has already started!");
        }
    }
    
    public class InvalidCoordinatesException: Exception
    {
        public InvalidCoordinatesException(int row, int col)
        {
            Console.Error.WriteLine($"Invalid coordinates: ({col}, {row})!");
        }
        
        public InvalidCoordinatesException(int startRow, int startCol, int endRow, int endCol)
        {
            Console.Error.WriteLine($"Invalid coordinates: ({startCol}, {startRow}), ({endCol}, {endRow})");
        }
    }
}