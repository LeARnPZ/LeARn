using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraphQueue : QueueStruct
{
    protected override void Start()
    {
        base.SetDirection();
        baseScale = transform.parent.localScale.x;
        iterator = 0;
    }

    public void VisualEnqueue(int value)
    {
        iterator = items.Count;
        GameObject newItem = Instantiate(prefab, this.transform);
        newItem.name = $"Block{items.Count}";

        if (iterator > 0)
            newItem.transform.localPosition = items[iterator - 1].transform.localPosition + offset * direction;
        else
            newItem.transform.localPosition = offset * direction + 3 * Vector3.left * direction.x;

        items.Insert(iterator, newItem);
        values.Insert(iterator, value);

        newItem.transform.GetChild(0).GetComponent<TextMeshPro>().text = value.ToString();
        newItem.transform.GetChild(1).GetComponent<TextMeshPro>().text = value.ToString();

        StartCoroutine(Blink(items[iterator], greenColor));
    }

    public void VisualDequeue()
    {
        base.PopItem();
    }

}
