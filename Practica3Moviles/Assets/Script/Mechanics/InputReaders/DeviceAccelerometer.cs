using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeviceAccelerometer
{
    public static bool hasAcceletometer
    {
        get { return SystemInfo.supportsAccelerometer; }
    }

    public static Quaternion Get()
    {
        return hasAcceletometer ? ReadAccelerometer() : Quaternion.identity;
    }

    private static Quaternion ReadAccelerometer()
    {
        return new Quaternion(Input.acceleration.x, Input.acceleration.y, -0.5f, 0.5f);
    }
}
