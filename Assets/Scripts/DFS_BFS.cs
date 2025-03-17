using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DFS_BFS : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    [Header("Ustawienia animacji")]
    [SerializeField]
    private int numberOfNodes;
    [SerializeField]
    private int startingNode;
    [SerializeField]
    private string searchType;

    [Header("Czas trwania")]
    [SerializeField]
    private float timeout;
    [SerializeField]
    private float animDuration;

    [Header("Obiekty-rodzice")]
    [SerializeField]
    private GameObject nodes;
    [SerializeField]
    private GameObject edges;

    private List<GameObject> nodesList = new();
    private List<GameObject> edgesList = new();

    private List<List<bool>> matrix = new();
    private bool[] visited;
    private Dictionary<int, List<int>> neighborsList = new();

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

    private IEnumerator ChangeColor(GameObject gameObject, Color newColor)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        Color currentColor = renderer.material.color;

        float elapsedTime = 0f;

        while (elapsedTime < animDuration)
        {
            renderer.material.color = Color.Lerp(currentColor, newColor, elapsedTime / animDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        renderer.material.color = newColor;
    }

    private IEnumerator ChangeSize(GameObject gameObject, Vector3 newScale)
    {
        Vector3 currentScale = gameObject.transform.localScale;

        float elapsedTime = 0f;

        while (elapsedTime < animDuration)
        {
            gameObject.transform.localScale = Vector3.Lerp(currentScale, newScale, elapsedTime / animDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.transform.localScale = newScale;
    }


    /// MACIERZ S¥SIEDZTWA TWORZONA RÊCZNIE!!!
    private void CreateMatrix()
    {
        for (int i = 0; i < numberOfNodes; i++)
        {
            matrix.Add(new());
            for (int j = 0; j < numberOfNodes; j++)
            {
                matrix[i].Add(false);
            }
        }

        matrix[0][1] = true;
        matrix[0][4] = true;
        matrix[1][2] = true;
        matrix[2][3] = true;
        matrix[2][4] = true;
        matrix[3][7] = true;
        matrix[4][5] = true;
        matrix[4][7] = true;
        matrix[5][6] = true;
        matrix[7][8] = true;
    }

    private void CreateNeighborsList()
    {
        for (int i = 0; i < numberOfNodes; i++)
        {
            neighborsList.Add(i, new List<int>());
            for (int j = 0; j < numberOfNodes; j++)
            {
                if (matrix[i][j])
                {
                    neighborsList[i].Add(j);
                }
            }
        }
    }

    private void DrawEdges()
    {
        for (int i = 0; i < numberOfNodes; i++)
        {
            for (int j = 0; j < numberOfNodes; j++)
            {
                if (matrix[i][j])
                {
                    GameObject line = new();
                    line.transform.parent = edges.transform;
                    line.name = $"{i}-{j}";

                    Vector3 from = nodesList[i].transform.position;
                    Vector3 to = nodesList[j].transform.position;

                    line.AddComponent<LineRenderer>();
                    LineRenderer lr = line.GetComponent<LineRenderer>();
                    lr.SetPosition(0, from);
                    lr.SetPosition(1, to);
                    lr.startWidth = lr.endWidth = 0.1f;
                    lr.material.color = Color.white;

                    edgesList.Add(line);
                }
            }
        }
    }

    private IEnumerator DFS(int start)
    {
        yield return new WaitForSeconds(timeout);

        Stack<int> stack = new();
        stack.Push(start);

        while (stack.Count > 0)
        {
            // Przejœcie do kolejnego wêz³a
            int n = stack.Pop();
            StartCoroutine(ChangeSize(nodesList[n], 1.5f * Vector3.one));
            yield return new WaitForSeconds(timeout);

            // Oznaczenie wêz³a jako odwiedzonego
            visited[n] = true;
            StartCoroutine(ChangeColor(nodesList[n], Color.green));
            yield return new WaitForSeconds(timeout);

            // Przeszukanie s¹siadów wêz³a
            foreach (int nb in neighborsList[n])
            {
                // Dodanie nieodwiedzonych s¹siadów do stosu
                if (!visited[nb] && !stack.Contains(nb))
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

    //IEnumerator BFS(int start)
    //{
    //    yield return new WaitForSeconds(timeout);

    //    List<int> nodesToVisit = new() { start };

    //    while (nodesToVisit.Count > 0)
    //    {
    //        // Go to the next node on the list to visit
    //        int n = nodesToVisit.First();
    //        GameObject node = nodes[n];
    //        nodesToVisit.Remove(n);
    //        node.transform.localScale = 2 * Vector3.one;
    //        yield return new WaitForSeconds(timeout);

    //        // Set the node as visited
    //        visited[n] = true;
    //        node.GetComponent<Renderer>().material.color = Color.green;
    //        yield return new WaitForSeconds(timeout);

    //        // Check all neighbors of the current node
    //        MarkEdges(n, Color.red);
    //        for (int i = 0; i < numberOfNodes; i++)
    //        {
    //            if (matrix[n][i] || matrix[i][n])
    //            {
    //                // If the neighbor was not visited, add it to the visit list (if does not exist yet)
    //                if (!visited[i])
    //                    if (!nodesToVisit.Contains(i))
    //                        nodesToVisit = nodesToVisit.Append(i).ToList();
    //            }
    //        }
    //        yield return new WaitForSeconds(timeout);

    //        node.transform.localScale = Vector3.one;
    //    }
    //}

    void Start()
    {
        CreateMatrix();
        CreateNeighborsList();
        visited = new bool[numberOfNodes];

        // Dodanie wêz³ów do listy i nadanie etykiet
        for (int i = 0; i < numberOfNodes; i++)
        {
            nodesList.Add(nodes.transform.GetChild(i).gameObject);
            nodesList[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = i.ToString();
            nodesList[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = i.ToString();
        }

        // Utworzenie krawêdzi ³¹cz¹cych odpowiednie wêz³y i dodanie ich do listy
        DrawEdges();

        if (searchType == "DFS")
            StartCoroutine(DFS(startingNode));
        //else if (searchType == "BFS")
        //    StartCoroutine(BFS(startingNode));
    }
}
