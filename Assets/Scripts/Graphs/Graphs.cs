using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Graphs : MonoBehaviour
{
    [Header("Ustawienia animacji")]
    [SerializeField]
    protected int startingNode;

    [Header("Czas trwania")]
    [SerializeField]
    protected float timeout;
    [SerializeField]
    protected float animDuration;

    [Header("Obiekty-rodzice")]
    [SerializeField]
    protected GameObject graphVersions;
    [SerializeField]
    protected GameObject searchOrder;

    protected int numberOfNodes;
    protected Color originalColor;
    protected bool isPaused;

    // Przechowywanie grafu
    protected List<List<bool>> matrix = new();
    protected Dictionary<int, List<int>> neighborsList = new();

    // Kontenery wêz³ów i krawêdzi, których obiekty-dzieci bêd¹ wrzucone do list
    protected GameObject nodes;
    protected GameObject edges;

    // Listy obiektów wêz³ów i krawêdzi
    protected List<GameObject> nodesList = new();
    protected List<GameObject> edgesList = new();

    protected Color blueColor = new Color(146 / 255f, 212 / 255f, 255 / 255f);
    protected Color yellowColor = new Color(243 / 255f, 220 / 255f, 102 / 255f);
    protected Color greenColor = new Color(150 / 255f, 236 / 255f, 174 / 255f);
    protected Color violetColor = new Color(205 / 255f, 160 / 255f, 255 / 255f);
    protected Color orangeColor = new Color(255 / 255f, 126 / 255f, 85 / 255f);
    protected Color pinkColor = new Color(255 / 255f, 160 / 255f, 179 / 255f);
    protected Color redColor = new Color(239 / 255f, 97 / 255f, 109 / 255f);

    protected Color redTextColor = new(210/255f, 16/255f, 30/255f);
    protected Color blueTextColor = new(18/255f, 64/255f, 97/255f);
    protected Color greenTextColor = new(44/255f, 167/255f, 58/255f);

    protected virtual IEnumerator SearchGraph() { yield return null; }

    protected IEnumerator ChangeColor(GameObject gameObject, Color newColor, bool isText = false)
    {
        if (isText)
        {
            TextMeshPro tmpro = gameObject.GetComponent<TextMeshPro>();
            Color currentColor = tmpro.color;

            float elapsedTime = 0f;

            while (elapsedTime < animDuration)
            {
                tmpro.color = Color.Lerp(currentColor, newColor, elapsedTime / animDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            tmpro.color = newColor;
        }
        else
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

    protected int GetEdgeWeight(GameObject edge)
    {
        return int.Parse(edge.transform.GetChild(0).GetComponent<TextMeshPro>().text);
    }

    protected void InitializeMatrix()
    {
        for (int i = 0; i < numberOfNodes; i++)
        {
            matrix.Add(new());
            for (int j = 0; j < numberOfNodes; j++)
            {
                matrix[i].Add(false);
            }
        }
    }

    public int GetNumberOfNodes()
    {
        return numberOfNodes;
    }

    /// MACIERZ S¥SIEDZTWA TWORZONA RÊCZNIE!!!
    protected void CreateMatrix(int version = 0)
    {
        switch (version)
        {
            case 0:
                numberOfNodes = 9; // <-- liczba wierzcho³ków w grafie
                InitializeMatrix(); // <-- utworzenie macierzy wype³nionej false'ami
                matrix[0][1] = true; matrix[1][0] = true;
                matrix[0][4] = true; matrix[4][0] = true;
                matrix[1][2] = true; matrix[2][1] = true;
                matrix[2][3] = true; matrix[3][2] = true;
                matrix[2][4] = true; matrix[4][2] = true;
                matrix[3][7] = true; matrix[7][3] = true;
                matrix[4][5] = true; matrix[5][4] = true;
                matrix[4][7] = true; matrix[7][4] = true;
                matrix[5][6] = true; matrix[6][5] = true;
                matrix[7][8] = true; matrix[8][7] = true;
                break;

            case 1:
                numberOfNodes = 8;
                InitializeMatrix();
                matrix[0][1] = true; matrix[1][0] = true;
                matrix[0][2] = true; matrix[2][0] = true;
                matrix[1][2] = true; matrix[2][1] = true;
                matrix[2][3] = true; matrix[3][2] = true;
                matrix[2][4] = true; matrix[4][2] = true;
                matrix[2][5] = true; matrix[5][2] = true;
                matrix[2][7] = true; matrix[7][2] = true;
                matrix[3][7] = true; matrix[7][3] = true;
                matrix[4][6] = true; matrix[6][4] = true;
                matrix[5][6] = true; matrix[6][5] = true;
                matrix[5][7] = true; matrix[7][5] = true;
                matrix[6][7] = true; matrix[7][6] = true;
                break;

            case 2:
                numberOfNodes = 7; 
                InitializeMatrix(); 
                matrix[0][1] = true; matrix[1][0] = true;
                matrix[0][2] = true; matrix[2][0] = true;
                matrix[1][3] = true; matrix[3][1] = true;
                matrix[1][4] = true; matrix[4][1] = true;
                matrix[2][5] = true; matrix[5][2] = true;
                matrix[2][6] = true; matrix[6][2] = true;
                break;

            case 3:
                numberOfNodes = 7;
                InitializeMatrix();
                //matrix[0][1] = true; matrix[1][0] = true;  
                matrix[0][2] = true; matrix[2][0] = true;
                matrix[0][3] = true; matrix[3][0] = true;
                //matrix[0][4] = true; matrix[4][0] = true;
                matrix[0][5] = true; matrix[5][0] = true;
                //matrix[0][6] = true; matrix[6][0] = true;
                matrix[1][5] = true; matrix[5][1] = true;
                matrix[1][3] = true; matrix[3][1] = true;
                matrix[3][4] = true; matrix[4][3] = true;
                matrix[4][2] = true; matrix[2][4] = true;
                matrix[2][6] = true; matrix[6][2] = true;
                matrix[6][5] = true; matrix[5][6] = true;
                break;

            case 4:
                numberOfNodes = 7;
                InitializeMatrix();
                matrix[0][1] = true; matrix[1][0] = true;
                matrix[1][2] = true; matrix[2][1] = true;
                matrix[1][4] = true; matrix[4][1] = true;
                matrix[2][3] = true; matrix[3][2] = true;
                matrix[2][5] = true; matrix[5][2] = true;
                matrix[2][6] = true; matrix[6][2] = true;
                matrix[3][5] = true; matrix[5][3] = true;
                matrix[5][6] = true; matrix[6][5] = true;
                break;
        }
    }

    protected void CreateNeighborsList()
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

    protected void DrawEdges()
    {
        for (int i = 0; i < numberOfNodes; i++)
        {
            for (int j = i; j < numberOfNodes; j++)
            {
                if (matrix[i][j])
                {
                    GameObject line = new($"{i}-{j}");
                    line.transform.parent = edges.transform;
                    line.transform.localScale = Vector3.one;
                    line.transform.localRotation = Quaternion.Euler(0, 0, 0);

                    Vector3 from = nodesList[i].transform.position;
                    Vector3 to = nodesList[j].transform.position;

                    line.AddComponent<LineRenderer>();
                    LineRenderer lr = line.GetComponent<LineRenderer>();
                    lr.SetPosition(0, from);
                    lr.SetPosition(1, to);
                    lr.startWidth = lr.endWidth = 0.1f * this.transform.localScale.x;
                    lr.material.color = blueColor;
                    lr.material.SetFloat("_Glossiness", 0);

                    if (name.Contains("Dijkstra"))
                    {
                        GameObject weight = new("EdgeWeight");
                        weight.transform.parent = line.transform;
                        weight.transform.localScale = Vector3.one;

                        Vector3 middle = (from + to) / 2f;
                        Vector3 direction = (to - from).normalized;
                        Vector3 up = Vector3.Cross(direction, line.transform.forward);
                        Vector3 position = middle + 0.25f * transform.localScale.x * up;
                        if (position.y < middle.y)
                        {
                            up = -up;
                            position = middle + 0.25f * transform.localScale.x * up;
                        }
                        weight.transform.position = position;

                        weight.AddComponent<TextMeshPro>();
                        TextMeshPro tmpro = weight.GetComponent<TextMeshPro>();
                        tmpro.text = Random.Range(1, 10).ToString();
                        tmpro.autoSizeTextContainer = true;
                        tmpro.fontSize = 4;
                        tmpro.font = Resources.Load<TMP_FontAsset>("Fonts/Montserrat-SemiBold SDF");
                    }

                    edgesList.Add(line);
                }
            }
        }
    }

    public virtual void Restart()
    {
        StopAllCoroutines();

        SpeedButton speedButton = FindObjectOfType<SpeedButton>();
        if (speedButton != null)
        {
            speedButton.SpeedButtonRestart();
        }

        foreach (GameObject node in nodesList)
        {
            node.transform.localScale = Vector3.one; 
            node.GetComponent<Renderer>().material.color = originalColor;
        }

        foreach (GameObject edge in edgesList)
        {
            Destroy(edge);
        }

        edgesList.Clear();
        Start();
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public void Pause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void AddOrder(int n)
    {
        TextMeshPro frontText = searchOrder.transform.GetChild(0).GetComponent<TextMeshPro>();
        TextMeshPro backText = searchOrder.transform.GetChild(1).GetComponent<TextMeshPro>();

        if (frontText.text.Equals(""))
        {
            frontText.text = n.ToString();
            backText.text = n.ToString();
        }
        else
        {
            frontText.text += $", {n}";
            backText.text += $", {n}";
        }
    }

    protected virtual void Awake()
    {
        // Wylosowanie wersji grafu oraz utworzenie do niego macierzy i list s¹siedztwa
        int graphVersion = (int)(Random.value * 10) % 5; // <-- po znaku modulo musi byæ liczba dostêpnych wersji grafu
        CreateMatrix(graphVersion);
        CreateNeighborsList();
        
        // Uaktywnienie odpowiedniej wersji grafu oraz pobranie jego wêz³ów i krawêdzi
        graphVersions.transform.GetChild(graphVersion).gameObject.SetActive(true);
        nodes = graphVersions.transform.GetChild(graphVersion).GetChild(0).gameObject;
        edges = graphVersions.transform.GetChild(graphVersion).GetChild(1).gameObject;
        originalColor = nodes.transform.GetChild(0).GetComponent<Renderer>().material.color;
    }

    protected void LateUpdate() //metoda wywo³ywana po Update - updatowanie pozycji krawedzi po przesunieciu kulek
    {
        UpdateEdges();
    }

    private void UpdateEdges()
    {
        for (int i = 0; i < edgesList.Count; i++)
        {
            LineRenderer lr = edgesList[i].GetComponent<LineRenderer>();
            string[] nodeNames = edgesList[i].name.Split('-');

            int from = int.Parse(nodeNames[0]);
            int to = int.Parse(nodeNames[1]);

            Vector3 fromPos = nodesList[from].transform.position;
            Vector3 toPos = nodesList[to].transform.position;

            lr.SetPosition(0, fromPos);
            lr.SetPosition(1, toPos);
        }
    }


    protected virtual void Start()
    {
        // Dodanie wêz³ów do listy i nadanie etykiet
        for (int i = 0; i < numberOfNodes; i++)
        {
            nodesList.Add(nodes.transform.GetChild(i).gameObject);
            nodesList[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = i.ToString();
            nodesList[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = i.ToString();
        }

        // Utworzenie krawêdzi ³¹cz¹cych odpowiednie wêz³y i dodanie ich do listy
        DrawEdges();

        // Odpauzowanie animacji po restarcie
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void UpdateEdgePositions()
    {
        for (int i = 0; i < edgesList.Count; i++)
        {
            GameObject line = edgesList[i];

            string[] parts = line.name.Split('-');
            if (parts.Length == 2 && int.TryParse(parts[0], out int a) && int.TryParse(parts[1], out int b))
            {
                if (a < nodesList.Count && b < nodesList.Count)
                {
                    Vector3 from = nodesList[a].transform.position;
                    Vector3 to = nodesList[b].transform.position;

                    LineRenderer lr = line.GetComponent<LineRenderer>();
                    if (lr != null)
                    {
                        lr.SetPosition(0, from);
                        lr.SetPosition(1, to);

                        float width = 0.1f * transform.localScale.x;
                        lr.startWidth = lr.endWidth = width;
                    }
                }
            }
        }
    }
}
