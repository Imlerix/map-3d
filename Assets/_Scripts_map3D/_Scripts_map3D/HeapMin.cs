using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts
{
    public class HeapMin: MonoBehaviour // куча минимальная
    {
        public List<int> list;

        public int GetHeapSize { get {return this.list.Count;} }

        public HeapMin()
        {
            list = new List<int>();
        }

        public void Add(int value)
        {
            list.Add(value);
            int i = GetHeapSize - 1;
            int parent = (i - 1) / 2;
            
            while (i > 0 && list[parent] > list[i])
            {
                int temp = list[i];
                list[i] = list[parent];
                list[parent] = temp;
                i = parent;
                parent = (i - 1) / 2;
            } 
        }

        public void Heapify(int i)
        {
            int leftChild;
            int rightChild;
            int smallestChild;
            
            for (;;)
            {
                leftChild = 2 * i + 1;
                rightChild = 2 * i + 2;
                smallestChild = i;

                if (leftChild < GetHeapSize && list[leftChild] < list[smallestChild])
                    smallestChild = leftChild;
                if (rightChild < GetHeapSize && list[rightChild] < list[smallestChild])
                    smallestChild = rightChild;
                if (smallestChild == i)
                    break;

                int temp = list[i];
                list[i] = list[smallestChild];
                list[smallestChild] = temp;
                i = smallestChild;
            }
        }

        public void BuildHeap(int[] sourseArray)
        {
            list = sourseArray.ToList();
            for (int i = GetHeapSize / 2; i >= 0; i--)
                Heapify(i);            
        }

        public int GetMin()
        {
            int result = list[0];
            list[0] = list[GetHeapSize - 1];
            list.RemoveAt(GetHeapSize - 1);
            Heapify(0);
            return result;
        }

        public int PeekMin()
        {
            return list[0];
        }

        public void SortHeap(int[] sourseArray)
        {
            BuildHeap(sourseArray);
            for (int i = sourseArray.Length - 1; i >= 0; i--)
            {
                sourseArray[i] = GetMin();
                Heapify(0);
            }
        }
    }
}