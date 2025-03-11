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
        if (!algorithms.ContainsKey("ListStruct")) algorithms.Add("ListStruct", 5);
        if (!algorithms.ContainsKey("MergeSort")) algorithms.Add("MergeSort", 6);


        // Add descriptions for indices
        if (!descriptions.ContainsKey(0)) descriptions.Add(0, "Stalin sort (znany rwnie jako dictator sort) to absurdalny algorytm sortowania, w ktrym kady element, ktry nie znajduje si we waciwej kolejnoci, jest po prostu usuwany z listy. W efekcie, na koniec faktycznie dostajemy posortowan list, jednak cz danych zostaje utracona.");
        if (!descriptions.ContainsKey(1)) descriptions.Add(1, "Sortowanie przez wybr to prosty algorytm sortowania, w ktrym za kadym razem wybierany jest najmniejszy element i zamieniany z kolejnym w kolejnoci, a lista stanie si cakowicie uporzdkowana.");
        if (!descriptions.ContainsKey(2)) descriptions.Add(2, "Sortowanie bbelkowe to algorytm sortowania, w ktrym ssiednie elementy s wielokrotnie porwnywane i zamieniane miejscami, a najwiksze wartoci wypyn na koniec listy. Proces ten jest powtarzany tak dugo, a lista stanie si posortowana.");
        if (!descriptions.ContainsKey(3)) descriptions.Add(3, "Stos to struktura danych dziaajca na zasadzie LIFO (Last In, First Out), co oznacza, e ostatni dodany element jest pierwszym usuwanym. Mona go porwna do stosu talerzy  nowy talerz kadziemy na wierzchu, a zdejmujemy zawsze ten, ktry jest na samej grze.\n\nPodstawowe operacje na stosie to:\n push  dodanie elementu na szczyt stosu,\n pop  usunicie elementu ze szczytu stosu,\n top  podejrzenie elementu na szczycie bez jego usuwania.");
        if (!descriptions.ContainsKey(4)) descriptions.Add(4, "Kolejka to struktura danych dziaajca na zasadzie FIFO (First In, First Out), co oznacza, e elementy s usuwane w takiej samej kolejnoci, w jakiej zostay dodane  jak w kolejce do kasy w sklepie.\n\nPodstawowe operacje na kolejce to:\n enqueue  dodanie elementu na koniec kolejki,\n dequeue  usunicie elementu z pocztku kolejki,\n front  podejrzenie pierwszego elementu bez jego usuwania.");
        if (!descriptions.ContainsKey(5)) descriptions.Add(5, "Lista to struktura danych umoliwiajca przechowywanie elementw w okrelonej kolejnoci, z moliwoci ich dynamicznego dodawania, usuwania i modyfikowania. W przeciwiestwie do stosu i kolejki, elementy mog by wstawiane lub usuwane w dowolnym miejscu.\n\nPodstawowe operacje na licie to:\n wstawianie  dodawanie elementu na pocztku, kocu lub w rodku listy,\n usuwanie  kasowanie dowolnego elementu,\n wyszukiwanie  odnalezienie elementu na podstawie wartoci lub indeksu,\n iterowanie  przechodzenie przez kolejne elementy listy.");
        if (!descriptions.ContainsKey(6)) descriptions.Add(6, "MergeSort to algorytm sortowania dzia³aj¹cy na zasadzie dziel i rz¹dŸ. Polega na rekurencyjnym dzieleniu tablicy na mniejsze podtablice, sortowaniu ich i scalaniu w jedn¹ posortowan¹ ca³oœæ.");

    }
}


