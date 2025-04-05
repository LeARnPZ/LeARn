using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        arriveCosts[startingNode] = 0;

        List<int> prevs = new();
        for (int i = 0; i < numberOfNodes; i++)
        {
            prevs.Add(-1);
        }

        for (int i = 0; i < numberOfNodes; i++)
        {
            int v = -1;
            int minCost = int.MaxValue;
            for (int j = 0; j < numberOfNodes; j++)
            {
                if (!visited[j] && arriveCosts[j] < minCost)
                {
                    minCost = arriveCosts[j];
                    v = j;
                }
            }
            if (v == -1) yield break;
            visited[v] = true;

            foreach (int w in neighborsList[v])
            {
                if (visited[w]) continue;

                if (arriveCosts[w] > arriveCosts[v] + GetEdgeWeight(edgesList.Find(edge => edge.name == $"{v}-{w}" || edge.name == $"{w}-{v}")))
                {
                    arriveCosts[w] = arriveCosts[v] + GetEdgeWeight(edgesList.Find(edge => edge.name == $"{v}-{w}" || edge.name == $"{w}-{v}"));
                    prevs[w] = v;
                }
            }
        }

        //Debug.Log("ARRIVE COSTS:");
        //arriveCosts.ForEach(x => Debug.Log(x));
        //Debug.Log("PREVS:");
        //prevs.ForEach(x => Debug.Log(x));

        yield return null;
    }

    protected override void Awake()
    {
        // Wylosowanie wersji grafu oraz utworzenie do niego macierzy i list s¹siedztwa
        int graphVersion = (int)(Random.value * 10) % 5; // <-- po znaku modulo musi byæ liczba dostêpnych wersji grafu
        CreateMatrix(graphVersion);
        CreateNeighborsList();

        // Uaktywnienie odpowiedniej wersji grafu oraz pobranie jego wêz³ów i krawêdzi
        graphVersions.transform.GetChild(graphVersion).gameObject.SetActive(true);
        nodes = graphVersions.transform.GetChild(graphVersion).GetChild(0).gameObject;
        edges = graphVersions.transform.GetChild(graphVersion).GetChild(1).gameObject;
    }

    protected override void Start()
    {
        // Dodanie wêz³ów do listy i nadanie etykiet
        for (int i = 0; i < numberOfNodes; i++)
        {
            nodesList.Add(nodes.transform.GetChild(i).gameObject);
            nodesList[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = i.ToString();
            nodesList[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = i.ToString();
        }

        // Utworzenie krawêdzi ³¹cz¹cych odpowiednie wêz³y i dodanie ich do listy
        DrawEdges(true);

        isPaused = false;
        Time.timeScale = 1f;

        // Uruchomienie animacji
        StartCoroutine(Dijkstra());
    }
}
