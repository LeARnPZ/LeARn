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
    private LimitWarning warning;

    protected Vector3 direction;
    protected int iterator;

    protected List<GameObject> items = new();
    protected List<int> values = new();

    protected abstract void SetDirection();

    public bool IsEmpty()
    {
        return items.Count == 0;
    }

    public virtual void AddItem()
    {
        int index = items.Count;
        if (index >= maxCount)
        {
            StartCoroutine(warning.ShowWarning());
            return;
        }

        items.Insert(iterator, Instantiate(prefab, this.transform));
        //items.Add(Instantiate(prefab, this.transform));

        items[iterator].name = $"Block{index}";
        if (iterator > 0)
            items[iterator].transform.localPosition = items[iterator - 1].transform.localPosition + offset * direction;
        else
            items[iterator].transform.localPosition = direction;

        values.Insert(iterator, (int)(Random.value * 100));
        items[iterator].transform.GetChild(0).GetComponent<TextMeshPro>().text = values[iterator].ToString();
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
    }

    protected void Start()
    {
        SetDirection();
        iterator = 0;
        for (int i = 0; i < 3; i++)
        {
            AddItem();
        }
        iterator = 0;

        warning = FindAnyObjectByType<LimitWarning>(FindObjectsInactive.Include);
    }
}
