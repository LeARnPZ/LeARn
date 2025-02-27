using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MergeSort : Sortings
{
    protected override IEnumerator Sort()
    {
        yield return StartCoroutine(MergeSortCoroutine(0, items.Count - 1));
    }

    private IEnumerator MergeSortCoroutine(int left, int right)
    {
        if (left < right)
        {
            int middle = (left + right) / 2;

            yield return StartCoroutine(MergeSortCoroutine(left, middle));
            yield return StartCoroutine(MergeSortCoroutine(middle + 1, right));
            yield return StartCoroutine(Merge(left, middle, right));
        }
    }

    private IEnumerator Merge(int left, int middle, int right)
    {
        List<GameObject> temp = new List<GameObject>();
        int i = left, j = middle + 1;

        while (i <= middle && j <= right)
        {
            int leftValue = GetValue(items[i]);
            int rightValue = GetValue(items[j]);

            if (leftValue <= rightValue)
            {
                temp.Add(items[i]);
                i++;
            }
            else
            {
                temp.Add(items[j]);
                j++;
            }
        }

        while (i <= middle)
        {
            temp.Add(items[i]);
            i++;
        }

        while (j <= right)
        {
            temp.Add(items[j]);
            j++;
        }

        for (int k = 0; k < temp.Count; k++)
        {
            GameObject obj = temp[k];
            Vector3 initialPosition = obj.transform.localPosition;
            Vector3 raisedPosition = initialPosition + Vector3.up * 0.5f;
            Vector3 newPosition = new Vector3(-numberOfItems / 2 + 1 + (left + k) * 1.2f, 0, 0);

            yield return StartCoroutine(MoveObject(obj, raisedPosition));
            yield return StartCoroutine(MoveObject(obj, newPosition));
        }
    }
}