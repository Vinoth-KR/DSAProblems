using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;


namespace DSAProblems
{
    public class ListNode
    {
        public int val;

        public int key;

        public ListNode next;

        public ListNode prev;
        public ListNode(int val = 0, ListNode next = null, ListNode prev = null, int key = 0)
        {
            this.val = val;
            this.key = key;
            this.next = next;
            this.prev = prev;
        }
    }

    public class LRUCache
    {
        private int m_Capacity;
        private Dictionary<int, ListNode> m_Cache;

        private ListNode head;

        private ListNode tail;



        public LRUCache(int capacity)
        {
            m_Capacity = capacity;

            head = new ListNode(-1, null, null, -1);
            tail = new ListNode(-1, null, null, -1);
            head.next = tail;
            tail.prev = head;

            m_Cache = new Dictionary<int, ListNode>();
        }

        public int get(int key)
        {
            if (m_Cache.ContainsKey(key))
            {
                var node = m_Cache[key];
                DeleteNode(node);
                InsertAfterHead(node);
                return node.val;
            }
            return -1;
        }

        public void put(int key, int value)
        {

            //Existing keys, update the values
            if (m_Cache.ContainsKey(key))
            {
                //Update the node.
                var existingNode = m_Cache[key];
                existingNode.val = value;
                DeleteNode(existingNode);
                InsertAfterHead(existingNode);
            }
            else
            {
                ListNode node = new ListNode(value, null, null, key);
                m_Cache.Add(key, node);
                InsertAfterHead(node);
            }


            if (m_Cache.Count > m_Capacity)
            {
                //get the tail node and delete it.
                var tailNode = tail.prev;
                m_Cache.Remove(tailNode.key);
                DeleteNode(tailNode);
            }
        }


        //private methods
        private void InsertAfterHead(ListNode node)
        {
            var temp = head.next;
            head.next = node;
            node.prev = head;
            node.next = temp;
            temp.prev = node;

        }

        private void DeleteNode(ListNode node)
        {
            var temp = node.prev;
            var temp2 = node.next;
            temp.next = temp2;
            temp2.prev = temp;
        }

        public void Display()
        {
            var temp = head;
            while (temp != null)
            {
                Console.WriteLine($"{temp.key} -> {temp.val}");
                temp = temp.next;
            }

            Console.WriteLine(new string('*', 20));
        }
    }

    class GrindProblems
    {

        #region #1 TwoSum
        /// <summary>
        /// Leetcode link : https://leetcode.com/problems/two-sum/
        /// </summary>
        internal static void TwoSum(int[] nums, int target)
        {
            /*
            DESCRIPTION : 
            Given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target.
            You may assume that each input would have exactly one solution, and you may not use the same element twice.
            You can return the answer in any order.
            */

            #region Basic Approach
            //Naive approach is to take one index and match it with all the other elements of the array
            // int len = nums.Length, idx = 0; 
            // List<int> result = new List<int>();
            // while(idx < len){

            //     int sum = target - nums[idx];

            //     int mvgIdx = idx + 1;
            //     while(mvgIdx < len){
            //         if(sum == nums[mvgIdx]){
            //             result.Add(idx);
            //             result.Add(mvgIdx);
            //             break;
            //         }
            //         mvgIdx++;
            //     }

            //     if(result.Count > 0) break;

            //     idx++;
            // }

            // result.ForEach(Console.Write);

            #endregion

            #region Memoization
            //Approach 2 : Memoization

            int idx = 0, len = nums.Length;
            Dictionary<int, int> set = new Dictionary<int, int>();

            //Add the difference number for all the elements in to the hashset
            while (idx < len)
            {
                if (set.ContainsKey(nums[idx]))
                {
                    Console.WriteLine($"{set[nums[idx]]} {idx}");
                }
                else
                {
                    set[target - nums[idx]] = idx;
                }

                idx++;
            }

            #endregion

        }
        #endregion

        #region #2 Valid Parantheses
        /// <summary>
        /// Leetcode link : https://leetcode.com/problems/valid-parentheses/description/
        /// </summary>
        internal static void ValidParanthesis(string s)
        {
            /*
            DESCRIPTION:
            Given a string s containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid.

            An input string is valid if:

                Open brackets must be closed by the same type of brackets.
                Open brackets must be closed in the correct order.
                Every close bracket has a corresponding open bracket of the same type.
            */

            #region Basic Approach
            int len = s.Length, idx = 0;
            Stack<char> stack = new Stack<char>();
            while (idx < len)
            {
                if (s[idx] == '(' || s[idx] == '{' || s[idx] == '[')
                {
                    stack.Push(s[idx]);
                }
                else if (s[idx] == ')' || s[idx] == '}' || s[idx] == ']')
                {
                    if (stack.Count == 0 ||
                    (s[idx] == ')' && stack.Pop() != '(') ||
                    (s[idx] == '}' && stack.Pop() != '{') ||
                    (s[idx] == ']' && stack.Pop() != '['))
                    {
                        Console.WriteLine("false");
                        return;
                    }
                    //stack.Pop();
                }

                idx++;
            }
            Console.WriteLine(stack.Count > 0 ? "false" : "true");
            #endregion

            #region Accessing from SetMap
            // int len = s.Length, idx = 0;
            // Stack<char> stack = new Stack<char>();
            // Dictionary<char, char> map = new Dictionary<char, char>()
            // {
            //     {'(',')'},
            //     {'[',']'}, 
            //     {'{','}'}
            // };

            // while(idx < len){
            //     if(map.ContainsKey(s[idx])){
            //         stack.Push(s[idx]);
            //     }else if(map.ContainsValue(s[idx])){
            //         if(stack.Count == 0 || map[stack.Peek()] != s[idx]){
            //             Console.WriteLine("false");
            //             return;
            //         }
            //         stack.Pop();
            //     }
            //     idx++;
            // }
            // Console.WriteLine("true");
            #endregion

        }
        #endregion

        #region #3 Merge Two SortedLists

