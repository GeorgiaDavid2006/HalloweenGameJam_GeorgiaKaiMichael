using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public UIManager uiManager;

    public static GameManager instance;

    void Start()
    {
        instance = this;
    }

    
    void Update()
    {
        
    }
}
