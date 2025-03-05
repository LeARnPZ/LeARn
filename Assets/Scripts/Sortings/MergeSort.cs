using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeSort : Sortings
{
    private int interpolationX = -5;
    private IEnumerator Sorting(List<GameObject> array)
    {
        Debug.Log("Dzia�aj prosz�");

        // Je�li lista ma 1 element, nie trzeba sortowa�
        if (array.Count <= 1)
        {
            yield break;
        }

        int mid = array.Count / 2;

        // Obliczamy odst�p, aby elementy nie nachodzi�y na siebie
        float distance = 2.0f; // Odst�p mi�dzy elementami w jednostkach
        float offsetLeft = -distance * mid / 2; // Rozpoczynamy od przesuni�cia w lewo
        float offsetRight = distance * mid / 2; // Rozpoczynamy od przesuni�cia w prawo
        float offsetUp = 2.0f; // Odst�p w kierunku g�ry dla obydwu stron

        // Pierwsza animacja - tylko rozdzielamy strony
        for (int i = 0; i < mid; i++)
        {
            // Przesuwamy elementy z lewej strony w lewo i w g�r�
            StartCoroutine(MoveObject(array[i], array[i].transform.localPosition + new Vector3(offsetLeft, offsetUp, 0)));
            yield return new WaitForSeconds(timeout);
        }

        for (int i = mid; i < array.Count; i++)
        {
            // Przesuwamy elementy z prawej strony w prawo i w g�r�
            StartCoroutine(MoveObject(array[i], array[i].transform.localPosition + new Vector3(offsetRight, offsetUp, 0)));
            yield return new WaitForSeconds(timeout);
        }

        // Czekamy chwil� po rozdzieleniu stron
        yield return new WaitForSeconds(timeout);

        // Teraz wykonujemy sortowanie lewej i prawej cz�ci
        List<GameObject> left = new List<GameObject>(array.GetRange(0, mid));
        List<GameObject> right = new List<GameObject>(array.GetRange(mid, array.Count - mid));

        // Sortowanie lewej cz�ci
        yield return StartCoroutine(Sorting(left));

        // Sortowanie prawej cz�ci
        yield return StartCoroutine(Sorting(right));

        
        // Scalanie obu cz�ci
        yield return StartCoroutine(Merge(left, right, array));
    }

    private IEnumerator Merge(List<GameObject> left, List<GameObject> right, List<GameObject> result)
    {
        int leftIndex = 0;
        int rightIndex = 0;
        int resultIndex = 0;

        // Tworzymy list�, aby przechowa� pozycje przed animowaniem
        List<Vector3> resultPositions = new List<Vector3>();

        // Przechodzimy przez obie listy, por�wnuj�c elementy i dodaj�c mniejsze do wyniku
        while (leftIndex < left.Count && rightIndex < right.Count)
        {
            if (GetValue(left[leftIndex]) <= GetValue(right[rightIndex]))
            {
                result[resultIndex] = left[leftIndex];
                Vector3 tmp = left[leftIndex].transform.localPosition;
                resultPositions.Add(new Vector3(interpolationX * 1.0f, tmp.y - 2.0f, 0)); // Zapisujemy docelow� pozycj�
                leftIndex++;
            }
            else
            {
                result[resultIndex] = right[rightIndex];
                Vector3 tmp = right[rightIndex].transform.localPosition;
                resultPositions.Add(new Vector3(interpolationX * 1.0f, tmp.y - 2.0f, 0)); // Zapisujemy docelow� pozycj�
                rightIndex++;
            }
            resultIndex++;
            interpolationX++;
            yield return new WaitForSeconds(timeout); 
        }

        // Dodajemy pozosta�e elementy z lewej cz�ci, je�li s�
        while (leftIndex < left.Count)
        {
            result[resultIndex] = left[leftIndex];
            Vector3 tmp = left[leftIndex].transform.localPosition;
            resultPositions.Add(new Vector3(interpolationX * 1.0f, tmp.y - 2.0f, 0)); // Zapisujemy docelow� pozycj�
            leftIndex++;
            resultIndex++;
            interpolationX++;
            yield return new WaitForSeconds(timeout); 
        }

        // Dodajemy pozosta�e elementy z prawej cz�ci, je�li s�
        while (rightIndex < right.Count)
        {
            result[resultIndex] = right[rightIndex];
            Vector3 tmp = right[rightIndex].transform.localPosition;
            resultPositions.Add(new Vector3(interpolationX * 1.0f, tmp.y - 2.0f, 0)); // Zapisujemy docelow� pozycj�
            rightIndex++;
            resultIndex++;
            interpolationX++;
            yield return new WaitForSeconds(timeout); // Czekamy chwil� na animacj�
        }

        // Teraz animujemy elementy w wyniku, aby przemie�ci�y si� na swoje ostateczne pozycje
        for (int i = 0; i < result.Count; i++)
        {
            StartCoroutine(MoveObject(result[i], resultPositions[i])); // Przemieszczamy do zapisanych pozycji
            yield return new WaitForSeconds(timeout); 

            /*
            //ustawiamy kolor dla porownywanych elementow
            if (i + 1 < result.Count) StartCoroutine(ChangeColor(result[i], Color.yellow));
            yield return new WaitForSeconds(timeout);
            if (i + 1 < result.Count) StartCoroutine(ChangeColor(result[i + 1], Color.yellow));
            yield return new WaitForSeconds(timeout);

            // Przemieszczamy do zapisanych pozycji
            StartCoroutine(MoveObject(result[i], resultPositions[i]));
            yield return new WaitForSeconds(timeout);
            if (i + 1 < result.Count) StartCoroutine(MoveObject(result[i + 1], resultPositions[i + 1]));
            yield return new WaitForSeconds(timeout);

            //wracamy do pierwotnego koloru
            if (i + 1 < result.Count) StartCoroutine(ChangeColor(result[i], Color.white));
            yield return new WaitForSeconds(timeout);
            if (i + 1 < result.Count) StartCoroutine(ChangeColor(result[i + 1], Color.white));
            yield return new WaitForSeconds(timeout);*/
        }
    }

    protected override IEnumerator Sort()
    {
        // Rozpocz�cie sortowania
        yield return StartCoroutine(Sorting(items));
    }
}
