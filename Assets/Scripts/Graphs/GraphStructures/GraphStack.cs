using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraphStack : StackStruct
{
    protected override void Start()
    {
        base.SetDirection();
        iterator = 0;
    }

    public void VisualPush(int value)
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
    }

    public void VisualPop()
    {
        base.PopItem();
    }
}
