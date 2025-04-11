using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class QuickSort : Sortings
{
    [SerializeField]
    protected GameObject pivotIndicatorPrefab;

    protected GameObject pivotIndicator = null;

    protected List<int[]> valueLists = new List<int[]>();

    void initializeValueLists()
    {
        int[] a = { 16, 51, 48, 7, 28, 33, 72, 44, 20, 37 };
        int[] b = { 33, 12, 63, 44, 15, 6, 27, 28, 95, 54 };
        int[] c = { 44, 26, 13, 34, 65, 62, 7, 41, 19, 20 };
        int[] d = { 65, 53, 13, 24, 38, 6, 27, 52, 44, 33 };
        int[] e = { 11, 23, 3, 66, 26, 14, 71, 37, 28, 55 };

        valueLists.Add(a);
        valueLists.Add(b);
        valueLists.Add(c);
        valueLists.Add(d);
        valueLists.Add(e);
    }

    void placePivotIndicator(int pivotIndex)
    {
        pivotIndicator = Instantiate(pivotIndicatorPrefab, this.transform);
        Vector3 pivotPosition = new(-numberOfItems / 2 + 1, 0, 0);
        pivotPosition = pivotPosition + 1.2f * pivotIndex * Vector3.right;

        pivotIndicator.transform.localPosition = pivotPosition + Vector3.down;
    }

    void destroyPivotIndicator()
    {
        Destroy(pivotIndicator);
        pivotIndicator = null;
    }

    IEnumerator SwapObjects(int i, int j, bool isPivot = false)
    {
        StartCoroutine(MoveObject(items[i], items[i].transform.localPosition + Vector3.back));  
        StartCoroutine(MoveObject(items[j], items[j].transform.localPosition + Vector3.forward));
        if (isPivot)
            StartCoroutine(MoveObject(pivotIndicator, pivotIndicator.transform.localPosition + Vector3.forward));
        yield return new WaitForSeconds(timeout);

        StartCoroutine(MoveObject(items[i], items[i].transform.localPosition + 1.2f * (j - i) * Vector3.right));
        StartCoroutine(MoveObject(items[j], items[j].transform.localPosition + 1.2f * (j - i) * Vector3.left));
        if (isPivot)
            StartCoroutine(MoveObject(pivotIndicator, pivotIndicator.transform.localPosition + 1.2f * (j - i) * Vector3.left));

        yield return new WaitForSeconds(timeout);

        StartCoroutine(MoveObject(items[i], items[i].transform.localPosition + Vector3.forward));
        StartCoroutine(MoveObject(items[j], items[j].transform.localPosition + Vector3.back));
        if (isPivot)
            StartCoroutine(MoveObject(pivotIndicator, pivotIndicator.transform.localPosition + Vector3.back));

        yield return new WaitForSeconds(timeout);
    }

    IEnumerator SortReccur(int low, int high)
    {
        if (low == high) 
        {
            StartCoroutine(ChangeColor(items[high], greenColor));
        } else if (low < high) {
            int pivot = GetValue(items[high]);
            StartCoroutine(ChangeColor(items[high], yellowColor));
            placePivotIndicator(high);

            yield return new WaitForSeconds(0.5f);

            int i = low - 1;

            for (int j = low; j <= high - 1; j++)
            {
                int jValue = GetValue(items[j]);

                StartCoroutine(ChangeColor(items[j], yellowColor));

                yield return new WaitForSeconds(timeout);

                if (jValue <= pivot)
                {
                    i++;
                    yield return StartCoroutine(ChangeColor(items[j], redColor));

                    if (i != j) 
                    {
                        yield return StartCoroutine(SwapObjects(i, j)); 
                        (items[i], items[j]) = (items[j], items[i]);
                        yield return StartCoroutine(ChangeColor(items[i], redColor));
                    }

                } else {
                    yield return StartCoroutine(ChangeColor(items[j], violetColor));
                }
            }

            if (i + 1 != high) 
            {
                yield return StartCoroutine(SwapObjects(i + 1, high, true));

                (items[i + 1], items[high]) = (items[high], items[i + 1]);

                StartCoroutine(ChangeColor(items[i + 1], greenColor));
            }  else {
                StartCoroutine(ChangeColor(items[high], greenColor));
            }
            destroyPivotIndicator();

            for (int k = low; k < i + 1; k++)
            {
                StartCoroutine(ChangeColor(items[k], blueColor));
            }

            for (int l = i + 2; l <= high; l++)
            {
                StartCoroutine(ChangeColor(items[l], blueColor));
            }

            yield return new WaitForSeconds(timeout/2);

            int pi = i + 1;

            yield return StartCoroutine(SortReccur(low, pi - 1));

            yield return new WaitForSeconds(timeout/2);

            yield return StartCoroutine(SortReccur(pi + 1, high));
        }
    }

    public override void Restart()
    {
        destroyPivotIndicator();
        base.Restart();
    }

    protected override void Start()
    {
        initializeValueLists();
        int elementsIndex = UnityEngine.Random.Range(0, valueLists.Count);
        int[] elements = valueLists[elementsIndex];

        Vector3 startPosition = new(-numberOfItems / 2 + 1, 0, 0);
        (int min, int max) range = GetSortingRange();
        for (int i = 0; i < numberOfItems; i++)
        {
            items.Add(Instantiate(prefab, this.transform));
            StartCoroutine(ChangeColor(items[i], blueColor));
            items[i].name = $"Ball{i}";
            items[i].transform.localPosition = startPosition + 1.2f * i * Vector3.right;
            if (values.Count < numberOfItems)
                values.Add(elements[i]);

            float scale = (float)(0.6 * Math.Max(0.5, (float)(values[i] * 0.1)));

            items[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = values[i].ToString();
            items[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = values[i].ToString();

            items[i].transform.GetChild(0).transform.localScale = new Vector3(1, (float)(1 / scale), 1);
            items[i].transform.GetChild(1).transform.localScale = new Vector3(1, (float)(1 / scale), 1);

            items[i].transform.localScale = new Vector3(1, scale, 1);
            Vector3 position = items[i].transform.position;
            items[i].transform.localPosition = new Vector3(items[i].transform.localPosition.x, 0.1f + scale * 0.5f, 0);
        }

        StartCoroutine(Sort());
    }

    protected override IEnumerator Sort()
    {
        yield return new WaitForSeconds(1);

        StartCoroutine(SortReccur(0, numberOfItems - 1));
    }    
}
