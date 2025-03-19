using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuickSort : Sortings
{
    IEnumerator SwapObjects(int i, int j)
    {
        StartCoroutine(MoveObject(items[i], items[i].transform.localPosition + Vector3.back));
        StartCoroutine(MoveObject(items[j], items[j].transform.localPosition + Vector3.forward));
        yield return new WaitForSeconds(timeout);

        Vector3 obji = items[i].transform.position;
        Vector3 objj = items[j].transform.position;

        StartCoroutine(MoveObject(items[i], items[i].transform.localPosition + 1.2f * (j - i) * Vector3.right));
        StartCoroutine(MoveObject(items[j], items[j].transform.localPosition + 1.2f * (j - i) * Vector3.left));
        yield return new WaitForSeconds(timeout);

        StartCoroutine(MoveObject(items[i], items[i].transform.localPosition + Vector3.forward));
        StartCoroutine(MoveObject(items[j], items[j].transform.localPosition + Vector3.back));
        yield return new WaitForSeconds(timeout);
    }

    IEnumerator SortReccur(int low, int high)
    {
        if (low < high)
        {
            int pivot = GetValue(items[high]);
            StartCoroutine(ChangeColor(items[high], Color.blue));

            yield return new WaitForSeconds(0.5f);

            int i = low - 1;

            for (int j = low; j <= high - 1; j++)
            {
                int jValue = GetValue(items[j]);

                StartCoroutine(ChangeColor(items[j], Color.yellow));

                yield return new WaitForSeconds(timeout);

                if (jValue < pivot)
                {
                    i++;
                    StartCoroutine(ChangeColor(items[i], Color.yellow));

                    if (i != j)
                    {
                        StartCoroutine(MoveObject(items[i], items[i].transform.localPosition + Vector3.back));
                        StartCoroutine(MoveObject(items[j], items[j].transform.localPosition + Vector3.forward));
                        yield return new WaitForSeconds(timeout);

                        Vector3 obji = items[i].transform.position;
                        Vector3 objj = items[j].transform.position;

                        StartCoroutine(MoveObject(items[i], items[i].transform.localPosition + 1.2f * (j - i) * Vector3.right));
                        StartCoroutine(MoveObject(items[j], items[j].transform.localPosition + 1.2f * (j - i) * Vector3.left));
                        yield return new WaitForSeconds(timeout);

                        StartCoroutine(MoveObject(items[i], items[i].transform.localPosition + Vector3.forward));
                        StartCoroutine(MoveObject(items[j], items[j].transform.localPosition + Vector3.back));
                        yield return new WaitForSeconds(timeout);

                        //StartCoroutine(SwapObjects(i, j)); 
                        //yield return new WaitForSeconds(3 * timeout);

                        (items[i], items[j]) = (items[j], items[i]);
                    } else {
                        StartCoroutine(ChangeColor(items[i], Color.white));
                    }
                }
            }

            if (i + 1 != high)
            {
                StartCoroutine(MoveObject(items[i + 1], items[i + 1].transform.localPosition + Vector3.back));
                StartCoroutine(MoveObject(items[high], items[high].transform.localPosition + Vector3.forward));
                yield return new WaitForSeconds(timeout);

                Vector3 obji1 = items[i + 1].transform.position;
                Vector3 objhigh = items[high].transform.position;

                StartCoroutine(MoveObject(items[i + 1], items[i + 1].transform.localPosition + 1.2f * (high - i - 1) * Vector3.right));
                StartCoroutine(MoveObject(items[high], items[high].transform.localPosition + 1.2f * (high - i - 1) * Vector3.left));
                yield return new WaitForSeconds(timeout);

                StartCoroutine(MoveObject(items[i + 1], items[i + 1].transform.localPosition + Vector3.forward));
                StartCoroutine(MoveObject(items[high], items[high].transform.localPosition + Vector3.back));
                yield return new WaitForSeconds(timeout);

                //StartCoroutine(SwapObjects(i + 1, high)); 
                //yield return new WaitForSeconds(3 * timeout);

                (items[i + 1], items[high]) = (items[high], items[i + 1]);

                StartCoroutine(ChangeColor(items[i + 1], Color.green));
            }
            else {
                StartCoroutine(ChangeColor(items[high], Color.green));
            }

            yield return new WaitForSeconds(timeout);

            int pi = i + 1;

            StartCoroutine(SortReccur(low, pi - 1));

            yield return new WaitForSeconds(timeout);

            StartCoroutine(SortReccur(pi + 1, high));
        }
    }

    protected override void Start()
    {
        Vector3 startPosition = new(-numberOfItems / 2 + 1, 0, 0);
        for (int i = 0; i < numberOfItems; i++)
        {
            items.Add(Instantiate(prefab, this.transform));
            items[i].name = $"Ball{i}";
            items[i].transform.localPosition = startPosition + 1.2f * i * Vector3.right;
            if (values.Count < numberOfItems)
                values.Add((int)(Random.value * 100));
            items[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = values[i].ToString();

            float scale = (float)(values[i]) > 1 ? (float)(values[i] * 0.1) : 1;
            items[i].transform.GetChild(0).transform.localScale = new Vector3(1, (float)(1 / scale), 1);

            items[i].transform.localScale = new Vector3(1, scale, 1);
            Vector3 position = items[i].transform.position;
            items[i].transform.position = new Vector3(position.x, scale != 1 ? (float)(((scale - 1) / 2) * 0.08 - 0.5) : 0, position.z);
        }

        for (int i = 0; i < numberOfItems; i++)
        {
            StartCoroutine(ChangeColor(items[i], Color.white));
        }

        StartCoroutine(Sort());
    }

    protected override IEnumerator Sort()
    {
        yield return new WaitForSeconds(1);

        StartCoroutine(SortReccur(0, numberOfItems - 1));
    }    
}
