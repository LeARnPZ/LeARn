using UnityEngine;

public class SpriteTransition : MonoBehaviour
{
    public Sprite[] sprites;  // Tablica z dwoma sprite'ami
    public float transitionDuration = 1f;  // Czas przejœcia miêdzy sprite'ami
    public AnimationCurve transitionCurve;  // Krzywa przejœcia (w Inspectorze)

    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex = 0;  // Aktualny indeks sprite'a
    private float transitionTime = 0f;  // Czas trwania animacji

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();  // Pobieramy SpriteRenderer
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[currentSpriteIndex];  // Ustawiamy pocz¹tkowy sprite
        }
    }

    void Update()
    {
        // Jeœli czas animacji przejœcia nie przekroczy³ zdefiniowanego czasu
        if (transitionTime < transitionDuration)
        {
            transitionTime += Time.deltaTime;  // Zwiêkszamy czas przejœcia
            float t = transitionTime / transitionDuration;  // Normalizacja czasu
            float easedT = transitionCurve.Evaluate(t);  // Zastosowanie easing (krzywej)

            // Interpolacja pomiêdzy dwoma sprite'ami
            spriteRenderer.sprite = GetInterpolatedSprite(easedT);
        }
    }

    // Funkcja do interpolacji miêdzy sprite'ami
    Sprite GetInterpolatedSprite(float t)
    {
        // Jeœli mamy dwa sprite'y, interpolujemy miêdzy nimi
        if (sprites.Length > 1)
        {
            return sprites[0]; // Mo¿esz zaimplementowaæ tutaj w³asn¹ logikê
        }

        return null;
    }

    // Funkcja zmieniaj¹ca sprite w zale¿noœci od wartoœci 't'
    public void ChangeSprite()
    {
        if (sprites.Length > 0)
        {
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
            transitionTime = 0f;  // Resetujemy czas przejœcia
        }
    }
}
