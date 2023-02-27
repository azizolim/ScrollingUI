using System;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private CanvasGroup scrollPanel;
    [SerializeField] private Button scrollButton;
    [SerializeField] private CanvasGroup mainPanel;
    [SerializeField] private Button playButton;
    [SerializeField] private SnapScrolling scroll;

    private ChangePanel _changePanel;
    private MainPanelDelegate _mainPanelDelegate;
    

    private void Start()
    {
        scroll.TryGetComponent(out IInit<MainPanelDelegate> init);
        _mainPanelDelegate = OnMainPanel;
        init.Initialize(_mainPanelDelegate);
        _changePanel = new ChangePanel();
        OnMainPanel();
        scrollButton.onClick.AddListener(OnScrollPanel);
        playButton.onClick.AddListener(OnPlayButton);
    }
    
    public void OnScrollPanel()
    {
        _changePanel.SetPanel(scrollPanel);
    }

   public void OnMainPanel()
    {
        _changePanel.SetPanel(mainPanel);
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("Game");
    }
}

public delegate void MainPanelDelegate(); 
