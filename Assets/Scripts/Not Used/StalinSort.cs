using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalinSort : Sortings
{
    [SerializeField]
    private Material particleMaterial;

    private void RearrangeObjects()
    {
        int count = items.Count;
        Vector3 startPosition = new(-count/2f + 1, 0, 0);
        int i = 0;
        foreach (GameObject item in items)
        {
            StartCoroutine(MoveObject(item, startPosition + 1.2f * i * Vector3.right));
            i++;
        }
    }

    private void AddParticles(GameObject gameObject)
    {
        ParticleSystem particles = gameObject.AddComponent<ParticleSystem>();
        ParticleSystemRenderer renderer = gameObject.GetComponent<ParticleSystemRenderer>();
        particles.Stop();

        ParticleSystem.MainModule particlesMain = particles.main;
        ParticleSystem.EmissionModule particlesEmission = particles.emission;
        ParticleSystem.ShapeModule particlesShape = particles.shape;

        particlesMain.loop = false;
        particlesMain.duration = animDuration;
        particlesMain.startColor = Color.yellow;
        particlesMain.startLifetime = 1;
        particlesMain.startSpeed = 0.1f;
        particlesMain.startSize = 0.1f;

        particlesEmission.rateOverTime = 15;

        particlesShape.shapeType = ParticleSystemShapeType.Sphere;
        particlesShape.radius = 0.1f;
        particlesShape.radiusThickness = 0.1f;

        renderer.material = particleMaterial;
        
        particles.Play();
    }

    //private IEnumerator ReadjustCamera()
    //{
    //    int count = items.Count;
    //    GameObject camera = GameObject.Find("Main Camera");
    //    if (count < 3 || count == camera.transform.position.z) yield break;

    //    float elapsedTime = 0;
    //    Vector3 currentPosition = camera.transform.position;
    //    Vector3 newPosition = new Vector3(0, 0.5f, -count);
    //    while (elapsedTime < animDuration)
    //    {
    //        camera.transform.position = Vector3.Lerp(currentPosition, newPosition, elapsedTime / animDuration);
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }
    //}

    protected override IEnumerator Sort()
    {
        yield return new WaitForSeconds(timeout);
        int removedItems = 0;
        for (int i = 0; i < numberOfItems-1-removedItems; i++)
        {
            int val1 = GetValue(items[i]);
            int val2 = GetValue(items[i+1]);

            StartCoroutine(ChangeColor(items[i], Color.yellow));
            StartCoroutine(ChangeColor(items[i+1], Color.yellow));
            yield return new WaitForSeconds(timeout);

            if (val2 < val1)
            {
                items[i+1].GetComponent<MeshRenderer>().enabled = false;
                items[i+1].transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                items[i + 1].transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
                AddParticles(items[i + 1]);
                yield return new WaitForSeconds(timeout);

                items.Remove(items[i+1]);
                removedItems++;
                i--;
                RearrangeObjects();
                //StartCoroutine(ReadjustCamera());
            }
            else
            {
                StartCoroutine(ChangeColor(items[i], Color.white));
            }

            StartCoroutine(ChangeColor(items[i + 1], Color.white));
            yield return new WaitForSeconds(timeout);
        }

        foreach (GameObject item in items)
            StartCoroutine(ChangeColor(item, Color.green));
    }
}
