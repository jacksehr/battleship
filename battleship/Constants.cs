namespace battleship
{
    static class Consts
    {
        public const int BoardSize = 10;
        public const int BoardEnd = 9;
    }
    
    internal enum CellType { Blank, Ship, Hit, Miss }
    internal enum Direction { Vertical, Horizontal }
    internal enum CellMarker { Blank = '~', Ship = 'O', Hit = 'X', Miss = '*' }
}
