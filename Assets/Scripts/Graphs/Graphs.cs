using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Graphs : MonoBehaviour
{
    [Header("Ustawienia animacji")]
    [SerializeField]
    protected int numberOfNodes;
    [SerializeField]
    protected int startingNode;

    [Header("Czas trwania")]
    [SerializeField]
    protected float timeout;
    [SerializeField]
    protected float animDuration;

    [Header("Obiekty-rodzice")]
    [SerializeField]
    protected GameObject nodes;
    [SerializeField]
    protected GameObject edges;

    protected List<List<bool>> matrix = new();
    protected Dictionary<int, List<int>> neighborsList = new();

    protected List<GameObject> nodesList = new();
    protected List<GameObject> edgesList = new();

    protected abstract IEnumerator SearchGraph();

    protected IEnumerator ChangeColor(GameObject gameObject, Color newColor)
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

    protected IEnumerator ChangeSize(GameObject gameObject, Vector3 newScale)
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
        matrix[1][0] = true;
        matrix[0][4] = true;
        matrix[4][0] = true;
        matrix[1][2] = true;
        matrix[2][1] = true;
        matrix[2][3] = true;
        matrix[3][2] = true;
        matrix[2][4] = true;
        matrix[4][2] = true;
        matrix[3][7] = true;
        matrix[7][3] = true;
        matrix[4][5] = true;
        matrix[5][4] = true;
        matrix[4][7] = true;
        matrix[7][4] = true;
        matrix[5][6] = true;
        matrix[6][5] = true;
        matrix[7][8] = true;
        matrix[8][7] = true;
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
                    lr.material.SetFloat("_Glossiness", 0);

                    edgesList.Add(line);
                }
            }
        }
    }

    protected void Start()
    {
        CreateMatrix();
        CreateNeighborsList();

        // Dodanie wêz³ów do listy i nadanie etykiet
        for (int i = 0; i < numberOfNodes; i++)
        {
            nodesList.Add(nodes.transform.GetChild(i).gameObject);
            nodesList[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = i.ToString();
            nodesList[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = i.ToString();
        }

        // Utworzenie krawêdzi ³¹cz¹cych odpowiednie wêz³y i dodanie ich do listy
        DrawEdges();

        // Uruchomienie animacji
        StartCoroutine(SearchGraph());
    }
}
