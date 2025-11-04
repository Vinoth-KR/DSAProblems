using System;

public static class TufProblems
{
    public static void ExecuteProblems()
    {
        FindSecondLargestAndSmallest(new int[] { 4, 67, 23, 89, 2, 90, 45 });
    }


    #region Step 2 : Arrays Problems
    #region Step 2.1 : Easy Level Problems

    #region 1. Find the largest element in an array
    public static void FindLargestElementInArray(int[] arr)
    {
        //int[] arr = { 34, 67, 23, 89, 2, 90, 45 };
        int largest = arr[0];

        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] > largest)
            {
                largest = arr[i];
            }
        }

        Console.WriteLine("The largest element in the array is: " + largest);
    }

    #endregion

    #region 2. Find second largest and second smallest elements in an array without sorting
    public static void FindSecondLargestAndSmallest(int[] arr)
    {

        int len = arr.Length;
        if (len < 3)
        {
            Console.WriteLine($"Second Smallest Element : -1, Second Largest Element : -1");
            return;
        }

        int i = 0;
        int currMax = Int32.MinValue, prevMax = Int32.MinValue;
        int currMin = Int32.MaxValue, prevMin = Int32.MaxValue;

        while (i < len)
        {
            if (arr[i] > currMax)
            {
                prevMax = currMax;
                currMax = arr[i];
            }
            else if (arr[i] > prevMax) prevMax = arr[i];

            if (arr[i] < currMin)
            {
                prevMin = currMin;
                currMin = arr[i];
            }
            else if (arr[i] < prevMin) prevMin = arr[i];

            i++;
        }

        System.Console.WriteLine($"Second smallest element : {(currMin == prevMin ? -1 : prevMin)}, Second largest element : {(currMax == prevMax ? -1 : prevMax)}");

    }
    #endregion
    #endregion
    #endregion
}