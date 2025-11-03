using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Runtime;

namespace DSAProblems
{
    class LeetcodeProblemsArrays
    {

        #region MergeSort
        public static string MergeSort(int[] arr)
        {
            sort(arr, 0, arr.Length - 1);

            return "sorted";
        }

        private static void sort(int[] arr, int l, int r)
        {
            if (l >= r) return;

            //Find the middle point 
            int m = (l + r) / 2;

            //Sort the halves first, Left half
            sort(arr, l, m);
            sort(arr, m + 1, r);

            merge(arr, l, m, r);
        }

        private static void merge(int[] arr, int left, int middle, int right)
        {
            int leftLength = middle - left + 1;
            int rightLength = right - middle;

            int[] leftArr = new int[leftLength];
            int[] rightArr = new int[rightLength];

            //Populate the temp arrays
            int i = 0, j = 0;
            while (i < leftLength)
            {
                leftArr[i] = arr[left + i];
                i++;
            }

            while (j < rightLength)
            {
                rightArr[j] = arr[middle + 1 + j];
                j++;
            }


            i = 0; j = 0; int k = left;
            //Compare the values of the temp sub arrays
            while (i < leftLength && j < rightLength)
            {
                if (leftArr[i] <= rightArr[j])
                {
                    arr[k] = leftArr[i];
                    i++;
                }
                else
                {
                    arr[k] = rightArr[j];
                    j++;
                }
                k++;
            }

            //Copy the leftover elements from left temp array
            while (i < leftLength)
            {
                arr[k] = leftArr[i];
                i++; k++;
            }

            //Copying the leftover elements from right array
            while (j < rightLength)
            {
                arr[k] = rightArr[j];
                k++; i++;
            }

        }

        #endregion

        #region SelectionSort
        public static string SelectionSort(int[] arr)
        {
            int len = arr.Length;

            for (int i = 0; i < len - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < len; j++)
                {
                    if (arr[j] < arr[minIndex])
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    //Swap
                    int temp = arr[i];
                    arr[i] = arr[minIndex];
                    arr[minIndex] = temp;
                }
            }

            arr.ToList().ForEach(a => Console.Write($"{a}, "));
            return "Sorted";
        }
        #endregion

