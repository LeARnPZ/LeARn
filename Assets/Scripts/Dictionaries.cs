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

        // Add descriptions for indices
        if (!descriptions.ContainsKey(0)) descriptions.Add(0, "Stalin sort (znany r�wnie� jako �dictator sort�) to absurdalny algorytm sortowania, w kt�rym ka�dy element, kt�ry nie znajduje si� we w�a�ciwej kolejno�ci, jest po prostu usuwany z listy. W efekcie, na koniec faktycznie dostajemy posortowan� list�, jednak cz�� danych zostaje utracona.");
        if (!descriptions.ContainsKey(1)) descriptions.Add(1, "Sortowanie przez wyb�r to prosty algorytm sortowania, w kt�rym za ka�dym razem wybierany jest najmniejszy element i zamieniany z kolejnym w kolejno�ci, a� lista stanie si� ca�kowicie uporz�dkowana.");
        if (!descriptions.ContainsKey(2)) descriptions.Add(2, "Sortowanie b�belkowe to algorytm sortowania, w kt�rym s�siednie elementy s� wielokrotnie por�wnywane i zamieniane miejscami, a� najwi�ksze warto�ci �wyp�yn�� na koniec listy. Proces ten jest powtarzany tak d�ugo, a� lista stanie si� posortowana.");
        if (!descriptions.ContainsKey(3)) descriptions.Add(3, "Stos to struktura danych dzia�aj�ca na zasadzie LIFO (Last In, First Out), co oznacza, �e ostatni dodany element jest pierwszym usuwanym. Mo�na go por�wna� do stosu talerzy � nowy talerz k�adziemy na wierzchu, a zdejmujemy zawsze ten, kt�ry jest na samej g�rze.\n\nPodstawowe operacje na stosie to:\n� push � dodanie elementu na szczyt stosu,\n� pop � usuni�cie elementu ze szczytu stosu,\n� top � podejrzenie elementu na szczycie bez jego usuwania.");
        if (!descriptions.ContainsKey(4)) descriptions.Add(4, "Kolejka to struktura danych dzia�aj�ca na zasadzie first in, first out. Elementy s� dodawane na ko�cu i usuwane z pocz�tku.");
    }
}
