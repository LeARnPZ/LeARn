using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Dictionaries
{
    public static readonly Dictionary<string, int> algorithms = new();
    public static readonly Dictionary<int, string> descriptions = new();
    public static readonly Dictionary<int, string> stepBySteps = new();

    private static void AddIndices()
    {
        /// Dodanie indeksów do nazw algorytmów
        algorithms.Add("BubbleSort", 0);
        algorithms.Add("QuickSort", 1);
        algorithms.Add("MergeSort", 2);
        algorithms.Add("BucketSort", 3);
        algorithms.Add("InsertionSort", 4);
        algorithms.Add("SelectSort", 5);
        algorithms.Add("StackStruct", 6);
        algorithms.Add("QueueStruct", 7);
        algorithms.Add("ListStruct", 8);
        algorithms.Add("DFSGraph", 9);
        algorithms.Add("BFSGraph", 10);
        algorithms.Add("DijkstraAlgo", 11);
        algorithms.Add("GrahamAlgo", 12);
    }

    private static void AddDescriptions()
    {
        /// Dodanie opisów do indeksów
        StreamReader sr = new("Assets/Scripts/Dictionaries/descriptions.txt");
        int i = 0;
        while (!sr.EndOfStream)
        {
            descriptions.Add(i, sr.ReadLine());
            sr.ReadLine();
            i++;
        }
        sr.Close();
    }

    private static void AddStepBySteps()
    {
        /// Dodanie opisów krok-po-kroku do indeksów
        stepBySteps.Add(0, "");
        stepBySteps.Add(1, "");
        stepBySteps.Add(2, "");
        stepBySteps.Add(3, "");
        stepBySteps.Add(4, "");
        stepBySteps.Add(5, "");
        stepBySteps.Add(6, "");
        stepBySteps.Add(7, "");
        stepBySteps.Add(8, "");
        stepBySteps.Add(9, "");
        stepBySteps.Add(10, "");
        stepBySteps.Add(11, "");
        stepBySteps.Add(12, "");
    }

    static Dictionaries()
    {
        AddIndices();
        AddDescriptions();
        AddStepBySteps();
    }
}