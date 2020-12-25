using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextFadeController : MonoBehaviour
{
    public string nextSceneName;
    public List<GameObject> textList;

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(nextSceneName);
        }

        foreach(GameObject text in textList)
        {
            TextFade textFade = text.GetComponent<TextFade>();
            if(!textFade.done)
            {
                text.SetActive(true);
                return;
            }
        }

        if(textList[textList.Count-1].GetComponent<TextFade>().done)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
