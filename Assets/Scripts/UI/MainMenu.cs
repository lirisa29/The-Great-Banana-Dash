using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Main UI")]
    public GameObject mainPanel;
    public Button closeButton;

    [Header("Buttons & Backgrounds")]
    public Button playButton;
    public GameObject playBackground;

    public Button howToPlayButton;
    public GameObject howToPlayBackground;

    public Button optionsButton;
    public GameObject optionsBackground;

    [Header("Child Panels")]
    public GameObject playPanel;
    public GameObject howToPlayPanel;
    public GameObject optionsPanel;

    private GameObject currentOpenPanel;
    private Button currentDisabledButton;
    private GameObject currentHiddenBackground;

    void Start()
    {
        // Hide all panels
        mainPanel.SetActive(false);
        playPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        optionsPanel.SetActive(false);

        // Ensure all backgrounds are active at start
        playBackground.SetActive(true);
        howToPlayBackground.SetActive(true);
        optionsBackground.SetActive(true);

        // Button listeners
        playButton.onClick.AddListener(() => OpenPanel(playPanel, playButton, playBackground));
        howToPlayButton.onClick.AddListener(() => OpenPanel(howToPlayPanel, howToPlayButton, howToPlayBackground));
        optionsButton.onClick.AddListener(() => OpenPanel(optionsPanel, optionsButton, optionsBackground));

        closeButton.onClick.AddListener(CloseCurrentPanel);
    }

    void OpenPanel(GameObject panelToOpen, Button buttonToDisable, GameObject backgroundToHide)
    {
        // Show main panel if not visible
        if (!mainPanel.activeSelf)
            mainPanel.SetActive(true);

        // Close current panel if another is open
        if (currentOpenPanel != null && currentOpenPanel != panelToOpen)
            currentOpenPanel.SetActive(false);

        // Re-enable previous button
        if (currentDisabledButton != null && currentDisabledButton != buttonToDisable)
            currentDisabledButton.interactable = true;

        // Re-enable previous background
        if (currentHiddenBackground != null && currentHiddenBackground != backgroundToHide)
            currentHiddenBackground.SetActive(true);

        // Assign new values
        currentOpenPanel = panelToOpen;
        currentDisabledButton = buttonToDisable;
        currentHiddenBackground = backgroundToHide;

        // Show new panel, disable button and hide background
        currentOpenPanel.SetActive(true);
        currentDisabledButton.interactable = false;
        currentHiddenBackground.SetActive(false);
    }

    void CloseCurrentPanel()
    {
        if (currentOpenPanel != null)
        {
            currentOpenPanel.SetActive(false);
            currentOpenPanel = null;
        }

        if (currentDisabledButton != null)
        {
            currentDisabledButton.interactable = true;
            currentDisabledButton = null;
        }

        if (currentHiddenBackground != null)
        {
            currentHiddenBackground.SetActive(true);
            currentHiddenBackground = null;
        }

        mainPanel.SetActive(false);
    }
}
