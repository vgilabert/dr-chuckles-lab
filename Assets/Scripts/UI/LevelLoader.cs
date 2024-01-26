using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public float transitionTime = 1;
    public Animator animator;
    [SerializeField]
    private GameObject background;

    private void Awake()
    {
        if(background != null)
            background.SetActive(true);
    }
    public IEnumerator LoadLevel(int index)
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);

    }
}
