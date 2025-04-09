using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BFSGraph : Graphs
{
    [SerializeField]
    private GraphQueue visualQueue;

    protected override IEnumerator SearchGraph()
    {
        yield return new WaitForSeconds(timeout);

        Queue<int> queue = new();
        queue.Enqueue(startingNode);
        visualQueue.VisualEnqueue(startingNode);
        StartCoroutine(ChangeColor(nodesList[startingNode], yellowColor));
        yield return new WaitForSeconds(timeout);
        List<int> visited = new();

        while (queue.Count > 0)
        {
            // Pobieramy element z kolejki i wizualnie go usuwamy
            int n = queue.Dequeue();
            visualQueue.VisualDequeue();
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

            // Zaznaczamy krawêdzie powi¹zane z wêz³em
            foreach (GameObject edge in edgesList)
            {
                if (edge.name.Contains($"{n}-") || edge.name.Contains($"-{n}"))
                {
                    StartCoroutine(ChangeColor(edge, orangeColor));
                }
            }
            yield return new WaitForSeconds(timeout);

            // Przetwarzamy s¹siadów wêz³a
            if (neighborsList[n].Any(nb => !visited.Contains(nb) && !queue.Contains(nb)))
            {
                foreach (int nb in neighborsList[n])
                {
                    if (!visited.Contains(nb) && !queue.Contains(nb))
                    {
                        StartCoroutine(ChangeColor(nodesList[nb], yellowColor));
                        queue.Enqueue(nb);
                        visualQueue.VisualEnqueue(nb);
                    }
                }
                yield return new WaitForSeconds(timeout);
            }

            // Przywracamy domyœlny kolor krawêdzi
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

}
