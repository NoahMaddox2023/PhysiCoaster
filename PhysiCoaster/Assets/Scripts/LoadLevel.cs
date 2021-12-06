using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("NextLevel"));
    }

    public void SceneSelect(int index)
    {
        SceneManager.LoadScene(index);
    }
}
