using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class QuickSort : Sortings
{
    IEnumerator SwapObjects(int i, int j)
    {
        StartCoroutine(MoveObject(items[i], items[i].transform.localPosition + Vector3.back));
        StartCoroutine(MoveObject(items[j], items[j].transform.localPosition + Vector3.forward));
        yield return new WaitForSeconds(timeout);

        StartCoroutine(MoveObject(items[i], items[i].transform.localPosition + 1.2f * (j - i) * Vector3.right));
        StartCoroutine(MoveObject(items[j], items[j].transform.localPosition + 1.2f * (j - i) * Vector3.left));
        yield return new WaitForSeconds(timeout);

        StartCoroutine(MoveObject(items[i], items[i].transform.localPosition + Vector3.forward));
        StartCoroutine(MoveObject(items[j], items[j].transform.localPosition + Vector3.back));
        yield return new WaitForSeconds(timeout);
    }

    IEnumerator SortReccur(int low, int high)
    {
        if (low == high) {
            StartCoroutine(ChangeColor(items[high], Color.green));
        }
        else if (low < high) {
            int pivot = GetValue(items[high]);
            StartCoroutine(ChangeColor(items[high], Color.magenta));

            yield return new WaitForSeconds(0.5f);

            int i = low - 1;

            for (int j = low; j <= high - 1; j++)
            {
                int jValue = GetValue(items[j]);

                StartCoroutine(ChangeColor(items[j], Color.blue));

                yield return new WaitForSeconds(timeout);

                if (jValue < pivot)
                {
                    i++;
                    StartCoroutine(ChangeColor(items[i], Color.cyan));

                    if (i != j)
                    {
                        yield return StartCoroutine(SwapObjects(i, j)); 
                        (items[i], items[j]) = (items[j], items[i]);
                        StartCoroutine(ChangeColor(items[i], Color.white));
                    }
                }
                else {
                    StartCoroutine(ChangeColor(items[j], Color.white));
                }
            }

            if (i + 1 != high)
            {
                yield return StartCoroutine(SwapObjects(i + 1, high)); 
                (items[i + 1], items[high]) = (items[high], items[i + 1]);

                StartCoroutine(ChangeColor(items[i + 1], Color.green));
            }
            else {
                StartCoroutine(ChangeColor(items[high], Color.green));                
            }

            for (int k = low; k < i + 1; k++)
            {
                StartCoroutine(ChangeColor(items[k], Color.white));
            }

            for (int l = i + 2; l <= high; l++)
            {
                StartCoroutine(ChangeColor(items[l], Color.white));
            }

            yield return new WaitForSeconds(timeout/2);

            int pi = i + 1;

            yield return StartCoroutine(SortReccur(low, pi - 1));

            yield return new WaitForSeconds(timeout/2);

            yield return StartCoroutine(SortReccur(pi + 1, high));
        }
    }

    protected override void Start()
    {
        Vector3 startPosition = new(-numberOfItems / 2 + 1, 0, 0);
        (int min, int max) range = GetSortingRange();

        for (int i = 0; i < numberOfItems; i++)
        {
            items.Add(Instantiate(prefab, this.transform));
            items[i].name = $"Ball{i}";
            items[i].transform.localPosition = startPosition + 1.2f * i * Vector3.right;
            if (values.Count < numberOfItems)
                values.Add(UnityEngine.Random.Range(range.min, range.max + 1));

            float scale = (float)(0.75 * Math.Max(0.5, (float)(values[i] * 0.1)));

            items[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = values[i].ToString();
            items[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = values[i].ToString();

            items[i].transform.GetChild(0).transform.localScale = new Vector3(1, (float)(1 / scale), 1);
            items[i].transform.GetChild(1).transform.localScale = new Vector3(1, (float)(1 / scale), 1);

            items[i].transform.localScale = new Vector3(1, scale, 1);
            Vector3 position = items[i].transform.position;
            items[i].transform.position = new Vector3(position.x, (float)(((scale - 1) / 2) * 0.08 - 0.5), position.z);
        }

        foreach (var item in items)
        {
            StartCoroutine(ChangeColor(item, Color.white));
        }

        StartCoroutine(Sort());
    }

    protected override IEnumerator Sort()
    {
        yield return new WaitForSeconds(1);

        StartCoroutine(SortReccur(0, numberOfItems - 1));
    }    
}
