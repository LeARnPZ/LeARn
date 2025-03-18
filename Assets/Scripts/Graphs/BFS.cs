using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS : Graphs
{
    public VisualQueue queueVisual;

    protected override IEnumerator SearchGraph()
    {
        yield return new WaitForSeconds(timeout);

        Queue<int> queue = new();
        // Dodajemy wêze³ pocz¹tkowy zarówno do logiki jak i wizualizacji
        queue.Enqueue(startingNode);
        queueVisual.EnqueueVisual(startingNode);
        yield return new WaitForSeconds(timeout*2);

        List<int> visited = new();
        while (queue.Count > 0)
        {
            // Pobieramy element z kolejki i wizualnie go usuwamy
            int n = queue.Dequeue();
            queueVisual.DequeueVisual();

            // Wizualizacja aktualnego wêz³a
            StartCoroutine(ChangeSize(nodesList[n], 1.5f * Vector3.one));
            yield return new WaitForSeconds(timeout);

            visited.Add(n);
            StartCoroutine(ChangeColor(nodesList[n], Color.green));
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
                    queueVisual.EnqueueVisual(nb);
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
