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
    protected int popIndex;

    protected List<GameObject> items = new();
    protected List<int> values = new();

    protected abstract void SetDirection();

    public bool IsEmpty()
    {
        return items.Count == 0;
    }

    public void AddItem()
    {
        int index = items.Count;
        if (index >= maxCount)
        {
            StartCoroutine(warning.ShowWarning());
            return;
        }

        items.Add(Instantiate(prefab, this.transform));
        items[index].name = $"Block{index}";
        if (index > 0)
            items[index].transform.localPosition = items[index - 1].transform.localPosition + offset * direction;
        else
            items[index].transform.localPosition = direction;

        values.Add((int)(Random.value * 100));
        items[index].transform.GetChild(0).GetComponent<TextMeshPro>().text = values[index].ToString();
    }

    public virtual void PopItem()
    {
        if (items.Count < 1) return;

        Rigidbody rigidbody = items[popIndex].GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.None;
        rigidbody.AddRelativeForce(1.5f * Vector3.up + Vector3.forward + Vector3.right, ForceMode.Impulse);
        
        Destroy(items[popIndex], 1f);

        items.RemoveAt(popIndex);
        values.RemoveAt(popIndex);
    }

    public virtual void PeekItem()
    {
        if (items.Count < 1) return;

        Rigidbody rigidbody = items[popIndex].GetComponent<Rigidbody>();
        rigidbody.AddRelativeForce(1.5f * Vector3.up, ForceMode.Impulse);
    }

    protected void Start()
    {
        SetDirection();
        for (int i = 0; i < 3; i++)
            AddItem();

        warning = FindAnyObjectByType<LimitWarning>(FindObjectsInactive.Include);
    }

}