        /*
        DESCRIPTION : 
        You are given the heads of two sorted linked lists list1 and list2.
        Merge the two lists into one sorted list. The list should be made by splicing together the nodes of the first two lists.
        Return the head of the merged linked list.
        */
        /// <summary>
        /// Leetcode link : https://leetcode.com/problems/merge-two-sorted-lists/description/
        /// </summary>
        internal static void MergeTwoSortedLists(ListNode list1, ListNode list2)
        {
            #region Basic Approach
            //     ListNode head = null, curr = null;

            //     while(list1 != null && list2 != null){
            //     if(list1.val > list2.val){
            //         if(head == null){
            //             head = list2;
            //             curr = head;
            //         }
            //         else
            //         {
            //         curr.next = list2;
            //         curr = list2;
            //         }
            //         list2 = list2.next;
            //     }
            //     else
            //     {
            //         if(head == null){
            //             head = list1;
            //             curr = head;
            //         }
            //         else
            //         {
            //         curr.next = list1;
            //         curr = list1;
            //         }
            //         list1 = list1.next;
            //     }
            // }

            // //Leftover elements 
            // while(list1 != null){
            //     curr.next = list1;
            //     curr = list1;
            //     list1 = list1.next;
            // }
            // while(list2 != null){
            //     curr.next = list2;
            //     curr = list2;
            //     list2 = list2.next;
            // }

            #endregion

            #region Approach 2 
            ListNode head = new ListNode();
            ListNode curr = head;

            while (list1 != null && list2 != null)
            {
                if (list1.val < list2.val)
                {
                    curr.next = list1;
                    list1 = list1.next;
                }
                else
                {
                    curr.next = list2;
                    list2 = list2.next;
                }
                curr = curr.next;
            }

            curr.next = list1 != null ? list1 : list2;


            #endregion

            var itr = head.next;
            while (itr != null)
            {
                Console.Write($"{itr.val}->");
                itr = itr.next;
            }

        }


        #endregion

        #region #4 Best Time to Buy and Sell Stock
        /*
        DESCRIPTION : 
        You are given an array prices where prices[i] is the price of a given stock on the ith day.
        You want to maximize your profit by choosing a single day to buy one stock and choosing a different day in the future to sell that stock.
        Return the maximum profit you can achieve from this transaction. If you cannot achieve any profit, return 0.
        */
        /// <summary>
        /// Leetcode link : https://leetcode.com/problems/best-time-to-buy-and-sell-stock/description/
        /// </summary>
        internal static void BestTimeToBuyAndSellStock(int[] prices)
        {
            #region Two-Pointer Approach
            // int left = 0 , right = prices.Length-1, maxProfit = 0;

            // while(left < prices.Length){    
            //     while(left < right){
            //         if(prices[left] < prices[right]){
            //             maxProfit = Math.Max(maxProfit, prices[right] - prices[left]);
            //         }
            //         right--;
            //     }
            //     left++;
            //     right = prices.Length-1;
            // }

            // Console.WriteLine($"Maximum Profit is {maxProfit}");
            #endregion

            #region MinPrice Approach 
            if (prices == null || prices.Length == 0) return;

            int minPrice = prices[0], maxProfit = 0;
            for (int i = 1; i < prices.Length; i++)
            {
                if (prices[i] < minPrice)
                {
                    minPrice = prices[i];
                }
                else
                {
                    maxProfit = Math.Max(maxProfit, prices[i] - minPrice);
                }
            }

            Console.WriteLine($"Maximum Profit is {maxProfit}");

            #endregion
        }
        #endregion

        #region #5 Valid Palindrome
        /*
        DESCRIPTION :
        A phrase is a palindrome if, after converting all uppercase letters into lowercase letters and removing all non-alphanumeric characters, it reads the same forward and backward. Alphanumeric characters include letters and numbers.

        Given a string s, return true if it is a palindrome, or false otherwise.
        */
        /// <summary>
        /// Leetcode link : https://leetcode.com/problems/valid-palindrome/description/
        /// </summary>
        internal static void IsPalindrome(string s)
        {
            var alphaNumericString = new StringBuilder();

            s.ToCharArray().Where(c => char.IsLetterOrDigit(c))
            .ToList().ForEach(c => alphaNumericString.Append(char.ToLower(c)));

            Console.WriteLine(alphaNumericString.ToString() == new string(alphaNumericString.ToString().Reverse().ToArray()));
        }
        #endregion

        #region #6 InvertBinaryTree
        /*
        DESCRIPTION:
        Given the root of a binary tree, invert the tree, and return its root.
        */
        // <summary>
        /// Leetcode link : https://leetcode.com/problems/invert-binary-tree/
        /// </summary>        
        internal static void InvertBinaryTree(TreeNode root)
        {
            root = InvertBinaryTreeDepth(root);

            var q = new Queue<TreeNode>();
            q.Enqueue(root);
            while (q.Count > 0)
            {

                var itr = q.Dequeue();
                Console.WriteLine($"{itr.val} ->");
                if (itr.left != null) q.Enqueue(itr.left);
                if (itr.right != null) q.Enqueue(itr.right);

            }

        }

        internal static TreeNode InvertBinaryTreeDepth(TreeNode root)
        {
            if (root == null) return null;

            var right = InvertBinaryTreeDepth(root.right);
            var left = InvertBinaryTreeDepth(root.left);
            root.left = right;
            root.right = left;

            return root;
        }
        #endregion

        #region #7 Valid Anagrams
        /*
        DESCRIPTION:
        Given two strings s and t, return true if t is an 
        anagram of s, and false otherwise.
        */
        /// <summary>
        /// Leet code link : https://leetcode.com/problems/valid-anagram/
        /// </summary>
        internal static bool IsAnagram(string s, string t)
        {
            // if(s.Length != t.Length) return false;

            // var map = new int[26];

            // Array.Fill(map, 0);

            // for(int i = 0; i < s.Length; i++){
            //     map[s[i] - 'a']++;
            // }

            // for(int i =0; i < t.Length; i++){
            //     map[t[i] - 'a']--;
            //     if(map[t[i] - 'a'] < 0) return false;
            // }

            // return true;


            //Using List Sequence Approach
            var list1 = s.ToList();
            var list2 = t.ToList();

            list1.Sort(); list2.Sort();

            return list1.SequenceEqual(list2);
        }
        #endregion

        #region #8 Binary Search
        /*
        DESCRIPTION :
        Given an array of integers nums which is sorted in ascending order, and an integer target, 
        write a function to search target in nums. If target exists, then return its index. 
        Otherwise, return -1.

        You must write an algorithm with O(log n) runtime complexity.
        */
        /// <summary>
        /// Leetcode link : https://leetcode.com/problems/binary-search/
        /// </summary>
        internal static int Search(int[] nums, int target)
        {
            int left = 0, right = nums.Length - 1;
            while (left < right)
            {
                int mid = (left + right) / 2;
                if (nums[mid] == target) return mid;

                if (nums[mid] > target) right = mid - 1;
                else left = mid + 1;
            }

            return -1;
        }
        #endregion

