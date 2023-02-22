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
    
    private ChangePanel _changePanel;

    private void Start()
    {
        _changePanel = new ChangePanel();
        OnMainPanel();
        scrollButton.onClick.AddListener(OnScrollPanel);
        playButton.onClick.AddListener(OnPlayButton);
        
    }
    
    public void OnScrollPanel()
    {
        _changePanel.SetPanel(scrollPanel);
    }

    void OnMainPanel()
    {
        _changePanel.SetPanel(mainPanel);
    }

    void OnPlayButton()
    {
        SceneManager.LoadScene("Game");
    }

}
