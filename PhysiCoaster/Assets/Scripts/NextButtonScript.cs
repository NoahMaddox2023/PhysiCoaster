using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    
    public void ButtonClick()
    { 
        if (xAxisText.enabled == false && xAxisDescriptionText.enabled == false && yAxisText.enabled == false && yAxisDescriptionText.enabled == false && nextButton.enabled == true && continueButton.enabled == false)
        {
            xAxisText.enabled = true;
        }
        else if (xAxisText.enabled == true && xAxisDescriptionText.enabled == false && yAxisText.enabled == false && yAxisDescriptionText.enabled == false && nextButton.enabled == true && continueButton.enabled == false)
        {
            xAxisDescriptionText.enabled = true;
        }
        else if (xAxisText.enabled == true && xAxisDescriptionText.enabled == true && yAxisText.enabled == false && yAxisDescriptionText.enabled == false && nextButton.enabled == true && continueButton.enabled == false)
        {
            yAxisText.enabled = true;
        }
        else if (xAxisText.enabled == true && xAxisDescriptionText.enabled == true && yAxisText.enabled == true && yAxisDescriptionText.enabled == false && nextButton.enabled == true && continueButton.enabled == false)
        {
            yAxisDescriptionText.enabled = true;
            xAxisDescriptionText.enabled = false;
            nextButton.enabled = false;
            nextButtonGameObject.SetActive(false);
            continueButton.enabled = true;
            continueButtonGameObject.SetActive(true);
        }        
    }
    public void BackClick()
    {
        if (xAxisText.enabled == false && xAxisDescriptionText.enabled == false && yAxisText.enabled == false && yAxisDescriptionText.enabled == false && nextButton.enabled == true && continueButton.enabled == false)
        {
            SceneManager.LoadScene("TitleScreen");
        }
        else if (xAxisText.enabled == true && xAxisDescriptionText.enabled == false && yAxisText.enabled == false && yAxisDescriptionText.enabled == false && nextButton.enabled == true && continueButton.enabled == false)
        {
            xAxisText.enabled = false;
        }
        else if (xAxisText.enabled == true && xAxisDescriptionText.enabled == true && yAxisText.enabled == false && yAxisDescriptionText.enabled == false && nextButton.enabled == true && continueButton.enabled == false)
        {
            xAxisDescriptionText.enabled = false;
        }
        else if (xAxisText.enabled == true && xAxisDescriptionText.enabled == true && yAxisText.enabled == true && yAxisDescriptionText.enabled == false && nextButton.enabled == true && continueButton.enabled == false)
        {
            yAxisText.enabled = false;
        }
        if (xAxisText.enabled == true && xAxisDescriptionText.enabled == false && yAxisText.enabled == true && yAxisDescriptionText.enabled == true && nextButton.enabled == false && continueButton.enabled == true)
        {
            yAxisDescriptionText.enabled = false;
            xAxisDescriptionText.enabled = true;
            yAxisText.enabled = true;
            nextButtonGameObject.SetActive(true);
            nextButton.enabled = true;
            continueButtonGameObject.SetActive(false);
            continueButton.enabled = false;        
        }
    }

}