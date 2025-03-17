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
        queue.Enqueue(startingNode); // Dodajemy w�ze� pocz�tkowy do kolejki
        List<int> visited = new();   // Lista odwiedzonych w�z��w

        while (queue.Count > 0)
        {
            // Przej�cie do kolejnego w�z�a
            int n = queue.Dequeue();  // Usuwamy w�ze� z przodu kolejki
            StartCoroutine(ChangeSize(nodesList[n], 1.5f * Vector3.one));
            yield return new WaitForSeconds(timeout);

            // Oznaczenie w�z�a jako odwiedzonego
            visited.Add(n);
            StartCoroutine(ChangeColor(nodesList[n], Color.green));
            yield return new WaitForSeconds(timeout);

            // Przeszukanie s�siad�w w�z�a
            foreach (GameObject edge in edgesList)
            {
                if (edge.name.Contains($"{n}-") || edge.name.Contains($"-{n}"))
                {
                    StartCoroutine(ChangeColor(edge, Color.red));
                }
            }
            yield return new WaitForSeconds(timeout);

            // Dodanie nieodwiedzonych s�siad�w do kolejki
            foreach (int nb in neighborsList[n])
            {
                if (!visited.Contains(nb) && !queue.Contains(nb))  // Sprawdzamy, czy s�siad nie by� ju� odwiedzony ani nie znajduje si� w kolejce
                {
                    StartCoroutine(ChangeColor(nodesList[nb], Color.yellow)); // Zmieniamy kolor w�z�a na ��ty
                    queue.Enqueue(nb); // Dodajemy s�siada do kolejki
                }
            }
            yield return new WaitForSeconds(timeout);

            foreach (GameObject edge in edgesList)
            {
                if (edge.name.Contains($"{n}-") || edge.name.Contains($"-{n}"))
                {
                    StartCoroutine(ChangeColor(edge, Color.white)); // Przywracamy kolor kraw�dzi do bia�ego
                }
            }

            StartCoroutine(ChangeSize(nodesList[n], Vector3.one)); // Przywracamy oryginalny rozmiar w�z�a
            yield return new WaitForSeconds(timeout);
        }
    }
}
