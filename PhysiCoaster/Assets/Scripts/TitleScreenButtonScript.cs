using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            counter++;
            if (counter == 1)
            {
                xAxisText.enabled = true;
            }
            else if (counter == 2)
            {
                xAxisDescriptionText.enabled = true;
            }
            else if (counter == 3)
            {
                yAxisText.enabled = true;
            }
            else if (counter == 4)
            {
                yAxisDescriptionText.enabled = true;
            }
            else if (counter == 5)
            {
                graphDescriptionText.enabled = true;
                nextButton.enabled = false;
                nextButtonGameObject.SetActive(false);
                titleScreenButton.enabled = true;
                titleScreenButtonGameObject.SetActive(true);
            }
        }
    }
}