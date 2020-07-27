﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace DP_Array_Problems
{
    class Program
    {
        static void Main(string[] args)
        {
        }
        #region Leetcode 698  Partition to K Equal Sum Subsets
        public bool CanPartitionKSubsets(int[] nums, int k)
        {
            int sum = nums.Sum();
            if (sum % k != 0) // If we cannot divide teh array equally
            {
                return false;
            }
            int subsum = sum / k;
            Array.Sort(nums);
            int index = nums.Length - 1;
            if (nums[index] > subsum)
            {
                return false;
            }
            while (index >= 0 && nums[index] == subsum)
            //If the current number is equal to the target sum, then it is a group itself
            // Therefore, we only need k-1 groups right now
            {
                index--;
                k--;
            }
            return partition(nums, index, new int[k], subsum);
        }
        public bool partition(int[] nums, int index, int[] subset, int target)
        // Subset is an array containing the sum of every groups
        {
            if (index < 0) // We successfully processed every number
            {
                return true;
            }
            int selected = nums[index];
            for (int i = 0; i < subset.Length; i++) // We can try out to put this current number in any existing group in subset
            {
                if (subset[i] + selected <= target)// If we can put this in
                {
                    subset[i] += selected;
                    if (partition(nums, index - 1, subset, target))
                    {
                        return true;
                    }
                    subset[i] -= selected; // We have to reverse the previous action
                }
            }
            return false;
        }
        #endregion
        #region Leetcode 5  Longest Palindromic Substring
        public int GetLength(int start, int end, string str)
        {
            while (start >= 0 && end < str.Length && str[start] == str[end])
            {
                --start;
                ++end;
            }
            return end - start - 1; // This is inclusive on both sides so then we have to minue one
        }
        public string LongestPalindrome(string s)
        {
            int start = 0;
            int len = 0;
            for (int i = 0; i < s.Length; i++)
            {
                int cur = Math.Max(GetLength(i, i, s), GetLength(i, i + 1, s));
                if (cur > len)
                {
                    len = cur;
                    start = i - (len - 1) / 2;
                }
            }
            return s.Substring(start, len);
        }
        #endregion
        #region Leetcode 152  Maximum Product Subarray
        public int MaxProduct(int[] nums)
        {
            int n = nums.Length;
            if (n == 0 || nums == null) { return 0; }
            int max = nums[0];
            int min = nums[0];
            int result = nums[0];
            for (int i = 1; i < n; i++)
            {
                int cur = nums[i];
                int r1 = cur * min;
                int r2 = cur * max;
                min = Math.Min(Math.Min(r1, r2), cur);
                max = Math.Max(Math.Max(r1, r2), cur);
                result = Math.Max(result, max);
            }
            return result;
        }
        #endregion
        #region Leetcode 300  Longest Increasing Subsquence
        public int LengthOfLIS(int[] nums)
        {
            int n = nums.Length;
            if (n == 0 || nums == null) { return 0; }
            int[] dp = new int[n];
            // dp[i] means the LIS that can be obtained by using the first i elements in nums
            Array.Fill(dp, 1); // Note that a number itself is considered as a subsquence
            int max = 1;
            for (int i = 0; i < n; i++)
            {
                int len = 1;
                int cur = nums[i];
                for (int j = 0; j < i; j++)
                {
                    if (nums[j] < cur)
                    {
                        len = Math.Max(len, dp[j] + 1);
                    }
                }
                dp[i] = len;
                if (len > max) { max = len; }
            }
            return max;
        }
        #endregion
        #region Leetcode 15  3 Sum
        // O(n2) Time Complexity
        // We use a double pointer to a 2 Sum
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            int n = nums.Length;
            Array.Sort(nums);
            IList<IList<int>> ans = new List<IList<int>>();
            for (int i = 0; i < n-2; i++) // We need to at least space out two numbers
            {
                int cur = nums[i];
                if (cur > 0) { break; }// There is no way that we are going to find two positive numbers that have the a negative sum
                else if (i > 0 && cur == nums[i - 1]) { continue; }// Avoid same value
                int target = -cur;
                int l = i + 1;
                int r = n - 1;
                while (l < r)
                {
                    int sum = nums[l] + nums[r];
                    if(sum== target)
                    {
                        ans.Add(new List<int>() { cur, nums[l], nums[r] });
                        // The following steps must be done to avoid Adding the same combination twice
                        // Note that there could be multiple combinations
                        ++l;
                        while(l<r&&nums[l] == nums[l - 1]) { ++l; }
                        --r;
                        while(l<r && nums[r] == nums[r + 1]) { --r; }
                    }
                    else if (sum < target) // We need to find a larger value
                    {
                        ++l;
                    }
                    else { --r; }
                }
                
            }
            return ans;
        }
        #endregion
        #region Leetcode 1143  Longest Common Subsequence
        public int LongestCommonSubsequence(string text1, string text2)
        {
            int n = text1.Length;
            int m = text2.Length;
            int[][] dp = new int[n + 1][];
            for (int i = 0; i <= n; ++i)
            {
                dp[i] = new int[m + 1];
                //dp[i][j] stores the LCS using text1[0-i] and text2[0-j]
            }
            for (int i = 1; i <= n; ++i)
            {
                for (int j = 1; j <= m; ++j)
                {
                    if (text2[j - 1] == text1[i - 1])// Note that the index must minue one
                    // If we are able to use this char
                    { 
                        dp[i][j] = dp[i - 1][j - 1] + 1;
                        // We expand one tile
                    }
                    else { dp[i][j] = Math.Max(dp[i - 1][j], dp[i][j - 1]); }// Then the answer should come fromthe previous text1 or text2
                }
            }
            return dp[n][m];
        }
        public int FindLength(int[] A, int[] B)
        {
            int l1 = A.Length;
            int l2 = B.Length;
            int[,] dp = new int[l1 + 1, l2 + 1];
            int max = 0;
            for (int i = 1; i <= l1; ++i)
            {
                for (int j = 1; j <= l2; ++j)
                {
                    if (A[i - 1] == B[j - 1])
                    {
                        dp[i, j] = Math.Max(dp[i, j], dp[i - 1, j - 1] + 1);
                        max = Math.Max(dp[i, j], max);
                    }

                }
            }
            return max;
        }
        #endregion
        #region Leetcode 84  Largest Rectangle in a Histogram
        // Our Ultimate goal for this question is to maintain a monetone increasing stack
        public int LargestRectangleArea(int[] heights)
        {
            int max = 0;
            Stack<int> s = new Stack<int>();
            // The index of the numbers are stored instead of the values
            s.Push(-1);
            // We put a buffer value;
            int n = heights.Length;
            for (int i = 0; i < n; i++) // Try to put every element in the stack
            {
                while(s.Peek()!=-1 && heights[s.Peek()] >= heights[i])
                {
                    max = Math.Max(max, heights[s.Pop()] * (i - s.Peek()/*Exclusive*/- 1));
                }
                s.Push(i);
            }
            while (s.Peek() != -1)
            {
                max = Math.Max(max, heights[s.Pop()] * (n - s.Peek() - 1));
            }
            return max;
        }
        #endregion
        #region Leetcode 91  Decode ways
        public int NumDecodings(string s)
        {
            int n = s.Length;
            if (n == 0 || s == null || s[0] == '0') { return 0; }
            else if (n == 1) { return 1; }
            // The number that starts with an zero is illegal
            int[] dp = new int[n];
            dp[0] = 1; // When there is only one character and it is not '0', there is one decode way
            for (int i = 1; i < n; i++)
            {
                bool r1 = Valid(s[i]);
                bool r2 = Valid(s[i - 1], s[i]);
                if (!r1 && !r2)
                {
                    return 0;
                    // We cannot decode the string
                }
                if (r1)
                {
                    dp[i] += dp[i - 1];
                }
                if (r2)
                {
                    dp[i] += (i >= 2) ? dp[i - 2] : 1;
                }
            }
            return dp[n - 1];
        }
        public bool Valid(char a)
        {
            return a != '0';
        }
        public bool Valid(char a, char b)
        {
            int value = Convert.ToInt32(a - '0') * 10 + Convert.ToInt32(b - '0');
            return value >= 10 && value <= 26;
        }
        #endregion
        #region Leetcode 279  Perfect Squares 
        public int NumSquares(int n)
        {
            int[] dp = new int[n + 1];
            for (int i = 0; i < n+1; i++)
            {
                dp[i] = i;
                // Foreach i, we can use i amount of one's
            }
            for (int i = 2; i <= n; i++)
            {
                for (int j = 0; j * j <= i; j++) // Enumerate every possibility
                {
                    dp[i] = Math.Min(dp[i], dp[i - j * j] + 1);
                }
            }
            return dp[n];
        }
        #endregion
        #region Leetcode 1320   Minimum Distance to Type a Word Using Two Fingers
        // Once the best solution to type word[0-i] is found, we only care about the last position of two fingers 
        // Moreover, we only need to record the position of the last finger because another finger is always on word[i-1]
        public int MinimumDistance(string word)
        {
            int n = word.Length;
            MDdp = new int[n][];
            for (int i = 0; i < n; i++)
            {
                MDdp[i] = new int[27];
                Array.Fill(MDdp[i], int.MinValue);
            }
            return MDfind(0, 26, word);
        }
        int[][] MDdp;
        int kRest = 26;
        public int MDfind(int index,int other,string word)
        {
            if(index == word.Length) { return 0; }// We have printed out the whole word
            else if (MDdp[index][other] != int.MinValue) { return MDdp[index][other]; }
            int prev = index == 0 ? kRest : word[index - 1] - 'A'; // Note that the another finger must be on word[i-1]
            int cur = word[index] - 'A';
            return MDdp[index][other] = Math.Min(MDfind(index + 1, other, word) + dist_cost(prev, cur)/*Using the same finger*/
                ,MDfind(index + 1, prev, word) + dist_cost(other, cur)/*Using the other finger*/);
        }
        public int dist_cost(int p1,int p2)
        {
            if(p1 == kRest) { return 0; }// It is not on the keyboard yet
            return Math.Abs(p1 / 6 - p2 / 6) + Math.Abs(p1 % 6 - p2 % 6);
        }

        #endregion
    }
}
