﻿namespace PathFinderToo.Vm
{
    /// <summary>
    /// This enum has nothing to do with logic and is only for the visual part of the algorithm.
    /// </summary>
    public enum VisualSquareType
    {
        Empty,
        StrongEmpty,
        Wall,
        Bomb,
        StartPoint,
        EndPoint,
        Sorrounding,
        Visited,
        FinishPath
    }
}