        #region #9 FloodFill
        /*
        DESCRIPTION:
        An image is represented by an m x n integer grid image where image[i][j] represents the pixel value of the image.
        You are also given an integer sr and sc representing the starting pixel (sr, sc) 
        of the flood fill, and a pixel value newColor representing the new 
        color of the starting pixel.
        */
        /// <summary>
        /// Leetcode link : https://leetcode.com/problems/flood-fill/
        /// </summary>
        internal static void FloodFill(int[][] image, int sr, int sc, int color)
        {

            if (image[sr][sc] == color) return;

            var res = FloodFillDepth(image, sr, sc, image[sr][sc], color);
        }

        internal static int[][] FloodFillDepth(int[][] image, int sr, int sc, int oldColor, int newColor)
        {

            // if(sr < 0 || sc < 0 || sr >= image.Length || sc >= image[0].Length || image[sr][sc] != oldColor) return image;  

            // image[sr][sc] = newColor;
            // FloodFillDepth(image, sr + 1, sc, oldColor, newColor);
            // FloodFillDepth(image, sr - 1, sc, oldColor, newColor);
            // FloodFillDepth(image, sr, sc + 1, oldColor, newColor);
            // FloodFillDepth(image, sr, sc - 1, oldColor, newColor);
            // return image;

            image[sr][sc] = newColor;

            //top side
            if (sr > 0 && image[sr - 1][sc] == oldColor) FloodFillDepth(image, sr - 1, sc, oldColor, newColor);
            //bottom  side    
            if (sr < image.Length - 1 && image[sr + 1][sc] == oldColor) FloodFillDepth(image, sr + 1, sc, oldColor, newColor);
            //left side
            if (sc > 0 && image[sr][sc - 1] == oldColor) FloodFillDepth(image, sr, sc - 1, oldColor, newColor);
            //right side
            if (sc < image[0].Length - 1 && image[sr][sc + 1] == oldColor) FloodFillDepth(image, sr, sc + 1, oldColor, newColor);
            return image;

        }
        #endregion

        #region #10 LowestCommonAncestor of a Binary Tree
        /*
        DESCRIPTION : 
        Given a binary tree, find the lowest common ancestor (LCA) of two given nodes in the tree.

        According to the definition of LCA on Wikipedia: “The lowest common ancestor is defined between 
        two nodes p and q as the lowest node in T that has both p and q as descendants (where we allow a node to be a descendant of itself)
        */
        /// <summary>
        /// Leetcode link : https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-search-tree/
        /// </summary>
        internal static TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            // //Base case 
            // if( root == null || root.val == p. val || root.val == q.val)
            // {
            //     return root;
            // }

            // var left = LowestCommonAncestor(root.left, p, q);
            // var right = LowestCommonAncestor(root.right, p, q);

            // if(left == null) return right;
            // if(right == null) return left;

            // return root;

            //BST_Comparison - Approach

            // if(root == null) return null;

            // if(root.val > p.val && root.val > q.val) return LowestCommonAncestor(root.left,p,q);
            // if(root.val < p.val && root.val < q.val) return LowestCommonAncestor(root.right,p,q);

            // return root;

            //Simple Approach
            while (true)
            {
                if (root.val > q.val && root.val > p.val) root = root.left;
                else if (root.val < q.val && root.val < p.val) root = root.right;
                else break;
            }

            return root;

        }
        #endregion

        #region #11 Balanced Binary Tree
        /*
        DESCRIPTION : 
        Given a binary tree, determine if it is height-balanced.
        A height-balanced binary tree is a binary tree in which the depth of
        the two subtrees of every node never differs by more than one.
        */
        /// <summary>
        /// Leetcode link : https://leetcode.com/problems/balanced-binary-tree/
        /// </summary>
        internal static bool IsBalanced(TreeNode root)
        {

            // if(root == null) return true;

            // if (Math.Abs(CheckHeight(root.left) - CheckHeight(root.right)) > 1) return false;

            // return IsBalanced(root.left) && IsBalanced(root.right);

            if (root == null) return true;

            return CheckHeight(root) != -1;
        }

        private static int CheckHeight(TreeNode node)
        {
            // if(node == null)
            // {
            //     return 0;
            // }

            // return Math.Max(CheckHeight(node.left), CheckHeight(node.right)) + 1;

            if (node == null)
            {
                return 0;
            }

            int leftHeight = CheckHeight(node.left);
            if (leftHeight == -1) return -1;

            int rightHeight = CheckHeight(node.right);
            if (rightHeight == -1) return -1;

            if (Math.Abs(leftHeight - rightHeight) > 1) return -1;

            return Math.Max(leftHeight, rightHeight) + 1;
        }
        #endregion

        #region #12 LinkedList Cycle

        /*
        DESCRIPTION : 
        Given head, the head of a linked list, determine if the linked list has a cycle in it.

        There is a cycle in a linked list if there is some node in the list that can be reached 
        again by continuously following the next pointer. Internally, pos is used to denote the index 
        of the node that tail's next pointer is connected to. Note that pos is not passed as a parameter.

        Return true if there is a cycle in the linked list. Otherwise, return false.
        */
        /// <summary>
        /// Leetcode link : https://leetcode.com/problems/linked-list-cycle/
        /// </summary>
        internal static bool HasCycle(ListNode head)
        {
            var slowPointer = head.next;
            var fastPointer = head.next?.next;

            while (slowPointer != null && fastPointer != null)
            {

                if (slowPointer == fastPointer) return true;

                slowPointer = slowPointer.next;
                fastPointer = fastPointer.next?.next;

                if (fastPointer == null) break;
            }

            return false;
        }
        #endregion

        #region #13 Implement Queue using Stacks
        /*
        DESCRIPTION :
        Implement the queue data structure using the stack data structure or using list*/
        #endregion

        #region #14 FirstBad Version
        /*
        DESCRIPTION :
        You are a product manager and currently leading a team to develop a new product. 
        Unfortunately, the latest version of your product fails the quality check. 
        Since each version is developed based on the previous version, all the versions after a 
        bad version are also bad.

        Suppose you have n versions [1, 2, ..., n] and you want to find out the first bad one, 
        which causes all the following ones to be bad.

        You are given an API bool isBadVersion(version) which returns whether version is bad. 
        Implement a function to find the first bad version. You should minimize the number of 
        calls to the API.
        */
        internal static void FirstBadVersion(int n)
        {
            int left = 1, right = n;
            // while(left < right){
            //     int mid = left + (right - left) / 2;
            //    // if(isBadVersion(mid)) right = mid;
            //     else left = mid + 1;
            // }
            Console.WriteLine(left);
        }
        #endregion

