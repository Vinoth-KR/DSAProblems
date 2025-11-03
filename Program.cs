using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;

namespace DSAProblems
{
    class Charge
    {
        public string ObjectId { get; set; }
        public string ChangeId { get; set; }

    }

    public delegate void OnAdded(object element, EventArgs eventArgs);

    //ExtensionMethods
    static class ListExtensions
    {

        private static Dictionary<object, OnAdded> handlers = new();

        // public static EventHandler OnElementAdded;

        public static void Publish<T>(this List<T> list, T element)
        {
            list.Add(element);
            //Trigger an event
            handlers[list].Invoke(element, EventArgs.Empty);
        }

        public static void Subscribe<T>(this List<T> list, OnAdded method)
        {
            handlers.Add(list, method);
        }



        //non static event and try to associate that with the list object.
        //Event could be trigerred from event, list object or any other object


    }

    class Program
    {

        void Callback(object element, EventArgs eventArgs)
        {
            Console.WriteLine(element.GetType().IsValueType ? element : "Object");
        }

        static void Main(string[] args)
        {

            Stopwatch st = new Stopwatch();
            st.Start();

            // var list = new List<int>();
            // var list2 = new List<string>();
            // list.Subscribe(new Program().Callback);
            // list2.Subscribe(new Program().Callback);
            // list.Publish(2);
            // list.Publish(3);
            // Console.WriteLine("****************");
            // list2.Publish("value");

            //var objList = new List<Charge>();



            //IndexofFirstOccurence(); //Prob #28

            //PowerOf();

            //BitManipulation();

            //IsUnique();

            //IsPermutation();     

            //URLify();

            //Permutations();

            //PalindromePermutation();

            //OneEditAway();

            //StringCompression();

            //RotateMatrix();

            //ZeroMatrix();

            //RotateString();

            //CombinationSum();

            //CombinationSumII();

            //DailyTemperatures();

            //FindSubArrIndexesWithSum();

            //MergeSort();

            //NQueens();

            //LongestSubStringWithKUniqueChars();

            //ClimbingStairs();

            //RansomNote();

            //CoinChange();

            //logAnalysis();

            //findLowestPrice();

            //tunrstile();

            //Leetcode next problems to try -> 77 | 17

            SelectionSort();

            //GrindProblemsHelper.ExecuteProblems();



            st.Stop();
            Console.WriteLine($"Timetaken to run the Algo : {st.ElapsedMilliseconds}ms");

        }



        private static void LongestSubStringWithKUniqueChars()
        {
            //aabacbebebe
            //aabacbebebe
            //5
            System.Console.WriteLine(Blind75.LongestSubStringWithKUniqueChars("aabacbebebe", 3));
        }

        private static void ClimbingStairs()
        {
            Console.WriteLine(Blind75.ClimbingStairs(6));
        }

        private static void RansomNote()
        {
            Console.WriteLine(Blind75.CanConstruct("abaca", "abmcada"));
        }

        private static void CoinChange()
        {
            //System.Console.WriteLine(LeetcodeProblems.CoinChange(new int[]{1,2,5}, 11));
            System.Console.WriteLine(Blind75.CoinChange(new int[] { 186, 419, 83, 408 }, 6249));
        }




        private static void CombinationSum()
        {
            Console.WriteLine(LeetcodeProblemsArrays.CombinationSum(new int[] { 2, 3, 6, 7 }, 7));
        }


        private static void MergeSort()
        {
            Console.WriteLine(LeetcodeProblemsArrays.MergeSort(new int[] { 46, 12, 20, 28, 9, 1 }));
        }

        private static void SelectionSort()
        {
            Console.WriteLine(LeetcodeProblemsArrays.SelectionSort(new int[] { 64, 25, 12, 22, 11 }));
        }

        private static void BubbleSort()
        {
            //System.Console.WriteLine(LeetcodeProblemsArrays.BubbleSort(new int[] { 64, 34, 25, 12, 22, 11, 90 }));
        }

        private static void DailyTemperatures()
        {
            Console.WriteLine(LeetcodeProblemsArrays.DailyTemperatures(new int[] { 73, 74, 75, 71, 69, 72, 76, 73 }));
        }

