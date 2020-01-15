namespace PathFinderToo.Logic
{
    public enum SquareType
    {
        Empty = 0,
        Wall,
        StartPoint,
        EndPoint,
        Bomb,
        /// <summary>
        /// Checked squares that are in a path that failed
        /// </summary>
        CheckedAndFailedPath,
        /// <summary>
        /// Currently checking path is the state of the block when it is in a path that the algorithm is currently
        /// checking but not the current block it's checking
        /// </summary>
        CurrentlyCheckingPath,
        /// <summary>
        /// Currently checking is the state at which a block is being checked by the algorithm
        /// </summary>
        CurrentlyChecking
    }
}
