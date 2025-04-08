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

            // Pomini�cie, je�li by� ju� odwiedzony
            if (visited.Contains(n))
            {
                StartCoroutine(ChangeSize(nodesList[n], Vector3.one));
                yield return new WaitForSeconds(timeout);
                continue;
            }

            // Oznaczenie w�z�a jako odwiedzonego
            visited.Add(n);
            StartCoroutine(ChangeColor(nodesList[n], greenColor));
            AddOrder(n);
            yield return new WaitForSeconds(timeout);

            // Zaznaczamy kraw�dzie powi�zane z w�z�em
            foreach (GameObject edge in edgesList)
            {
                if (edge.name.Contains($"{n}-") || edge.name.Contains($"-{n}"))
                {
                    StartCoroutine(ChangeColor(edge, orangeColor));
                }
            }
            yield return new WaitForSeconds(timeout);

            // Przetwarzamy s�siad�w w�z�a
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

            // Przywracamy domy�lny kolor kraw�dzi
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
