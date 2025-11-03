using System;
using System.Collections.Generic;
using System.Linq;

namespace DSAProblems
{

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }

    }


    public class MyQueue()
    {
        private List<int> st = new List<int>();

        public void Push(int x)
        {
            st.Add(x);
        }

        public int Pop()
        {
            if (st.Count == 0) return -1;

            int val = st[0];
            st.RemoveAt(0);
            return val;
        }

        public int Peek()
        {
            if (st.Count == 0) return -1;
            return st.ElementAt(0);
        }

        public bool Empty()
        {
            return st.Count == 0;
        }
    }



    class GrindProblemsHelper
    {
        /// <summary>
        /// Master method to call all of the problems under grind75
        /// </summary>
        public static void ExecuteProblems()
        {
            //TwoSum();

            //ValidParanthesis();

            //MergeTwoSortedLists();

            //BestTimeToBuyAndSellStock();

            //IsPalindrome();

            //MajorityElement();

            //ContainsDuplicate();

            //InvertBinaryTree();

            //BreakPalindrome();

            //ValidAnagram();

            //BinarySearch();

            //LowestCommonAncestor();

            //BalancedBinaryTree();

            //LinkedListCycle();

            //ClimbingStairs();

            //LongestPalindrome();

            //AddBinary();

            //MiddleOfLinkedList();

            //DiameterOfBinaryTree();

            //MaximumDepthOfBinaryTree();

            //MaxSumSubArray();

            //LongestSubstringWithoutRepeatingCharacters();

            //ContainerWithMostWater();

            //DailyTemperature();

            //MergeIntervals();

            //RemoveNthNodeFromEnd();

            //LRUCacheDriver();

            //SortColors();

            //HouseRobber();

            //HouseRobberII();

            //UniquePaths();

            //MinPathSum();

            //MinFallingPathSum();

            //CoinChangeII();

            RottenOranges();
        }

        private static void TwoSum()
        {
            var nums = new int[] { 2, 11, 15, 7 };
            int target = 9;
            GrindProblems.TwoSum(nums, target);
        }

        private static void ValidParanthesis()
        {

            GrindProblems.ValidParanthesis("{([])}");
        }

        private static void MergeTwoSortedLists()
        {
            var list1 = new ListNode(1, new ListNode(2, new ListNode(4)));
            var list2 = new ListNode(1, new ListNode(3, new ListNode(4)));
            GrindProblems.MergeTwoSortedLists(list1, list2);
        }

        private static void BestTimeToBuyAndSellStock()
        {
            var prices = new int[] { 7, 1, 5, 3, 6, 4 };
            //var prices = new int[] {7,6,4,3,1};
            GrindProblems.BestTimeToBuyAndSellStock(prices);
        }

        private static void IsPalindrome()
        {
            string s = "race a car";
            GrindProblems.IsPalindrome(s);
        }

        private static void MajorityElement()
        {
            // var nums = new int[] {3,2,3};
            var nums = new int[] { 2, 2, 1, 1, 1, 2, 2 };
            GrindProblems.MajorityElement(nums);
        }

        private static void ContainsDuplicate()
        {
            var nums = new int[] { 1, 2, 3, 4 };
            GrindProblems.ContainsDuplicate(nums);
        }

        private static void FirstBadVersion()
        {
            GrindProblems.FirstBadVersion(28);
        }

        private static void InvertBinaryTree()
        {
            var root = new TreeNode(4, new TreeNode(2, new TreeNode(1), new TreeNode(3)), new TreeNode(7, new TreeNode(6), new TreeNode(9)));
            GrindProblems.InvertBinaryTree(root);
        }

        private static void BreakPalindrome()
        {
            GrindProblems.BreakPalindrome("ababa");
        }

        private static void ValidAnagram()
        {
            Console.WriteLine(GrindProblems.IsAnagram("rat", "tar"));
        }

        private static void BinarySearch()
        {
            Console.WriteLine(GrindProblems.Search(new int[] { -1, 0, 3, 5, 9, 12 }, 2));
        }

        private static void LowestCommonAncestor()
        {
            var root = new TreeNode(6, new TreeNode(2, new TreeNode(0), new TreeNode(4, new TreeNode(3), new TreeNode(5))), new TreeNode(8, new TreeNode(7), new TreeNode(9)));
            Console.WriteLine(GrindProblems.LowestCommonAncestor(root, new TreeNode(4), new TreeNode(7)).val);
        }

        private static void BalancedBinaryTree()
        {
            //var root = new TreeNode(1, new TreeNode(2, new TreeNode(3, new TreeNode(4))), new TreeNode(2, null, new TreeNode(3, null, new TreeNode(4))));
            //var root = new TreeNode(3, new TreeNode(9), new TreeNode(20, new TreeNode(15), new TreeNode(7)));
            var root = new TreeNode(1, new TreeNode(2, new TreeNode(3, new TreeNode(4), new TreeNode(4)), new TreeNode(3)), new TreeNode(2));
            Console.WriteLine(GrindProblems.IsBalanced(root));
        }

        private static void LinkedListCycle()
        {
            var head = new ListNode(3);
            var node2 = new ListNode(2);
            var node0 = new ListNode(0);
            var nodeMinus4 = new ListNode(-4);

            head.next = node2;
            node2.next = node0;
            node0.next = nodeMinus4;
            //nodeMinus4.next = node2; // Creating a cycle here

            Console.WriteLine(GrindProblems.HasCycle(head));
        }

        private static void ClimbingStairs()
        {
            Console.WriteLine(GrindProblems.ClimbingStairs(4));
        }

        private static void LongestPalindrome()
        {
            Console.WriteLine(GrindProblems.LongestPalindrome("aaa"));
        }

        private static void AddBinary()
        {
            Console.WriteLine(GrindProblems.AddBinary("1010", "1011"));
        }

        private static void MiddleOfLinkedList()
        {
            Console.WriteLine(GrindProblems.MiddleNode(new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5, new ListNode(6))))))).val);
        }

        private static void DiameterOfBinaryTree()
        {
            Console.WriteLine(GrindProblems.DiameterOfBinaryTree(new TreeNode(1, new TreeNode(2, new TreeNode(4), new TreeNode(5)), new TreeNode(3))));
            //Console.WriteLine(GrindProblems.DiameterOfBinaryTree(new TreeNode(1, new TreeNode(2))));
        }

        private static void MaximumDepthOfBinaryTree()
        {
            Console.WriteLine(GrindProblems.MaxDepth(new TreeNode(1, new TreeNode(2, new TreeNode(4), new TreeNode(5)), new TreeNode(3))));
        }

        private static void MaxSumSubArray()
        {
            //var res = GrindProblems.MaxSubArray(new int[] {-2,1,-3,4,-1,2,1,-5,4});
            var res = GrindProblems.MaxSubArray(new int[] { 5, 4, -1, 7, 8 });
            //res.ToList().ForEach(Console.WriteLine);
            Console.WriteLine(res);
        }

        private static void LongestSubstringWithoutRepeatingCharacters()
        {
            Console.WriteLine(GrindProblems.LengthOfLongestSubstring("cadbzabcd"));
        }

        private static void ContainerWithMostWater()
        {
            Console.WriteLine(GrindProblems.ContainerWithMostWater(new int[] { 1, 8, 100, 2, 100, 4, 8, 3, 7 }));
        }

        private static void DailyTemperature()
        {
            GrindProblems.DailyTemperature(new int[] { 73, 74, 75, 71, 69, 72, 76, 73 }).ToList().ForEach(Console.WriteLine);
        }

        private static void MergeIntervals()
        {
            //GrindProblems.MergeIntervals(new int[][]{new int[]{1,3}, new int[]{2,6}, new int[]{8,10}, new int[]{15,18}}).ToList().ForEach(arr => Console.WriteLine($"[{arr[0]},{arr[1]}]"));
            //GrindProblems.MergeIntervals(new int[][]{new int[]{1,3}, new int[]{0,0}}).ToList().ForEach(arr => Console.WriteLine($"[{arr[0]},{arr[1]}]"));
            GrindProblems.MergeIntervals(new int[][] { new int[] { 2, 3 }, new int[] { 2, 2 }, new int[] { 3, 3 }, new int[] { 1, 3 }, new int[] { 5, 7 }, new int[] { 2, 2 }, new int[] { 4, 6 } }).ToList().ForEach(arr => Console.WriteLine($"[{arr[0]},{arr[1]}]"));

        }

        private static void RemoveNthNodeFromEnd()
        {
            var head = GrindProblems.RemoveNthFromEnd(new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5))))), 2);
            //var head = GrindProblems.RemoveNthFromEnd(new ListNode(1, new ListNode(2)), 1);

            while (head != null)
            {
                Console.Write($"{head.val}->");
                head = head.next;
            }
        }

        private static void LRUCacheDriver()
        {
            GrindProblems.LRUCacheImpl();
        }

        private static void UpdateMatrix01()
        {

        }

        private static void SortColors()
        {
            var res = GrindProblems.SortColors(new int[] { 2, 0, 2, 1, 1, 0 });
            res.ToList().ForEach(Console.WriteLine);
        }

        private static void HouseRobber()
        {
            Console.WriteLine(GrindProblems.Rob(new int[] { 1, 2, 3, 1 }));
        }

        private static void HouseRobberII()
        {
            Console.WriteLine(GrindProblems.RobHouse(new int[] { 2, 3, 2 }));
        }

        private static void UniquePaths()
        {
            Console.WriteLine(GrindProblems.UniquePaths(3, 7));
        }

        private static void MinPathSum()
        {
            int[][] grid = new int[][]
            {
                new int[] {1, 3, 1},
                new int[] {1, 5, 1},
                new int[] {4, 2, 1}
            };

            Console.WriteLine(GrindProblems.MinPathSum(grid));
        }

        private static void MinFallingPathSum()
        {
            int[][] grid = new int[][]
           {
                new int[] {2, 1, 3},
                new int[] {6, 5, 4},
                new int[] {7, 8, 9}
           };

            Console.WriteLine(GrindProblems.MinFallingPathSum(grid));
        }

        private static void CoinChangeII()
        {
            Console.WriteLine(GrindProblems.CoinChangeII(5, new int[] { 1, 2, 5 }));
        }

        private static void RottenOranges()
        {
            int[][] grid = new int[][]
         {
                new int[] {2,1,1},
                new int[] {1,1,0},
                new int[] {0,1,1}
         };

            Console.WriteLine(GrindProblems.RottenOranges(grid));
        }
    }
}