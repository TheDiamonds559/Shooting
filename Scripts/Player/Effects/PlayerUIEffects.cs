using System;
using System.Collections;
using TMPro;
using UnityEngine;

public static class PlayerUIEffects 
{
    public static void CrossOutText(ref TMP_Text item)
    {
        item.text = "<s>" + item.text + "</s>";
    }

    public static IEnumerator FloatElement(RectTransform rectTransform, Vector2 startPos, Vector2 endPos, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            t = Mathf.Clamp01(t);

            float easeT = 1 - Mathf.Pow(1 - t, 2);

            rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, easeT);
            yield return null;
        }

        rectTransform.anchoredPosition = endPos;
    }
    public static IEnumerator FloatElement(RectTransform rectTransform, Vector2 startPos, Vector2 endPos, float duration, Action callback)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            t = Mathf.Clamp01(t);

            float easeT = 1 - Mathf.Pow(1 - t, 2);

            rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, easeT);
            yield return null;
        }

        rectTransform.anchoredPosition = endPos;
        callback();
    }
}
