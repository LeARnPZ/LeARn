using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFSGraph : Graphs
{
    [SerializeField]
    private GraphQueue graphQueue;

    protected override IEnumerator SearchGraph()
    {
        yield return new WaitForSeconds(timeout);

        Queue<int> queue = new();
        // Dodajemy wêze³ pocz¹tkowy zarówno do logiki jak i wizualizacji
        queue.Enqueue(startingNode);
        graphQueue.EnqueueVisual(startingNode);
        StartCoroutine(ChangeColor(nodesList[startingNode], Color.yellow));
        yield return new WaitForSeconds(timeout);

        List<int> visited = new();
        while (queue.Count > 0)
        {
            // Pobieramy element z kolejki i wizualnie go usuwamy
            int n = queue.Dequeue();
            graphQueue.DequeueVisual();

            // Wizualizacja aktualnego wêz³a
            StartCoroutine(ChangeSize(nodesList[n], 1.5f * Vector3.one));
            yield return new WaitForSeconds(timeout);

            visited.Add(n);
            StartCoroutine(ChangeColor(nodesList[n], Color.green));
            addText(n.ToString());
            yield return new WaitForSeconds(timeout);

            // Zaznaczamy krawêdzie powi¹zane z wêz³em
            foreach (GameObject edge in edgesList)
            {
                if (edge.name.Contains($"{n}-") || edge.name.Contains($"-{n}"))
                {
                    StartCoroutine(ChangeColor(edge, Color.red));
                }
            }
            yield return new WaitForSeconds(timeout);

            // Przetwarzamy s¹siadów wêz³a
            foreach (int nb in neighborsList[n])
            {
                if (!visited.Contains(nb) && !queue.Contains(nb))
                {
                    StartCoroutine(ChangeColor(nodesList[nb], Color.yellow));
                    queue.Enqueue(nb);
                    graphQueue.EnqueueVisual(nb);
                }
            }
            yield return new WaitForSeconds(timeout);

            // Przywracamy domyœlny kolor krawêdzi
            foreach (GameObject edge in edgesList)
            {
                if (edge.name.Contains($"{n}-") || edge.name.Contains($"-{n}"))
                {
                    StartCoroutine(ChangeColor(edge, Color.white));
                }
            }

            StartCoroutine(ChangeSize(nodesList[n], Vector3.one));
            yield return new WaitForSeconds(timeout);
        }
    }

}
