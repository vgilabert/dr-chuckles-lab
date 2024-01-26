using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Rendering.FilterWindow;

public class PotInfos : MonoBehaviour
{

    [SerializeField]
    GameObject magical;
    [SerializeField]
    GameObject ordinary;
    [SerializeField]
    GameObject special;

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
    }

    public void ResetUI()
    {
        magical.SetActive(false);
        ordinary.SetActive(false);
        special.SetActive(false);
    }


}
