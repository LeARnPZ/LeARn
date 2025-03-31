using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Dictionaries
{
    public static readonly Dictionary<string, int> algorithms = new();
    public static readonly Dictionary<int, string> descriptions = new();
    public static readonly Dictionary<int, string> stepBySteps = new();

    /// Dodanie indeksów do nazw algorytmów
    private static void AddIndices()
    {
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

    /// Dodanie opisów algorytmów
    private static void AddDescriptions()
    {
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

    /// Dodanie opisów krok-po-kroku
    private static void AddStepBySteps()
    {
        StreamReader sr = new("Assets/Scripts/Dictionaries/steps.txt");
        int i = -1;
        string text = "";
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            if (line.Contains("###"))
            {
                if (i < 0)
                {
                    i++;
                    continue;
                }

                stepBySteps.Add(i, text);
                text = "";
                i++;
            }
            else
            {
                text += line + '\n';
            }
        }
        sr.Close();
    }

    static Dictionaries()
    {
        AddIndices();
        AddDescriptions();
        AddStepBySteps();
    }
}