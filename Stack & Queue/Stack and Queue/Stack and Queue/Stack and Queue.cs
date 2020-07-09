﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Stack_and_Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        #region Leetcode 235/232  Using Stack/Queue to Build a Queue/Stack
        public class MyQueue
        {
            Stack<int> data = new Stack<int>();
            public void Push(int x)
            {
                Stack<int> temp = new Stack<int>();

                while (data.Count != 0)
                {
                    temp.Push(data.Pop());
                }
                temp.Push(x);
                // It is the first data that is going to be accessed with Pop()
                while (temp.Count != 0)
                {
                    data.Push(temp.Pop());
                }
            }
            public int Pop()
            {
                return data.Pop();
            }
            public int Peek()
            {
                return data.Peek();
            }
            public bool Empty()
            {
                return data.Count == 0;
            }
        }
        public class MyStack
        {
            Queue<int> data = new Queue<int>();
            public void Push(int x)
            {
                Queue<int> temp = new Queue<int>();
                temp.Enqueue(x);
                // It is the first element that is going to be accessed with Dequeue()
                while (data.Count != 0)
                {
                    temp.Enqueue(data.Dequeue());
                }
                while (temp.Count != 0)
                {
                    data.Enqueue(temp.Dequeue());
                }
            }
            public int Pop()
            {
                return data.Dequeue();
            }

            /** Get the top element. */
            public int Top()
            {
                return data.Peek();
            }
            public bool Empty()
            {
                return data.Count == 0;
            }
        }
        #endregion
        #region Leetcode 155  Min Stack
        public class MinStack
        {
            Stack<int> data = new Stack<int>();
            Stack<int> min = new Stack<int>();
            // We use another stack to record the min value at every moment
            public void Push(int x)
            {
                data.Push(x);
                if (min.Count ==0) { min.Push(x); }
                else
                {
                    if (x > min.Peek()) { x = min.Peek(); }// We set x to the value of smallest element in the stack
                    // which means that the current min is still the original min
                    min.Push(x);
                }
            }

            public void Pop()
            {
                min.Pop();
                data.Pop();
            }
            public int Top()
            {
                return data.Peek();
            }
            public int GetMin()
            {
                return min.Peek();
            }
        }
        #endregion
    }
}