using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    
    public List<string> ingredientDroppedPhrases;
    public List<string> ingredientAddedPhrases;

    [SerializeField]
    GameObject dialogueBox;
    
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        ingredientDroppedPhrases = new List<string>()
        {
            "Do you even know how much that costs?!",
            "Don't expect me to clean your mess!",
            "What was that?!"
        };
        ingredientAddedPhrases = new List<string>()
        {
            "What are you trying to do?",
            "You don't know what you're doing, do you?",
            "I'm not sure that's a good idea...",
            "AAAAAAAAAH!!! ...Oh wait, that's not so bad."
        };
    }
    
    public void TriggerDialogue(DialogueType dialogueType)
    {
        List<string> dialogueList = GetDialogueList(dialogueType);
        dialogueBox.SetActive(true);

        if (dialogueList != null && dialogueList.Count > 0)
        {
            string randomPhrase = dialogueList[Random.Range(0, dialogueList.Count)];
            Debug.Log(randomPhrase);
        }
    }

    private List<string> GetDialogueList(DialogueType dialogueType)
    {
        switch (dialogueType)
        {
            case DialogueType.IngredientDropped:
                return ingredientDroppedPhrases;
            case DialogueType.IngredientAdded:
                return ingredientAddedPhrases;
            default:
                return null;
        }
    }
}

public enum DialogueType
{
    IngredientDropped,
    IngredientAdded,
    PotionCreated,
    Help,
}
