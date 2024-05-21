using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LeantweenTransitionType
{
    Linear,
    Cubic,
    Bounce
}

public enum FadeType
{
    None,
    FadeIn,
    FadeOut
}

public enum TransitionDirection
{
    None,
    Left,
    Right,
    Up,
    Down
}

[RequireComponent(typeof(CanvasGroup))]
public class LeanTweenTransition : MonoBehaviour
{
    [SerializeField] float transitionSpeed;
    [SerializeField] float distance;
    [SerializeField] FadeType fadeType;
    [SerializeField] TransitionDirection transitionDirection;
    [SerializeField] LeantweenTransitionType leantweenTransitionType;
    CanvasGroup canvasGroup;

    Vector3 startPos;
    bool _isTweening;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        startPos = transform.localPosition;
    }

    public void TriggerTransition(TransitionDirection direction = TransitionDirection.None)
    {
        if (direction == TransitionDirection.None)
        {
            direction = transitionDirection;
        }

        switch (direction)
        {
            case TransitionDirection.Left:
            Debug.Log($"start pos is {startPos} and distance with vector is {Vector3.left*distance}");
                TransitWithDirection(startPos + Vector3.left * distance);
                break;
            case TransitionDirection.Right:
                TransitWithDirection(startPos + Vector3.right * distance);
                break;
            case TransitionDirection.Up:
                TransitWithDirection(startPos + Vector3.up * distance);
                break;
            case TransitionDirection.Down:
                TransitWithDirection(startPos + Vector3.down * distance);
                break;
            default:
                break;
        }
    }
    public void TriggerTransition(Vector3 endDest)
    {
        TransitWithDirection(endDest);
    }

    void TransitWithDirection(Vector3 endPoint)
    {
        //if (_isTweening) return;
        _isTweening = true;

        LTSeq sequence = LeanTween.sequence();

        switch (fadeType)
        {
            case FadeType.FadeIn:
                sequence.append(LeanTween.value(0f, 1f, transitionSpeed).setOnUpdate((float value) =>
                {
                    canvasGroup.alpha = value;
                }));
                break;
            case FadeType.FadeOut:
                sequence.append(LeanTween.value(1f, 0f, transitionSpeed).setOnUpdate((float value) =>
                {
                    canvasGroup.alpha = value;
                }));
                break;
            default:
                break;
        }
        switch (leantweenTransitionType)
        {
            case LeantweenTransitionType.Linear:

                sequence.append(LeanTween.moveLocal(gameObject, endPoint, transitionSpeed).
                setEase(LeanTweenType.linear));
                break;
            case LeantweenTransitionType.Cubic:
                sequence.append(LeanTween.moveLocal(gameObject, endPoint, transitionSpeed).
                setEase(LeanTweenType.easeInOutCubic));
                break;
            case LeantweenTransitionType.Bounce:
                sequence.append(LeanTween.moveLocal(gameObject, endPoint, transitionSpeed).
                setEase(LeanTweenType.easeOutBounce));
                break;
            default:
                break;
        }
        startPos = endPoint;
        sequence.append(LeanTween.alpha(gameObject,1,0f).setOnComplete(()=>{_isTweening = false;
        }));
    }
}
