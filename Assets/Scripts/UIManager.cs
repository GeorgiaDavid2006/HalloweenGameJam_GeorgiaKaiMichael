using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;
    public GameObject mainMenuScreen;

    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    public TMP_Text promptText;
    public string prompt;

    public void ShowPrompt()
    {
        promptText.text = prompt;
    }

    public void HidePrompt()
    {
        promptText.text = " ";
    }

    public void ShowDialoguePanel()
    {
        dialoguePanel.SetActive(true);
    }

    public void HideDialoguePanel()
    {
        dialoguePanel.SetActive(false);
    }

    public void SetDialogueText(string dialogueString)
    {
        dialogueText.text = dialogueString;
    }

    public void StartGame()
    {
        mainMenuScreen.SetActive(false);
        player.SetActive(true);
        mainCamera.SetActive(true);
    }
}
