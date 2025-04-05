using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DijkstraAlgo : Graphs
{
    protected IEnumerator Dijkstra()
    {
        List<bool> visited = new();
        for (int i = 0; i < numberOfNodes; i++)
        {
            visited.Add(false);
        }

        List<int> arriveCosts = new();
        for(int i = 0; i < numberOfNodes; i++)
        {
            arriveCosts.Add(int.MaxValue);
        }

        List<int> prevs = new();
        for (int i = 0; i < numberOfNodes; i++)
        {
            prevs.Add(-1);
        }

        yield return null;
    }

    protected override void Awake()
    {
        // Wylosowanie wersji grafu oraz utworzenie do niego macierzy i list s�siedztwa
        int graphVersion = 0; //(int)(Random.value * 10) % 5; // <-- po znaku modulo musi by� liczba dost�pnych wersji grafu
        CreateMatrix(graphVersion);
        CreateNeighborsList();

        // Uaktywnienie odpowiedniej wersji grafu oraz pobranie jego w�z��w i kraw�dzi
        graphVersions.transform.GetChild(graphVersion).gameObject.SetActive(true);
        nodes = graphVersions.transform.GetChild(graphVersion).GetChild(0).gameObject;
        edges = graphVersions.transform.GetChild(graphVersion).GetChild(1).gameObject;
    }

    protected override void Start()
    {
        // Dodanie w�z��w do listy i nadanie etykiet
        for (int i = 0; i < numberOfNodes; i++)
        {
            nodesList.Add(nodes.transform.GetChild(i).gameObject);
            nodesList[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = i.ToString();
            nodesList[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = i.ToString();
        }

        // Utworzenie kraw�dzi ��cz�cych odpowiednie w�z�y i dodanie ich do listy
        DrawEdges(true);

        isPaused = false;
        Time.timeScale = 1f;

        // Uruchomienie animacji
        Dijkstra();
    }
}
