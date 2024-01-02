using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeviceRotation
{
    private static bool gyroInitialized = false;

    public static bool hasGyroscope
    {
        get { return SystemInfo.supportsGyroscope; }
    }

    public static Quaternion Get() 
    {
        if (!gyroInitialized) 
        {
            InitGyro();
        }
        return hasGyroscope? ReadGyroscopeRotation() : Quaternion.identity;
    }

    private static void InitGyro() 
    {
        if (hasGyroscope) 
        {
            Input.gyro.enabled=true;
            Debug.Log($"Gyro: {Input.gyro.userAcceleration}");
            Debug.Log($"Gyro: {Input.gyro.rotationRate}");
            Input.gyro.updateInterval = 0.0167f;
        }
        gyroInitialized = true;
    }

    private static Quaternion ReadGyroscopeRotation() 
    {
        return new Quaternion(0.5f,0.5f,-0.5f,0.5f) * Input.gyro.attitude * new Quaternion(0,0,1,0);
    }
}
