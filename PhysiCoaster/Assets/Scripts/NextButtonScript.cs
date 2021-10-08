using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButtonScript : MonoBehaviour
{
    public Text xAxisText;
    public Text yAxisText;
    public Text xAxisDescriptionText;
    public Text yAxisDescriptionText;
    public Button nextButton;
    public Button continueButton;
    public GameObject nextButtonGameObject; 
    public GameObject continueButtonGameObject;
    private int counter; 

    public void Start()
    {
        xAxisText.GetComponent<Text>();
        yAxisText.GetComponent<Text>();
        xAxisDescriptionText.GetComponent<Text>();
        yAxisDescriptionText.GetComponent<Text>();
        nextButton.GetComponent<Button>();
        continueButton.GetComponent<Button>();
        continueButtonGameObject.GetComponent<GameObject>();
        nextButtonGameObject.GetComponent<GameObject>();

        xAxisText.enabled = false;
        yAxisText.enabled = false;
        xAxisDescriptionText.enabled = false;
        yAxisDescriptionText.enabled = false;
        continueButton.enabled = false;
        continueButtonGameObject.SetActive(false);
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
                nextButton.enabled = false;
                nextButtonGameObject.SetActive(false);
                continueButton.enabled = true;
                continueButtonGameObject.SetActive(true);
            }
        }
    }
}