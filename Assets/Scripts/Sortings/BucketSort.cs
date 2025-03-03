using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketSort : Sortings
{
    private List<List<GameObject>> buckets; // Buckets to hold items
    private int bucketCount = 5; // Number of buckets (adjustable)

    protected override IEnumerator Sort()
    {
        yield return new WaitForSeconds(1);

        // Step 1: Create and initialize buckets
        buckets = new List<List<GameObject>>();
        for (int i = 0; i < bucketCount; i++)
            buckets.Add(new List<GameObject>());

        // Find min and max values to determine the range
        int minValue = int.MaxValue, maxValue = int.MinValue;
        foreach (var item in items)
        {
            int value = GetValue(item);
            minValue = Mathf.Min(minValue, value);
            maxValue = Mathf.Max(maxValue, value);
        }

        float range = (maxValue - minValue) / (float)bucketCount + 1;

        // Step 2: Place items into appropriate buckets
        foreach (var item in items)
        {
            int value = GetValue(item);
            int bucketIndex = Mathf.FloorToInt((value - minValue) / range);
            buckets[bucketIndex].Add(item);

            StartCoroutine(ChangeColor(item, Color.yellow));
            yield return new WaitForSeconds(timeout);
        }

        // Step 3: Sort each bucket and reposition items
        int index = 0;
        for (int i = 0; i < bucketCount; i++)
        {
            List<GameObject> bucket = buckets[i];

            // Sort bucket using Insertion Sort
            for (int j = 1; j < bucket.Count; j++)
            {
                GameObject keyItem = bucket[j];
                int keyValue = GetValue(keyItem);
                int k = j - 1;

                StartCoroutine(ChangeColor(keyItem, Color.blue));
                yield return new WaitForSeconds(timeout);

                while (k >= 0 && GetValue(bucket[k]) > keyValue)
                {
                    // Move the larger item up
                    StartCoroutine(MoveObject(bucket[k], bucket[k].transform.localPosition + Vector3.up));
                    yield return new WaitForSeconds(timeout);

                    bucket[k + 1] = bucket[k];
                    k--;
                }

                bucket[k + 1] = keyItem;

                // Move keyItem down to its sorted position
                // StartCoroutine(MoveObject(keyItem, keyItem.transform.localPosition + Vector3.down));
                yield return new WaitForSeconds(timeout);
            }

            // Step 4: Move sorted items back to the main list
            foreach (var sortedItem in bucket)
            {
                items[index] = sortedItem;
                StartCoroutine(ChangeColor(sortedItem, Color.green));
                yield return StartCoroutine(MoveObject(sortedItem, sortedItem.transform.localPosition + Vector3.up));
                yield return StartCoroutine(MoveObject(sortedItem, new Vector3(index, sortedItem.transform.localPosition.y, sortedItem.transform.localPosition.z)));
                yield return StartCoroutine(MoveObject(sortedItem, sortedItem.transform.localPosition + Vector3.down));
                yield return new WaitForSeconds(timeout);
                index++;
            }
        }

        Debug.Log("Bucket Sort Finished!");
    }
}



