using UnityEngine;
using UnityEngine.UI;

public interface ITransitionEffect
{
    void ApplyTransition(CanvasGroup current, CanvasGroup next);
}