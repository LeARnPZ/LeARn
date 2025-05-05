using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Dictionaries
{
    public static readonly Dictionary<string, int> algorithms = new();
    public static readonly Dictionary<int, string> descriptions = new();
    public static readonly Dictionary<int, string> stepBySteps = new();
    public static readonly Dictionary<int, string> complexityValues = new();

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
        algorithms.Add("DijkstraGraph", 11);
        algorithms.Add("GrahamAlgo", 12);
    }

    private static void ContentToDictionary(string[] content, Dictionary<int, string> dictionary)
    {
        int i = -1;
        string text = "";
        foreach (string line in content)
        {
            if (line.Contains("###"))
            {
                if (i < 0)
                {
                    i++;
                    continue;
                }

                dictionary.Add(i, text);
                text = "";
                i++;
            }
            else
            {
                text += line + '\n';
            }
        }
    }

    /// Dodanie opisów algorytmów
    private static void AddDescriptions()
    {
        TextAsset file = (TextAsset)Resources.Load("Dictionaries/descriptions");
        string[] content = file.text.Split('\n');
        ContentToDictionary(content, descriptions);
    }

    /// Dodanie opisów krok-po-kroku
    private static void AddStepBySteps()
    {
        TextAsset file = (TextAsset) Resources.Load("Dictionaries/steps");
        string[] content = file.text.Split('\n');
        ContentToDictionary(content, stepBySteps);
    }

    /// Dodanie z³o¿onoœci
    private static void AddComplexityValues()
    {
        TextAsset file = (TextAsset)Resources.Load("Dictionaries/complexity");
        string[] content = file.text.Split('\n');
        ContentToDictionary(content, complexityValues);
    }

    static Dictionaries()
    {
        AddIndices();
        AddDescriptions();
        AddStepBySteps();
        AddComplexityValues();
    }
}