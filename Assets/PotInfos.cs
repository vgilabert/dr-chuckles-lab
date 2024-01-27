using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotInfos : MonoBehaviour
{

    [SerializeField]
    GameObject magical;
    [SerializeField]
    GameObject ordinary;
    [SerializeField]
    GameObject special;
    [SerializeField]
    Button clearButton;

    void Start()
    {
        
    }

    public void UpdateUI(ElementType type)
    {
        switch (type)
        {
            case ElementType.Magical:
                magical.SetActive(true);
                break;
            case ElementType.Ordinary:
                ordinary.SetActive(true);
                break;
            case ElementType.Special:
                special.SetActive(true);
                break;
        }
        clearButton.interactable = true;
    }

    public void ResetUI()
    {
        magical.SetActive(false);
        ordinary.SetActive(false);
        special.SetActive(false);
        clearButton.interactable = false;
    }


}
