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
    private float bucketYOffset = -1.5f;
    private Color[] bucketColors = { Color.red, Color.blue, Color.green, Color.yellow, Color.magenta };
    private Dictionary<int, Transform> bucketContainers = new Dictionary<int, Transform>();

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

        //loopowanie przez buckety
        for (int i = 0; i < bucketCount; i++)
        {
            //loopowanie w buckecie
            List<GameObject> bucket = buckets[i];
            for (int j = 1; j < bucket.Count; j++)
            {
                GameObject keyItem = bucket[j];
                int keyValue = GetValue(bucket[j]);
                int k = j - 1;
                StartCoroutine(ChangeColor(keyItem, Color.cyan));
                yield return new WaitForSeconds(timeout);
                // podnoszenie analizowanego elemetnu w buckecie
                Vector3 liftPosition = keyItem.transform.localPosition + Vector3.up * liftHeight;
                StartCoroutine(MoveObject(keyItem, liftPosition));
                yield return new WaitForSeconds(timeout);
                bool moved = false;

                // loopowanie przez bucket od pierwszej pozycji
               while (k >= 0 && k < j && GetValue(bucket[k]) > keyValue)
                {
                    bucket[k + 1] = bucket[k]; 
                    StartCoroutine(MoveObject(bucket[k], bucketContainers[i].position + new Vector3((k + 1) * intraBucketSpacing, 0, 0)));
                    yield return new WaitForSeconds(timeout);
                    k--;
                    moved = true;
                }
                
                if ( GetValue(bucket[k+1]) > keyValue )
                {                    
                    bucket[k + 1] = keyItem;
                    Vector3 targetPosition = bucketContainers[i].position + new Vector3((k + 1) * intraBucketSpacing, liftHeight, 0);
                    StartCoroutine(MoveObject(keyItem, targetPosition));
                    yield return new WaitForSeconds(timeout);
                    Vector3 lowerPosition = keyItem.transform.localPosition - Vector3.up * liftHeight;
                    StartCoroutine(MoveObject(keyItem, lowerPosition));
                    StartCoroutine(ChangeColor(keyItem, bucketColors[i]));
                    yield return new WaitForSeconds(timeout);
                }

                if (!moved)
                {
                    Vector3 lowerPosition = keyItem.transform.localPosition - Vector3.up * liftHeight;
                    StartCoroutine(MoveObject(keyItem, lowerPosition));
                    yield return new WaitForSeconds(timeout);
                }
            }
            
            foreach (var sortedItem in bucket)
            {
                StartCoroutine(ChangeColor(sortedItem, Color.green));
                Vector3 sortedPosition = new Vector3(index * itemSpacing, 0, 0);
                yield return StartCoroutine(MoveObject(sortedItem, sortedPosition));
                index++;
            }
        }
    }
    //+index - do góry; -index do dołu
    private void CreateBucketContainer(int index)
    {
        GameObject bucketObj = new GameObject("BucketContainer" + index);
        bucketObj.transform.position = new Vector3(-5, bucketYOffset + index * bucketSpacing, 0);
        bucketContainers[index] = bucketObj.transform;
    }
}
