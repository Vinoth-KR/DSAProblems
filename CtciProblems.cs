using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DSAProblems
{
    public class CtciProblems 
    {

        #region Permutations of a string
        public static void Permutations(string str)
        {
            Permutation(str, string.Empty);
        }

        static int permCount = 1;
        private static void Permutation(string str, string prefix)
        {
            if(str.Length == 0 ){
                Console.WriteLine($"{prefix}-{permCount}");
                permCount++;               
            }
            else 
            {
                for(int i = 0; i< str.Length; i++){
                    string rem = str.Substring(0,i) + str.Substring(i+1);
                    Permutation(rem, prefix+ str[i]);
                }
            }
        }
        #endregion

        public static int power(int a, int b)
        {
            if(b < 0) return 0;
            else if( b == 0 ) return 1;
            else return a * power(a, b-1);
        }

        public static bool IsUnique(string str)
        {
            //sorted string and checking for ASCII code - O(N log N)
            #region Sorted String and ASCII code check
            // char[] charArr = str.ToCharArray();

            // Array.Sort(charArr);

            // int len = charArr.Length;
            // int i = 0;

            // while( i< len-1)
            // {
            //     if(charArr[i] == charArr[i+1]) return false;

            //     i++;

            // }

            // return true;

            #endregion


            #region Using Boolean Array. - Extended ASCI - UTF-8 Encoder

           const int MAX_LEN = 256;
           int arrLen = str.Length;

            //If the length of the array is more than the Extended ASCII character length, then some characters would've repeated.
            if(arrLen > MAX_LEN ) return false;

           bool[] bFlags = new bool[MAX_LEN];
           
           int idx = 0;
           while(idx < arrLen)
           {
            //Getting the ASCII - Code for the character
              int asciiVal = str[idx];
            //Checking the flag for the obtained ASCII code.
             if(bFlags[asciiVal]) return false;
            
             bFlags[asciiVal] = true;
           }
            //Retuns true, when there's no repeated character.
            return true;

            #endregion
        }
    
        public static bool IsPermutation(string str1, string str2)
        {
            Dictionary<char,int> map = new Dictionary<char, int>();

            foreach(char ch in str1)
            {
                if(map.ContainsKey(ch)) map[ch] += 1;
                else map[ch] = 1;                
            }

            foreach(char ch in str2){
                if(!map.ContainsKey(ch)) return false;
                else
                {
                    if(map[ch] == 0) return false;

                    map[ch] -= 1;
                }  
            }

            return true;
        }

        public static void URLify(string val, int trueLen)
        {
            //C# Inbuilt method.
            //val = val.Replace(" ", "%20");

            // int idx = 0;

            // StringBuilder sb = new StringBuilder();

            // while(idx < val.Length)
            // {
            //     if(val[idx]==' ') sb.Append("%20");
            //     else  sb.Append(val[idx]);

            //     idx++;
            // }

            // System.Console.WriteLine(sb.ToString());

            char[] chAr = val.ToCharArray();

            int count = 0, idx = 0;

            for(int i =0; i < trueLen; i++)
            {
                if(chAr[i] == ' ') count++;
            }   

            idx = trueLen + count * 2;

            if(trueLen < chAr.Length) chAr[trueLen] = '\0';

            for(int i = trueLen - 1; i >= 0; i--)
            {
                if(chAr[i] == ' '){
                    chAr[idx-1] = '0';
                    chAr[idx-2] = '2';
                    chAr[idx-3] = '%';
                    idx -= 3; 
                }else 
                {
                    chAr[idx-1] = chAr[i];
                    idx -= 1;
                }
            }

            var res = new string(chAr);

            System.Console.WriteLine(res);

        }

        public static bool PalindromePermutation(string str)
        {
            var chAr = str.ToLower().ToCharArray();

            Dictionary<char, int> dictMap = new Dictionary<char, int>();

            int len = chAr.Length, idx = 0;
            
            while(idx < len)
            {
                if(chAr[idx]== ' ') 
                {
                    idx++;
                    continue;
                }

                if(dictMap.ContainsKey(chAr[idx])) dictMap[chAr[idx]] += 1;
                else dictMap[chAr[idx]] = 1;

                idx++;
            }

            //Check the value stored in the Map and look out for more than one odd value for any key
            var oneOdd = dictMap.Where(x => x.Value == 1).Count();

            if(oneOdd <= 1 ) return true;

            return false;
        }

        internal static bool OneEditAway(string v1, string v2)
        {
            if(Math.Abs(v1.Length-v2.Length) > 1) return false;
            
            //Taking the length of the shortest string
            // int idx = 0, idx2 = 0, len = v2.Length < v1.Length ? v2.Length : v1.Length;
            // bool oneEditFound = false;

            // while(idx < len && idx2 < len)
            // {
            //     if(v1[idx] != v2[idx2])
            //     {
            //         if(oneEditFound) return false;
            //         oneEditFound = true;

            //         if(v1.Length == v2.Length) idx2++; 
            //     }else {
            //         idx2++;
            //     }

            //     idx++;
            // }


            Dictionary<char,int> dict = new Dictionary<char, int>();

            string lStr = v1.Length > v2.Length ? v1 : v2;
            string sStr = v1.Equals(lStr) ? v2 : v1;

            int len = lStr.Length, idx = 0;

            while(idx < len)
            {
                if(dict.ContainsKey(lStr[idx])) dict[lStr[idx]] += 1;
                else dict[lStr[idx]] = 1;
                idx++;
            }

            idx = 0; len = sStr.Length;
            while(idx < len)
            {
                if(dict.ContainsKey(sStr[idx])) dict[sStr[idx]] -= 1;
                idx++;
            }

            int c = dict.Where(itm => itm.Value != 0).Count();

            if(c < 2) return true;

           return false;
        }

        internal static string StringCompression(string str){
            StringBuilder sb = new StringBuilder();
            int idx = 1, count = 1, len = str.Length;

            char ch = str[0];

            while(idx < len)
            {
                if(ch == str[idx])
                    count++;
                else
                {
                    sb.Append(String.Format("{0}{1}",ch,count));

                    ch = str[idx];
                    count = 1;

                     
                }

                idx++; 
            }

            //Last Index check
            sb.Append(String.Format("{0}{1}",ch,count));

            string res = sb.ToString();

            return res.Length > str.Length ? str : res;
        }


        internal static int[][] Rotate90deg(int[][] matrix)
        {

            #region Reverse&Transpose

            // int len = matrix.Length;
            // //Reversing each element the matrix
            // ReverseMatrix(matrix);


            // //Transposing the matrix
            // int section = 0;
            // while(section < len){
            //     int element = section+1;
            //     while(element < len){
            //         int temp = matrix[section][element];
            //         matrix[section][element] = matrix[element][section];
            //         matrix[element][section] = temp;
            //         element++;
            //     }
            //     section++;
            // }

            #endregion

            #region CTCI Approach

            int sect = 0, len = matrix.Length;

            while(sect < len/2){
                int start = sect, end = len-1-sect;
                int idx = start;
                while(idx < end){
                    int mvgIdx = idx-start;
                    
                    int temp = matrix[start][idx];
                    
                    //top[i] = left[i]
                    matrix[start][idx] = matrix[end-mvgIdx][start];
                    
                    //left = bottom
                    matrix[end-mvgIdx][start] = matrix[end][end-mvgIdx];

                    //bottom -> right
                    matrix[end][end-mvgIdx] = matrix[idx][end];

                    //right-> temp(Top)
                    matrix[idx][end] = temp;

                    idx++;

                }
                sect++;
            }



            #endregion

            return matrix;
        }


        private static int[][] ReverseMatrix(int[][] matrix)
        {
            int len = matrix.Length;

             int idx = 0, endIdx = len-1;

            while(idx < endIdx){
                int[] temp = matrix[idx];
                matrix[idx] = matrix[endIdx];
                matrix[endIdx] = temp;

                idx++; endIdx--;
            }


            return matrix;
        }

        private static int[] ReverseIntegerArr(int[] arr){
            int len = arr.Length;

            int idx = 0, endIdx = len-1;

            while(idx < endIdx){
                int temp = arr[idx];
                arr[idx] = arr[endIdx];
                arr[endIdx] = temp;

                idx++; endIdx--;
            }

            return arr;
        }


        internal static int[][] ZeroMatrix(int[][] matrix)
        {
            int M = matrix.Length;
            int N = matrix[0].Length;

            int rowIdx =0, colIdx = 0;

            List<Tuple<int, int>> coords = new List<Tuple<int, int>>();

            while(rowIdx < M){
                while(colIdx < N){
                    if(matrix[rowIdx][colIdx] == 0) 
                        coords.Add(new Tuple<int, int>(rowIdx, colIdx));
                    
                    colIdx++;
                }
                rowIdx++; colIdx = 0;
            }    

            foreach(var points in  coords){
                int row = points.Item1;
                int col = points.Item2;

                rowIdx = 0; 
                while(rowIdx < M){
                    matrix[rowIdx][col] = 0;
                    rowIdx++;
                }

                colIdx = 0;
                while(colIdx < N){
                    matrix[row][colIdx] = 0;
                    colIdx++;
                }
            }

            return matrix;
        }
    
        internal static bool RotateString(string s, string goal)
        {
             if(s.Length != goal.Length) return false;

            int mask = 0, bitPos = 0;

            int len = s.Length, idx = 0; 
            while(idx < len){
                bitPos = s[idx] -'a'-1;
                mask |= (1 << bitPos);
                idx++;
            }

            len = goal.Length; idx = 0;
            while(idx < len){
                bitPos = goal[idx]-'a'-1;
                mask &= ~(1 << bitPos);
                idx++;
            }

            if(mask == 0) return true;

             return false;
        }
    }
}