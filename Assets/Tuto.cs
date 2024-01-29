using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tuto : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject ThrowPotionText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerPotionThrowInfos(bool active)
    {
        ThrowPotionText.SetActive(active);
    }
}
