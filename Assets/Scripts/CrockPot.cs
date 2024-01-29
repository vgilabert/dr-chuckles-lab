using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityCore.Audio;
using UnityEngine;

public class CrockPot : MonoBehaviour
{

    private List<Element> elements;

    private bool isFull = false;
    private bool hasMagical = false;
    private bool hasOrdinary = false;
    private bool hasSpecial = false;
    private bool isCreatingPotion = false;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip soundFX;
    [SerializeField]
    private PotInfos potInfos;
    [SerializeField]
    GameObject addIngredientVFX;

    public Vector3 potionSpawnOffset;

    void Start()
    {
        elements = new List<Element>();
        audioSource.clip = soundFX;
        audioSource.Play();

        GameObject.Find("PotionHolder").transform.position = transform.position + potionSpawnOffset;
    }

    void Update()
    {
        if (audioSource != null)
        {
            audioSource.volume = PlayerPrefs.GetFloat("SFX");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Element element = other.GetComponent<Element>();
        GrabObject grabObject = other.GetComponent<GrabObject>();
        if(element != null && !isCreatingPotion)
        {
            if (elements.Contains(element))
            {
                return;
            }

            if(grabObject.isGrabbed == false && !isFull)
            {
                bool isValid = false;
                switch (element.element.elementType)
                {
                    case ElementType.Magical:
                        if(hasMagical == false)
                        {
                            hasMagical = true;
                            isValid = true;
                            elements.Add(element);
                            potInfos.UpdateUI(ElementType.Magical);
                        }
                        break;
                    case ElementType.Ordinary:
                        if(hasOrdinary == false)
                        {
                            hasOrdinary = true;
                            isValid = true;
                            elements.Add(element);
                            potInfos.UpdateUI(ElementType.Ordinary);
                        }
                        break;
                    case ElementType.Special:
                        if(hasSpecial == false)
                        {
                            hasSpecial = true;
                            isValid = true;
                            elements.Add(element);
                            potInfos.UpdateUI(ElementType.Special);
                        }
                        break;

                }

                if (isValid)
                {
                    Destroy(Instantiate(addIngredientVFX, transform.position, Quaternion.identity),1f);
                    grabObject.isInPot = true;
                    grabObject.isGrabable = false;
                    DialogueManager.Instance.TriggerDialogue(DialogueType.IngredientAdded);
                    AudioController.Instance.PlayAudio(UnityCore.Audio.AudioType.SFX_Ingredient);
                }
                else grabObject.Respawn();
            }

            if(hasSpecial && hasMagical && hasOrdinary)
            {
                isFull = true;
                CheckIngredients();
            }
        }
    }

    private void CheckIngredients()
    {
        Debug.Log("Check potion ingredients");
        var potions = GameManager.Instance.potions;
        var ingredients = elements
            .OrderBy(i => i.element.elementName)
            .Select(i => i.element.elementName)
            .ToList();

        for (int i = 0; i < potions.Count; ++i)
        {
            var potionIngredients = potions[i].ingredients
                .OrderBy(ingredient => ingredient.elementName)
                .Select(ingredient => ingredient.elementName)
                .ToList();

            if (ingredients.SequenceEqual(potionIngredients))
            {
                Debug.Log(potions[i].potionName);
                StartCoroutine(CreatePotion(potions[i]));
                break;
            }
        }
    }

    public void Clear()
    {
        isFull = false;
        hasMagical = false;
        hasOrdinary = false;
        hasSpecial = false;
        isCreatingPotion = false;

        foreach (Element element in elements)
        {
            Debug.Log(element.name);
            element.GetComponent<GrabObject>().Respawn();
        }
        elements = new List<Element>();
        potInfos.ResetUI();
    }

    IEnumerator CreatePotion(PotionObject potionObj)
    {
        yield return new WaitForSeconds(.2f);
        Instantiate(potionObj.resultObject, transform.position + potionSpawnOffset, Quaternion.identity);

        Clear();
        isCreatingPotion = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + potionSpawnOffset, 0.2f);
    }
}
