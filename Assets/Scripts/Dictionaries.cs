using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dictionaries : MonoBehaviour
{
    public static Dictionary<string, int> algorithms = new Dictionary<string, int>();
    public static Dictionary<int, string> descriptions = new Dictionary<int, string>();

    private void Awake()
    {
        // Dodanie indeksów do nazw algorytmów
        if (!algorithms.ContainsKey("StalinSort")) algorithms.Add("StalinSort", 0);
        if (!algorithms.ContainsKey("SelectionSort")) algorithms.Add("SelectionSort", 1);
        if (!algorithms.ContainsKey("BubbleSort")) algorithms.Add("BubbleSort", 2);
        if (!algorithms.ContainsKey("StackStruct")) algorithms.Add("StackStruct", 3);
        if (!algorithms.ContainsKey("QueueStruct")) algorithms.Add("QueueStruct", 4);
        if (!algorithms.ContainsKey("ListStruct")) algorithms.Add("ListStruct", 5);
        if (!algorithms.ContainsKey("MergeSort")) algorithms.Add("MergeSort", 6);
        if (!algorithms.ContainsKey("BFSGraph")) algorithms.Add("BFSGraph", 7);
        if (!algorithms.ContainsKey("DFSGraph")) algorithms.Add("DFSGraph", 8);

        // Dodanie opisów do indeksów
        if (!descriptions.ContainsKey(0)) descriptions.Add(0, "Stalin sort (znany równie¿ jako „dictator sort”) to absurdalny algorytm sortowania, w którym ka¿dy element, który nie znajduje siê we w³aœciwej kolejnoœci, jest po prostu usuwany z listy. W efekcie, na koniec faktycznie dostajemy posortowan¹ listê, jednak czêœæ danych zostaje utracona.");
        if (!descriptions.ContainsKey(1)) descriptions.Add(1, "Sortowanie przez wybór to prosty algorytm sortowania, w którym za ka¿dym razem wybierany jest najmniejszy element i zamieniany z kolejnym w kolejnoœci, a¿ lista stanie siê ca³kowicie uporz¹dkowana.");
        if (!descriptions.ContainsKey(2)) descriptions.Add(2, "Sortowanie b¹belkowe to algorytm sortowania, w którym s¹siednie elementy s¹ wielokrotnie porównywane i zamieniane miejscami, a¿ najwiêksze wartoœci „wyp³yn¹” na koniec listy. Proces ten jest powtarzany tak d³ugo, a¿ lista stanie siê posortowana.");
        if (!descriptions.ContainsKey(3)) descriptions.Add(3, "Stos to struktura danych dzia³aj¹ca na zasadzie LIFO (Last In, First Out), co oznacza, ¿e ostatni dodany element jest pierwszym usuwanym. Mo¿na go porównaæ do stosu talerzy – nowy talerz k³adziemy na wierzchu, a zdejmujemy zawsze ten, który jest na samej górze.\n\nPodstawowe operacje na stosie to:\n• push – dodanie elementu na szczyt stosu,\n• pop – usuniêcie elementu ze szczytu stosu,\n• top – podejrzenie elementu na szczycie bez jego usuwania.");
        if (!descriptions.ContainsKey(4)) descriptions.Add(4, "Kolejka to struktura danych dzia³aj¹ca na zasadzie FIFO (First In, First Out), co oznacza, ¿e elementy s¹ usuwane w takiej samej kolejnoœci, w jakiej zosta³y dodane – jak w kolejce do kasy w sklepie.\n\nPodstawowe operacje na kolejce to:\n• enqueue – dodanie elementu na koniec kolejki,\n• dequeue – usuniêcie elementu z pocz¹tku kolejki,\n• front – podejrzenie pierwszego elementu bez jego usuwania.");
        if (!descriptions.ContainsKey(5)) descriptions.Add(5, "Lista to struktura danych umo¿liwiaj¹ca przechowywanie elementów w okreœlonej kolejnoœci, z mo¿liwoœci¹ ich dynamicznego dodawania, usuwania i modyfikowania. W przeciwieñstwie do stosu i kolejki, elementy mog¹ byæ wstawiane lub usuwane w dowolnym miejscu.\n\nPodstawowe operacje na liœcie to:\n• wstawianie – dodawanie elementu na pocz¹tku, koñcu lub w œrodku listy,\n• usuwanie – kasowanie dowolnego elementu,\n• wyszukiwanie – odnalezienie elementu na podstawie wartoœci lub indeksu,\n• iterowanie – przechodzenie przez kolejne elementy listy.");
        if (!descriptions.ContainsKey(6)) descriptions.Add(6, "MergeSort to algorytm sortowania dzia³aj¹cy na zasadzie dziel i rz¹dŸ. Polega na rekurencyjnym dzieleniu tablicy na mniejsze podtablice, sortowaniu ich i scalaniu w jedn¹ posortowan¹ ca³oœæ.");
        if (!descriptions.ContainsKey(7)) descriptions.Add(7, "Algorytm BFS (Breadth-First Search) to algorytm przeszukiwania grafu lub drzewa, który eksploruje wêz³y w kolejnoœci ich odleg³oœci od wêz³a pocz¹tkowego, odwiedzaj¹c najpierw wêz³y na poziomie 1, potem na poziomie 2, i tak dalej.");
        if (!descriptions.ContainsKey(8)) descriptions.Add(8, "Algorytm DFS (Depth-First Search) to algorytm przeszukiwania grafu lub drzewa, który eksploruje wêz³y w g³¹b, odwiedzaj¹c s¹siadów wzd³u¿ jednej œcie¿ki, zanim wróci do wêz³ów na innych œcie¿kach.");
    }
}