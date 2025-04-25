using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DFSGraph : Graphs
{
    [SerializeField]
    private GraphStack visualStack;

    protected override IEnumerator SearchGraph()
    {
        yield return new WaitForSeconds(timeout);

        Stack<int> stack = new();
        stack.Push(startingNode);
        visualStack.VisualPush(startingNode);
        StartCoroutine(ChangeColor(nodesList[startingNode], yellowColor));
        yield return new WaitForSeconds(timeout);
        List<int> visited = new();

        while (stack.Count > 0)
        {
            // Przejœcie do kolejnego wêz³a
            int n = stack.Pop();
            visualStack.VisualPop();
            StartCoroutine(ChangeSize(nodesList[n], 1.5f * Vector3.one));
            yield return new WaitForSeconds(timeout);

            // Pominiêcie, jeœli by³ ju¿ odwiedzony
            if (visited.Contains(n))
            {
                StartCoroutine(ChangeSize(nodesList[n], Vector3.one));
                yield return new WaitForSeconds(timeout);
                continue;
            }

            // Oznaczenie wêz³a jako odwiedzonego
            visited.Add(n);
            StartCoroutine(ChangeColor(nodesList[n], greenColor));
            AddOrder(n);
            yield return new WaitForSeconds(timeout);

            // Przeszukanie s¹siadów wêz³a
            foreach (GameObject edge in edgesList)
            {
                if (edge.name.Contains($"{n}-") || edge.name.Contains($"-{n}"))
                {
                    StartCoroutine(ChangeColor(edge, orangeColor));
                }
            }
            yield return new WaitForSeconds(timeout);

            // Dodanie nieodwiedzonych s¹siadów do stosu
            if (neighborsList[n].Any(nb => !visited.Contains(nb)))
            {
                List<int> tmpList = new(neighborsList[n]);
                tmpList.Reverse();
                foreach (int nb in tmpList)
                {
                    if (!visited.Contains(nb))
                    {
                        StartCoroutine(ChangeColor(nodesList[nb], yellowColor));
                        stack.Push(nb);
                        visualStack.VisualPush(nb);
                    }
                }
                yield return new WaitForSeconds(timeout);
            }

            foreach (GameObject edge in edgesList)
            {
                if (edge.name.Contains($"{n}-") || edge.name.Contains($"-{n}"))
                {
                    StartCoroutine(ChangeColor(edge, blueColor));
                }
            }

            StartCoroutine(ChangeSize(nodesList[n], Vector3.one));
            yield return new WaitForSeconds(timeout);
        }
    }

    protected override void Start()
    {
        // Wywo³anie Start() z klasy nadrzêdnej
        base.Start();

        // Wyczyszczenie tekstu w belce z kolejnoœci¹
        searchOrder.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
        searchOrder.transform.GetChild(1).GetComponent<TextMeshPro>().text = "";

        // Uruchomienie animacji
        StartCoroutine(SearchGraph());
    }
}
