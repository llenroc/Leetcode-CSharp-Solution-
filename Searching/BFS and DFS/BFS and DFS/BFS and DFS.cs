﻿using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace BFS_and_DFS
{
    public enum States
    {
        visiting,
        visited,
        unvisited
    }
    public enum Colors
    {
        blue,
        red,
        uncolored
    }
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
    public class Node
    {
        public int val;
        public IList<Node> children;

        public Node() { }

        public Node(int _val)
        {
            val = _val;
        }

        public Node(int _val, IList<Node> _children)
        {
            val = _val;
            children = _children;
        }
    }
    #region Union Find Template
    public class UnionFind
    {
        int[] parents;
        int[] size;
        int N;

        public UnionFind(int n)
        {
            N = n;
            size = new int[n];
            parents = new int[n];

            for (int i = 0; i < N; i++)
            {
                parents[i] = i;
                size[i] = 1;
            }
        }
        public int Find(int target)
        {
            if(target == parents[target]) { return target; }
            return parents[target] = Find(parents[target]); // Path compression
        }
        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);
            if(rootX != rootY)
            {
                parents[rootY] = parents[rootX];
                size[rootX] += size[rootX];
            }
            
        }
    }
    #endregion
    #region Union Find Template with Max Size
    public class UnionFindMax
    {
        int[] parents;
        int[] size;
        public int max;
        public UnionFindMax(int n)
        {
            max = 0;
            size = new int[n];
            parents = new int[n];

            for (int i = 0; i < n; i++)
            {
                parents[i] = i;
                size[i] = 1;
            }
        }
        public int Find(int target)
        {
            if (target == parents[target]) { return target; }
            return parents[target] = Find(parents[target]); // Path compression
        }
        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);
            if (rootX != rootY)
            {
                parents[rootY] = parents[rootX];
                size[rootX] += size[rootY];
                max = Math.Max(max, size[rootX]);
            }

        }
    }
    #endregion
    class Program
    {
        static void Main(string[] args)
        {
        }
        public bool IsLeave(TreeNode node)
        {
            return node.left != null && node.right != null;
        }
        public long SumTreeNodes(TreeNode root)
        {
            long sum = 0;
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            while (q.Count != 0)
            {
                TreeNode cur = q.Dequeue();
                sum += cur.val;
                if (cur.left != null)
                {
                    q.Enqueue(cur.left);
                }
                if (cur.right != null)
                {
                    q.Enqueue(cur.right);
                }
            }

            return sum;
        }
        public IList<IList<int>> Traverse(TreeNode root)
        {
            IList<IList<int>> ret = new List<IList<int>>();
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            while (q.Count != 0)
            {
                int count = q.Count;
                List<int> cur = new List<int>();
                for (int i = 0; i < count; i++)
                {
                    TreeNode node = q.Dequeue();
                    cur.Add(node.val);
                    if (node.left != null)
                    {
                        q.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        q.Enqueue(node.right);
                    }
                }
                ret.Add(cur);
            }
            return ret;
        }
        public int HeightofTree(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            int level = 0;
            while (q.Count != 0)
            {
                level++;
                int count = q.Count;
                for (int i = 0; i < count; i++)
                {
                    TreeNode cur = q.Dequeue();

                    if (cur.left != null)
                    {
                        q.Enqueue(cur.left);
                    }
                    if (cur.right != null)
                    {
                        q.Enqueue(cur.right);
                    }
                }
            }
            return level;
        }
        #region Leetcode 662  Maximum Width of Binary Tree

        public int WidthOfBinaryTree(TreeNode root)
        {
            List<(TreeNode, int)> level = new List<(TreeNode, int)>();
            level.Add((root, 0));
            int ans = 1;
            while (level.Count > 0)
            {
                ans = Math.Max(ans, -level[0].Item2 + level[level.Count - 1].Item2 + 1);
                List<(TreeNode, int)> cur = new List<(TreeNode, int)>();
                foreach (var item in level)
                {
                    TreeNode left = item.Item1.left;
                    TreeNode right = item.Item1.right;
                    int d = item.Item2;
                    if (left != null)
                    {
                        cur.Add((left, d * 2));
                    }
                    if (right != null)
                    {
                        cur.Add((right, d * 2 + 1));
                    }
                }
                level = cur;
            }
            return ans;
        }

        #endregion
        #region Leetcode 797  All Paths From Source to Target
        public IList<IList<int>> AllPathsSourceTarget(int[][] graph)
        {
            APTfind(0, graph, new List<int>());
            return APTans;
        }
        IList<IList<int>> APTans = new List<IList<int>>();
        public void APTfind(int cur, int[][] graph, List<int> path)
        {
            path.Add(cur);
            if (cur == graph.Length - 1)
            {
                APTans.Add(new List<int>(path));
            }

            foreach (int next in graph[cur])
            {
                APTfind(next, graph, path);
            }
            path.RemoveAt(path.Count - 1);
        }

        #endregion
        #region Leetcode 78  Subsets
        public void Subsets(int[] nums)
        {
            int n = nums.Length;
            for (int i = 0; i <= n; ++i)
            { //We try out every possible length of our subset
                IList<int> cur = new List<int>();
                Subset_dfs(0, ref cur, nums, i);
            }
        }
        IList<IList<int>> Subset_ans = new List<IList<int>>();
        public void Subset_dfs(int index, ref IList<int> path, int[] nums, int k)
        {
            if (path.Count == k)
            {
                // We have reached our destination
                Subset_ans.Add(new List<int>(path));
                return;
            }
            for (int i = index; i < nums.Length; ++i)
            {
                // We try every combination by letting it add all the number that is before it 
                path.Add(nums[i]);
                Subset_dfs(i + 1, ref path, nums, k);
                path.RemoveAt(path.Count - 1);
                // We reverse the previous action
            }

        }
        #region Extension: Find all the subarrays in an array
        public IList<IList<int>> Subarrays(int[] nums)
        {
            int n = nums.Length;
            for (int i = 1; i <= n; i++) // Enumerate every possible length
            {
                for (int index = 0; index <= n - i; index++) // Enumerate every possible starting point
                {
                    IList<int> cur = new List<int>();
                    Subarray_dfs(i, nums, ref cur, index);
                }
            }
            return Subarray_ans;
        }
        public IList<IList<int>> Subarray_ans = new List<IList<int>>();
        public void Subarray_dfs(int k, int[] nums, ref IList<int> path, int index)
        {
            if (path.Count == k)
            {
                Subarray_ans.Add(path);
                return;
            }
            path.Add(nums[index]);
            Subarray_dfs(k, nums, ref path, index + 1);
        }
        #endregion
        #endregion
        #region Leetcode 987  Vertical Order Traversal of a Binary Tree
        public IList<IList<int>> VerticalTraversal(TreeNode root)
        {
            Queue<(int, int, TreeNode)> q = new Queue<(int, int, TreeNode)>();
            List<(int, int, int)> info = new List<(int, int, int)>();
            // (x, y, node value)
            q.Enqueue((0, 0, root));
            while (q.Count != 0)
            {
                var cur = q.Dequeue();
                int x = cur.Item1;
                int y = cur.Item2;
                TreeNode node = cur.Item3;
                info.Add((x, y, node.val));
                if (node.left != null)
                {
                    q.Enqueue((x - 1, y + 1, node.left));
                }
                if (node.right != null)
                {
                    q.Enqueue((x + 1, y + 1, node.right));
                }

            }
            info.Sort();
            IList<IList<int>> ans = new List<IList<int>>();
            int index = 0;
            while (index < info.Count)
            {
                IList<int> level = new List<int>();
                int cur_level = info[index].Item1;
                while (index < info.Count && cur_level == info[index].Item1)
                {
                    level.Add(info[index++].Item3);
                }
                ans.Add(level);
            }
            return ans;
        }

        #endregion
        #region Leetcode 437  Path Sum III
        public int PathSum(TreeNode root, int sum)
        {
            pathsumiiimem[0] = 1;
            pathsumiiidfs(root, sum, 0);
            return pathsumiiians;
        }
        Dictionary<int, int> pathsumiiimem = new Dictionary<int, int>();
        // Key: The prefix sum 
        // Value: The amount of ways to get to this prefix sum
        int pathsumiiians = 0;
        public void pathsumiiidfs(TreeNode root, int target, int curSum)
        {
            if (root == null) { return; }
            curSum += root.val;
            int oldSum = curSum - target;
            if (pathsumiiimem.ContainsKey(oldSum))
            {
                pathsumiiians += pathsumiiimem[oldSum];
            }
            if (!pathsumiiimem.ContainsKey(curSum)) // We now have a way to reach curSum
            {
                pathsumiiimem[curSum] = 1;
            }
            else
            {
                pathsumiiimem[curSum]++;
            }
            pathsumiiidfs(root.left, target, curSum);
            pathsumiiidfs(root.right, target, curSum);
            pathsumiiimem[curSum]--; // We no longer can accessed this path
            return;
        }
        #endregion
        #region Leetcode 994  Rotting Oranges
        public int OrangesRotting(int[][] grid)
        {
            int fresh = 0;
            int m = grid.Length;
            if (m == 0 || grid == null) { return 0; }
            int n = grid[0].Length;
            Queue<(int, int)> q = new Queue<(int, int)>();
            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (grid[i][j] == 1)
                    {
                        ++fresh;
                    }
                    else if (grid[i][j] == 2)
                    {
                        q.Enqueue((j, i));
                    }
                }
            }
            if (fresh == 0)
            {
                return 0;
            }
            int steps = 0;
            while (q.Count != 0)
            {
                int count = q.Count;
                for (int i = 0; i < count; ++i)
                {
                    var cur = q.Dequeue();
                    int x = cur.Item1;
                    int y = cur.Item2;
                    if (y + 1 < m && grid[y + 1][x] == 1)
                    {
                        q.Enqueue((x, y + 1));
                        --fresh;
                        grid[y + 1][x] = 2;
                    }
                    if (y - 1 >= 0 && grid[y - 1][x] == 1)
                    {
                        q.Enqueue((x, y - 1));
                        --fresh;
                        grid[y - 1][x] = 2;

                    }
                    if (x - 1 >= 0 && grid[y][x - 1] == 1)
                    {
                        q.Enqueue((x - 1, y));
                        --fresh;
                        grid[y][x - 1] = 2;

                    }
                    if (x + 1 < n && grid[y][x + 1] == 1)
                    {
                        q.Enqueue((x + 1, y));
                        --fresh;
                        grid[y][x + 1] = 2;
                    }
                }
                ++steps;

            }
            return fresh == 0 ? steps - 1 : -1;
        }
        #endregion
        #region Leetcode 404  Sum of Left Leaves
        public int SumOfLeftLeaves(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            int sum = 0;
            while (q.Count != 0)
            {
                TreeNode cur = q.Dequeue();
                TreeNode left = cur.left;
                TreeNode right = cur.right;
                if (right != null)
                {
                    q.Enqueue(right);
                }
                if (left != null)
                {
                    if (IsLeave(left))
                    {
                        sum += left.val;
                        continue;
                    }
                    q.Enqueue(left);
                }
            }
            return sum;
        }
        #endregion
        #region Leetcode 952  Largest Component Size by Common Factor
        public int LargestComponentSize(int[] nums)
        {
            int n = nums.Length;
            Dictionary<int, int> dict = new Dictionary<int, int>();
            // Key: Factor of a number
            // Value: Node index of the number
            UnionFindMax uf = new UnionFindMax(n);

            for (int i = 0; i < n; ++i)
            {
                int cur = nums[i];
                for (int j = 2; j * j <= cur; ++j)
                { // Try every factor of cur excluding one
                    if (cur % j == 0)
                    {
                        if (!dict.ContainsKey(j))
                        {
                            dict[j] = i;
                        }
                        else
                        {
                            uf.Union(i, dict[j]);
                            // We do not need to update the dictionary because numbers of the same factor have been unioned already
                        }

                        if (!dict.ContainsKey(cur / j))
                        {
                            dict[cur / j] = i;
                        }
                        else
                        {
                            uf.Union(i, dict[cur / j]);
                        }
                    }

                }
                if (!dict.ContainsKey(cur))
                { // Cur itself could also be a factor
                    dict[cur] = i;
                }
                else
                {
                    uf.Union(i, dict[cur]);
                }
            }

            return uf.max;
        }
        #endregion
        #region Leetcode 1022  Sum of Root To Leaf Binary Numbers
        public int SumRootToLeaf(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            SRTLdfs(root, 0);
            return ans;
        }
        int ans = 0;
        public void SRTLdfs(TreeNode root, int sum)
        {
            sum = (sum << 1) | root.val;
            if (root.left == null && root.right == null)
            {
                ans += sum;
                return;
            }
            if (root.left != null)
            {
                SRTLdfs(root.left, sum);
            }
            if (root.right != null)
            {
                SRTLdfs(root.right, sum);
            }
        }
        #endregion
        #region Leetcode 1305  All Elements in Two Binary Search Trees
        public IList<int> GetAllElements(TreeNode root1, TreeNode root2)
        {
            List<int> r1 = new List<int>(), r2 = new List<int>();
            InOrderTraversal(root1, ref r1);
            InOrderTraversal(root2, ref r2);

            return MergeTwoSortedList(r1, r2);
        }
        public void InOrderTraversal(TreeNode root, ref List<int> nodes)
        {
            if (root == null)
            {
                return;
            }
            InOrderTraversal(root.left, ref nodes);

            nodes.Add(root.val);

            InOrderTraversal(root.right, ref nodes);
        }
        public List<int> MergeTwoSortedList(List<int> list1, List<int> list2)
        {
            int l1 = list1.Count;
            int l2 = list2.Count;

            if (l1 == 0 || list1 == null)
            {
                return new List<int>(list2);
            }
            else if (l2 == 0 || list2 == null)
            {
                return new List<int>(list1);
            }

            int i1 = 0, i2 = 0;
            List<int> merged = new List<int>();

            while (i1 < l1 || i2 < l2)
            {
                if (i1 == l1)
                {
                    merged.Add(list2[i2++]);
                    continue;
                }
                else if (i2 == l2)
                {
                    merged.Add(list1[i1++]);
                    continue;
                }

                int cur = Math.Min(list1[i1], list2[i2]);
                merged.Add(cur);
                if (cur == list1[i1])
                {
                    ++i1;
                }
                else
                {
                    ++i2;
                }
            }
            return merged;
        }
        #endregion
        #region Leetcode 1339  Maximum Product of Splitted Binary Tree
        public int MaxProduct(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            MPdfs(root, SumTreeNodes(root));
            return (int)(MPans % kmod);
        }
        int kmod = (int)Math.Pow(10, 9) + 7;
        long MPans = int.MinValue;
        public long MPdfs(TreeNode node, long sum)
        {
            if (node == null)
            {
                return 0;
            }
            long current = MPdfs(node.left, sum) + MPdfs(node.right, sum) + node.val;

            MPans = Math.Max(MPans, current * (sum - current));

            return current;
        }

        #endregion
        #region Leetcode 216  Combination Sum III
        public IList<IList<int>> CombinationSum3(int k, int n)
        {
            dfs(new List<int>(), k, n, 1);
            return CP3ans;
        }
        IList<IList<int>> CP3ans = new List<IList<int>>();
        public void dfs(List<int> path, int k, int n, int start)
        {
            if (path.Count > k)
            {
                return;
            }
            if (path.Count == k && n == 0)
            {
                CP3ans.Add(new List<int>(path));
                return;
            }
            for (int i = start; i <= 9 && i <= n; ++i)
            {
                path.Add(i);
                dfs(path, k, n - i, i + 1);
                path.RemoveAt(path.Count - 1);
            }
        }
        #endregion
        #region Leetcode 980  Unique Path III
        public int UniquePathsIII(int[][] grid)
        {
            int n = grid.Length;
            int m = grid[0].Length;

            int sx = -1, sy = -1;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (grid[i][j] == 0)
                    {
                        ++empty;
                    }
                    else if (grid[i][j] == 1)
                    {
                        sy = i;
                        sx = j;
                    }
                }
            }
            UP3dfs(grid, sx, sy);
            return UP3ans;
        }
        private int empty = 1, UP3ans = 0;
        public void UP3dfs(int[][] grid, int x, int y)
        {
            if (x < 0 || y < 0 || x >= grid[0].Length || y >= grid.Length || grid[y][x] < 0)
            {
                return;
            }
            else if (grid[y][x] == 2)
            {
                if (empty == 0)
                {
                    ++UP3ans;
                }
                return;
            }
            --empty;
            grid[y][x] = -2;
            // Then it is always illegal to access whenever the value is negative
            // -1 - obstacle
            // -2 - currently visiting

            UP3dfs(grid, x + 1, y);
            UP3dfs(grid, x - 1, y);
            UP3dfs(grid, x, y + 1);
            UP3dfs(grid, x, y - 1);
            // Go through four directions

            empty++;
            grid[y][x] = 0;
            // Reverse
        }
        #endregion
        #region Leetcode 399  Evaluate Division
        // The ultimate theory for this question is to find the path length between a and b
        public double[] CalcEquation(IList<IList<string>> equations, double[] values, IList<IList<string>> queries)
        {
            int n = queries.Count;
            Dictionary<string, Dictionary<string, double>> graph = new Dictionary<string, Dictionary<string, double>>();
            // graph[a][b]: The value of a / b or the path length from a to b

            for (int i = 0; i < values.Length; i++)
            {
                string a = equations[i][0];
                string b = equations[i][1];
                double value = values[i];

                if (!graph.ContainsKey(a)) { graph[a] = new Dictionary<string, double>(); }
                if (!graph.ContainsKey(b)) { graph[b] = new Dictionary<string, double>(); }

                // Build a graph that has two directions
                graph[a].Add(b, value);
                graph[b].Add(a, 1.0 / value);
            }
            double[] ans = new double[n];

            for (int i = 0; i < n; i++)
            {
                string a = queries[i][0];
                string b = queries[i][1];
                if (!graph.ContainsKey(a) || !graph.ContainsKey(b))
                {
                    ans[i] = -1.0;
                    continue;
                }
                ans[i] = CEdfs(a, b, new HashSet<string>(), graph);
            }
            return ans;
        }
        // This function returns the path length from a to b, if it exists or otherwise -1.0
        private double CEdfs(string a, string b, HashSet<string> seen, Dictionary<string, Dictionary<string, double>> graph)
        {
            if (a == b)
            // The division is complete
            {
                return 1.0;
            }
            seen.Add(a);

            foreach (var dict in graph[a])
            // Looking for a middle point: next 
            // The answer would be graph[a][next] + graph[next][b]
            {
                string next = dict.Key;
                if (seen.Contains(next))
                {
                    continue;
                }
                double result = CEdfs(next, b, seen, graph);
                if (result > 0)
                // If a path exists
                {
                    return result * graph[a][next];
                }
            }
            return -1.0;
        }
        #endregion
        #region Leetcode 103  Binary Tree Zigzag Level Order Traversal
        public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            if (root == null)
            {
                return new List<IList<int>>();
            }
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            IList<IList<int>> ans = new List<IList<int>>();
            bool reverse = false;

            while (q.Count != 0)
            {
                int count = q.Count;
                List<int> cur = new List<int>();

                for (int i = 0; i < count; i++)
                {
                    TreeNode node = q.Dequeue();
                    cur.Add(node.val);
                    if (node.left != null)
                    {
                        q.Enqueue(node.left);
                    }
                    if (node.right != null)
                    {
                        q.Enqueue(node.right);
                    }
                }

                if (reverse)
                {
                    cur.Reverse();
                }
                ans.Add(cur);
                reverse = !reverse;
            }

            return ans;
        }
        #endregion
        #region Leetcode 107  Binary Tree Level Order Traversal II 
        public IList<IList<int>> LevelOrderBottom(TreeNode root)
        {
            if (root == null)
            {
                return new List<IList<int>>();
            }
            int n = HeightofTree(root);
            IList<int>[] ans = new IList<int>[n];
            Queue<TreeNode> q = new Queue<TreeNode>();
            int index = n - 1;
            q.Enqueue(root);
            while (q.Count != 0)
            {
                int count = q.Count;
                List<int> cur = new List<int>();
                for (int i = 0; i < count; i++)
                {
                    TreeNode node = q.Dequeue();
                    cur.Add(node.val);
                    if (node.left != null)
                    {
                        q.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        q.Enqueue(node.right);
                    }
                }
                ans[index--] = cur;
            }
            return ans.ToList();
        }
        #endregion
        #region Leetcode 130  Surrounded Regions
        // We can change our thought here to: find all the secure points (points that has a connection to the border points) 
        // and the rest of them are insecure
        public void Solve(char[][] board)
        {
            int m = board.Length;
            if (m == 0)
            {
                return;
            }
            int n = board[0].Length;
            for (int i = 0; i < m; i++)
            {
                SRdfs(ref board, n - 1, i);
                SRdfs(ref board, 0, i);
            }
            for (int i = 0; i < n; i++)
            {
                SRdfs(ref board, i, 0);
                SRdfs(ref board, i, m - 1);
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (board[i][j] == 'O')
                    {
                        board[i][j] = 'X';
                    }
                    else if (board[i][j] == 'S')
                    {
                        board[i][j] = 'O';
                    }
                }
            }
        }
        private void SRdfs(ref char[][] board, int x, int y)
        {
            if (x < 0 || y < 0 || x >= board[0].Length || y >= board.Length || board[y][x] != 'O')
            {
                return;
            }

            board[y][x] = 'S';
            SRdfs(ref board, x + 1, y);
            SRdfs(ref board, x - 1, y);
            SRdfs(ref board, x, y + 1);
            SRdfs(ref board, x, y - 1);
        }
        #endregion
        #region Leetcode 199  Binary Tree Right Side View
        public IList<int> RightSideView(TreeNode root)
        {
            if (root == null)
            {
                return new List<int>();
            }
            IList<int> ans = new List<int>();
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            while (q.Count != 0)
            {
                TreeNode level = null;
                int count = q.Count;

                for (int i = 0; i < count; i++)
                {
                    TreeNode cur = q.Dequeue();
                    level = cur;

                    if (cur.left != null)
                    {
                        q.Enqueue(cur.left);
                    }
                    if (cur.right != null)
                    {
                        q.Enqueue(cur.right);
                    }
                }
                ans.Add(level.val);
            }
            return ans;
        }
        #endregion
        #region Leetcode 200  Number of Islands
        public int NumIslands(char[][] grid)
        {
            int m = grid.Length;
            if (m == 0)
            {
                return 0;
            }
            int count = 0, n = grid[0].Length;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] == '1')
                    {
                        count++;
                        NIdfs(ref grid, j, i);
                    }
                }
            }
            return count;
        }
        private void NIdfs(ref char[][] grid, int x, int y)
        {
            if (x < 0 || y < 0 || x >= grid[0].Length || y >= grid.Length || grid[y][x] != '1')
            {
                return;
            }

            grid[y][x] = '0';

            NIdfs(ref grid, x + 1, y);
            NIdfs(ref grid, x - 1, y);
            NIdfs(ref grid, x, y + 1);
            NIdfs(ref grid, x, y - 1);
        }
        #endregion
        #region Course Schedule Series
        // Leetcode 207  Course Schedule
        public bool CanFinish(int numCourses, int[][] prerequisites)
        {
            List<int>[] map = new List<int>[numCourses];
            // map[i]: the prerequisite courses that have to be taken before taking course i
            for (int i = 0; i < numCourses; i++)
            {
                map[i] = new List<int>();
            }
            foreach (int[] p in prerequisites)
            {
                map[p[0]].Add(p[1]);
            }
            States[] states = new States[numCourses];
            for (int i = 0; i < numCourses; i++)
            {
                states[i] = States.unvisited;
            }
            for (int i = 0; i < numCourses; i++)
            {
                if (!CFdfs(i, map, ref states))
                {
                    return false;
                }
            }
            return true;
        }
        public int[] FindOrder(int numCourses, int[][] prerequisites)
        {
            List<int>[] graph = new List<int>[numCourses];
            for (int i = 0; i < numCourses; ++i)
            {
                graph[i] = new List<int>();
            }
            foreach (int[] course in prerequisites)
            {
                graph[course[0]].Add(course[1]);
            }
            States[] states = new States[numCourses];
            for (int i = 0; i < numCourses; i++)
            {
                states[i] = States.unvisited;
            }
            List<int> ans = new List<int>();
            for (int i = 0; i < numCourses; ++i)
            {
                if (!CFdfs(i, graph, ref states, ref ans))
                {
                    return new int[] { };
                }
            }
            return ans.ToArray();
        }
        private bool CFdfs(int i, List<int>[] map, ref States[] states, ref List<int> ans)
        {
            if (states[i] == States.visiting)
            {
                return false;
            }
            else if (states[i] == States.visited)
            {
                return true;
            }

            states[i] = States.visiting;

            foreach (int next in map[i])
            {
                if (!CFdfs(next, map, ref states, ref ans))
                {
                    return false;
                }
            }

            states[i] = States.visited;
            ans.Add(i);
            return true;
        }
        private bool CFdfs(int i, List<int>[] map, ref States[] states)
        {
            if (states[i] == States.visiting)
            {
                return false;
            }
            else if (states[i] == States.visited)
            {
                return true;
            }

            states[i] = States.visiting;

            foreach (int next in map[i])
            {
                if (!CFdfs(next, map, ref states))
                {
                    return false;
                }
            }

            states[i] = States.visited;
            return true;
        }
        #endregion
        #region Leetcode 513  Find Bottom Left Tree Value
        public int FindBottomLeftValue(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            TreeNode last = null;
            while (q.Count != 0)
            {
                int count = q.Count;
                bool get = false;
                for (int i = 0; i < count; ++i)
                {
                    TreeNode cur = q.Dequeue();
                    if (!get)
                    {
                        get = true;
                        last = cur;
                    }

                    if (cur.left != null)
                    {
                        q.Enqueue(cur.left);
                    }
                    if (cur.right != null)
                    {
                        q.Enqueue(cur.right);
                    }
                }
            }
            return last.val;
        }
        #endregion
        #region Leetcode 515  Find Largest Value in Each Tree Row
        public IList<int> LargestValues(TreeNode root)
        {
            if (root == null)
            {
                return new List<int>();
            }
            IList<int> ans = new List<int>();
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            while (q.Count != 0)
            {
                int max = int.MinValue;
                int count = q.Count;
                for (int i = 0; i < count; i++)
                {
                    TreeNode cur = q.Dequeue();

                    max = Math.Max(max, cur.val);

                    if (cur.left != null)
                    {
                        q.Enqueue(cur.left);
                    }
                    if (cur.right != null)
                    {
                        q.Enqueue(cur.right);
                    }
                }
                ans.Add(max);
            }
            return ans;
        }
        #endregion
        #region Leetcode 429  N-ary Tree Level Order Traversal
        public IList<IList<int>> LevelOrder(Node root)
        {
            if (root == null)
            {
                return new List<IList<int>>();
            }
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);
            IList<IList<int>> ans = new List<IList<int>>();
            while(q.Count != 0)
            {
                int count = q.Count;
                List<int> level = new List<int>();
                for (int i = 0; i < count; i++)
                {
                    Node cur = q.Dequeue();
                    level.Add(cur.val);
                    foreach(Node children in cur.children)
                    {
                        if(children != null)
                        {
                            q.Enqueue(children);
                        }
                    }
                }
                ans.Add(level);
            }
            return ans;
        }
        #endregion
        #region Leetcode 559  Maximum Depth of N-ary Tree
        public int MaxDepth(Node root)
        {
            if(root == null)
            {
                return 0;
            }
            int height = 0;
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);
            while(q.Count != 0)
            {
                height++;
                int count = q.Count;
                for (int i = 0; i < count; i++)
                {
                    Node cur = q.Dequeue();
                    foreach(Node child in cur.children)
                    {
                        if(child != null)
                        {
                            q.Enqueue(child);
                        }
                    }
                }
            }
            return height;
        }
        #endregion
        #region Leetcode 785  Is Graph Bipartite?
        // If a graph is bipartitie, then no two adjacent nodes should be in the same group
        public bool IsBipartite(int[][] graph)
        {
            int n = graph.Length;
            int[] numbers = new int [n];

            for (int i = 0; i < n; i++)
            {
                if(numbers[i] == 0 && !IBdfs(graph,i,1,ref numbers))
                {
                    return false;
                }
            }
            return true;
        }
        private bool IBdfs(int[][] graph, int cur, int index, ref int[] numbers)
        {
            numbers[cur] = index;

            foreach(int next in graph[cur])
            {
                if(numbers[next] == index)
                {
                    return false;
                }
                if(numbers[next] == 0 && !IBdfs(graph,next,-index,ref numbers))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
