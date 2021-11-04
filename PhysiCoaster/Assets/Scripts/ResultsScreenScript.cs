using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsScreenScript : MonoBehaviour
{
    public void ResultsScreenButtonClick()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
