using System;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private CanvasGroup scrollPanel;
    [SerializeField] private CanvasGroup mainPanel;
    [SerializeField] private CanvasGroup shopPanel;
    [SerializeField] private CanvasGroup buttonsPanel;
    [SerializeField] private Button scrollButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button onMainPanelButton;
    private ChangePanel _changePanel;

    private void Start()
    {
        _changePanel = new ChangePanel();
        OnMainPanel();
        ButtonsPanel();
        scrollButton.onClick.AddListener(OnScrollPanel);
        shopButton.onClick.AddListener(OnShopPAnel);
        onMainPanelButton.onClick.AddListener(OnMainPanel);
    }

    public void ButtonsPanel()
    {
        _changePanel.SetPanel(buttonsPanel);
    }

    public void OnMainPanel()
    {
        _changePanel.SetPanel(mainPanel);
    }

    public void OnScrollPanel()
    {
        _changePanel.SetPanel(scrollPanel);
    }

    public void OnShopPAnel()
    {
        _changePanel.SetPanel(shopPanel);
    }
}
