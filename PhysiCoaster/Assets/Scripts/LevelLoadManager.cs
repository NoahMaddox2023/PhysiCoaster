using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LevelLoadManager : MonoBehaviour
{
    public GameObject[] levelButtons;
    private bool[] canPlay = new bool[10];
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            using (StreamReader sr = new StreamReader(Application.streamingAssetsPath + "/SavedLevels.txt"))
            {
                string[] allBools = sr.ReadToEnd().Split('\n');
                for (int i = 0; i < 10; i++)
                {
                    canPlay[i] = Convert.ToBoolean(allBools[i]);
                }
                sr.Close();
            }
        }
        catch (FileNotFoundException fnfe)
        {
            using (StreamWriter sw = new StreamWriter(Application.streamingAssetsPath + "/SavedLevels.txt"))
            {
                sw.Write("True\nFalse\nFalse\nFalse\nFalse\nFalse\nFalse\nFalse\nFalse\nFalse");
                sw.Close();
            }
            for (int i = 0; i < 10; i++)
            {
                canPlay[i] = false;
            }
        }
        for (int i = 0; i < 10; i++)
        {
            if (!canPlay[i])
            {
                levelButtons[i].SetActive(false);
            }
        }
    }
}
