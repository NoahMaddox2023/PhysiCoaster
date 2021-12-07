using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalLevelCheck : MonoBehaviour
{
    public Button nextLevelButton;
    public GameObject nextLevelButtonGameObject;
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("NextLevel"));
        if (PlayerPrefs.GetInt("NextLevel") > 14)
        {
            nextLevelButton.enabled = false;
            nextLevelButtonGameObject.SetActive(false);
        }
    }
}
