using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    
    public List<string> ingredientDroppedPhrases;
    public List<string> ingredientAddedPhrases;

    public List<string> potionCreatedPhrases;
    public List<string> potionDroppedPhrases;
    public List<string> potionExplodedPhrases;

    private int lastIngredientDroppedIndex;
    private int lastIngredientAddedIndex;
    private int lastPotionCreatedIndex;
    private int lastPotionDroppedIndex;
    private int index;

    [SerializeField]
    bool isDialogActive = false;

    [Range(0.0f, 1.0f)]
    public float frequency;
    private float currentFrequency;

    [SerializeField]
    private float timeBetweenDialog;
    [SerializeField]
    private bool couldDown;

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
    
    
    public void TriggerDialogue(DialogueType dialogueType)
    {
        if(Random.Range(0f, 1f) < frequency)
        {
            List<string> dialogueList = GetDialogueList(dialogueType);


            if (dialogueList != null && dialogueList.Count > 0 && !isDialogActive && !couldDown)
            {
                string randomPhrase = ChooseRandomPhrase(dialogueList, dialogueType);
                StartCoroutine(DisplayDialogue(randomPhrase));
            }
            return;
        }

    }

    string ChooseRandomPhrase(List<string> dialogueList,DialogueType dialogueType)
    {
        int choosenIndex = Random.Range(0, dialogueList.Count);
        while(choosenIndex == index)
        {
            choosenIndex = Random.Range(0, dialogueList.Count);
        }
        string randomPhrase = dialogueList[choosenIndex];

        Debug.Log(dialogueType + " event triggered the choosen phrase " +dialogueList[choosenIndex] +" at index  " +choosenIndex + " from " + dialogueList);

        switch (dialogueType)
        {
            case DialogueType.IngredientDropped:
                lastIngredientDroppedIndex = choosenIndex;
                break;
            case DialogueType.IngredientAdded:
                lastIngredientAddedIndex = choosenIndex;
                break;
            case DialogueType.PotionCreated:
                lastPotionCreatedIndex = choosenIndex;
                break;
            case DialogueType.PotionDropped:
                lastPotionDroppedIndex = choosenIndex;
                break;
        }
        return randomPhrase;
    }
    IEnumerator DisplayDialogue(string fullText)
    {
        Debug.Log(fullText);

        dialogueBox.SetActive(true);
        dialogueBox.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "";
        isDialogActive = true;
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            dialogueBox.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(dialogueDuration);
        dialogueBox.SetActive(false);
        isDialogActive = false;
        couldDown = true;
        StartCoroutine(dialogCouldDown());
    }

    IEnumerator dialogCouldDown()
    {
        yield return new WaitForSeconds(timeBetweenDialog);
        couldDown = false;
    }
    public void SetPotionExplodedPhrases(List<string> phrases)
    {
        if(phrases.Count>0)
            potionExplodedPhrases = phrases;
    }

    private List<string> GetDialogueList(DialogueType dialogueType)
    {
        switch (dialogueType)
        {
            case DialogueType.IngredientDropped:
                index = lastIngredientDroppedIndex;
                return ingredientDroppedPhrases;
            case DialogueType.IngredientAdded:
                index = lastIngredientAddedIndex;
                return ingredientAddedPhrases;
            case DialogueType.PotionCreated:
                index = lastPotionCreatedIndex;
                return potionCreatedPhrases;
            case DialogueType.PotionDropped:
                index = lastPotionDroppedIndex;
                return potionDroppedPhrases;
            case DialogueType.PotionExploded:
                index = -1;
                return potionExplodedPhrases;

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
    PotionDropped,
    PotionExploded,
    Help
}
