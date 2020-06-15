namespace PathFinderToo.Logic
{
    public enum SquareType
    {
        Empty = 0,
        Wall,
        /// <summary>
        /// Strong empty means mud, the cost is increased
        /// </summary>
        StrongEmpty,
        StartPoint,
        EndPoint,
        Bomb
    }
}
