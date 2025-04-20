using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Structures : MonoBehaviour
{
    [SerializeField]
    protected GameObject prefab;
    [SerializeField]
    protected int maxCount;
    [SerializeField]
    protected float offset;
    [SerializeField]
    private float animDuration;
    private LimitWarning warning;

    protected Vector3 direction;
    protected int iterator;

    protected List<GameObject> items = new();
    protected List<int> values = new();

    protected Color blueColor = new Color(146 / 255f, 212 / 255f, 255 / 255f);
    protected Color yellowColor = new Color(243 / 255f, 220 / 255f, 102 / 255f);
    protected Color greenColor = new Color(150 / 255f, 236 / 255f, 174 / 255f);
    protected Color violetColor = new Color(205 / 255f, 160 / 255f, 255 / 255f);
    protected Color orangeColor = new Color(255 / 255f, 126 / 255f, 85 / 255f);
    protected Color pinkColor = new Color(255 / 255f, 160 / 255f, 179 / 255f);

    protected abstract void SetDirection();

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

    protected IEnumerator Blink(GameObject gameObject, Color color)
    {
        Color originalColor = gameObject.GetComponent<Renderer>().material.color;
        yield return StartCoroutine(ChangeColor(gameObject, color));
        yield return StartCoroutine(ChangeColor(gameObject, originalColor));
    }

    public int GetCount()
    {
        return items.Count;
    }

    public int GetMaxCount()
    {
        return maxCount;
    }

    public int GetIterator()
    {
        return iterator;
    }

    public virtual void AddItem()
    {
        if (items.Count >= maxCount)
        {
            //StartCoroutine(warning.ShowWarning());
            return;
        }

        items.Insert(iterator, Instantiate(prefab, this.transform));

        items[iterator].name = $"Block{items.Count-1}";
        if (iterator > 0)
            items[iterator].transform.localPosition = items[iterator - 1].transform.localPosition + offset * direction;
        else
            items[iterator].transform.localPosition = offset * direction + 3 * Vector3.left * direction.x;

        values.Insert(iterator, (int)(Random.value * 100));
        items[iterator].transform.GetChild(0).GetComponent<TextMeshPro>().text = values[iterator].ToString();
        items[iterator].transform.GetChild(1).GetComponent<TextMeshPro>().text = values[iterator].ToString();

        StartCoroutine(Blink(items[iterator], greenColor));
    }

    public virtual void PopItem()
    {
        if (items.Count < 1) return;
        if (iterator >= items.Count)
        {
            Debug.Log("Out of bounds.");
            return;
        }

        Rigidbody rigidbody = items[iterator].GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.None;
        rigidbody.AddRelativeForce(2 * Vector3.up + 0.5f * Vector3.forward + 0.5f * Vector3.right, ForceMode.Impulse);

        StartCoroutine(Blink(items[iterator], orangeColor));

        Destroy(items[iterator], 1f);

        items.RemoveAt(iterator);
        values.RemoveAt(iterator);
    }

    public virtual void PeekItem()
    {
        if (items.Count < 1) return;
        if (iterator >= items.Count)
        {
            Debug.Log("Out of bounds.");
            return;
        }

        Rigidbody rigidbody = items[iterator].GetComponent<Rigidbody>();
        rigidbody.AddRelativeForce(2 * Vector3.up, ForceMode.Impulse);

        StartCoroutine(Blink(items[iterator], yellowColor));
    }


    public void Restart()
    {
        StopAllCoroutines();


        foreach (GameObject item in items)
        {
            Destroy(item);
        }
        items.Clear();
        values.Clear();

        iterator = 0;
    }

    protected virtual void Start()
    {
        SetDirection();
        iterator = 0;
        for (int i = 0; i < 3; i++)
        {
            AddItem();
        }
        iterator = 0;

        //warning = FindAnyObjectByType<LimitWarning>(FindObjectsInactive.Include);
    }
}
