using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DSAProblems
{
    class LeetcodeProblems
    {

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



    }
}