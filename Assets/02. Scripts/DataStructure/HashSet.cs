using log4net.DateFormatter;
using System;
using System.Collections.Generic;
using UnityEngine;

public struct Slot<T>
{
    public readonly int HashCode { get; }
    public int Next { get; set; }
    public readonly T Item { get; }

    public Slot(int hashCode, int next, T item)
    {
        HashCode = hashCode;
        Next = next;
        Item = item;
    }
}

public class HashSet<T>
{
    private int[] _buckets;
    private Slot<T>[] _slots;
    private int _count;
    private int _freeList;

    public int Count { get => _count; }

    public HashSet(int capacity)
    {
        _buckets = new int[capacity];
        _slots = new Slot<T>[capacity];
        _freeList = -1;

        for (int i = 0; i < _buckets.Length; i++)
        {
            _buckets[i] = -1;
        }
    }

    public void Add(T item)
    {
        int hashCode = HashFunc(item);
        int bucketIndex = GetBucketIndex(hashCode);

        for (int curSlot = _buckets[bucketIndex]; curSlot != -1; curSlot = _slots[curSlot].Next)
        {
            if (_slots[curSlot].HashCode == hashCode && Equals(_slots[curSlot].Item, item))
            {
                return;
            }
        }

        int index;
        if (0 <= _freeList)
        {
            index = _freeList;
            _freeList = _slots[index].Next;
        }
        else
        {
            if (_count == _slots.Length)
            {
                Resize();
                bucketIndex = GetBucketIndex(hashCode);
            }
            index = _count;
            _count++;
        }

        _slots[index] = new Slot<T>(hashCode, _buckets[bucketIndex], item);
        _buckets[bucketIndex] = index;
    }

    public bool Remove(T item)
    {
        int hashCode = HashFunc(item);
        int bucketIndex = GetBucketIndex(hashCode);
        int prevSlot = -1;

        for (int curSlot = _buckets[bucketIndex]; curSlot != -1; curSlot = _slots[curSlot].Next)
        {
            if (_slots[curSlot].HashCode == hashCode && Equals(_slots[curSlot].Item, item))
            {
                if (prevSlot < 0)
                {
                    _buckets[bucketIndex] = _slots[curSlot].Next;
                }
                else
                {
                    _slots[prevSlot].Next = _slots[curSlot].Next;
                }

                _slots[curSlot] = new Slot<T>(-1, _freeList, default);
                _freeList = curSlot;
                _count--;
                return true;
            }
            prevSlot = curSlot;
        }

        return false;
    }

    public void Clear()
    {
        if (_count > 0)
        {
            Array.Clear(_slots, 0, _count);
            Array.Fill(_buckets, -1);
            _count = 0;
            _freeList = -1;
        }
    }

    public bool TryGetValue(T item, out T returnedItem)
    {
        int hashCode = HashFunc(item);
        int bucketIndex = GetBucketIndex(hashCode);

        for (int i = _buckets[bucketIndex]; i != -1; i = _slots[i].Next)
        {
            if (_slots[i].HashCode == hashCode && Equals(_slots[i].Item, item))
            {
                returnedItem = _slots[i].Item;
                return true;
            }
        }
        returnedItem = default;
        return false;
    }

    private int HashFunc(T item)
    {
        return item == null ? 0 : item.GetHashCode();
    }

    private int GetBucketIndex(int hashCode)
    {
        return hashCode % _buckets.Length;
    }

    private void Resize()
    {
        int newSize = _count * 2 + 1;
        int[] newBuckets = new int[newSize];
        Slot<T>[] newSlots = new Slot<T>[newSize];

        Array.Copy(_slots, newSlots, _count);
        Array.Fill(newBuckets, -1);

        for (int i = 0; i < _count; i++)
        {
            int bucketIndex = GetBucketIndex(newSlots[i].HashCode);
            newSlots[i].Next = newBuckets[bucketIndex];
            newBuckets[bucketIndex] = i;
        }

        _buckets = newBuckets;
        _slots = newSlots;
    }
}
