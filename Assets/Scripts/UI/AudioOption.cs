using UnityEngine;
using UnityEngine.UI;
using UnityCore.Audio;

public class AudioOption : MonoBehaviour
{
    [SerializeField]
    Slider Music;
    [SerializeField]
    Slider SFX;

    // Start is called before the first frame update
    void Start()
    {
        Music.value = PlayerPrefs.GetFloat("Music");
        SFX.value = PlayerPrefs.GetFloat("SFX");
        AudioController.instance.UpdateSFX();
        AudioController.instance.UpdateMusic();
    }
    

    public void UpdateMusic(float s)
    {
        PlayerPrefs.SetFloat("Music", s);
        AudioController.instance.UpdateMusic();
    }
    
    public void UpdateSFX(float s)
    {        
        PlayerPrefs.SetFloat("SFX", s);
        AudioController.instance.UpdateSFX();
    }
}
