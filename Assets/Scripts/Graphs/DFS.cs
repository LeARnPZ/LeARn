using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DFS : Graphs
{
    //int MarkEdges(int node, Color color)
    //{
    //    int count = 0;
    //    for (int i = 0; i < numberOfNodes; i++)
    //    {
    //        GameObject line = GameObject.Find($"{node}-{i}");
    //        if (line == null)
    //            line = GameObject.Find($"{i}-{node}");
    //        if (line != null)
    //        {
    //            line.GetComponent<Renderer>().material.color = color;
    //            count++;
    //        }
    //    }
    //    return count;
    //}

    protected override IEnumerator SearchGraph()
    {
        yield return new WaitForSeconds(timeout);

        Stack<int> stack = new();
        stack.Push(startingNode);
        List<int> visited = new();

        while (stack.Count > 0)
        {
            // Przejœcie do kolejnego wêz³a
            int n = stack.Pop();
            StartCoroutine(ChangeSize(nodesList[n], 1.5f * Vector3.one));
            yield return new WaitForSeconds(timeout);

            // Oznaczenie wêz³a jako odwiedzonego
            visited.Add(n);
            StartCoroutine(ChangeColor(nodesList[n], Color.green));
            yield return new WaitForSeconds(timeout);

            // Przeszukanie s¹siadów wêz³a
            foreach (int nb in neighborsList[n])
            {
                // Dodanie nieodwiedzonych s¹siadów do stosu
                if (!visited.Contains(nb) && !stack.Contains(nb))
                {
                    StartCoroutine(ChangeColor(nodesList[nb], Color.yellow));
                    stack.Push(nb);
                }
            }
            yield return new WaitForSeconds(timeout);

            StartCoroutine(ChangeSize(nodesList[n], Vector3.one));
            yield return new WaitForSeconds(timeout);
        }
    }
}
