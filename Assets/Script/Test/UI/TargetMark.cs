using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TargetMark : MonoBehaviour
{
    public float duration;
    public UIDetectBar UIdetectBar;
    private bool hasDoneOnce;
    [SerializeField] private Transform endingPosition;
    [SerializeField] private Transform startPosition;

    private void Awake()
    {
        UIdetectBar = transform.parent.GetComponentInChildren<UIDetectBar>();
    }

    private void Start()
    {
        hasDoneOnce = false;
    }

    private void Update()
    {
        if (UIdetectBar.detectBar.fillAmount > 0  && hasDoneOnce == false)
        {
            moving(endingPosition.position, duration);
            hasDoneOnce = true;
        }

        if (UIdetectBar.detectBar.fillAmount <= 0 && hasDoneOnce == true)
        {
            moving(startPosition.position, duration);
            hasDoneOnce = false;
        }

    }

    protected void moving(Vector3 EndingPosition, float duration)
    {
        Tween moveTween = transform.DOMove(EndingPosition, duration);
        moveTween.SetEase(Ease.InOutQuad);
    }
}
