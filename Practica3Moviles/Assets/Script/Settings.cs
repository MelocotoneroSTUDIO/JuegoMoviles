using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    public static bool useGyro = true;
    public static bool useAccelerometer = false;
    public static bool useUnbiasedRotation = false;

    public static float MasterVolume = 1;
    public static float MusicVolume = 1;
    public static float SFXVolume = 1;

    public static void LoadSettings() 
    {
        useGyro = PlayerPrefs.GetInt("Gyro")==1? true:false;
        useAccelerometer = PlayerPrefs.GetInt("Accelerometer") == 1 ? true : false;
        useUnbiasedRotation = PlayerPrefs.GetInt("UnbiasedRot") == 1 ? true : false;

        MasterVolume = PlayerPrefs.GetFloat("MasterVolume");
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
    }

    public static void SaveSettings() 
    {
        PlayerPrefs.SetFloat("MasterVolume", MasterVolume);
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        PlayerPrefs.SetFloat("SFXVolume", SFXVolume);

        if (useGyro)
        {
            PlayerPrefs.SetInt("Gyro", 1);
        }
        else 
        {
            PlayerPrefs.SetInt("Gyro", 0);
        }

        if (useAccelerometer)
        {
            PlayerPrefs.SetInt("Accelerometer", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Accelerometer", 0);
        }

        if (useUnbiasedRotation)
        {
            PlayerPrefs.SetInt("UnbiasedRot", 1);
        }
        else
        {
            PlayerPrefs.SetInt("UnbiasedRot", 0);
        }
    }
}
