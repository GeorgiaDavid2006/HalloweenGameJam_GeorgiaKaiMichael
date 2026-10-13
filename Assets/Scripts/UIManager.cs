using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    public TMP_Text promptText;
    public string prompt;

    public float messageDuration = 3.0f;
    public float fadeOutTime = 0.5f;

    public TMP_Text messageText;
    private Coroutine messageFadeCoroutine;

    private void Awake()
    {
        DisplayMessage("");
    }

    public void ShowPrompt()
    {
        promptText.text = prompt;
        ClearMessage();
    }

    public void HidePrompt()
    {
        promptText.text = " ";
    }

    public void DisplayMessage(string message)
    {

        if (messageFadeCoroutine != null)
        {
            StopCoroutine(messageFadeCoroutine);
        }

        messageFadeCoroutine = StartCoroutine(DisplayMessageAndFade(message));
    }

    public void ClearMessage()
    {
        if (messageFadeCoroutine != null)
        {
            StopCoroutine(messageFadeCoroutine);
        }

        messageText.text = "";
    }

    private IEnumerator DisplayMessageAndFade(string message)
    {
        messageText.text = message;
        messageText.alpha = 1;

        float elapsedTime = 0f;

        yield return new WaitForSeconds(messageDuration);

        Color originalColor = messageText.color;

        while (elapsedTime < messageDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutTime);

            messageText.alpha = alpha;

            yield return null;
        }
        messageText.text = "";
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
}
