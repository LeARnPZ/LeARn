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
        if (!descriptions.ContainsKey(0)) descriptions.Add(0, "Stalin sort (znany równie¿ jako „dictator sort”) to absurdalny algorytm sortowania, w którym ka¿dy element, który nie znajduje siê we w³aœciwej kolejnoœci, jest po prostu usuwany z listy. W efekcie, na koniec faktycznie dostajemy posortowan¹ listê, jednak czêœæ danych zostaje utracona.");
        if (!descriptions.ContainsKey(1)) descriptions.Add(1, "Sortowanie przez wybór to prosty algorytm sortowania, w którym za ka¿dym razem wybierany jest najmniejszy element i zamieniany z kolejnym w kolejnoœci, a¿ lista stanie siê ca³kowicie uporz¹dkowana.");
        if (!descriptions.ContainsKey(2)) descriptions.Add(2, "Sortowanie b¹belkowe to algorytm sortowania, w którym s¹siednie elementy s¹ wielokrotnie porównywane i zamieniane miejscami, a¿ najwiêksze wartoœci „wyp³yn¹” na koniec listy. Proces ten jest powtarzany tak d³ugo, a¿ lista stanie siê posortowana.");
        if (!descriptions.ContainsKey(3)) descriptions.Add(3, "Stos to struktura danych dzia³aj¹ca na zasadzie last in, first out. Elementy s¹ dodawane i usuwane z jednego koñca – wierzcho³ka stosu.");
        if (!descriptions.ContainsKey(4)) descriptions.Add(4, "Kolejka to struktura danych dzia³aj¹ca na zasadzie first in, first out. Elementy s¹ dodawane na koñcu i usuwane z pocz¹tku.");
        if (!descriptions.ContainsKey(5)) descriptions.Add(5, "Kolejka to struktura danych dzia³aj¹ca na zasadzie first in, first out. Elementy s¹ dodawane na koñcu i usuwane z pocz¹tku.");
    }
}
