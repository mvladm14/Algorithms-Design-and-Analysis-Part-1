namespace MedianMaintenance_with_Heaps.Models
{
    public class MedianMaintenanceAlgorith
    {
        private readonly Heap<int> _minHeap = new Heap<int>(true);
        private readonly Heap<int> _maxHeap = new Heap<int>(false);
        public int GetMedianWithNewItem(int newItem)
        {
            var maxHeapCount = _maxHeap.Count;
            var minHeapCount = _minHeap.Count;
            if (maxHeapCount > minHeapCount)
            {
                if (maxHeapCount > 0 && _maxHeap.Peek() > newItem)
                {
                    var temp = _maxHeap.ExtractTop();
                    _maxHeap.Insert(newItem);
                    newItem = temp;
                }
                _minHeap.Insert(newItem);
            }
            else
            {
                if (minHeapCount > 0 && _minHeap.Peek() < newItem)
                {
                    var temp = _minHeap.ExtractTop();
                    _minHeap.Insert(newItem);
                    newItem = temp;
                }
                _maxHeap.Insert(newItem);
            }
            return _maxHeap.Peek();
        }
    }
}
