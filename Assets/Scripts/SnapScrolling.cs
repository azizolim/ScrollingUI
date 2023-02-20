using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapScrolling : MonoBehaviour
{
    [Range(1, 50)] [Header("controllers")] [SerializeField]
    private int panelCount;

    [Range(0, 500)] [SerializeField] private int panelOffset;

    [Header("Scrolling objects")] [SerializeField]
    private ScrollPanel panelPrefab;

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private float snapSpeed;
    [SerializeField] private float scaleOffset;
    [SerializeField] private float scaleSpeed;

    private ScrollPanel[] _panels;
    private Vector2[] _panelsPosition;
    private RectTransform _contentRect;
    private int _selectedPanelID;
    private bool _isScrolling;
    private Vector2 _contentVector;
    private Vector2[] _panelsScale;

    private void Start()
    {
        _panels = new ScrollPanel[panelCount];
        _panelsPosition = new Vector2[panelCount];
        _contentRect = GetComponent<RectTransform>();
        _panelsScale = new Vector2[panelCount];
        for (int i = 0; i < panelCount; i++)
        {
            _panels[i] = Instantiate(panelPrefab, transform, false);
            if (i == 0) continue;
            _panels[i].transform.localPosition =
                new Vector2(
                    _panels[i - 1].transform.localPosition.x + panelPrefab.GetComponent<RectTransform>().sizeDelta.x +
                    panelOffset, _panels[i].transform.localPosition.y);

            _panelsPosition[i] = -_panels[i].transform.localPosition;
        }
    }

    private void FixedUpdate()
    {
        if (_contentRect.anchoredPosition.x >= _panelsPosition[0].x && !_isScrolling || _contentRect.anchoredPosition.x <= _panelsPosition[_panelsPosition.Length-1].x && !_isScrolling)
        {
            scrollRect.inertia = false;
        }
        float nearestPosition = float.MaxValue;
        for (int i = 0; i < panelCount; i++)
        {
            float distance = Math.Abs(_contentRect.anchoredPosition.x - _panelsPosition[i].x);
            if (distance < nearestPosition)
            {
                nearestPosition = distance;
                _selectedPanelID = i;
            }

            float scale = Mathf.Clamp(1 / (distance / panelOffset) * scaleOffset, 0.5f, 1f);
            _panelsScale[i].x = Mathf.SmoothStep(_panels[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
            _panelsScale[i].y = Mathf.SmoothStep(_panels[i].transform.localScale.y, scale, scaleSpeed * Time.fixedDeltaTime);
            _panels[i].transform.localScale = _panelsScale[i];
        }

        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        if (scrollVelocity < 400 && !_isScrolling) scrollRect.inertia = false;
        if (_isScrolling || scrollVelocity>400) return;
        _contentVector.x =
            Mathf.SmoothStep(_contentRect.anchoredPosition.x, _panelsPosition[_selectedPanelID].x, snapSpeed*Time.fixedDeltaTime);
        _contentRect.anchoredPosition = _contentVector;
    }

    public void Scroll(bool scroll)
    {
        _isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }
}