        #region #15 RansomNote
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

        #region #16 Climbing Stairs
        /*
        DESCRIPTION :
        You are climbing a staircase. It takes n steps to reach the top.
        Each time you can either climb 1 or 2 steps. In how many distinct ways can you climb to the top?
        */
        /// <summary>
        /// Leetcode link : https://leetcode.com/problems/climbing-stairs/
        /// </summary>
        internal static int ClimbingStairs(int v)
        {
            var stairs = new int[v + 1];
            //Populate the array like fibonnaci series
            stairs[1] = 1; stairs[2] = 2;
            int val = 3;
            while (val <= v)
            {
                stairs[val] = stairs[val - 1] + stairs[val - 2];
                val++;
            }
            return stairs[v];
        }

        #endregion

        #region #17 LongestPalindrome
        /*
        DESCRIPTION :
        Given a string s which consists of lowercase or uppercase letters, return the length of the longest 
        palindrome that can be built with those letters.
        Letters are case sensitive, for example, "Aa" is not considered a palindrome.
        */
        internal static int LongestPalindrome(string s)
        {
            // var dict = s.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

            // int val = 0, odd = 0;

            // for(int i = 0; i< s.Length; i++){
            //     if(dict[s[i]] % 2 ==0) val++;
            //     else{
            //         odd++;
            //         if(odd <= 1) val++;
            //         dict[s[i]]--;
            //     }
            // }

            // return val;

            //Create a dictMap

            var map = new Dictionary<char, int>();

            foreach (char ch in s)
            {
                if (map.ContainsKey(ch)) map[ch] += 1;
                else map[ch] = 1;
            }

            int len = 0, oddLen = 0;

            foreach (var key in map.Keys)
            {
                while (map[key] >= 2)
                {
                    len += 2;
                    map[key] -= 2;
                }

                if (map[key] == 1)
                {
                    oddLen = 1;
                }
            }

            return len + oddLen;


        }
        #endregion

        #region #18 Reverse LinkedList

        /*
        DESCRIPTION : 
        Given the head of a singly linked list, reverse the list, and return the reversed list.
        */
        internal static ListNode ReverseList(ListNode head)
        {
            ListNode prev = null, curr = head, next = null;
            while (curr != null)
            {
                next = curr.next;
                curr.next = prev;
                prev = curr;
                curr = next;
            }
            return prev;
        }
        #endregion

        #region #19 Majority Element
        /*
        DESCRIPTION :
        Given an array nums of size n, return the majority element.

        The majority element is the element that appears more than ⌊n / 2⌋ times. 
        You may assume that the majority element always exists in the array.
        */
        internal static void MajorityElement(int[] nums)
        {
            var key = nums.GroupBy(key => key).Where(g => g.Count() > nums.Length / 2)
                .FirstOrDefault().Key;

            Console.WriteLine(key);
        }
        #endregion

        #region #20 Add Binary
        /*
        DESCRIPTION :
        Given two binary strings a and b, return their sum as a binary string.
        */
        /// <summary>
        /// Leetcode link : https://leetcode.com/problems/add-binary/
        /// </summary>
        internal static string AddBinary(string a, string b)
        {
            int maxLen = Math.Max(a.Length, b.Length);
            a = a.PadLeft(maxLen, '0');
            b = b.PadLeft(maxLen, '0');

            string res = "";
            int sum = 0, carry = 0;

            int idx = maxLen - 1;
            while (idx >= 0)
            {
                sum = carry;
                sum += a[idx] - '0';
                sum += b[idx] - '0';

                res += (sum % 2).ToString();
                carry = sum / 2;
                idx--;
            }
            if (carry != 0) res += '1';

            return string.Join("", res.Reverse());
        }
        #endregion

        #region #21 Diameter of Binary tree
        /*
        DESCRIPTION : Given the root of a binary tree, return the length of the diameter of the tree.

        The diameter of a binary tree is the length of the longest path between any two nodes in a tree. 
        This path may or may not pass through the root.

        The length of a path between two nodes is represented by the number of edges between them.
        */
        internal static int DiameterOfBinaryTree(TreeNode root)
        {
            int maxDiameter = 0;
            Diameter(root, ref maxDiameter);
            return maxDiameter;

            int Diameter(TreeNode root, ref int maxDiameter)
            {
                if (root == null) return 0;

                int left = Diameter(root.left, ref maxDiameter);
                int right = Diameter(root.right, ref maxDiameter);

                maxDiameter = Math.Max(maxDiameter, left + right);

                return Math.Max(left, right) + 1;
            }

            // if(root == null) return 0;

            // var left = DiameterOfBinaryTree(root.left);
            // var right = DiameterOfBinaryTree(root.right);

            // return Math.Max(left, right) + 1;
        }
        #endregion

