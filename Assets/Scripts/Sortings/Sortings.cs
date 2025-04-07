using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Sortings : MonoBehaviour
{
    [SerializeField]
    protected GameObject prefab;
    [SerializeField]
    protected int numberOfItems;
    [SerializeField]
    protected float animDuration;
    [SerializeField]
    protected float timeout;

    protected List<GameObject> items = new();
    protected List<int> values = new();

    protected bool isPaused;

    protected Color blueColor = new Color(146 / 255f, 212 / 255f, 255 / 255f);
    protected Color yellowColor = new Color(243 / 255f, 220 / 255f, 102 / 255f);
    protected Color greenColor = new Color(150 / 255f, 236 / 255f, 174 / 255f);
    protected Color violetColor = new Color(205 / 255f, 160 / 255f, 255 / 255f);
    protected Color orangeColor = new Color(255 / 255f, 126 / 255f, 85 / 255f);
    protected Color pinkColor = new Color(255 / 255f, 160 / 255f, 179 / 255f);

    protected int GetValue(GameObject gameObject)
    {
        return int.Parse(gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text);
    }

    protected (int min, int max) GetSortingRange()
    {
        switch (PlayerPrefs.GetString("algorithm", "Default"))
        {
            case "BucketSort":
                return (0, 30);
            case "QuickSort":
                return (2, 80);
            default:
                return (0, 100);
        }
    }

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

    protected IEnumerator MoveObject(GameObject gameObject, Vector3 newPosition)
    {
        Vector3 currentPosition = gameObject.transform.localPosition;

        float elapsedTime = 0;

        while (elapsedTime < animDuration)
        {
            gameObject.transform.localPosition = Vector3.Lerp(currentPosition, newPosition, elapsedTime / animDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.transform.localPosition = newPosition;
    }

    public void Pause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public virtual void Restart()
    {
        //isPaused = false;
        //Time.timeScale = 1f;
        StopAllCoroutines();
        foreach (GameObject item in items)
            Destroy(item);
        items.Clear();
        Start();
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    protected abstract IEnumerator Sort();

    protected virtual void Start()
    {
        Vector3 startPosition = new(-numberOfItems / 2 + 1, 1f, 0);
        (int min, int max) range = GetSortingRange();

        for (int i = 0; i < numberOfItems; i++)
        {
            items.Add(Instantiate(prefab, this.transform));
            items[i].name = $"Ball{i}";
            items[i].transform.localPosition = startPosition + 1.2f * i * Vector3.right;
            if (values.Count < numberOfItems)
                values.Add(Random.Range(range.min, range.max + 1));
            items[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = values[i].ToString();
            items[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = values[i].ToString();
        }
        isPaused = false;
        Time.timeScale = 1f;
        StartCoroutine(Sort());
    }
}
