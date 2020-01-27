﻿using PathFinderToo.Logic;
using PathFinderToo.Vm.Algorithms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderToo.Vm
{
    public partial class PFViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Disables the UI components for the algorithms to run
        /// should be a bool property
        /// </summary>
        public void DisableAllUIComponents() { }

        private bool solved = false;

        /// <summary>
        /// fully async A* algorithm implementation
        /// </summary>
        /// <param name="curr">leave null</param>
        /// <returns>solved or not</returns>
        public async Task AStarAlgorithmAsync()
        {
            while(!solved)
            {
                await AStarAlgorithmSteppedAsync(CurrentState);
            }
        }

        private AlgorithmState CurrentState = null;
        private ObservableCollection<AlgorithmState> States = new ObservableCollection<AlgorithmState>();

        public async Task AStarAlgorithmSteppedAsync(AlgorithmState last = null)
        {
            Debug.WriteLine("A* called");
            // first time check
            if (last is null)
            {
                Debug.WriteLine("First time run");
                last = new AlgorithmState(SquaresList, new List<PFSquare>() { PFSquare.StartPoint }, PFSquare.StartPoint, null);
                Debug.WriteLine($"Last:{Environment.NewLine}{last}");
                PFSquare.StartPoint.Visited = true;
                States.Add(CurrentState);
                Debug.WriteLine($"Added current state to states collection");
            }
            else
            {
                PFSquare.StartPoint.VisualType = VisualSquareType.StartEndPoint;
                PFSquare.EndPoint.VisualType = VisualSquareType.StartEndPoint;
                last = CurrentState;
            }
            // end condition
            if (last.CurrentlyChecking == PFSquare.EndPoint)
            {
                Debug.WriteLine($"Solution found");
                solved = true;
                return;
            }
            Debug.WriteLine($"Last checked square is not the endpoint, continue");

            // calculate current square
            PFSquare currentlyChecking = GetLowestFCostSquare(last.Available);
            Debug.WriteLine($"Currently checking the square {currentlyChecking}");
            
            currentlyChecking.Visited = true;
            last.Available.Remove(currentlyChecking);
            Debug.WriteLine($"Removing currently checking square");
            //if(currentlyChecking != PFSquare.StartPoint && currentlyChecking != PFSquare.EndPoint)
            last.CurrentlyChecking.VisualType = VisualSquareType.Visited;

            // could be wrapped
            List<PFSquare> sorroundings = await CalculateSquareSorroundings(currentlyChecking);
            Debug.WriteLine($"Got sorroundings of {currentlyChecking}");
            sorroundings = RemoveVisited(sorroundings);
            Debug.WriteLine($"Removed visited squares");
            
            var newAvailable = new List<PFSquare>(last.Available);
            newAvailable.AddRange(sorroundings);
            Debug.WriteLine($"Added last's sorroundings and removed all visited.");
            CurrentState = new AlgorithmState(SquaresList, newAvailable, currentlyChecking, last);
            States.Add(CurrentState);
            Debug.WriteLine("");
            Debug.WriteLine(CurrentState);
        }

        #region Helpers

        private bool SquareIsWalkable(PFSquare s) => s.Type != SquareType.Bomb && s.Type != SquareType.Wall;
        
        private List<PFSquare> RemoveVisited(List<PFSquare> list)
        {
            var newList = new List<PFSquare>(list);
            foreach(var s in list)
            {
                if (s.Visited)
                {
                    newList.Remove(s);
                }
            }
            return newList;
        }

        #region A*

        private PFSquare GetLowestFCostSquare(List<PFSquare> list)
        {
            PFSquare currMin = list[0];

            foreach(var square in list)
            {
                if (currMin.FCost > square.FCost)
                    currMin = square;
            }

            return currMin;
        }

        // TODO: update UI components affected (put it in Calculate function for each of the squares)
        private async Task<List<PFSquare>> CalculateSquareSorroundings(PFSquare square)
        {
            var x = square.X;
            var y = square.Y;
            List<Task> tasks = new List<Task>();
            List<PFSquare> toR = new List<PFSquare>();
            // this list is any square sorrounding the square that we need to check
            List<(int, int)> toCheck = new List<(int, int)>()
            {
                (x + 1, y + 1),
                (x, y + 1),
                (x - 1, y + 1),
                (x + 1, y),
                (x, y),
                (x - 1, y),
                (x + 1, y - 1),
                (x, y - 1),
                (x - 1, y - 1),
            };

            // because we are removing elements in toCheck I'd like to avoid using the same list
            foreach (var t in toCheck.ToList())
            {
                if (t.Item1 < 0 || t.Item1 > 52 || t.Item2 < 0 || t.Item2 > 52)
                {
                    toCheck.Remove(t);
                }
                else if (!SquareIsWalkable(SquaresList[t.Item1 * t.Item2]))
                {
                    toCheck.Remove(t);
                }
            }

            foreach (var t in toCheck)
            {
                PFSquare curr = SquaresList[t.Item1 * 53 + t.Item2];
                tasks.Add(Task.Run(curr.CalculateCosts));
                toR.Add(curr);
                if(curr.VisualType != VisualSquareType.StartEndPoint && curr.VisualType != VisualSquareType.Visited)
                    curr.VisualType = VisualSquareType.Sorrounding;
            }

            await Task.WhenAll(tasks);
            return toR;
        }

        /// <summary>
        /// Sorts the sorroundings list so that it will be sorted by lowest FCost
        /// </summary>
        /// <param name="sorroundings">list of nodes</param>
        /// <returns></returns>

        #endregion

        #endregion
    }
}