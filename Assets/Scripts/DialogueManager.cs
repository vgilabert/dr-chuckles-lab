using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    
    public List<string> ingredientDroppedPhrases;
    public List<string> ingredientAddedPhrases;

    [Range(0.0f, 1.0f)]
    public float frequency;

    [SerializeField]
    GameObject dialogueBox;
    [SerializeField]
    float dialogueDuration = 2f;

    public float delay = 0.05f;
    private string currentText = "";

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
        if(Random.Range(0f, 1f) < frequency)
        {
            List<string> dialogueList = GetDialogueList(dialogueType);
            dialogueBox.SetActive(true);


            if (dialogueList != null && dialogueList.Count > 0)
            {
                string randomPhrase = dialogueList[Random.Range(0, dialogueList.Count)];
                dialogueBox.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = randomPhrase;
                StartCoroutine(DisplayDialogue(randomPhrase));
                Debug.Log(randomPhrase);
            }
            return;
        }

    }

    IEnumerator DisplayDialogue(string fullText)
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            dialogueBox.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(dialogueDuration);
        dialogueBox.SetActive(false);
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
