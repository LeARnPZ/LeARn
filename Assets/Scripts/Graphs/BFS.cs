using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS : Graphs
{
    protected override IEnumerator SearchGraph()
    {
        yield return new WaitForSeconds(timeout);

        Queue<int> queue = new();  
        queue.Enqueue(startingNode); // Dodajemy wêze³ pocz¹tkowy do kolejki
        List<int> visited = new();   // Lista odwiedzonych wêz³ów

        while (queue.Count > 0)
        {
            // Przejœcie do kolejnego wêz³a
            int n = queue.Dequeue();  // Usuwamy wêze³ z przodu kolejki
            StartCoroutine(ChangeSize(nodesList[n], 1.5f * Vector3.one));
            yield return new WaitForSeconds(timeout);

            // Oznaczenie wêz³a jako odwiedzonego
            visited.Add(n);
            StartCoroutine(ChangeColor(nodesList[n], Color.green));
            yield return new WaitForSeconds(timeout);

            // Przeszukanie s¹siadów wêz³a
            foreach (GameObject edge in edgesList)
            {
                if (edge.name.Contains($"{n}-") || edge.name.Contains($"-{n}"))
                {
                    StartCoroutine(ChangeColor(edge, Color.red));
                }
            }
            yield return new WaitForSeconds(timeout);

            // Dodanie nieodwiedzonych s¹siadów do kolejki
            foreach (int nb in neighborsList[n])
            {
                if (!visited.Contains(nb) && !queue.Contains(nb))  // Sprawdzamy, czy s¹siad nie by³ ju¿ odwiedzony ani nie znajduje siê w kolejce
                {
                    StartCoroutine(ChangeColor(nodesList[nb], Color.yellow)); // Zmieniamy kolor wêz³a na ¿ó³ty
                    queue.Enqueue(nb); // Dodajemy s¹siada do kolejki
                }
            }
            yield return new WaitForSeconds(timeout);

            foreach (GameObject edge in edgesList)
            {
                if (edge.name.Contains($"{n}-") || edge.name.Contains($"-{n}"))
                {
                    StartCoroutine(ChangeColor(edge, Color.white)); // Przywracamy kolor krawêdzi do bia³ego
                }
            }

            StartCoroutine(ChangeSize(nodesList[n], Vector3.one)); // Przywracamy oryginalny rozmiar wêz³a
            yield return new WaitForSeconds(timeout);
        }
    }
}
