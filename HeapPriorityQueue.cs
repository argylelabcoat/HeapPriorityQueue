/*
 * Copyright (c) 2011, Matthew Hughes
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
 *
 * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
 * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityQueue
{
    /// <summary>
    /// Priority Queue using a Minimum Heap for the backend
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HeapPriorityQueue<P,T> : IPriorityQueue<P,T> where P:IComparable<P>
    {
        private MinimumHeap<P,T> heap;

        /// <summary>
        /// Builds new Priority Queue with initial size
        /// </summary>
        /// <param name="initialSize"></param>
        public HeapPriorityQueue(int initialSize)
        {
            heap = new MinimumHeap<P,T>(initialSize);
        }

        /// <summary>
        /// Is Queue Empty?
        /// </summary>
        public bool IsEmpty { get { return heap.IsEmpty; } }


        #region IPriorityQueue<P,T> Members

        /// <summary>
        /// Add an element to the queue with an associated priority
        /// </summary>
        /// <param name="item"></param>
        /// <param name="priority"></param>
        public void InsertWithPriority(P priority, T item)
        {
            heap.Insert(priority,item);
        }

        /// <summary>
        /// Remove the element from the queue that has the highest priority, and return it
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            T value = heap.Minimum;
            heap.RemoveMin();
            return value;
        }

        /// <summary>
        /// Look at the element with highest priority without removing it
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            return heap.Minimum;
        }

        #endregion
    }
}
