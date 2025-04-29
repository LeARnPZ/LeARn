using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeSort : Sortings
{

    private IEnumerator Sorting(List<GameObject> array)
    {
        // Je�li lista ma tylko jeden element, nie trzeba jej sortowa�
        if (array.Count <= 1) yield break;

        int mid = array.Count / 2; // Wyznaczamy �rodek listy

        float baseDistance = 1.0f; // Mniejszy odst�p, aby elementy nie rozchodzi�y si� za bardzo
        float offsetUp = 1.5f; // Mniejsze przesuni�cie w g�r�, aby unikn�� nadmiernego rozpraszania

        yield return new WaitForSeconds(timeout);

        // Przesuwamy elementy lewej cz�ci w lewo i w g�r�
        for (int i = 0; i < mid; i++)
        {
            float newX = array[i].transform.localPosition.x - baseDistance; // Przesuni�cie w lewo
            Vector3 newPos = new Vector3(newX, array[i].transform.localPosition.y + offsetUp, 0);
            StartCoroutine(MoveObject(array[i], newPos)); // Animujemy przesuni�cie
            yield return new WaitForSeconds(timeout); // Kr�tka pauza mi�dzy ruchami
        }

        // Przesuwamy elementy prawej cz�ci w prawo i w g�r�
        for (int i = mid; i < array.Count; i++)
        {
            float newX = array[i].transform.localPosition.x + baseDistance; // Przesuni�cie w prawo
            Vector3 newPos = new Vector3(newX, array[i].transform.localPosition.y + offsetUp, 0);
            StartCoroutine(MoveObject(array[i], newPos)); // Animujemy przesuni�cie
            yield return new WaitForSeconds(timeout);
        }

        yield return new WaitForSeconds(timeout); // Kr�tka przerwa przed dalszym sortowaniem

        // Dzielimy list� na dwie cz�ci
        List<GameObject> left = new List<GameObject>(array.GetRange(0, mid));
        List<GameObject> right = new List<GameObject>(array.GetRange(mid, array.Count - mid));

        // Rekurencyjne sortowanie lewej cz�ci
        yield return StartCoroutine(Sorting(left));

        // Rekurencyjne sortowanie prawej cz�ci
        yield return StartCoroutine(Sorting(right));

        // Scalanie obu posortowanych cz�ci
        yield return StartCoroutine(Merge(left, right, array));
    }


    private IEnumerator Merge(List<GameObject> left, List<GameObject> right, List<GameObject> result)
    {
        int leftIndex = 0, rightIndex = 0, resultIndex = 0;

        // Wyznaczamy �rodkow� pozycj�, aby elementy podczas scalania nie rozchodzi�y si�
        float startX = (left[0].transform.localPosition.x + right[right.Count - 1].transform.localPosition.x) / 2;
        float stepX = 1.0f; // Ma�y odst�p poziomy mi�dzy elementami
        float startY = left[0].transform.localPosition.y - 1.5f; // Przesuwamy ni�ej, aby zachowa� struktur� drzewa

        List<Vector3> resultPositions = new List<Vector3>(); // Lista docelowych pozycji element�w

        // Scalanie dw�ch list, wybieraj�c mniejsze warto�ci
        while (leftIndex < left.Count && rightIndex < right.Count)
        {
            GameObject leftObj = left[leftIndex];
            GameObject rightObj = right[rightIndex];


            StartCoroutine(ChangeColor(leftObj, yellowColor));
            yield return StartCoroutine(BounceObject(leftObj));

            StartCoroutine(ChangeColor(rightObj, yellowColor));
            yield return StartCoroutine(BounceObject(rightObj));

            yield return new WaitForSeconds(timeout);


            if (GetValue(left[leftIndex]) <= GetValue(right[rightIndex]))
            {
                result[resultIndex] = left[leftIndex++]; // Wybieramy mniejszy element
            }
            else
            {
                result[resultIndex] = right[rightIndex++]; // Wybieramy mniejszy element
            }

            // Ustawiamy docelow� pozycj� w nowym zbiorze
            resultPositions.Add(new Vector3(startX + resultIndex * stepX, startY, 0));
            resultIndex++;


            //Przywracamy pierwotny kolor po por�wnaniu
            StartCoroutine(ChangeColor(leftObj, blueColor));
            StartCoroutine(ChangeColor(rightObj, blueColor));
            yield return new WaitForSeconds(timeout);

            while (resultPositions.Count > 0 && result.Count > 0)
            {
                Vector3 positionToMove = resultPositions[0];
                GameObject objectToMove = result[resultIndex - resultPositions.Count];


                resultPositions.RemoveAt(0);

                StartCoroutine(MoveObject(objectToMove, positionToMove));
                yield return new WaitForSeconds(timeout);
            }

        }

        // Dodajemy pozosta�e elementy z lewej cz�ci (je�li jakie� zosta�y)
        while (leftIndex < left.Count)
        {
            result[resultIndex] = left[leftIndex++];
            resultPositions.Add(new Vector3(startX + resultIndex * stepX, startY, 0));
            resultIndex++;
        }

        // Dodajemy pozosta�e elementy z prawej cz�ci (je�li jakie� zosta�y)
        while (rightIndex < right.Count)
        {
            result[resultIndex] = right[rightIndex++];
            resultPositions.Add(new Vector3(startX + resultIndex * stepX, startY, 0));
            resultIndex++;
        }

        // Animujemy przesuwanie element�w na ich ostateczne pozycje
        while (resultPositions.Count > 0 && result.Count > 0)
        {
            Vector3 positionToMove = resultPositions[0];
            GameObject objectToMove = result[resultIndex - resultPositions.Count];


            resultPositions.RemoveAt(0);

            StartCoroutine(MoveObject(objectToMove, positionToMove));
            yield return new WaitForSeconds(timeout);
        }

    }


    private IEnumerator BounceObject(GameObject obj, float bounceHeight = 0.5f, float bounceTime = 0.3f)
    {
        Vector3 originalPos = obj.transform.localPosition;
        Vector3 upPos = originalPos + new Vector3(0, bounceHeight, 0);

        // Podskok
        float elapsed = 0;
        while (elapsed < bounceTime)
        {
            obj.transform.localPosition = Vector3.Lerp(originalPos, upPos, elapsed / bounceTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        obj.transform.localPosition = upPos;

        // Powr�t
        elapsed = 0;
        while (elapsed < bounceTime)
        {
            obj.transform.localPosition = Vector3.Lerp(upPos, originalPos, elapsed / bounceTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        obj.transform.localPosition = originalPos;
    }


    public override void Restart()
    {
        base.Restart();
    }

    private IEnumerator EndSorting(List<GameObject> array)
    {

        float waveDelay = 0.1f; // op�nienie mi�dzy kolejnymi elementami
        float bounceHeight = 0.4f;

        for (int i = 0; i < array.Count; i++)
        {
            // Zmieniamy kolor na zielony i podskakujemy
            StartCoroutine(ChangeColor(array[i], greenColor));
            StartCoroutine(BounceObject(array[i], bounceHeight));

            yield return new WaitForSeconds(waveDelay);
        }
    }

    protected override IEnumerator Sort()
    {
        yield return StartCoroutine(Sorting(items));


        yield return new WaitForSeconds(timeout * 2f);
        yield return StartCoroutine(EndSorting(items));

    }
}
