using UnityEngine;

public class SpriteTransition : MonoBehaviour
{
    public Sprite[] sprites;  // Tablica z dwoma sprite'ami
    public float transitionDuration = 1f;  // Czas przej�cia mi�dzy sprite'ami
    public AnimationCurve transitionCurve;  // Krzywa przej�cia (w Inspectorze)

    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex = 0;  // Aktualny indeks sprite'a
    private float transitionTime = 0f;  // Czas trwania animacji

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();  // Pobieramy SpriteRenderer
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[currentSpriteIndex];  // Ustawiamy pocz�tkowy sprite
        }
    }

    void Update()
    {
        // Je�li czas animacji przej�cia nie przekroczy� zdefiniowanego czasu
        if (transitionTime < transitionDuration)
        {
            transitionTime += Time.deltaTime;  // Zwi�kszamy czas przej�cia
            float t = transitionTime / transitionDuration;  // Normalizacja czasu
            float easedT = transitionCurve.Evaluate(t);  // Zastosowanie easing (krzywej)

            // Interpolacja pomi�dzy dwoma sprite'ami
            spriteRenderer.sprite = GetInterpolatedSprite(easedT);
        }
    }

    // Funkcja do interpolacji mi�dzy sprite'ami
    Sprite GetInterpolatedSprite(float t)
    {
        // Je�li mamy dwa sprite'y, interpolujemy mi�dzy nimi
        if (sprites.Length > 1)
        {
            return sprites[0]; // Mo�esz zaimplementowa� tutaj w�asn� logik�
        }

        return null;
    }

    // Funkcja zmieniaj�ca sprite w zale�no�ci od warto�ci 't'
    public void ChangeSprite()
    {
        if (sprites.Length > 0)
        {
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
            transitionTime = 0f;  // Resetujemy czas przej�cia
        }
    }
}
