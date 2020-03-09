using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platinio.UI;

public class PopUpScript : MonoBehaviour
{
    [SerializeField] private Vector2 startPosition = Vector2.zero;
    [SerializeField] private Vector2 desirePosition = Vector2.zero;
    [SerializeField] private RectTransform canvas = null;
    [SerializeField] private float time = 0.5f;
    [SerializeField] private Ease enterEase = Ease.EaseInOutExpo;
    [SerializeField] private Ease exitEase = Ease.EaseInOutExpo;

    private bool isVisible = false;

    private RectTransform thisRect = null;

    private void Start() {
        thisRect = GetComponent<RectTransform>();

        thisRect.anchoredPosition = thisRect.FromAbsolutePositionToAnchoredPosition(startPosition, canvas);
    }

    public void Show() {
        if (isVisible)
            return;

        thisRect.MoveUI(desirePosition, canvas, time).SetEase(enterEase).SetOnComplete(delegate
        {
            isVisible = true;
        });

    }

    public void Hide() {
        if (!isVisible)
            return;

        thisRect.MoveUI(startPosition, canvas, time).SetEase(exitEase).SetOnComplete(delegate
        {
            isVisible = false;
        });
    }
}
