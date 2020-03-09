using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platinio.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Vector2 startPosition = Vector2.zero;
    [SerializeField] private Vector2 desirePosition = Vector2.zero;
    [SerializeField] private RectTransform canvas = null;
    [SerializeField] private float time = 0.5f;
    [SerializeField] private Ease enterEase = Ease.EaseInOutExpo;
    [SerializeField] private Ease exitEase = Ease.EaseInOutExpo;

    private bool isVisible = false;
    private RectTransform thisRect = null;

    // Start is called before the first frame update
    void Start()
    {
        thisRect = GetComponent<RectTransform>();
        thisRect.anchoredPosition = thisRect.FromAbsolutePositionToAnchoredPosition(startPosition, canvas);
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartGame() {
        Hide();
    }

    public void QuitGame() {
#if UNITY_STANDALONE
        Application.Quit();
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void Show() {
        if (isVisible)
            return;

        thisRect.MoveUI(desirePosition, canvas, time).SetEase(enterEase).SetOnComplete(delegate {
            isVisible = true;
        });

    }

    public void Hide() {
        if (!isVisible)
            return;

        thisRect.MoveUI(startPosition, canvas, time).SetEase(exitEase).SetOnComplete(delegate {
            isVisible = false;
        });
    }
}
