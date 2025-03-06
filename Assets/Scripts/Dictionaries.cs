using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dictionaries : MonoBehaviour
{
    public static Dictionary<string, int> algorithms = new Dictionary<string, int>();
    public static Dictionary<int, string> descriptions = new Dictionary<int, string>();

    // Start is called before the first frame update
    private void Awake()
    {
        // Add indices for algorithms
        if (!algorithms.ContainsKey("StalinSort")) algorithms.Add("StalinSort", 0);
        if (!algorithms.ContainsKey("SelectionSort")) algorithms.Add("SelectionSort", 1);
        if (!algorithms.ContainsKey("BubbleSort")) algorithms.Add("BubbleSort", 2);
        if (!algorithms.ContainsKey("StackStruct")) algorithms.Add("StackStruct", 3);
        if (!algorithms.ContainsKey("QueueStruct")) algorithms.Add("QueueStruct", 4);
        if (!algorithms.ContainsKey("MergeSort")) algorithms.Add("MergeSort", 5);

        // Add descriptions for indices
        if (!descriptions.ContainsKey(0)) descriptions.Add(0, "Stalin sort (znany również jako „dictator sort”) to absurdalny algorytm sortowania, w którym każdy element, który nie znajduje się we właściwej kolejności, jest po prostu usuwany z listy. W efekcie, na koniec faktycznie dostajemy posortowaną listę, jednak część danych zostaje utracona.");
        if (!descriptions.ContainsKey(1)) descriptions.Add(1, "Sortowanie przez wybór to prosty algorytm sortowania, w którym za każdym razem wybierany jest najmniejszy element i zamieniany z kolejnym w kolejności, aż lista stanie się całkowicie uporządkowana.");
        if (!descriptions.ContainsKey(2)) descriptions.Add(2, "Sortowanie bąbelkowe to algorytm sortowania, w którym sąsiednie elementy są wielokrotnie porównywane i zamieniane miejscami, aż największe wartości „wypłyną” na koniec listy. Proces ten jest powtarzany tak długo, aż lista stanie się posortowana.");
        if (!descriptions.ContainsKey(3)) descriptions.Add(3, "Stos to struktura danych działająca na zasadzie last in, first out. Elementy są dodawane i usuwane z jednego końca – wierzchołka stosu.");
        if (!descriptions.ContainsKey(4)) descriptions.Add(4, "Kolejka to struktura danych działająca na zasadzie first in, first out. Elementy są dodawane na końcu i usuwane z początku.");
        if (!descriptions.ContainsKey(5)) descriptions.Add(5, "MergeSort to algorytm sortowania działający na zasadzie dziel i rządź. Polega na rekurencyjnym dzieleniu tablicy na mniejsze podtablice, sortowaniu ich i scalaniu w jedną posortowaną całość.");
    }
}
