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
    /// Binary heap storing type T data with type K keys where K is comparable to K
    /// </summary>
    public class MinimumHeap<K,T> where K:IComparable<K>
    {
        private List<HeapNode<K,T>> data;
        
        /// <summary>
        /// Is true if heap is empty, false if not
        /// </summary>
        public bool IsEmpty { get { return data.Count == 0; } }
        /// <summary>
        /// Returns the minmal node (assuming there is one)
        /// </summary>
        public T Minimum
        {
            get
            {
                if (IsEmpty)
                    throw new Exception("Heap is empty");
                else
                    return data[0].Data;
            }
        }

        /// <summary>
        /// Constructor, intializes data list to intial size
        /// </summary>
        /// <param name="size">initial data list size</param>
        public MinimumHeap(int size)
        {
            data = new List<HeapNode<K,T>>(size);
        }

        /// <summary>
        /// Inserts a node into the heap
        /// </summary>
        /// <param name="value">datum to store</param>
        /// <param name="key">key value/priority</param>
        public void Insert(K key, T value)
        {
            HeapNode<K,T> node = new HeapNode<K,T>(key, value);
            data.Add(node);
            SiftUp(data.Count - 1);
        }
        /// <summary>
        /// Removes Root Node
        /// </summary>
        public void RemoveMin()
        {
            if (IsEmpty)
                throw new Exception("Heap is empty");
            else
            {
                data[0] = data[data.Count - 1];
                data.RemoveAt(data.Count - 1);
                if (!IsEmpty)
                    SiftDown(0);
            }
        }

        /// <summary>
        /// Obtains left-child for a given node
        /// </summary>
        /// <param name="nodeIndex">index of node whose child we want</param>
        /// <returns></returns>
        private int GetLeftChildIndex(int nodeIndex)
        {
            return 2 * nodeIndex + 1;
        }
        /// <summary>
        /// Obtains right-child index for a given node
        /// </summary>
        /// <param name="nodeIndex">index of node whose child we want</param>
        /// <returns>index of right child (may not be valid)</returns>
        private int GetRightChildIndex(int nodeIndex)
        {
            return 2 * nodeIndex + 2;
        }
        /// <summary>
        /// Obtains index for parent of a given node
        /// </summary>
        /// <param name="nodeIndex">index of node</param>
        /// <returns>index of parent node</returns>
        private int GetParentIndex(int nodeIndex)
        {
            return (nodeIndex - 1) / 2;
        }
        /// <summary>
        /// Sifts up the tree/heap (recursive) to fit a new node into place
        /// </summary>
        /// <param name="nodeIndex">index of the new node</param>
        private void SiftUp(int nodeIndex)
        {
            int parentIndex;
            HeapNode<K,T> swapNode;

            if (nodeIndex != 0) // ensure that we are not on the _first_ node (if so this is pointless)
            {
                parentIndex = GetParentIndex(nodeIndex);

                if (data[parentIndex].Key.CompareTo(data[nodeIndex].Key)>0)
                {
                    swapNode = data[parentIndex];
                    data[parentIndex] = data[nodeIndex];
                    data[nodeIndex] = swapNode;
                    SiftUp(parentIndex);
                }
            }
        }
        /// <summary>
        /// Sift Nodes Down The Tree (recursive) looking for minimal node
        /// This is called after removing the root/minimal node from the heap
        /// </summary>
        /// <param name="nodeIndex">index of node in list array</param>
        private void SiftDown(int nodeIndex)
        {
            int leftChildIndex, rightChildIndex, minIndex;
            HeapNode<K,T> swapNode;

            // get children indices (may not be valid, checks are below)
            leftChildIndex = GetLeftChildIndex(nodeIndex);
            rightChildIndex = GetRightChildIndex(nodeIndex);

            if (rightChildIndex >= data.Count) // is there no right child?
            {
                if (leftChildIndex >= data.Count) // is there no left child?
                    return;
                else                           // there is left child, but no right.
                    minIndex = leftChildIndex;
            }
            else // there is a right child and thus, guaranteed due to the nature of arrays, a left
            {
                if (data[leftChildIndex].Key.CompareTo(data[rightChildIndex].Key) <=0) // find lesser child
                    minIndex = leftChildIndex;
                else
                    minIndex = rightChildIndex;
            }
            if (data[nodeIndex].Key.CompareTo(data[minIndex].Key) > 0) // did we find a node less than where we came from?
            {
                swapNode = data[minIndex];
                data[minIndex] = data[nodeIndex];
                data[nodeIndex] = swapNode;
                SiftDown(minIndex);
            }
        }

        /// <summary>
        /// Create nice string representation of the array (not tree form, unfortunately, I'm not that awesome)
        /// </summary>
        /// <returns>string showing array contents</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            bool first = true;
            foreach (HeapNode<K,T> item in data)
            {
                if(!first)
                    sb.Append(",");
                else
                    first=false;
                sb.Append(item.ToString());
            }
            sb.Append("]");
            return sb.ToString();
        }

    }
}
