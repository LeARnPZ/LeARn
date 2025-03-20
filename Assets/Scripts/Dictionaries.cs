using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dictionaries : MonoBehaviour
{
    public static Dictionary<string, int> algorithms = new Dictionary<string, int>();
    public static Dictionary<int, string> descriptions = new Dictionary<int, string>();

    private void Awake()
    {
        // Dodanie indeks�w do nazw algorytm�w
        if (!algorithms.ContainsKey("StalinSort")) algorithms.Add("StalinSort", 0);
        if (!algorithms.ContainsKey("SelectionSort")) algorithms.Add("SelectionSort", 1);
        if (!algorithms.ContainsKey("BubbleSort")) algorithms.Add("BubbleSort", 2);
        if (!algorithms.ContainsKey("StackStruct")) algorithms.Add("StackStruct", 3);
        if (!algorithms.ContainsKey("QueueStruct")) algorithms.Add("QueueStruct", 4);
        if (!algorithms.ContainsKey("ListStruct")) algorithms.Add("ListStruct", 5);
        if (!algorithms.ContainsKey("MergeSort")) algorithms.Add("MergeSort", 6);
        if (!algorithms.ContainsKey("BFSGraph")) algorithms.Add("BFSGraph", 7);
        if (!algorithms.ContainsKey("DFSGraph")) algorithms.Add("DFSGraph", 8);

        // Dodanie opis�w do indeks�w
        if (!descriptions.ContainsKey(0)) descriptions.Add(0, "Stalin sort (znany r�wnie� jako �dictator sort�) to absurdalny algorytm sortowania, w kt�rym ka�dy element, kt�ry nie znajduje si� we w�a�ciwej kolejno�ci, jest po prostu usuwany z listy. W efekcie, na koniec faktycznie dostajemy posortowan� list�, jednak cz�� danych zostaje utracona.");
        if (!descriptions.ContainsKey(1)) descriptions.Add(1, "Sortowanie przez wyb�r to prosty algorytm sortowania, w kt�rym za ka�dym razem wybierany jest najmniejszy element i zamieniany z kolejnym w kolejno�ci, a� lista stanie si� ca�kowicie uporz�dkowana.");
        if (!descriptions.ContainsKey(2)) descriptions.Add(2, "Sortowanie b�belkowe to algorytm sortowania, w kt�rym s�siednie elementy s� wielokrotnie por�wnywane i zamieniane miejscami, a� najwi�ksze warto�ci �wyp�yn�� na koniec listy. Proces ten jest powtarzany tak d�ugo, a� lista stanie si� posortowana.");
        if (!descriptions.ContainsKey(3)) descriptions.Add(3, "Stos to struktura danych dzia�aj�ca na zasadzie LIFO (Last In, First Out), co oznacza, �e ostatni dodany element jest pierwszym usuwanym. Mo�na go por�wna� do stosu talerzy � nowy talerz k�adziemy na wierzchu, a zdejmujemy zawsze ten, kt�ry jest na samej g�rze.\n\nPodstawowe operacje na stosie to:\n� push � dodanie elementu na szczyt stosu,\n� pop � usuni�cie elementu ze szczytu stosu,\n� top � podejrzenie elementu na szczycie bez jego usuwania.");
        if (!descriptions.ContainsKey(4)) descriptions.Add(4, "Kolejka to struktura danych dzia�aj�ca na zasadzie FIFO (First In, First Out), co oznacza, �e elementy s� usuwane w takiej samej kolejno�ci, w jakiej zosta�y dodane � jak w kolejce do kasy w sklepie.\n\nPodstawowe operacje na kolejce to:\n� enqueue � dodanie elementu na koniec kolejki,\n� dequeue � usuni�cie elementu z pocz�tku kolejki,\n� front � podejrzenie pierwszego elementu bez jego usuwania.");
        if (!descriptions.ContainsKey(5)) descriptions.Add(5, "Lista to struktura danych umo�liwiaj�ca przechowywanie element�w w okre�lonej kolejno�ci, z mo�liwo�ci� ich dynamicznego dodawania, usuwania i modyfikowania. W przeciwie�stwie do stosu i kolejki, elementy mog� by� wstawiane lub usuwane w dowolnym miejscu.\n\nPodstawowe operacje na li�cie to:\n� wstawianie � dodawanie elementu na pocz�tku, ko�cu lub w �rodku listy,\n� usuwanie � kasowanie dowolnego elementu,\n� wyszukiwanie � odnalezienie elementu na podstawie warto�ci lub indeksu,\n� iterowanie � przechodzenie przez kolejne elementy listy.");
        if (!descriptions.ContainsKey(6)) descriptions.Add(6, "MergeSort to algorytm sortowania dzia�aj�cy na zasadzie dziel i rz�d�. Polega na rekurencyjnym dzieleniu tablicy na mniejsze podtablice, sortowaniu ich i scalaniu w jedn� posortowan� ca�o��.");
        if (!descriptions.ContainsKey(7)) descriptions.Add(7, "Algorytm BFS (Breadth-First Search) to algorytm przeszukiwania grafu lub drzewa, kt�ry eksploruje w�z�y w kolejno�ci ich odleg�o�ci od w�z�a pocz�tkowego, odwiedzaj�c najpierw w�z�y na poziomie 1, potem na poziomie 2, i tak dalej.");
        if (!descriptions.ContainsKey(8)) descriptions.Add(8, "Algorytm DFS (Depth-First Search) to algorytm przeszukiwania grafu lub drzewa, kt�ry eksploruje w�z�y w g��b, odwiedzaj�c s�siad�w wzd�u� jednej �cie�ki, zanim wr�ci do w�z��w na innych �cie�kach.");
    }
}