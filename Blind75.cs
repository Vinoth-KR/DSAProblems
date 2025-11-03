using System;
using System.Collections.Generic;

namespace DSAProblems
{
    class Blind75
    {

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

        #region #70 - ClimbingStairs
        //LeetCode problem
        // Link : https://leetcode.com/problems/climbing-stairs/
        // Link : https://www.designgurus.io/viewer/document/grokking-dynamic-programming/637f48f8e57c531cec3998d2
        internal static int ClimbingStairs(int v)
        {
            /* DESCRIPTION : 
            At any point of time, one stair or two stairs can be climbed,
            thus for the given number 'v', the possible ways to climb the stairs must be computed
            */

            //create a map similar to fibonacci series with predefined values like, stairs[1] = 1 and stairs[2] = 2, 
            // work the way up untill the number is reached.           

            if (v == 0) return 0;

            var stairs = new Dictionary<int, int>();
            stairs[1] = 1; stairs[2] = 2;

            if (v <= 2) return stairs[v];
            int val = 3;
            while (val <= v)
            {
                stairs[val] = stairs[val - 1] + stairs[val - 2];
                val++;
            }

            return stairs[val - 1];
        }
        #endregion

        #region LongestSubstringWithKUniqueChars
        //GeeksForGeeks problem
        //Link : https://www.geeksforgeeks.org/find-the-longest-substring-with-k-unique-characters-in-a-given-string/
        internal static int LongestSubStringWithKUniqueChars(string s, int k)
        {
            // int len = s.Length;

            // if(len < k ) return -1;

            // int left = 0, right = 0, maxLen = -1;

            // Dictionary<char, int> set = new Dictionary<char, int>();

            // while(right < len)
            // {
            //     if(set.ContainsKey(s[right]))
            //         set[s[right]] += 1;
            //     else
            //         set[s[right]] = 1;

            //     while(set.Count > k)
            //     {
            //         set[s[left]] -= 1;
            //         if(set[s[left]] == 0)
            //             set.Remove(s[left]);

            //         left += 1;
            //     }

            //     if(set.Count == k) maxLen = Math.Max(maxLen, right-left+1);

            //     right++;
            // }

            // return maxLen;

            int len = s.Length;
            if (len < k) return -1;

            int left = 0, right = 0, maxLen = 0, ansStart = 0;
            //Add a dict map
            var map = new Dictionary<char, int>();

            while (right < len)
            {
                if (map.ContainsKey(s[right])) map[s[right]] += 1;
                else map[s[right]] = 1;

                while (map.Count > k)
                {
                    map[s[left]]--;
                    if (map[s[left]] == 0) map.Remove(s[left]);
                    left += 1;
                }

                if (map.Count == k)
                {
                    if ((right - left + 1) > maxLen)
                    {
                        maxLen = right - left + 1;
                        ansStart = left;
                    }
                }

                right++;
            }

            Console.WriteLine($"The substring in {s}, {s.Substring(ansStart, maxLen)} with the length of {maxLen}");
            return maxLen;
        }
        #endregion

        #region #383. RansomNote
        //LeetCode Problem
        //Link : https://leetcode.com/problems/ransom-note/
        public static bool CanConstruct(string ransomNote, string magazine)
        {
            /* DESCRIPTION
            Check whether the ransomNote can be constructed from the available magazine,
            must make sure that each letter in magazine can be used in the ransomNote only once.
            Imp constaint to be noted : all chars in magazine are lowercase letters
            */

            const int MAX_CHARS = 26;
            var charMap = new int[MAX_CHARS];

            Array.Fill(charMap, 0);

            int magazineLen = magazine.Length;
            int idx = 0;
            while (idx < magazineLen)
            {
                charMap[magazine[idx] - 'a'] += 1;
                idx++;
            }

            //Reset the index to 0 and start working on the ransomNote, if it can be constructed
            int ransomLen = ransomNote.Length;
            idx = 0;
            while (idx < ransomLen)
            {
                if (charMap[ransomNote[idx] - 'a'] == 0) return false;
                charMap[ransomNote[idx] - 'a']--;
                idx++;
            }
            return true;
        }
        #endregion

        #region #322. Coin Change
        //Link : https://leetcode.com/problems/coin-change/
        public static int CoinChange(int[] coins, int amount)
        {
            /*DESCRIPTION
            Given coins of different denominations, check whether the amount can be made from the coins
            Return the minimum coins required to get to the amount.
            Constraints : Each coin can be used any number of times.
            */

            //Backtracking algorithm
            // if(amount == 0) return 0;

            // var res = BackTrackCoins(coins, amount, 0);
            // return res == Int32.MaxValue ? -1 : res;

            //Dynamic Programming
            if (amount == 0) return 0;

            var map = new int[amount + 1];
            Array.Fill(map, amount + 1);
            map[0] = 0;

            //Loop through all coins and for each coin iterate till amount 
            // to find the dp for all values till amount
            foreach (var coin in coins)
            {
                for (int i = coin; i <= amount; i++)
                {
                    map[i] = Math.Min(map[i], map[i - coin] + 1);
                }
            }

            return map[amount] == amount + 1 ? -1 : map[amount];


        }


        private static int BackTrackCoins(int[] coins, int amount, int idx)
        {
            if (amount == 0)
            {
                return 0;
            }
            if (idx >= coins.Length)
            {
                return Int32.MaxValue;
            }
            int count1 = Int32.MaxValue;
            if (coins[idx] <= amount)
            {
                int res = BackTrackCoins(coins, amount - coins[idx], idx);
                if (res != Int32.MaxValue)
                {
                    count1 = res + 1;
                }
            }
            int count2 = BackTrackCoins(coins, amount, idx + 1);
            return Math.Min(count1, count2);

        }

        #region OldApproach - Candidates
        //         public class Solution {
        //     public int CoinChange(int[] coins, int amount) {
        //         if(amount == 0) return 0;

        //             var candidates = new List<int>();
        //             Array.Sort(coins);
        //             int idx = coins.Length - 1;
        //             var result = BackTrackCoins(coins, amount, candidates, idx);
        //             return result;
        //     }

        //     private int BackTrackCoins(int[] coins, int amount, List<int> candidates, int idx)
        //         {
        //             if(idx < 0) return -1;

        //             if(coins[idx] > amount)
        //             {                
        //                 if(candidates.Contains(coins[idx]))
        //                 { 
        //                     candidates.Remove(coins[idx]);
        //                     amount += coins[idx];
        //                 }
        //                 idx--;                
        //             }
        //             else
        //             {
        //                 candidates.Add(coins[idx]);
        //                 amount -= coins[idx];

        //                 if(amount == 0) return candidates.Count;   
        //                 if(Array.IndexOf(coins, amount) != -1){
        //                     candidates.Add(coins[Array.IndexOf(coins, amount)]);
        //                     return candidates.Count;
        //                 }                
        //             }

        //             return BackTrackCoins(coins, amount, candidates, idx);
        //         }
        // }
        #endregion

        #endregion

    }
}