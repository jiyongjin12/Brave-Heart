using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ListExtentions
{
    public static T RandomItem<T>(this List<T> list)
    {
        if (list.Count == 0)
            throw new IndexOutOfRangeException("List is Empty");

        var randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }
}
