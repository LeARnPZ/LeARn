using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DijkstraAlgo : Graphs
{
    private readonly string INF = "∞";
    private List<GameObject> arriveCostTexts = new();

    private void CreateArriveCostText(int n)
    {
        GameObject gameObject = new($"ArriveCost{n}");
        gameObject.transform.parent = nodesList[n].transform;
        gameObject.transform.localPosition = new Vector3(0, 1, 0);
        gameObject.transform.localScale = Vector3.one;

        gameObject.AddComponent<TextMeshPro>();
        gameObject.GetComponent<TextMeshPro>().text = INF;
        gameObject.GetComponent<TextMeshPro>().fontSize = 6;
        gameObject.GetComponent<TextMeshPro>().color = Color.red;
        gameObject.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
        
        arriveCostTexts.Add(gameObject);
    }

    protected IEnumerator Dijkstra()
    {
        // Lista ze wskaźnikami odwiedzenia wierzchołków
        List<bool> visited = new();
        for (int i = 0; i < numberOfNodes; i++)
        {
            visited.Add(false);
        }

        // Lista kosztów dotarcia do wierzchołków
        List<int> arriveCosts = new();
        for(int i = 0; i < numberOfNodes; i++)
        {
            arriveCosts.Add(int.MaxValue);
            CreateArriveCostText(i);
        }
        arriveCosts[startingNode] = 0;
        arriveCostTexts[startingNode].GetComponent<TextMeshPro>().text = "0";

        // Lista poprzedników wierzchołków
        List<int> prevs = new();
        for (int i = 0; i < numberOfNodes; i++)
        {
            prevs.Add(-1);
        }

        // Algorytm Dijkstry
        for (int i = 0; i < numberOfNodes; i++)
        {
            // Znajdujemy nieodwiedzony wierzchołek o najniższym koszcie dotarcia
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
            StartCoroutine(ChangeSize(nodesList[v], 1.5f * Vector3.one));
            StartCoroutine(ChangeColor(nodesList[v], yellowColor));
            yield return new WaitForSeconds(timeout);

            // Ustawiamy wierzchołek jako odwiedzony
            visited[v] = true;
            StartCoroutine(ChangeColor(nodesList[v], greenColor));
            yield return new WaitForSeconds(timeout);

            // Sprawdzamy sąsiadów wierzchołka
            foreach (int w in neighborsList[v])
            {
                GameObject edge = edgesList.Find(e => e.name == $"{v}-{w}" || e.name == $"{w}-{v}");
                StartCoroutine(ChangeColor(edge, orangeColor));
                yield return new WaitForSeconds(timeout);

                // Pomijamy odwiedzonych sąsiadów
                if (visited[w])
                {
                    StartCoroutine(ChangeColor(edge, blueColor));
                    continue;
                }
                StartCoroutine(ChangeColor(nodesList[w], yellowColor));
                yield return new WaitForSeconds(timeout);

                // Jeśli do sąsiada lepiej jest dotrzeć przez aktualny wierzchołek, niż dotychczas znalezioną drogą...
                int currentEdge = GetEdgeWeight(edgesList.Find(edge => edge.name == $"{v}-{w}" || edge.name == $"{w}-{v}"));
                if (arriveCosts[w] > arriveCosts[v] + currentEdge)
                {
                    // ...ustawiamy nowy koszt dotarcia oraz nowego poprzednika
                    arriveCosts[w] = arriveCosts[v] + currentEdge;
                    prevs[w] = v;
                }

                StartCoroutine(ChangeColor(edge, blueColor));
                StartCoroutine(ChangeColor(nodesList[w], originalColor));
            }
            
            StartCoroutine(ChangeSize(nodesList[v], Vector3.one));
        }

        //Debug.Log("ARRIVE COSTS:");
        //arriveCosts.ForEach(x => Debug.Log(x));
        //Debug.Log("PREVS:");
        //prevs.ForEach(x => Debug.Log(x));

        yield return null;
    }

    protected override void Awake()
    {
        // Wylosowanie wersji grafu oraz utworzenie do niego macierzy i list sąsiedztwa
        int graphVersion = 0;// (int)(Random.value * 10) % 5; // <-- po znaku modulo musi być liczba dostępnych wersji grafu
        CreateMatrix(graphVersion);
        CreateNeighborsList();

        // Uaktywnienie odpowiedniej wersji grafu oraz pobranie jego węzłów i krawędzi
        graphVersions.transform.GetChild(graphVersion).gameObject.SetActive(true);
        nodes = graphVersions.transform.GetChild(graphVersion).GetChild(0).gameObject;
        edges = graphVersions.transform.GetChild(graphVersion).GetChild(1).gameObject;
        originalColor = nodes.transform.GetChild(0).GetComponent<Renderer>().material.color;
    }

    protected override void Start()
    {
        // Dodanie węzłów do listy i nadanie etykiet
        for (int i = 0; i < numberOfNodes; i++)
        {
            nodesList.Add(nodes.transform.GetChild(i).gameObject);
            nodesList[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = i.ToString();
            nodesList[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = i.ToString();
        }

        // Utworzenie krawędzi łączących odpowiednie węzły i dodanie ich do listy
        DrawEdges(true);

        isPaused = false;
        Time.timeScale = 1f;

        // Uruchomienie animacji
        StartCoroutine(Dijkstra());
    }
}
