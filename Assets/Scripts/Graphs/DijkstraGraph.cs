﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DijkstraGraph : Graphs
{
    private readonly string INF = "∞";
    private List<GameObject> arriveCostTexts = new();
    private bool isFinished = false;

    List<bool> visited = new();
    List<int> arriveCosts = new();
    private List<int> prevs = new();
    private GameObject dropdown;

    private void CreateArriveCostText(int n)
    {
        GameObject gameObject = new($"ArriveCost{n}");
        gameObject.transform.parent = nodesList[n].transform;
        gameObject.transform.localPosition = new Vector3(0, 0.75f, -0.25f);
        gameObject.transform.localScale = Vector3.one;

        gameObject.AddComponent<TextMeshPro>();
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);

        TextMeshPro tmpro = gameObject.GetComponent<TextMeshPro>();
        tmpro.font = Resources.Load<TMP_FontAsset>("Fonts/Montserrat-SemiBold SDF Variant");
        tmpro.fontSize = 6;
        tmpro.color = redTextColor;
        tmpro.alignment = TextAlignmentOptions.Center;
        tmpro.text = INF;

        arriveCostTexts.Add(gameObject);
    }

    private void SetArriveCostText(int n, int val)
    {
        arriveCostTexts[n].GetComponent<TextMeshPro>().text = val.ToString();
    }

    private void ChangeTextColorDuringComparison(int v, int w, GameObject edge, Color color)
    {
        StartCoroutine(ChangeColor(arriveCostTexts[v], color, true));
        StartCoroutine(ChangeColor(arriveCostTexts[w], color, true));
        if (edge != null) StartCoroutine(ChangeColor(edge.transform.GetChild(0).gameObject, color, true));
    }

    protected IEnumerator Dijkstra()
    {
        // Lista ze wskaźnikami odwiedzenia wierzchołków
        for (int i = 0; i < numberOfNodes; i++)
        {
            visited.Add(false);
        }

        // Lista kosztów dotarcia do wierzchołków
        for(int i = 0; i < numberOfNodes; i++)
        {
            arriveCosts.Add(int.MaxValue);
            CreateArriveCostText(i);
        }
        arriveCosts[startingNode] = 0;
        arriveCostTexts[startingNode].GetComponent<TextMeshPro>().text = "0";

        // Lista poprzedników wierzchołków
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
                // Pomijamy odwiedzonych sąsiadów
                if (visited[w]) continue;

                // Zaznaczenie krawędzi
                GameObject currentEdge = edgesList.Find(edge => edge.name == $"{v}-{w}" || edge.name == $"{w}-{v}");
                StartCoroutine(ChangeColor(currentEdge, redColor));
                yield return new WaitForSeconds(timeout);

                // Zaznaczenie wierzchołka
                StartCoroutine(ChangeColor(nodesList[w], yellowColor));
                yield return new WaitForSeconds(timeout);

                // Jeśli do sąsiada lepiej jest dotrzeć przez aktualny wierzchołek, niż dotychczas znalezioną drogą...
                ChangeTextColorDuringComparison(v, w, currentEdge, blueTextColor);
                yield return new WaitForSeconds(timeout);

                if (arriveCosts[w] > arriveCosts[v] + GetEdgeWeight(currentEdge))
                {
                    ChangeTextColorDuringComparison(v, w, currentEdge, greenTextColor);
                    yield return new WaitForSeconds(timeout);

                    // ...ustawiamy nowy koszt dotarcia oraz nowego poprzednika
                    arriveCosts[w] = arriveCosts[v] + GetEdgeWeight(currentEdge);
                    SetArriveCostText(w, arriveCosts[w]);
                    prevs[w] = v;
                    yield return new WaitForSeconds(timeout);
                }

                // Przywrócenie kolorów
                ChangeTextColorDuringComparison(v, w, null, redTextColor);
                StartCoroutine(ChangeColor(currentEdge.transform.GetChild(0).gameObject, Color.white, true));
                StartCoroutine(ChangeColor(currentEdge, blueColor));
                StartCoroutine(ChangeColor(nodesList[w], originalColor));
                yield return new WaitForSeconds(timeout);
            }
            
            StartCoroutine(ChangeSize(nodesList[v], Vector3.one));
        }

        yield return null;
        isFinished = true;
        dropdown.transform.GetChild(1).GetComponent<TMP_Dropdown>().interactable = true;
        MarkPath(1);
    }

    public void MarkPath(int destination)
    {
        if (destination >= numberOfNodes || !isFinished)
            return;

        foreach (GameObject edge in edgesList)
            StartCoroutine(ChangeColor(edge, blueColor));

        int current = destination;
        while (current != startingNode)
        {
            int prev = prevs[current];
            GameObject edge = edgesList.Find(e => e.name == $"{current}-{prev}" || e.name == $"{prev}-{current}");
            StartCoroutine(ChangeColor(edge, redColor));
            current = prev;
        }
    }

    public override void Restart()
    {
        foreach (GameObject obj in arriveCostTexts)
            Destroy(obj);

        arriveCostTexts.Clear();
        visited.Clear();
        prevs.Clear();
        arriveCosts.Clear();

        dropdown.transform.GetChild(1).GetComponent<TMP_Dropdown>().SetValueWithoutNotify(0);

        base.Restart();
    }

    protected override void Start()
    {
        // Wywołanie Start() z klasy nadrzędnej
        base.Start();

        // Uruchomienie animacji
        StartCoroutine(Dijkstra());

        dropdown = GameObject.Find("DijkstraDropdownContainer");
        dropdown.transform.GetChild(1).GetComponent<TMP_Dropdown>().interactable = false;
    }

    private void Update()
    {
        // Obracanie tekstu o 180 stopni w zależności od pozycji kamery
        Camera cam = Camera.main;
        if (Vector3.Dot(transform.forward, cam.transform.position - transform.position) < 0)
        {
            foreach (GameObject arriveText in arriveCostTexts)
                arriveText.transform.SetLocalPositionAndRotation(new Vector3(0, 0.75f, -0.25f), Quaternion.Euler(0, 0, 0));

            foreach (GameObject edge in edgesList)
                edge.transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            foreach (GameObject arriveText in arriveCostTexts)
                arriveText.transform.SetLocalPositionAndRotation(new Vector3(0, 0.75f, 0.25f), Quaternion.Euler(0, 180, 0));
            foreach (GameObject edge in edgesList)
                edge.transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
