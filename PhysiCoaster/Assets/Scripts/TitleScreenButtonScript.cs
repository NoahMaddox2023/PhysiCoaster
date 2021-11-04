using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenButtonScript : MonoBehaviour
{
    public Text xAxisText;
    public Text yAxisText;
    public Text xAxisDescriptionText;
    public Text yAxisDescriptionText;
    public Text graphDescriptionText;
    public Button nextButton;
    public Button titleScreenButton;
    public GameObject nextButtonGameObject;
    public GameObject titleScreenButtonGameObject;
    private int counter;

    public void Start()
    {
        xAxisText.GetComponent<Text>();
        yAxisText.GetComponent<Text>();
        xAxisDescriptionText.GetComponent<Text>();
        yAxisDescriptionText.GetComponent<Text>();
        graphDescriptionText.GetComponent<Text>();
        nextButton.GetComponent<Button>();
        titleScreenButton.GetComponent<Button>();
        titleScreenButtonGameObject.GetComponent<GameObject>();
        nextButtonGameObject.GetComponent<GameObject>();

        xAxisText.enabled = false;
        yAxisText.enabled = false;
        xAxisDescriptionText.enabled = false;
        yAxisDescriptionText.enabled = false;
        graphDescriptionText.enabled = false;
        titleScreenButton.enabled = false;
        titleScreenButtonGameObject.SetActive(false);
    }

    public void ButtonClick()
    {
        if (xAxisText.enabled == false && xAxisDescriptionText.enabled == false && yAxisText.enabled == false && yAxisDescriptionText.enabled == false && graphDescriptionText.enabled == false && nextButton.enabled == true && titleScreenButton.enabled == false)
        {
            xAxisText.enabled = true;
        }
        else if (xAxisText.enabled == true && xAxisDescriptionText.enabled == false && yAxisText.enabled == false && yAxisDescriptionText.enabled == false && graphDescriptionText.enabled == false && nextButton.enabled == true && titleScreenButton.enabled == false)
        {
            xAxisDescriptionText.enabled = true;
        }
        else if (xAxisText.enabled == true && xAxisDescriptionText.enabled == true && yAxisText.enabled == false && yAxisDescriptionText.enabled == false && graphDescriptionText.enabled == false && nextButton.enabled == true && titleScreenButton.enabled == false)
        {
            yAxisText.enabled = true;
        }
        else if (xAxisText.enabled == true && xAxisDescriptionText.enabled == true && yAxisText.enabled == true && yAxisDescriptionText.enabled == false && graphDescriptionText.enabled == false && nextButton.enabled == true && titleScreenButton.enabled == false)
        {
            yAxisDescriptionText.enabled = true;
            xAxisDescriptionText.enabled = false;
        }
        else if (xAxisText.enabled == true && xAxisDescriptionText.enabled == false && yAxisText.enabled == true && yAxisDescriptionText.enabled == true && graphDescriptionText.enabled == false && nextButton.enabled == true && titleScreenButton.enabled == false)
        {
            graphDescriptionText.enabled = true;
            yAxisDescriptionText.enabled = false;
            nextButton.enabled = false;
            nextButtonGameObject.SetActive(false);
            titleScreenButton.enabled = true;
            titleScreenButtonGameObject.SetActive(true);
        }
    }

    public void BackClick()
    {
        if (xAxisText.enabled == false && xAxisDescriptionText.enabled == false && yAxisText.enabled == false && yAxisDescriptionText.enabled == false && graphDescriptionText.enabled == false && nextButton.enabled == true && titleScreenButton.enabled == false)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else if (xAxisText.enabled == true && xAxisDescriptionText.enabled == false && yAxisText.enabled == false && yAxisDescriptionText.enabled == false && graphDescriptionText.enabled == false && nextButton.enabled == true && titleScreenButton.enabled == false)
        {
            xAxisText.enabled = false;
        }
        else if (xAxisText.enabled == true && xAxisDescriptionText.enabled == true && yAxisText.enabled == false && yAxisDescriptionText.enabled == false && graphDescriptionText.enabled == false && nextButton.enabled == true && titleScreenButton.enabled == false)
        {
            xAxisDescriptionText.enabled = false;
        }
        else if (xAxisText.enabled == true && xAxisDescriptionText.enabled == true && yAxisText.enabled == true && yAxisDescriptionText.enabled == false && graphDescriptionText.enabled == false && nextButton.enabled == true && titleScreenButton.enabled == false)
        {
            yAxisText.enabled = false;
        }
        else if (xAxisText.enabled == true && xAxisDescriptionText.enabled == false && yAxisText.enabled == true && yAxisDescriptionText.enabled == true && graphDescriptionText.enabled == false && nextButton.enabled == true && titleScreenButton.enabled == false)
        {
            yAxisDescriptionText.enabled = false;
            xAxisDescriptionText.enabled = true;
        }
        else if (xAxisText.enabled == true && xAxisDescriptionText.enabled == false && yAxisText.enabled == true && yAxisDescriptionText.enabled == false && graphDescriptionText.enabled == true && nextButton.enabled == false && titleScreenButton.enabled == true)
        {
            yAxisDescriptionText.enabled = true;
            graphDescriptionText.enabled = false;
            nextButton.enabled = true;
            nextButtonGameObject.SetActive(true);
            titleScreenButton.enabled = false;
            titleScreenButtonGameObject.SetActive(false);
        }
    }
}