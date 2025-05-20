using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IteratorAnim : MonoBehaviour
{
    [Header("Movement settings")]
    [Range(0f, 0.5f)] public float offsetPercent = 0.3f;
    public float lerpDuration = 2f;

    [Header("Sprites")]
    public Sprite normalSprite;
    public Sprite edgeSprite;
    public float edgeSpriteDuration = 0.2f;

    public RectTransform rectTransform;
    public RectTransform parentRect;
    private Image image;

    private Vector2 leftPos;
    private Vector2 rightPos;
    private Vector2 leftPosDown;
    private Vector2 rightPosUp;
    private float lastParentWidth = -1f;

    private string iconName;
    private bool isAnimating = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRect = transform.parent?.GetComponent<RectTransform>();
        image = GetComponent<Image>();

        if (image == null || parentRect == null || rectTransform == null)
        {
            enabled = false;
            return;
        }

        iconName = gameObject.name;

    }

    private void LateUpdate()
    {
        if (parentRect != null && !Mathf.Approximately(parentRect.rect.width, lastParentWidth))
        {
            lastParentWidth = parentRect.rect.width;
            CalculateTargetPositions();

            if (!isAnimating)
            {
                if(iconName == "MoveIteratorIcon")
                {
                    StartCoroutine(MoveIteratorLoop());
                } 
                else if (iconName == "PhoneIcon")
                {
                    StartCoroutine(MovePhoneLoop());
                }
                else if (iconName == "MoveObjectIcon")
                {
                    Debug.Log("MoveObject Anim");
                    StartCoroutine(MoveObjectLoop());
                }

            }
        }
    }

    private void CalculateTargetPositions()
    {
        float parentWidth = parentRect.rect.width;
        float maxOffset = parentWidth * offsetPercent;

        leftPos = new Vector2(-maxOffset, rectTransform.anchoredPosition.y);
        rightPos = new Vector2(maxOffset, rectTransform.anchoredPosition.y);

        float parentHeight = parentRect.rect.height;
        float maxOffsetHeight = parentHeight * 0.1f;

        leftPosDown = new Vector2(-maxOffset, rectTransform.anchoredPosition.y-maxOffsetHeight);
        rightPosUp = new Vector2(maxOffset, rectTransform.anchoredPosition.y+maxOffsetHeight);
    }

    private IEnumerator MoveIteratorLoop()
    {
        isAnimating = true;
        while (true)
        {
            yield return LerpToPosition(rightPos);
            ShowEdgeSprite();
            yield return new WaitForSeconds(edgeSpriteDuration);
            ResetSprite();
            yield return new WaitForSeconds(edgeSpriteDuration);
            ShowEdgeSprite();
            yield return new WaitForSeconds(edgeSpriteDuration);
            ResetSprite();

            yield return LerpToPosition(leftPos);
            ShowEdgeSprite();
            yield return new WaitForSeconds(edgeSpriteDuration);
            ResetSprite();
            yield return new WaitForSeconds(edgeSpriteDuration);
            ShowEdgeSprite();
            yield return new WaitForSeconds(edgeSpriteDuration);
            ResetSprite();
        }
    }

    private IEnumerator MovePhoneLoop()
    {
        isAnimating = true;
        while (true)
        {
            yield return LerpToPosition(rightPos);
            yield return new WaitForSeconds(edgeSpriteDuration);

            yield return LerpToPosition(leftPos);
            yield return new WaitForSeconds(edgeSpriteDuration);

        }
    }

    private IEnumerator MoveObjectLoop()
    {
        isAnimating = true;
        while (true)
        {
            rectTransform.anchoredPosition = leftPosDown;
            yield return new WaitForSeconds(0.4f);
            ShowEdgeSprite();
            yield return LerpToPosition(rightPosUp);
            ResetSprite();
        }
    }

    private IEnumerator LerpToPosition(Vector2 target)
    {
        Vector2 start = rectTransform.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < lerpDuration)
        {
            if (!transform.parent.parent.gameObject.activeInHierarchy)
                yield break;

            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / lerpDuration);
            rectTransform.anchoredPosition = Vector2.Lerp(start, target, t);
            yield return null;
        }

        rectTransform.anchoredPosition = target;
    }

    private void ShowEdgeSprite()
    {
        if (edgeSprite != null)
            image.sprite = edgeSprite;
    }

    private void ResetSprite()
    {
        if (normalSprite != null)
            image.sprite = normalSprite;
    }
}
