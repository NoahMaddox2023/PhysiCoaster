using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsScreenScript : MonoBehaviour
{
    public void Level1ResultScreen()
    {
        SceneManager.LoadScene("Level1ResultsScreen");
    }
    public void Level2ResultScreen()
    {
        SceneManager.LoadScene("Level2ResultsScreen");
    }
    public void Level3ResultScreen()
    {
        SceneManager.LoadScene("Level3ResultsScreen");
    }
}
