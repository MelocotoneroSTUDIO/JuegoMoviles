using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsBehaviour : MonoBehaviour
{
    [SerializeField] Button gyroButton;
    [SerializeField] Button accelerometerButton;
    [SerializeField] Button unbiasedRotationButton;

    [SerializeField] Color SelectedColor;
    [SerializeField] Color UnselectedColor;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopGame() 
    {
        Time.timeScale = 0;
    }

    public void ResumeGame() 
    {
        Time.timeScale =  1;
    }

    public void UseGyro() 
    {
        Settings.useGyro = true;
        Settings.useAccelerometer = false;

        gyroButton.image.color = SelectedColor;
        accelerometerButton.image.color = UnselectedColor;
        unbiasedRotationButton.interactable = true;

        if (Settings.useUnbiasedRotation)
        {
            unbiasedRotationButton.image.color = SelectedColor;
        }
        else 
        {
            unbiasedRotationButton.image.color = UnselectedColor;
        }
    }

    public void UseAccelerometer() 
    {
        Debug.Log("Use accelerometer");

        Settings.useGyro = false;
        Settings.useAccelerometer = true;

        gyroButton.image.color = UnselectedColor;
        accelerometerButton.image.color = SelectedColor;
        unbiasedRotationButton.image.color = UnselectedColor;
        unbiasedRotationButton.interactable = false;
    }

    public void UseUnbiasedRotation() 
    {
        if (Settings.useUnbiasedRotation)
        {
            Settings.useUnbiasedRotation = false;
            unbiasedRotationButton.image.color = UnselectedColor;
        }
        else
        {
            Settings.useUnbiasedRotation = true;
            unbiasedRotationButton.image.color = SelectedColor;
        }
    }

    void Initialize() 
    {
        if (Settings.useGyro)
        {
            gyroButton.image.color = SelectedColor;
        }
        else
        {
            gyroButton.image.color = UnselectedColor;
        }

        if (Settings.useAccelerometer)
        {
            accelerometerButton.image.color = SelectedColor;
            unbiasedRotationButton.interactable = false;
        }
        else
        {
            accelerometerButton.image.color = UnselectedColor;
        }


        if (Settings.useUnbiasedRotation)
        {
            unbiasedRotationButton.image.color = SelectedColor;
        }
        else
        {
            unbiasedRotationButton.image.color = UnselectedColor;
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
