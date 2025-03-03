using System.Collections;
using UnityEngine;

public class SelectionSortDB : Sortings
{
    private int minIndex;
    private int maxIndex;

    private IEnumerator FindMinMax(int fromIndex, int toIndex)
    {
        int minVal = int.MaxValue;
        int maxVal = int.MinValue;
        minIndex = -1;
        maxIndex = -1;

        for (int i = fromIndex; i <= toIndex; i++)
        {
            int value = GetValue(items[i]);

            // Find the minimum value in the range
            if (value < minVal)
            {
                if (minIndex >= 0)
                    StartCoroutine(ChangeColor(items[minIndex], Color.white));
                minVal = value;
                minIndex = i;
                StartCoroutine(ChangeColor(items[i], Color.yellow));
                yield return new WaitForSeconds(timeout);
            }
            // Find the maximum value in the range
            if (value > maxVal)
            {
                if (maxIndex >= 0)
                    StartCoroutine(ChangeColor(items[maxIndex], Color.white));
                maxVal = value;
                maxIndex = i;
                StartCoroutine(ChangeColor(items[i], Color.red));
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
        for (int i = 0; i < numberOfItems / 2; i++) // We loop only half of the list
        {
            // Find both the minimum and maximum for each pass
            yield return StartCoroutine(FindMinMax(i, numberOfItems - i - 1));

            if (minIndex != maxIndex) // Ensure min and max are not the same item
            {
                Vector3 minInitialPos = items[minIndex].transform.localPosition;
                Vector3 maxInitialPos = items[maxIndex].transform.localPosition;
                Vector3 leftPos = items[i].transform.localPosition;
                Vector3 rightPos = items[numberOfItems - i - 1].transform.localPosition;

                // Move min item down, swap left
                StartCoroutine(MoveObject(items[minIndex], leftPos + Vector3.down));
                StartCoroutine(MoveObject(items[i], minInitialPos + Vector3.up));
                yield return new WaitForSeconds(timeout);

                // Move max item up, swap right
                StartCoroutine(MoveObject(items[maxIndex], rightPos + Vector3.up));
                StartCoroutine(MoveObject(items[numberOfItems - i - 1], maxInitialPos + Vector3.down));
                yield return new WaitForSeconds(timeout);

                // Swap x-coordinates
                StartCoroutine(MoveObject(items[minIndex], new Vector3(leftPos.x, items[minIndex].transform.localPosition.y, leftPos.z)));
                StartCoroutine(MoveObject(items[i], new Vector3(minInitialPos.x, items[i].transform.localPosition.y, minInitialPos.z)));
                StartCoroutine(MoveObject(items[maxIndex], new Vector3(rightPos.x, items[maxIndex].transform.localPosition.y, rightPos.z)));
                StartCoroutine(MoveObject(items[numberOfItems - i - 1], new Vector3(maxInitialPos.x, items[numberOfItems - i - 1].transform.localPosition.y, maxInitialPos.z)));
                yield return new WaitForSeconds(timeout);

                // Move them back to their original y-axis positions
                StartCoroutine(MoveObject(items[minIndex], new Vector3(leftPos.x, leftPos.y, leftPos.z)));
                StartCoroutine(MoveObject(items[i], new Vector3(minInitialPos.x, minInitialPos.y, minInitialPos.z)));
                StartCoroutine(MoveObject(items[maxIndex], new Vector3(rightPos.x, rightPos.y, rightPos.z)));
                StartCoroutine(MoveObject(items[numberOfItems - i - 1], new Vector3(maxInitialPos.x, maxInitialPos.y, maxInitialPos.z)));
                yield return new WaitForSeconds(timeout);

                // Swap items in the array
                (items[i], items[minIndex]) = (items[minIndex], items[i]);
                (items[numberOfItems - i - 1], items[maxIndex]) = (items[maxIndex], items[numberOfItems - i - 1]);

                yield return new WaitForSeconds(timeout);
            }

            // Mark items as sorted (green)
            StartCoroutine(ChangeColor(items[i], Color.green));
            StartCoroutine(ChangeColor(items[numberOfItems - i - 1], Color.green));
            yield return new WaitForSeconds(timeout);
        }

        Debug.Log("Double Selection Sort finished.");
    }
}