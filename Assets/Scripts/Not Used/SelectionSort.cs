using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSort : Sortings
{ 
    private int minIndex;

    private IEnumerator FindMin(int fromIndex)
    {
        int minVal = int.MaxValue;
        minIndex = -1;

        for (int i = fromIndex; i < numberOfItems; i++)
        {
            StartCoroutine(ChangeColor(items[i], Color.blue));
            int value = GetValue(items[i]);
            yield return new WaitForSeconds(timeout);
            if (value < minVal)
            {
                if (minIndex >= 0)
                    StartCoroutine(ChangeColor(items[minIndex], Color.white));
                minVal = value;
                minIndex = i;
                StartCoroutine(ChangeColor(items[i], Color.yellow));
                yield return new WaitForSeconds(timeout);
            }
            else
            {
                StartCoroutine(ChangeColor(items[i], Color.white));
                yield return new WaitForSeconds(timeout);
            }
        }
    }

    protected override IEnumerator Sort()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < numberOfItems; i++)
        {
            yield return StartCoroutine(FindMin(i));

            if (i != minIndex)
            {
                StartCoroutine(MoveObject(items[i], items[i].transform.localPosition + Vector3.down));
                StartCoroutine(MoveObject(items[minIndex], items[minIndex].transform.localPosition + Vector3.up));
                yield return new WaitForSeconds(timeout);


                Vector3 posI = items[i].transform.localPosition;
                Vector3 posMin = items[minIndex].transform.localPosition;
                StartCoroutine(MoveObject(items[i], new Vector3(posMin.x, -posMin.y, posMin.z)));
                StartCoroutine(MoveObject(items[minIndex], new Vector3(posI.x, -posI.y, posI.z)));
                yield return new WaitForSeconds(timeout);

                StartCoroutine(MoveObject(items[i], items[i].transform.localPosition + Vector3.up));
                StartCoroutine(MoveObject(items[minIndex], items[minIndex].transform.localPosition + Vector3.down));
                yield return new WaitForSeconds(timeout);

                (items[i], items[minIndex]) = (items[minIndex], items[i]);
            }

            StartCoroutine(ChangeColor(items[i], Color.green));
            yield return new WaitForSeconds(timeout);
        }

        Debug.Log("Sorting finished.");
    }
}
