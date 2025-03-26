using System.Collections;
using System.Collections.Generic;

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
        descriptions.Add(0, "Sortowanie b¹belkowe to algorytm sortowania, w którym s¹siednie elementy s¹ wielokrotnie porównywane i zamieniane miejscami, a¿ najwiêksze wartoœci „wyp³yn¹” na koniec listy. Proces ten jest powtarzany tak d³ugo, a¿ lista stanie siê posortowana.");
        
        descriptions.Add(1, "Sortowanie szybkie - brak opisu.");
        
        descriptions.Add(2, "Sortowanie przez scalanie to algorytm sortowania dzia³aj¹cy na zasadzie dziel i rz¹dŸ. Polega na rekurencyjnym dzieleniu tablicy na mniejsze podtablice, sortowaniu ich i scalaniu w jedn¹ posortowan¹ ca³oœæ.");
        
        descriptions.Add(3, "Sortownaie kube³kowe - brak opisu.");
        
        descriptions.Add(4, "Sortowanie przez wstawianie - brak opisu.");
        
        descriptions.Add(5, "Sortowanie przez wybór to prosty algorytm sortowania, w którym za ka¿dym razem wybierany jest najmniejszy element i zamieniany z kolejnym w kolejnoœci, a¿ lista stanie siê ca³kowicie uporz¹dkowana.");
        
        descriptions.Add(6, "Stos to struktura danych dzia³aj¹ca na zasadzie LIFO (Last In, First Out), co oznacza, ¿e ostatni dodany element jest pierwszym usuwanym. Mo¿na go porównaæ do stosu talerzy – nowy talerz k³adziemy na wierzchu, a zdejmujemy zawsze ten, który jest na samej górze.\n\nPodstawowe operacje na stosie to:\n• push – dodanie elementu na szczyt stosu,\n• pop – usuniêcie elementu ze szczytu stosu,\n• top – podejrzenie elementu na szczycie bez jego usuwania.");
        
        descriptions.Add(7, "Kolejka to struktura danych dzia³aj¹ca na zasadzie FIFO (First In, First Out), co oznacza, ¿e elementy s¹ usuwane w takiej samej kolejnoœci, w jakiej zosta³y dodane – jak w kolejce do kasy w sklepie.\n\nPodstawowe operacje na kolejce to:\n• enqueue – dodanie elementu na koniec kolejki,\n• dequeue – usuniêcie elementu z pocz¹tku kolejki,\n• front – podejrzenie pierwszego elementu bez jego usuwania.");
        
        descriptions.Add(8, "Lista to struktura danych umo¿liwiaj¹ca przechowywanie elementów w okreœlonej kolejnoœci, z mo¿liwoœci¹ ich dynamicznego dodawania, usuwania i modyfikowania. W przeciwieñstwie do stosu i kolejki, elementy mog¹ byæ wstawiane lub usuwane w dowolnym miejscu.\n\nPodstawowe operacje na liœcie to:\n• wstawianie – dodawanie elementu na pocz¹tku, koñcu lub w œrodku listy,\n• usuwanie – kasowanie dowolnego elementu,\n• wyszukiwanie – odnalezienie elementu na podstawie wartoœci lub indeksu,\n• iterowanie – przechodzenie przez kolejne elementy listy.");

        descriptions.Add(9, "Algorytm DFS (Depth-First Search) to algorytm przeszukiwania grafu lub drzewa, który eksploruje wêz³y w g³¹b, odwiedzaj¹c s¹siadów wzd³u¿ jednej œcie¿ki, zanim wróci do wêz³ów na innych œcie¿kach.");
        
        descriptions.Add(10, "Algorytm BFS (Breadth-First Search) to algorytm przeszukiwania grafu lub drzewa, który eksploruje wêz³y w kolejnoœci ich odleg³oœci od wêz³a pocz¹tkowego, odwiedzaj¹c najpierw wêz³y na poziomie 1, potem na poziomie 2, i tak dalej.");
        
        descriptions.Add(11, "Algorytm Dijkstry - brak opisu.");
        
        descriptions.Add(12, "Algorytm Grahama - brak opisu.");
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