using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BucketSort : Sortings
{
    private List<List<GameObject>> buckets;
    private int bucketCount = 5;
    private float bucketSpacing = 2.5f;
    private float itemSpacing = 1.2f;
    private float liftHeight = 1.5f;
    private float intraBucketSpacing = 1.2f;
    private float bucketYOffset = 7.5f;

    private Color[] bucketColors;

    // private Color[] bucketColors = { redColor, violetColor, orangeColor, pinkColor, redColor};
    private Dictionary<int, Transform> bucketContainers = new Dictionary<int, Transform>();

    void Awake()
    {
        bucketColors = new Color[] { redColor, violetColor, orangeColor, pinkColor, redColor };
    }

    protected override IEnumerator Sort()
    {
        buckets = new List<List<GameObject>>();
        for (int i = 0; i < bucketCount; i++)
        {
            buckets.Add(new List<GameObject>());
            CreateBucketContainer(i);
        }

        int minValue = int.MaxValue, maxValue = int.MinValue;
        foreach (var item in items)
        {
            int value = GetValue(item);
            minValue = Mathf.Min(minValue, value);
            maxValue = Mathf.Max(maxValue, value);
        }
        
        float range = (maxValue - minValue) / (float)bucketCount + 1;
        
        foreach (var item in items)
        {
            int value = GetValue(item);
            int bucketIndex = Mathf.FloorToInt((value - minValue) / range);
            buckets[bucketIndex].Add(item);
            
            Vector3 bucketPosition = bucketContainers[bucketIndex].position + new Vector3(buckets[bucketIndex].Count * intraBucketSpacing, 0, 0);
            StartCoroutine(MoveObject(item, bucketPosition));
            StartCoroutine(ChangeColor(item, bucketColors[bucketIndex]));
            yield return new WaitForSeconds(timeout);
        }
        
        int index = 0;

        for (int i = 0; i < bucketCount; i++)
        {
            List<GameObject> bucket = buckets[i];
            for (int j = 0; j < bucket.Count; j++)
            {
                Vector3 correctPosition = bucketContainers[i].position + new Vector3((j + 1) * intraBucketSpacing, 0, 0);
                StartCoroutine(MoveObject(bucket[j], correctPosition));
            }
            // yield return new WaitForSeconds(timeout);
        }

        // Loopowanie przez buckety
        for (int i = 0; i < bucketCount; i++)
        {
            // Loopowanie w buckecie - insertion sort
            List<GameObject> bucket = buckets[i];
            for (int j = 1; j < bucket.Count; j++)
            {
                GameObject keyItem = bucket[j];
                int keyValue = GetValue(keyItem);
                int k = j - 1;
                
                StartCoroutine(ChangeColor(keyItem, yellowColor));
                yield return new WaitForSeconds(timeout);
                
                Vector3 liftPosition = bucketContainers[i].position + new Vector3((j + 1) * intraBucketSpacing, liftHeight, 0);
                StartCoroutine(MoveObject(keyItem, liftPosition));
                yield return new WaitForSeconds(timeout);
                
                while (k >= 0 && GetValue(bucket[k]) > keyValue)
                {
                    bucket[k + 1] = bucket[k];
                    
                    Vector3 newPos = bucketContainers[i].position + new Vector3((k + 2) * intraBucketSpacing, 0, 0);
                    StartCoroutine(MoveObject(bucket[k], newPos));
                    yield return new WaitForSeconds(timeout);
                    
                    k--;
                }
                
                bucket[k + 1] = keyItem;
                
                Vector3 finalPosition = bucketContainers[i].position + new Vector3((k + 2) * intraBucketSpacing, liftHeight, 0);
                StartCoroutine(MoveObject(keyItem, finalPosition));
                yield return new WaitForSeconds(timeout);
                
                Vector3 lowerPosition = bucketContainers[i].position + new Vector3((k + 2) * intraBucketSpacing, 0, 0);
                StartCoroutine(MoveObject(keyItem, lowerPosition));
                StartCoroutine(ChangeColor(keyItem, bucketColors[i]));
                yield return new WaitForSeconds(timeout);
            }
            
            foreach (var sortedItem in bucket)
            {
                StartCoroutine(ChangeColor(sortedItem, greenColor));
                Vector3 sortedPosition = new Vector3(index * itemSpacing, 0, 0);
                yield return StartCoroutine(MoveObject(sortedItem, sortedPosition));
                index++;
            }
        }
    }

    private void CreateBucketContainer(int index)
    {
        GameObject bucketObj = new GameObject("BucketContainer" + index);
        bucketObj.transform.position = new Vector3(-10, bucketYOffset - index * bucketSpacing, 0);
        bucketContainers[index] = bucketObj.transform;
    }
}