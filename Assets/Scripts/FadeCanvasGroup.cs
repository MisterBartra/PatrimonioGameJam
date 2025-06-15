using System.Collections;
using UnityEngine;

public class FadeCanvasGroup : MonoBehaviour, ITransitionEffect
{
    public float transitionDuration = 1f;

    public void ApplyTransition(CanvasGroup current, CanvasGroup next)
    {
        StartCoroutine(FadeRoutine(current, next));
    }

    private IEnumerator FadeRoutine(CanvasGroup current, CanvasGroup next)
    {
        float elapsedTime = 0f;
        next.gameObject.SetActive(true);

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = elapsedTime / transitionDuration;
            current.alpha = 1 - alpha;
            next.alpha = alpha;
            yield return null;
        }

        current.gameObject.SetActive(false);
        current.alpha = 1;
    }
}