        private static void FindSubArrIndexesWithSum()
        {
            Console.WriteLine(GetArrayString(LeetcodeProblemsArrays.FindSubArrIndexesWithSum(new int[] { 1, 3, 5, 6, 7, 2 }, 12).ToArray()));
        }

        private static void CombinationSumII()
        {
            Console.WriteLine(LeetcodeProblemsArrays.CombinationSumII(new int[] { 10, 1, 2, 7, 6, 1, 5 }, 8));
        }

        private static void NQueens()
        {
            var values = LeetcodeProblemsArrays.NQueens(4);

            foreach (var value in values)
            {
                System.Console.WriteLine(string.Join(",", value));
            }
        }

        private static string GetArrayString(int[] arr)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in arr)
            {
                sb.Append($"{item.ToString()}, ");
            }

            return sb.ToString();
        }
        private static void RotateString()
        {
            System.Console.WriteLine(CtciProblems.RotateString("abcde", "cdeab"));
        }

        private static void ZeroMatrix()
        {
            var input = new int[][]{
                new int[] {5,1,9,11},
                new int[] {2,4,0,10},
                new int[] {13,0,6,7}
                };

            PrintMatrix(input);
            System.Console.WriteLine("---------------");

            CtciProblems.ZeroMatrix(input);

            PrintMatrix(input);
        }

        private static void OneEditAway()
        {
            System.Console.WriteLine(CtciProblems.OneEditAway("bales", "pasle"));
        }

        private static void RotateMatrix()
        {
            // var input = new int[][]{
            //     new int[] {1,2,3},
            //     new int[] {4,5,6},
            //     new int[] {7,8,9}
            //     };

            var input = new int[][]{
                new int[] {5,1,9,11},
                new int[] {2,4,8,10},
                new int[] {13,3,6,7},
                new int[] {15,14,12,16}
                };

            PrintMatrix(input);
            System.Console.WriteLine("---------------");

            var res = CtciProblems.Rotate90deg(input);

            PrintMatrix(res);
        }

        private static void PrintMatrix(int[][] res)
        {
            int len = res.Length;
            int colLen = res[0].Length;

            int rowIdx = 0, colIdx = 0;

            while (rowIdx < len)
            {
                while (colIdx < colLen)
                {
                    System.Console.Write($"{res[rowIdx][colIdx]} ");
                    colIdx++;
                }
                rowIdx++; colIdx = 0;
                System.Console.WriteLine();
            }
        }

        private static void StringCompression()
        {
            System.Console.WriteLine(CtciProblems.StringCompression("SSttvvvXXXBYNW"));
        }

        private static void PalindromePermutation()
        {
            System.Console.WriteLine(CtciProblems.PalindromePermutation("Perarep"));
        }

        static void IndexofFirstOccurence()
        {
            Console.WriteLine($"Index pos: {LeetcodeProblems.IndexofFirstOccurence("Test", "te")}");
        }

        static void BitManipulation()
        {
            int ix = 1039382;

            System.Console.WriteLine(Convert.ToString(ix, toBase: 2));

            ix = ix | (1 << 5);

            ix &= ~(1 << 4);

            System.Console.WriteLine(Convert.ToString(ix, toBase: 2));

            System.Console.WriteLine(ix);

        }

        static void ValidParanthesis()
        {
            Console.WriteLine($"Parantheses Valid : {Blind75.ValidParantheses("(){}")}");
        }


        #region Ctci Problems

        static void Permutations()
        {
            CtciProblems.Permutations("Tactcoa");
        }

        static void PowerOf()
        {
            System.Console.WriteLine($"{CtciProblems.power(2, 4)}");
        }

        static void IsUnique()
        {
            string str = "Depz";
            System.Console.WriteLine($"All characters are unique in {str} : {CtciProblems.IsUnique(str)}");
        }

        static void IsPermutation()
        {
            string str1 = "Aadav";
            string str2 = "vaada";

            System.Console.WriteLine($"The two strings are permutation of each other : {CtciProblems.IsPermutation(str1, str2)}");
        }

        static void URLify()
        {
            string val = "My name is Venkat Aadav            ";
            CtciProblems.URLify(val, 23);
        }

        #endregion
    }
}
