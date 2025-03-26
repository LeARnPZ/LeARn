using System.Collections;
using System.Collections.Generic;

public static class Dictionaries
{
    public static readonly Dictionary<string, int> algorithms = new();
    public static readonly Dictionary<int, string> descriptions = new();
    public static readonly Dictionary<int, string> stepBySteps = new();

    private static void AddIndices()
    {
        /// Dodanie indeks�w do nazw algorytm�w
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
        /// Dodanie opis�w do indeks�w
        descriptions.Add(0, "Sortowanie b�belkowe to algorytm sortowania, w kt�rym s�siednie elementy s� wielokrotnie por�wnywane i zamieniane miejscami, a� najwi�ksze warto�ci �wyp�yn�� na koniec listy. Proces ten jest powtarzany tak d�ugo, a� lista stanie si� posortowana.");
        
        descriptions.Add(1, "Sortowanie szybkie - brak opisu.");
        
        descriptions.Add(2, "Sortowanie przez scalanie to algorytm sortowania dzia�aj�cy na zasadzie dziel i rz�d�. Polega na rekurencyjnym dzieleniu tablicy na mniejsze podtablice, sortowaniu ich i scalaniu w jedn� posortowan� ca�o��.");
        
        descriptions.Add(3, "Sortownaie kube�kowe - brak opisu.");
        
        descriptions.Add(4, "Sortowanie przez wstawianie - brak opisu.");
        
        descriptions.Add(5, "Sortowanie przez wyb�r to prosty algorytm sortowania, w kt�rym za ka�dym razem wybierany jest najmniejszy element i zamieniany z kolejnym w kolejno�ci, a� lista stanie si� ca�kowicie uporz�dkowana.");
        
        descriptions.Add(6, "Stos to struktura danych dzia�aj�ca na zasadzie LIFO (Last In, First Out), co oznacza, �e ostatni dodany element jest pierwszym usuwanym. Mo�na go por�wna� do stosu talerzy � nowy talerz k�adziemy na wierzchu, a zdejmujemy zawsze ten, kt�ry jest na samej g�rze.\n\nPodstawowe operacje na stosie to:\n� push � dodanie elementu na szczyt stosu,\n� pop � usuni�cie elementu ze szczytu stosu,\n� top � podejrzenie elementu na szczycie bez jego usuwania.");
        
        descriptions.Add(7, "Kolejka to struktura danych dzia�aj�ca na zasadzie FIFO (First In, First Out), co oznacza, �e elementy s� usuwane w takiej samej kolejno�ci, w jakiej zosta�y dodane � jak w kolejce do kasy w sklepie.\n\nPodstawowe operacje na kolejce to:\n� enqueue � dodanie elementu na koniec kolejki,\n� dequeue � usuni�cie elementu z pocz�tku kolejki,\n� front � podejrzenie pierwszego elementu bez jego usuwania.");
        
        descriptions.Add(8, "Lista to struktura danych umo�liwiaj�ca przechowywanie element�w w okre�lonej kolejno�ci, z mo�liwo�ci� ich dynamicznego dodawania, usuwania i modyfikowania. W przeciwie�stwie do stosu i kolejki, elementy mog� by� wstawiane lub usuwane w dowolnym miejscu.\n\nPodstawowe operacje na li�cie to:\n� wstawianie � dodawanie elementu na pocz�tku, ko�cu lub w �rodku listy,\n� usuwanie � kasowanie dowolnego elementu,\n� wyszukiwanie � odnalezienie elementu na podstawie warto�ci lub indeksu,\n� iterowanie � przechodzenie przez kolejne elementy listy.");

        descriptions.Add(9, "Algorytm DFS (Depth-First Search) to algorytm przeszukiwania grafu lub drzewa, kt�ry eksploruje w�z�y w g��b, odwiedzaj�c s�siad�w wzd�u� jednej �cie�ki, zanim wr�ci do w�z��w na innych �cie�kach.");
        
        descriptions.Add(10, "Algorytm BFS (Breadth-First Search) to algorytm przeszukiwania grafu lub drzewa, kt�ry eksploruje w�z�y w kolejno�ci ich odleg�o�ci od w�z�a pocz�tkowego, odwiedzaj�c najpierw w�z�y na poziomie 1, potem na poziomie 2, i tak dalej.");
        
        descriptions.Add(11, "Algorytm Dijkstry - brak opisu.");
        
        descriptions.Add(12, "Algorytm Grahama - brak opisu.");
    }

    private static void AddStepBySteps()
    {
        /// Dodanie opis�w krok-po-kroku do indeks�w
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