        #region #22 Middle of LinkedList
        /*
        DESCRIPTION :
        Given the head of a singly linked list, return the middle node of the linked list.
        If there are two middle nodes, return the second middle node.
        */
        /// <summary>
        /// Leetcode link : https://leetcode.com/problems/middle-of-the-linked-list/
        /// </summary>
        internal static ListNode MiddleNode(ListNode head)
        {
            ListNode slow = head, fast = head;
            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }
            return slow;
        }

        #endregion

        #region #23 MaxDepth of Binary Tree
        /*
        DESCRIPTION :
        Given the root of a binary tree, return its maximum depth.

        A binary tree's maximum depth is the number of nodes along the longest
        path from the root node down to the farthest leaf node.
        */
        /// <summary>
        /// Leet code link : https://leetcode.com/problems/maximum-depth-of-binary-tree/
        /// </summary>
        internal static int MaxDepth(TreeNode root)
        {
            if (root == null) return 0;

            int left = MaxDepth(root.left);
            int right = MaxDepth(root.right);

            return Math.Max(left, right) + 1;

        }
        #endregion

        #region #24 Contains Duplicate
        /*
        DESCRIPTION :
        Given an integer array nums, return true if any value appears at least twice in the array, 
        and return false if every element is distinct.
        */
        /// <summary>
        /// Leetcode link : https://leetcode.com/problems/contains-duplicate/description/
        /// </summary>
        internal static void ContainsDuplicate(int[] nums)
        {
            #region HashSet Approach
            // var hashSet = new HashSet<int>();
            // foreach(var num in nums){
            //     if(hashSet.Contains(num)){
            //         Console.WriteLine("True");
            //         break;
            //     }
            //     hashSet.Add(num);
            // }
            // Console.WriteLine("False");
            #endregion

            #region Distinct Approach
            Console.WriteLine(nums.Length != nums.Distinct().Count());
            #endregion
        }
        #endregion

        #region #25 Maximum SubArray
        /*
        DESCRIPTION:
        Given an integer array nums, find the subarray
        with the largest sum, and return its sum.
        */
        internal static int MaxSubArray(int[] nums)
        {
            int maxSum = int.MinValue, currentSum = 0;

            foreach (int num in nums)
            {
                currentSum = Math.Max(num, currentSum + num);
                maxSum = Math.Max(maxSum, currentSum);
            }

            return maxSum;
            // //use two pointer approach to get the subarray with the largest sum
            // int len = nums.Length, l = 0, r = 0, maxSum = int.MinValue, sum = 0, maxL = 0, maxR = 0;

            // while(r < len){
            //     sum += nums[r];

            //     if(sum > maxSum){
            //         maxSum = sum;
            //         maxL = l;
            //         maxR = r;
            //     }
            //     else{
            //         sum -= nums[l];
            //         l++;
            //     }

            //     r++;
            // }

            // //This functino returns the subarray with maximum sum.
            // //The actual problem is to return just max sum
            // return nums.Skip(maxL).Take(maxR - maxL + 1).ToArray();

        }

        #endregion


        #region #27 01 Matrix
        /*
        DESCRIPTION :
        Given a matrix of binary digits, return the distance of nearest 0 for each cell in the matrix .
        */
        internal static int[][] UpdateMatrix(int[][] matrix)
        {
            int m = matrix.Length;
            int n = matrix[0].Length;

            int[][] res = new int[m][];

            //Create a matrix with each cell value assigned to int.MaxValue.
            for (int i = 0; i < m; i++)
            {
                res[i] = new int[n];
                for (int j = 0; j < n; j++)
                {
                    res[i][j] = int.MaxValue;
                }
            }


            //First Top-Left Approach
            int rowIdx = 0, colIdx = 0;
            while (rowIdx < m)
            {
                colIdx = 0;
                while (colIdx < n)
                {
                    //Step 1 : Check for cell value is 0.
                    if (matrix[rowIdx][colIdx] == 0)
                    {
                        res[rowIdx][colIdx] = 0;
                    }

                    //Step 2 : If not 0, then check for min value of top or left call value + 1.
                    else if (colIdx > 0 && rowIdx > 0)
                    {
                        res[rowIdx][colIdx] = Math.Min(res[rowIdx][colIdx - 1], res[rowIdx - 1][colIdx]) + 1;
                    }
                    else if (colIdx > 0)
                    {
                        res[rowIdx][colIdx] = Math.Min(res[rowIdx][colIdx - 1] + 1, res[rowIdx][colIdx]);
                    }
                    else if (rowIdx > 0)
                    {
                        res[rowIdx][colIdx] = Math.Min(res[rowIdx - 1][colIdx] + 1, res[rowIdx][colIdx]);
                    }
                    colIdx++;
                }
                rowIdx++;
            }

            //Bottom - Right Approach for ensuring the correctness ofthe minimum distance
            rowIdx = m - 1;
            while (rowIdx >= 0)
            {
                colIdx = n - 1;
                while (colIdx >= 0)
                {
                    if (res[rowIdx][colIdx] != 0)
                    {
                        if (rowIdx < m - 1 && colIdx < n - 1)
                        {
                            res[rowIdx][colIdx] = Math.Min(res[rowIdx][colIdx],
                            Math.Min(res[rowIdx + 1][colIdx], res[rowIdx][colIdx + 1]) + 1);
                        }
                        else if (rowIdx < m - 1)
                        {
                            res[rowIdx][colIdx] = Math.Min(res[rowIdx][colIdx], res[rowIdx + 1][colIdx] + 1);
                        }
                        else if (colIdx < n - 1)
                        {
                            res[rowIdx][colIdx] = Math.Min(res[rowIdx][colIdx], res[rowIdx][colIdx + 1] + 1);
                        }

                    }
                    colIdx--;
                }
                rowIdx--;
            }

            return res;
        }
        #endregion

        #region #49 Sort Colors
        /*
        DESCRIPTION : Leetcode LInk : https://leetcode.com/problems/sort-colors/description/
        Given an array nums with n objects colored red, white, or blue, sort them in-place so that objects of the same color are adjacent, with the colors in the order red, white, and blue.

        We will use the integers 0, 1, and 2 to represent the color red, white, and blue, respectively.
        */
        internal static int[] SortColors(int[] nums)
        {
            #region - Using Dict
            // var mp = new Dictionary<int,int>();
            // mp[0] = 0; mp[1] = 0; mp[2] = 0;

            // foreach(int el in nums)
            // {
            //     mp[el] += 1;
            // }

            // int idx = 0, len = nums.Length;
            // foreach(var kvp in mp)
            // {
            //     int count = kvp.Value;
            //     while(count > 0 && idx < len){
            //         nums[idx] = kvp.Key;
            //         count--;
            //         idx++;
            //     }
            // }

            #endregion

            #region - Using three pointers
            //Create pointers to hold the position of 0, 1, 2
            int low = 0, mid = 0, high = nums.Length;

            while (mid < high)
            {
                if (nums[mid] == 0)
                {
                    int temp = nums[low];
                    nums[low] = nums[mid];
                    nums[mid] = temp;
                    low++;
                    mid++;
                }
                else if (nums[mid] == 1)
                {
                    mid++;
                }
                else if (nums[mid] == 2)
                {
                    int temp = nums[high - 1];
                    nums[high - 1] = nums[mid];
                    nums[mid] = temp;
                    high--;
                }
            }


            #endregion

            return nums;

        }
        #endregion


        #region HouseRobber
        /*
        Leetcode link : https://leetcode.com/problems/house-robber/
        */
        internal static int Rob(int[] nums)
        {
            int len = nums.Length;
            int prev2 = 0;
            int prev = nums[0];
            for (int i = 1; i < len; i++)
            {
                int take = nums[i];
                if (i > 1) take += prev2;

                int curr = Math.Max(take, prev);
                prev2 = prev;
                prev = curr;
            }
            return prev;
        }
        #endregion

        #region HouseRobber- II
        /*
        Leetcode link : https://leetcode.com/problems/house-robber-ii/description/
        */
        internal static int RobHouse(int[] nums)
        {

            //First and last elements cannot be together, since they are connected in circular fashion
            //Edge case
            if (nums.Length == 1) return nums[0];
            //Ideally construct two sums, leaving last element and first element of the array separately
            //Get the max of the above two computations.
            //Step 1 : Computing without last element of the array
            int firstComp = Rob(nums.Take(nums.Length - 1).ToArray());

            //Step 2 : Computing without first element of the array
            int secondComp = Rob(nums.Skip(1).ToArray());

            return Math.Max(firstComp, secondComp);
        }
        #endregion

        #region UniquePaths
        /*
        Leetcode link : https://leetcode.com/problems/unique-paths/description/
        */
        internal static int UniquePaths(int m, int n)
        {
            //Start from the bottom of the matrix and workout the way till origin (0,0)
            //Step 1 : Create a dp with m x n
            #region Recursion_Memoization Method           
            // int[][] dp = new int[m][];
            // for (int i = 0; i < m; i++)
            // {
            //     dp[i] = new int[n];
            //     Array.Fill(dp[i], -1);
            // };

            // return UniquePathRecursive(m - 1, n - 1, dp);

            #endregion

            #region Tabulation Approach - Bottom Up
            int[][] dp = new int[m][];
            for (int i = 0; i < m; i++)
            {
                dp[i] = new int[n];
            };

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == 0 && j == 0) dp[i][j] = 1;
                    else
                    {
                        int up = 0, left = 0;
                        if (j > 0) left = dp[i][j - 1];
                        if (i > 0) up = dp[i - 1][j];
                        dp[i][j] = up + left;
                    }
                }
            }

            return dp[m - 1][n - 1];
            #endregion
        }

        internal static int UniquePathRecursive(int m, int n, int[][] dp)
        {
            //Base case
            if (m == 0 && n == 0) return 1;
            if (m < 0 || n < 0) return 0;

            //Check in the dp, if there's value 
            if (dp[m][n] != -1) return dp[m][n];

            var up = UniquePathRecursive(m - 1, n, dp);
            var left = UniquePathRecursive(m, n - 1, dp);
            return dp[m][n] = up + left;
        }
        #endregion

        #region Minimum Path Sum
        /*
        DESCRIPTION : Leetcode link : https://leetcode.com/problems/minimum-path-sum/
        */
        internal static int MinPathSum(int[][] grid)
        {
            #region Recursion_Memoization method
            // int m = grid.Length, n = grid[0].Length;
            // int[][] dp = new int[m][];
            // for (int i = 0; i < m; i++)
            // {
            //     dp[i] = new int[n];
            //     Array.Fill(dp[i], -1);
            // }

            // return MinPathSumRec(grid, m - 1, n - 1, dp);
            #endregion

            #region Tabulation Approach - Bottom up
            int m = grid.Length, n = grid[0].Length;
            int[][] dp = new int[m][];
            for (int i = 0; i < m; i++)
            {
                dp[i] = new int[n];
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int up = 50000, left = 50000;
                    if (i == 0 && j == 0) dp[i][j] = grid[i][j];
                    else
                    {
                        if (i > 0) up = grid[i][j] + dp[i - 1][j];
                        if (j > 0) left = grid[i][j] + dp[i][j - 1];
                        dp[i][j] = Math.Min(up, left);
                    }
                }
            }

            return dp[m - 1][n - 1];
            #endregion
        }

        internal static int MinPathSumRec(int[][] grid, int i, int j, int[][] dp)
        {
            //Base case
            if (i == 0 && j == 0) return grid[i][j];
            if (i < 0 || j < 0) return 50000;

            if (dp[i][j] != -1) return dp[i][j];
            int up = grid[i][j] + MinPathSumRec(grid, i - 1, j, dp);
            int left = grid[i][j] + MinPathSumRec(grid, i, j - 1, dp);

            return dp[i][j] = Math.Min(up, left);

        }

        #endregion

        #region Minimum Falling PathSum
        /*
        Leetcode link : https://leetcode.com/problems/minimum-falling-path-sum/description/
        */
        internal static int MinFallingPathSum(int[][] mat)
        {
            int m = mat.Length, n = mat[0].Length;
            #region Recursion_Memoization Approach
            // int[][] dp = new int[m][];
            // for (int i = 0; i < m; i++)
            // {
            //     dp[i] = new int[n];
            //     Array.Fill(dp[i], -1);
            // }

            // int res = 50000;
            // for (int j = 0; j < n; j++)
            // {
            //     res = Math.Min(res, MinFallingPathSumRec(mat, m - 1, j, dp));
            // }

            // return res;
            #endregion

            #region Tabulation Approach
            int[][] dp = new int[m][];
            for (int i = 0; i < m; i++)
            {
                dp[i] = new int[n];
                Array.Fill(dp[i], 0);
            }

            for (int j = 0; j < n; j++)
            {
                dp[0][j] = mat[0][j];
            }

            for (int i = 1; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int up = mat[i][j] + dp[i - 1][j];
                    int ld = mat[i][j];
                    if (j - 1 >= 0) ld += dp[i - 1][j - 1];
                    else ld += 50000;
                    int rd = mat[i][j];
                    if (j + 1 < m) rd += dp[i - 1][j + 1];
                    else rd += 50000;

                    dp[i][j] = Math.Min(Math.Min(up, ld), rd);
                }
            }

            int res = 50000;
            for (int j = 0; j < n; j++)
            {
                res = Math.Min(res, dp[m - 1][j]);
            }

            return res;
            #endregion
        }

        internal static int MinFallingPathSumRec(int[][] mat, int i, int j, int[][] dp)
        {
            if (j < 0 || j >= mat[0].Length) return 50000;
            if (i == 0) return mat[0][j];

            if (dp[i][j] != -1) return dp[i][j];
            int up = mat[i][j] + MinFallingPathSumRec(mat, i - 1, j, dp);
            int ld = mat[i][j] + MinFallingPathSumRec(mat, i - 1, j - 1, dp);
            int rd = mat[i][j] + MinFallingPathSumRec(mat, i - 1, j + 1, dp);

            return dp[i][j] = Math.Min(Math.Min(up, ld), rd);
        }
        #endregion

        #region CoinChange II
        /*
        Leetcode link : https://leetcode.com/problems/coin-change-ii/
        */

        internal static int CoinChangeII(int amount, int[] coins)
        {
            // int[][] dp = new int[coins.Length][];
            // for (int i = 0; i < coins.Length; i++)
            // {
            //     dp[i] = new int[amount + 1];
            //     Array.Fill(dp[i], -1);
            // }
            // return CoinChangeIIRec(coins, amount, coins.Length - 1, dp);


            int[] dp = new int[amount + 1];
            dp[0] = 1;

            foreach (int coin in coins)
            {
                for (int j = coin; j <= amount; j++)
                {
                    dp[j] += dp[j - coin];
                }
            }

            return dp[amount];
        }

        internal static int CoinChangeIIRec(int[] a, int amount, int idx, int[][] dp)
        {
            if (idx == 0)
            {
                return amount % a[0] == 0 ? 1 : 0;
            }
            if (dp[idx][amount] != -1) return dp[idx][amount];

            int notTake = 0 + CoinChangeIIRec(a, amount, idx - 1, dp);
            int take = 0;
            if (a[idx] <= amount) take = CoinChangeIIRec(a, amount - a[idx], idx, dp);

            return dp[idx][amount] = notTake + take;
        }
        #endregion

        #region RottenOranges
        /*
        Leetcode link : https://leetcode.com/problems/rotting-oranges/description/
        */
        internal static int RottenOranges(int[][] grid)
        {
            //BFS approach for the grid.
            //Step 1 : Define a visited grid of the same size as the original grid.
            //Step 2 : Define a queue to take up the indexes and the time at which they are rotten.

            int m = grid.Length;
            int n = grid[0].Length;
            int freshCount = 0;
            int[][] visited = new int[m][];
            for (int i = 0; i < m; i++)
            {
                visited[i] = new int[n];
            }

            Queue<Pair> qu = new Queue<Pair>();

            //Get the list of rotten oranges at the beginning.
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] == 2)
                    {
                        qu.Enqueue(new Pair(i, j, 0));
                        visited[i][j] = 2;
                    }

                    if (grid[i][j] == 1) freshCount++;
                }
            }

            int minT = 0, cnt = 0;
            int[] dRow = new int[] { -1, 0, 1, 0 };
            int[] dCol = new int[] { 0, 1, 0, -1 };
            while (qu.Count > 0)
            {
                int r = qu.Peek().row;
                int c = qu.Peek().col;
                int t = qu.Peek().tm;
                minT = Math.Max(t, minT);
                qu.Dequeue();
                //Write a loop for checking 4-directional oranges.
                for (int i = 0; i < 4; i++)
                {
                    int nrow = r + dRow[i];
                    int ncol = c + dCol[i];

                    if (nrow >= 0 && nrow < m && ncol >= 0 && ncol < n
                    && visited[nrow][ncol] != 2 && grid[nrow][ncol] == 1)
                    {
                        qu.Enqueue(new Pair(nrow, ncol, t + 1));
                        visited[nrow][ncol] = 2;
                        cnt++;
                    }
                }

            }

            if (cnt != freshCount) return -1;

            return minT;
        }

        #endregion

        #region BreakPalindrome

        /*
        DESCRIPTION:
       Given a palindromic string of lowercase English letters palindrome, replace exactly one character with any lowercase 
       English letter so that the resulting string is not a palindrome and that it is the lexicographically smallest one possible.

        Return the resulting string. If there is no way to replace a character to make it not a palindrome, return an empty string.

        A string a is lexicographically smaller than a string b (of the same length) if in the first position where a 
        and b differ, a has a character strictly smaller than the corresponding character in b. For example, "abcc" is lexicographically 
        smaller than "abcd" because the first position they differ is at the fourth character, and 'c' is smaller than 'd'.
        */

        /// <summary>
        /// Leetcode link : https://leetcode.com/problems/break-a-palindrome/
        /// </summary>
        internal static void BreakPalindrome(string palindrome)
        {
            if (palindrome.Length == 1)
            {
                Console.WriteLine("EmptyString");
                return;
            }

            var chArr = palindrome.ToArray();
            int idx = 0, len = chArr.Length;

            while (idx <= len / 2)
            {
                if (chArr[idx] != 'a')
                {
                    chArr[idx] = 'a';
                    break;
                }
                idx++;
            }

            //If the input palindrome and the chArr are same, then the palindrome string has only 'a'. replace it with b to get the smallest string
            if (string.Equals(palindrome, string.Concat(chArr)))
            {
                chArr[len - 1] = 'b';
            }
            else if (string.Equals(new string(chArr), new string(chArr.Reverse().ToArray())))
            {
                chArr = palindrome.ToCharArray();
                chArr[len - 1] = 'b';
            }

            Console.WriteLine(string.Concat(chArr));

        }
        #endregion


        #region Longest Substring Without Repeating Characters
        /*
        DESCRIPTION :
        Given a string s, find the length of the longest substring  without repeating characters.
        */
        internal static int LengthOfLongestSubstring(string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;

            int len = s.Length, l = 0, r = 0, maxLen = 0;

            var map = new int[256];
            Array.Fill(map, -1);

            while (r < len)
            {
                int idx = s[r];

                if (map[idx] != -1 && l <= map[idx])
                {
                    l = map[idx] + 1;
                }

                map[idx] = r;

                maxLen = Math.Max(maxLen, r - l + 1);

                r++;
            }

            return maxLen;
        }
        #endregion

        #region Container with Most Water
        /*
        DESCRIPTION:
        You are given an integer array height of length n. There are n vertical lines drawn such that the two endpoints of
        the ith line are (i, 0) and (i, height[i]).

        Find two lines that together with the x-axis form a container, such that the container contains the most water.
        Return the maximum amount of water a container can store.

        Notice that you may not slant the container.
        */
        /// <summary>
        /// Leetcode link : https://leetcode.com/problems/container-with-most-water/
        /// </summary>
        internal static int ContainerWithMostWater(int[] height)
        {
            int maxVolume = Int32.MinValue, len = height.Length, left = 0, right = len - 1;

            while (left < len && right >= 0 && left < right)
            {

                maxVolume = Math.Max(maxVolume, Math.Min(height[left], height[right]) * (right - left));

                if (height[left] > height[right]) right--;
                else if (height[left] <= height[right]) left++;
            }

            return maxVolume;
        }
        #endregion

        #region Daily Temperature
        /*
        DESCRIPTION : 
        Given an array of integers temperatures represents the daily temperatures, return an array answer such that answer[i] 
        is the number of days you have to wait after the ith day to get a warmer temperature. 
        If there is no future day for which this is possible, keep answer[i] == 0 instead.
        */
        internal static int[] DailyTemperature(int[] temperatures)
        {
            // var res = new List<int>();
            // int len = temperatures.Length, idx = 0;

            // while(idx< len){
            //     int mvgIdx = idx+1;
            //     while(mvgIdx < len && temperatures[mvgIdx] <= temperatures[idx])
            //     {
            //         mvgIdx++;
            //     }

            //     if(mvgIdx != len) res.Add(mvgIdx - idx);

            //     idx++;
            // }

            // return res;

            //Remembering the days with lowest temperature with the help of a stack
            int len = temperatures.Length, idx = 0;
            int[] res = new int[len];
            Stack<int> st = new();
            while (idx < len)
            {
                while (st.Count > 0 && temperatures[idx] > temperatures[st.Peek()])
                {
                    int top = st.Pop();
                    res[top] = idx - top;
                }

                st.Push(idx);
                idx++;
            }



            return res;

        }
        #endregion

        #region MergeIntervals
        /*
        DESCRIPTION : 
        Given an array of intervals where intervals[i] = [starti, endi], merge all overlapping intervals, 
        and return an array of the non-overlapping intervals that cover all the intervals in the input.
        */
        internal static int[][] MergeIntervals(int[][] intervals)
        {
            var res = new List<int[]>();
            int len = intervals.Length, start = 0, end = 0;

            Array.Sort(intervals, (x, y) => x[0].CompareTo(y[0]));

            for (int i = 0; i < len; i++)
            {
                start = intervals[i][0];
                end = intervals[i][1];

                if (res.Count > 0 && res[res.Count - 1][1] >= end) continue;

                for (int j = i + 1; j < len; j++)
                {
                    if (intervals[j][0] <= end)
                    {
                        end = Math.Max(end, intervals[j][1]);
                    }
                    else break;
                }

                res.Add(new int[] { start, end });

            }
            return res.ToArray();
        }
        #endregion

        #region RemoveNthnodeFromLinkedList
        /*DESCRIPTION :
        Given the head of a linked list, remove the nth node from the end of the list and return its head.
        */
        internal static ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            //Approach 1 : Compute the height and then remove the node at that height
            // ListNode node = head;
            // int len = 0;
            // while(node != null)
            // {
            //     len++;
            //     node = node.next;
            // }

            // int counter = 0;
            // var dummyNode = new ListNode(-1);
            // dummyNode.next = head;
            // node = dummyNode;
            // while(node != null)
            // {
            //     if(len - n == counter)
            //     {
            //         node.next = node.next?.next;
            //         break;
            //     }
            //     counter++;
            //     node = node.next;
            // }

            // return dummyNode.next;

            //Approach 2 : Using Stacks
            ListNode node = head;
            Stack<ListNode> st = new();
            while (node != null)
            {
                st.Push(node);
                node = node.next;
            }

            for (int i = 0; i < n; i++) st.Pop();
            if (st.Count != 0) st.Peek().next = st.Peek().next?.next;
            else head = head.next;

            return head;
        }
        #endregion


        #region LRU Cache
        /*
        DESCRIPTION :
        Design a data structure that follows the constraints of a Least Recently Used (LRU) cache.

        Implement the LRUCache class:

        LRUCache(int capacity) Initialize the LRU cache with positive size capacity.
        int get(int key) Return the value of the key if the key exists, otherwise return -1.
        void put(int key, int value) Update the value of the key if the key exists. Otherwise, add the key-value pair to the cache. If the number of keys exceeds the capacity from this operation, evict the least recently used key.
        The functions get and put must each run in O(1) average time complexity.
        */
        /// <summary>
        /// Leetcode link : https://leetcode.com/problems/lru-cache/description/ 
        /// </summary>
        internal static void LRUCacheImpl()
        {
            var lruCache = new LRUCache(4);
            lruCache.put(1, 1);
            lruCache.put(2, 2);
            lruCache.put(3, 3);
            lruCache.put(4, 4);
            lruCache.get(1);
            lruCache.Display();
            lruCache.put(5, 5);
            lruCache.Display();
            lruCache.get(1);
            lruCache.Display();
            lruCache.get(2);
            lruCache.get(3);
            lruCache.Display();
            lruCache.get(4);
            lruCache.get(5);
            lruCache.Display();
        }
        #endregion

        #region Fibonacci Memoization
        /*
        DESCRIPTION :
        Given a number find the fibonacci of that number using memoization
        */
        /// <summary>
        /// Leetcode link : https://leetcode.com/problems/fibonacci-number/
        /// </summary>
        static Dictionary<int, int> memo = new Dictionary<int, int>();
        internal static int FibonacciMemoization(int n)
        {
            if (memo.ContainsKey(n)) return memo[n];
            if (n <= 1) return n;
            int result = FibonacciMemoization(n - 1) + FibonacciMemoization(n - 2);
            memo[n] = result;
            return result;
        }
        #endregion

        #region Merge Sort
        /*
        DESCRIPTION :
        Given an array of integers, sort the array using merge sort
        */
        /// <summary>
        /// Leetcode link : https://leetcode.com/problems/sort-an-array/
        /// </summary>
        internal static void MergeSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int m = left + (right - left) / 2;

                MergeSort(arr, left, m);
                MergeSort(arr, m + 1, right);

                Merge(arr, left, m, right);
            }
        }

        private static void Merge(int[] arr, int left, int m, int right)
        {
            int n1 = m - left + 1;
            int n2 = right - m;

            int[] leftArr = new int[n1];
            int[] rightArr = new int[n2];

            Array.Copy(arr, left, leftArr, 0, n1);
            Array.Copy(arr, m + 1, rightArr, 0, n2);

            int i = 0, j = 0;
            int k = left;
            while (i < n1 && j < n2)
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

            while (i < n1)
            {
                arr[k] = leftArr[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                arr[k] = rightArr[j];
                j++;
                k++;
            }
        }
        #endregion

    }

    class Pair
    {
        public int row { get; set; }
        public int col { get; set; }
        public int tm { get; set; }

        public Pair(int r, int c, int t)
        {
            row = r;
            col = c;
            tm = t;
        }
    }
}