        #region BubbleSort
        public static void BubbleSort(int[] arr)
        {
            int len = arr.Length;
            for (int i = 0; i < len - 1; i++)
            {
                for (int j = 0; j < len - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }

            arr.ToList().ForEach(a => Console.Write($"{a}, "));
        }
        #endregion

        #region #739-Daily Temperatures
        //Link :
        public static int[] DailyTemperatures(int[] temperatures)
        {
            // temperatures = new int[] {30,60,90};
            // int len = temperatures.Length;
            // int idx = 0, mvgIdx;
            // int[] res = new int[len];

            // while(idx < len)
            // {
            //     mvgIdx = idx+1;
            //     while(mvgIdx < len)
            //     {
            //         if(temperatures[mvgIdx] > temperatures[idx])
            //         {
            //             res[idx] = mvgIdx-idx;
            //             break;
            //         }
            //         mvgIdx++;
            //     }
            //     idx++;
            // }

            // return res;


            #region Stack

            int[] results = new int[temperatures.Length];
            Stack<int> previousDaysStack = new();

            for (int today = 0; today < temperatures.Length; today++)
            {
                int todaysTemperature = temperatures[today];
                while (previousDaysStack.Count > 0 &&
                       todaysTemperature > temperatures[previousDaysStack.Peek()])
                {
                    int aPreviousDay = previousDaysStack.Pop();
                    results[aPreviousDay] = today - aPreviousDay;
                }

                previousDaysStack.Push(today);
            }

            return results;

            #endregion
        }

        #endregion

        #region Find indexes of SubArray with given sum
        //Link : https://www.geeksforgeeks.org/problems/subarray-with-given-sum-1587115621/1?page=1&sortBy=submissions
        public static List<int> FindSubArrIndexesWithSum(int[] array, int target)
        {
            var res = new List<int>();
            array = [135, 101, 170, 125, 79, 159, 163, 65, 106, 146, 82, 28, 162, 92, 196, 143, 28, 37, 192, 5, 103, 154, 93,
             183, 22, 117, 119, 96, 48, 127, 172, 139, 70, 113, 68, 100, 36, 95, 104, 12, 123, 134];

            target = 468;


            int len = array.Length;
            int sum = 0;
            Queue<int> indexQueue = new Queue<int>();

            for (int idx = 0; idx < len; idx++)
            {

                while (indexQueue.Count > 0 && sum + array[idx] > target)
                {
                    var ix = indexQueue.Dequeue();
                    sum -= array[ix];
                }

                indexQueue.Enqueue(idx);
                sum += array[idx];

                if (sum == target)
                {
                    // Add the indexes to the res;
                    res.Add(indexQueue.Dequeue() + 1);
                    res.Add(indexQueue.Last() + 1);
                    break;
                }
            }

            return res.Count > 0 ? res : [-1];


        }

        #endregion

        #region #28-Find the Index of the First Occurrence in a String
        //Link : https://leetcode.com/problems/find-the-index-of-the-first-occurrence-in-a-string/description/
        public static int IndexofFirstOccurence(string haystack, string needle)
        {

            //Movind Window approach
            int needleLen = needle.Length;
            int hayStackLen = haystack.Length;

            int offset = 0, index = 0;

            while (offset + needleLen <= hayStackLen)
            {
                index = 0;
                while (index <= needleLen)
                {
                    if (index == needleLen) return offset;

                    if (needle[index] == haystack[index + offset]) index++;
                    else
                    {
                        offset++; break;
                    }
                }
            }


            return -1;
        }
        #endregion

        #region #39-Combination Sum
        //Link : https://leetcode.com/problems/combination-sum/
        public static string CombinationSum(int[] arr, int target)
        {
            List<List<int>> sol = new List<List<int>>();

            List<int> list = new List<int>();

            BackTrackSum(arr, list, sol, target, 0);

            return "Success";
        }

        public static void BackTrackSum(int[] candidates, List<int> set, IList<List<int>> sol, int target, int index)
        {
            if (set.Sum() == target)
            {
                sol.Add(set.ToList());
                return;
            }

            while (index < candidates.Length)
            {
                if (set.Sum() + candidates[index] > target)
                {
                    index++;
                    continue;
                }

                set.Add(candidates[index]);
                BackTrackSum(candidates, set, sol, target, index);
                set.RemoveAt(set.Count - 1);

                index++;
            }
        }
        #endregion


        #region #40 -Combination Sum II
        //LINK : https://leetcode.com/problems/combination-sum-ii/
        public static string CombinationSumII(int[] arr, int target)
        {
            List<List<int>> sol = new List<List<int>>();

            List<int> list = new List<int>();

            BackTrackSumII(arr, list, sol, target, 0);

            return "Success";
        }

        public static void BackTrackSumII(int[] candidates, List<int> set, IList<List<int>> sol, int target, int index)
        {
            if (set.Sum() == target)
            {
                if (sol.All(sl => sl.Except(set).Any()))
                {
                    sol.Add(set.ToList());
                }

                return;
            }

            while (index < candidates.Length)
            {
                if (set.Sum() + candidates[index] > target)
                {
                    index++;
                    continue;
                }

                set.Add(candidates[index]);
                BackTrackSumII(candidates, set, sol, target, index + 1);
                set.RemoveAt(set.Count - 1);

                index++;
            }
        }

        #endregion

        #region #20-Valid Parentheses
        //Link : https://leetcode.com/problems/valid-parentheses/
        public static bool ValidParantheses(string str)
        {
            Dictionary<char, char> dict = new Dictionary<char, char>{
                {'(', ')'},
                {'{', '}'},
                {'[', ']'}
            };



            return false;
        }
        #endregion

        #region #51 -N - Queens
        public static List<List<string>> NQueens(int n)
        {
            List<List<string>> output = new List<List<string>>();

            //Base case
            if (n == 1)
            {
                output.Add(new List<string>() { "Q" });
                return output;
            }

            //Prepare the board
            char[][] board = new char[n][];

            for (int i = 0; i < n; i++)
            {
                board[i] = new char[n];
                for (int j = 0; j < n; j++)
                {
                    board[i][j] = '.';
                }
            }

            SolveBoard(board, output, 0);

            return output;
        }

        private static void SolveBoard(char[][] board, List<List<string>> output, int row)
        {
            if (row == board.Length)
            {
                var ret = new List<string>();

                foreach (var bRow in board)
                {
                    ret.Add(string.Join("", bRow));
                }
                output.Add(ret);
                return;
            }

            for (int col = 0; col < board.Length; col++)
            {
                if (IsSafe(board, row, col))
                {
                    board[row][col] = 'Q';
                    SolveBoard(board, output, row + 1);
                    board[row][col] = '.';
                }
            }
        }

        private static bool IsSafe(char[][] board, int row, int col)
        {
            for (int i = 0; i < row; i++)
            {
                if (board[i][col] == 'Q') return false;

                int leftDiagonal = col - (row - i);
                int rightDiagonal = col + (row - i);

                if (leftDiagonal >= 0 && board[i][leftDiagonal] == 'Q') return false;

                if (rightDiagonal < board.Length && board[i][rightDiagonal] == 'Q') return false;
            }

            return true;
        }

        #endregion
    }
}