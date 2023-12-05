using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static class CanvasGrouopExtensions
{
    public static Tweener FadeOut(this CanvasGroup canvasGroup, float duration)
    {
        return canvasGroup.DOFade(0.0f, duration);
    }
    
    public static Tweener FadeIn(this CanvasGroup canvasGroup, float duration)
    {
        return canvasGroup.DOFade(1.0f, duration);
    }
}
