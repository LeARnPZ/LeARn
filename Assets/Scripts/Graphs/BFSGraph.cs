using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BFSGraph : Graphs
{
    [SerializeField]
    private GraphQueue graphQueue;

    protected override IEnumerator SearchGraph()
    {
        yield return new WaitForSeconds(timeout);

        Queue<int> queue = new();
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
            StartCoroutine(ChangeSize(nodesList[n], 1.5f * Vector3.one));
            yield return new WaitForSeconds(timeout);

            // Pomini�cie, je�li by� ju� odwiedzony
            if (visited.Contains(n))
            {
                StartCoroutine(ChangeSize(nodesList[n], Vector3.one));
                yield return new WaitForSeconds(timeout);
                continue;
            }

            // Oznaczenie w�z�a jako odwiedzonego
            visited.Add(n);
            StartCoroutine(ChangeColor(nodesList[n], Color.green));
            addText(n.ToString());
            yield return new WaitForSeconds(timeout);

            // Zaznaczamy kraw�dzie powi�zane z w�z�em
            foreach (GameObject edge in edgesList)
            {
                if (edge.name.Contains($"{n}-") || edge.name.Contains($"-{n}"))
                {
                    StartCoroutine(ChangeColor(edge, Color.red));
                }
            }
            yield return new WaitForSeconds(timeout);

            // Przetwarzamy s�siad�w w�z�a
            if (neighborsList[n].Any(nb => !visited.Contains(nb)))
            {
                foreach (int nb in neighborsList[n])
                {
                    if (!visited.Contains(nb))
                    {
                        StartCoroutine(ChangeColor(nodesList[nb], Color.yellow));
                        queue.Enqueue(nb);
                        graphQueue.EnqueueVisual(nb);
                    }
                }
                yield return new WaitForSeconds(timeout);
            }

            // Przywracamy domy�lny kolor kraw�dzi
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
