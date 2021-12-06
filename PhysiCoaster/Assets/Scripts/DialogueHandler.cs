using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    public string[] sentences;
    public string nameOfSpeaker;
    public Text mainText, nameText;
    public float timeBetweenLetters;
    public float timeToFadeIn, timeToFadeOut;
    public GameObject[] thingsToFadeInOut;
    public GameObject[] thingsToTurnOnOff;
    public GameObject grid;
    [HideInInspector]
    public int sentenceCount = 0, letterCount = 0;
    private bool nextSentence = false;
    private bool isDone = false;
    private bool endOfSentence = false;
    private bool isReadyToStartText = false;
    private bool allowedToStartAgain = true;
    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0;
        StartFadeIn();
        nameText.text = nameOfSpeaker;
        mainText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Grid script is currently: " + grid.GetComponent<GridPlacement>().enabled);
        if (allowedToStartAgain)
        {
            if (Input.anyKeyDown)
            {
                isReadyToStartText = true;
                allowedToStartAgain = false;
            }
        }
        if (isReadyToStartText)
        {
            isReadyToStartText = false;
            StartCoroutine(MakeText(timeBetweenLetters));
        }
        
        if (endOfSentence)
        {
            if (Input.anyKeyDown)
            {
                Debug.Log("End of sentence and a key was pressed");
                nextSentence = true;
            }
        }
        if (isDone)
        {
            StartFadeOut();
            isDone = false;
        }
        else
        {
            grid.GetComponent<GridPlacement>().enabled = false;
        }
        //if (sentenceCount < sentences.Length && letterCount == sentences[sentenceCount].Length)
        //{
        //    if (Input.anyKeyDown)
        //    {
        //        Debug.Log("End of sentence and a key was pressed");
        //        nextSentence = true;
        //    }
        //}
        //if (!isDone)
        //{
        //    StartCoroutine(PrintText(timeBetweenLetters));
        //}
    }

    //public IEnumerator PrintText(float timeBetweenLetters)
    //{
    //    if (sentenceCount < sentences.Length)
    //    {
    //        currentSentence = sentences[sentenceCount];
    //        if (letterCount < currentSentence.Length)
    //        {
    //            currentPrintedSentence = currentSentence.Substring(0, letterCount+1);
    //            mainText.text = currentPrintedSentence;
    //            letterCount++;
    //        }
    //        else if (nextSentence)
    //        {
    //            Debug.Log("Starting next sentence");
    //            letterCount = 0;
    //            sentenceCount++;
    //            nextSentence = false;
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("Hit last sentence");
    //        isDone = true;
    //    }
    //    yield return new WaitForSeconds(timeBetweenLetters);
    //}

    public IEnumerator MakeText(float timeBetweenLetters)
    {
        for (int i = 0; i < sentences.Length; i++)
        {
            string sentence = sentences[i];
            for (int j = 0; j <= sentence.Length; j++)
            {
                //Debug.Log("Flag 1 hit");
                mainText.text = sentence.Substring(0, j);
                if (j != sentence.Length)
                {
                    yield return new WaitForSeconds(timeBetweenLetters);
                }
                else
                {
                    endOfSentence = true;
                }
                nextSentence = false;
            }
            while (!nextSentence)
            {
                //Debug.Log("Flag 2 hit");
                yield return null;
            }
            endOfSentence = false;
        }
        isDone = true;
    }

    public void StartFadeIn()
    {
        //foreach (GameObject g in thingsToFadeInOut)
        //{
        //    Color c;
        //    bool isImage = true;
        //    try
        //    {
        //        c = g.GetComponent<Image>().color;
        //    }
        //    catch
        //    {
        //        c = g.GetComponent<Text>().color;
        //        isImage = false;
        //    }
        //    //c.a = 0;
        //    if (isImage)
        //    {
        //        g.GetComponent<Image>().color = c;
        //    }
        //    else
        //    {
        //        g.GetComponent<Text>().color = c;
        //    }
        //    Debug.Log("Completed changing colors' alpha to 0");
        //    StartCoroutine(FadeIn(timeToFadeIn, g));
        //}
        //Debug.Log("Grid script before: " + grid.GetComponent<GridPlacement>().enabled);
        grid.GetComponent<GridPlacement>().enabled = false;
        //Debug.Log("Grid script after: " + grid.GetComponent<GridPlacement>().enabled);
        foreach (GameObject go in thingsToTurnOnOff)
        {
            go.SetActive(false);
        }
        StartCoroutine(FadeIn(timeToFadeIn, thingsToFadeInOut[2]));
    }
    public void StartFadeOut()
    {
        //foreach (GameObject g in thingsToFadeInOut)
        //{
        //    StartCoroutine(FadeIn(timeToFadeOut, g));
        //}
        StartCoroutine(FadeOut(timeToFadeOut, thingsToFadeInOut[2]));
    }

    public IEnumerator FadeIn(float decayTime, GameObject objToFade)
    {
        Color c;
        bool isImage = true;
        float temp;
        try
        {
            temp = 1 / decayTime;
        }
        catch
        {
            temp = Time.smoothDeltaTime * Application.targetFrameRate-1;
        }
        try
        {
            c = objToFade.GetComponent<Image>().color;
        }
        catch
        {
            c = objToFade.GetComponent<Text>().color;
            isImage = false;
        }
        for (float alpha = 0; alpha <= 1; alpha += temp * Time.smoothDeltaTime)
        {
            //Debug.Log("Current alpha: " + alpha);
            //c.a = alpha;
            if (isImage)
            {
                objToFade.GetComponent<Image>().material.color = new Color (c.r, c.g, c.b, alpha);
            }
            else
            {
                objToFade.GetComponent<Text>().material.color = new Color(c.r, c.g, c.b, alpha);
            }
            yield return null;
        }
        Debug.Log("Fading in is done");
    }

    public IEnumerator FadeOut(float revealTime, GameObject objToFade)
    {
        Color c;
        bool isImage = true;
        float temp;
        try
        {
            temp = 1 / revealTime;
        }
        catch
        {
            temp = Time.smoothDeltaTime * Application.targetFrameRate - 1;
        }
        try
        {
            c = objToFade.GetComponent<Image>().color;
        }
        catch
        {
            c = objToFade.GetComponent<Text>().color;
            isImage = false;
        }
        for (float alpha = 1; alpha >= 0; alpha -= temp * Time.smoothDeltaTime)
        {
            //c.a = alpha;
            if (isImage)
            {
                objToFade.GetComponent<Image>().material.color = new Color(c.r, c.g, c.b, alpha);
            }
            else
            {
                objToFade.GetComponent<Text>().material.color = new Color(c.r, c.g, c.b, alpha);
            }
            yield return null;
        }
        foreach (GameObject go in thingsToTurnOnOff)
        {
            go.SetActive(true);
        }
        grid.GetComponent<GridPlacement>().enabled = true;
        if (isImage)
        {
            objToFade.GetComponent<Image>().material.color = new Color(c.r, c.g, c.b, 1);
        }
        else
        {
            objToFade.GetComponent<Text>().material.color = new Color(c.r, c.g, c.b, 1);
        }
        gameObject.SetActive(false);
        //Time.timeScale = 1;
    }
}
