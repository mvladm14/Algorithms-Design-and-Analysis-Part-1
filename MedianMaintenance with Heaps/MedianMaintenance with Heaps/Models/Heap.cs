using System;
using System.Collections.Generic;

namespace MedianMaintenance_with_Heaps.Models
{
    public class Heap<T> where T : IComparable<T>
    {

        private delegate bool StopBubleUp(T parent, T child);
        private delegate void Heapify(int top);

        private readonly StopBubleUp _stopBubleUp;
        private readonly Heapify _heapify;

        protected List<T> Data = new List<T>();

        #region Constructor
        public Heap(bool isMinHeap)
        {
            if (isMinHeap)
            {
                _stopBubleUp = StopMinHeapBubbleUp;
                _heapify = MinHeapify;
            }
            else
            {
                _stopBubleUp = StopMaxHeapBubbleUp;
                _heapify = MaxHeapify;
            }
        }
        #endregion

        #region private methods
        private bool StopMinHeapBubbleUp(T parent, T child)
        {
            return parent.CompareTo(child) <= 0;
        }
        private bool StopMaxHeapBubbleUp(T parent, T child)
        {
            return parent.CompareTo(child) >= 0;
        }
        private void MinHeapify(int i)
        {
            int smallestIndex;
            int l = 2 * (i + 1) - 1;
            int r = 2 * (i + 1);
            if (l < Data.Count && (Data[l].CompareTo(Data[i]) < 0))
            {
                smallestIndex = l;
            }
            else
            {
                smallestIndex = i;
            }
            if (r < Data.Count && (Data[r].CompareTo(Data[smallestIndex]) < 0))
            {
                smallestIndex = r;
            }
            if (smallestIndex != i)
            {
                T tmp = Data[i];
                Data[i] = Data[smallestIndex];
                Data[smallestIndex] = tmp;
                MinHeapify(smallestIndex);
            }
        }
        private void MaxHeapify(int i)
        {
            int biggestIndex;
            int l = 2 * (i + 1) - 1;
            int r = 2 * (i + 1);
            if (l < Data.Count && (Data[l].CompareTo(Data[i]) > 0))
            {
                biggestIndex = l;
            }
            else
            {
                biggestIndex = i;
            }
            if (r < Data.Count && (Data[r].CompareTo(Data[biggestIndex]) > 0))
            {
                biggestIndex = r;
            }
            if (biggestIndex != i)
            {
                T tmp = Data[i];
                Data[i] = Data[biggestIndex];
                Data[biggestIndex] = tmp;
                MaxHeapify(biggestIndex);
            }
        }
        #endregion
        public virtual void Insert(T item)
        {
            Data.Add(item);
            int itemInd = Data.Count - 1;
            while (itemInd > 0)
            {
                int parentInd = (itemInd + 1) / 2 - 1;
                T parent = Data[parentInd];
                if (_stopBubleUp(parent, item))
                {
                    break;
                }
                T tmp = Data[itemInd];
                Data[itemInd] = Data[parentInd];
                Data[parentInd] = tmp;
                itemInd = parentInd;
            }
            _heapify(0);
        }
        public virtual T ExtractTop()
        {
            if (Data.Count <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            T top = Data[0];
            Data[0] = Data[Data.Count - 1];
            Data.RemoveAt(Data.Count - 1);
            _heapify(0);
            return top;
        }
        public T Peek()
        {
            return Data[0];
        }
        public int Count
        {
            get { return Data.Count; }
        }
    }
}
