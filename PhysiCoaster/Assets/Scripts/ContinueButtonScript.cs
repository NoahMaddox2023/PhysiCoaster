using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButtonScript : MonoBehaviour
{
    public void ContinueButton()
    {
        SceneManager.LoadScene("Tutorial2");
    }
}
