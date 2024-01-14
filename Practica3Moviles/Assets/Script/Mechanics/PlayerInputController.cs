using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] bool useGyro;
    [SerializeField] bool useUnbiasedRotation;
    [SerializeField] bool useAccelerometer;
    [SerializeField] float AccelerationThreshold = 0.05f;

    [SerializeField] float speed = 5;
    Vector3 localRotation;
    private float yRotation;
    private float xRotation;

    void Start()
    {
        Input.gyro.enabled = true;
        Debug.Log(DeviceRotation.hasGyroscope);
        //localRotation = transform.rotation.eulerAngles;
        UpdateControls();

        CalculateRotation();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateRotation();
    }

    void CalculateRotation()
    {
        if (useGyro)
        {
            if (useUnbiasedRotation)
            {
                yRotation += -Input.gyro.rotationRateUnbiased.y;
                xRotation += -Input.gyro.rotationRateUnbiased.x;

                transform.eulerAngles = new Vector3(xRotation, 0, yRotation);
            }
            else
            {
                Quaternion deviceRotation = DeviceRotation.Get();
                transform.rotation = deviceRotation;
            }
        }
        if (useAccelerometer)
        {
            //Quaternion deviceAcceleration = DeviceAccelerometer.Get();
            //transform.rotation = deviceAcceleration;

            float curSpeed = Time.deltaTime * speed;

            // first update the current rotation angles with input from acceleration axis

            float xAcceleration = Input.acceleration.x;
            float yAcceleration = Input.acceleration.y;

            float tempX = xAcceleration >= AccelerationThreshold || xAcceleration <= -AccelerationThreshold ? -xAcceleration * curSpeed : 0;
            float tempY = yAcceleration >= AccelerationThreshold || yAcceleration <= -AccelerationThreshold ? yAcceleration * curSpeed : 0;

            if (tempX == 0)
            {
                localRotation.z = 0;
            }
            localRotation.z += tempX;

            if (tempY == 0)
            {
                localRotation.x = 0;
            }
            localRotation.x += tempY;

            localRotation.z = Mathf.Clamp(localRotation.z, -1f, 1f);
            localRotation.x = Mathf.Clamp(localRotation.x, -1f, 1f);

            // then rotate this object accordingly to the new angle
            transform.Rotate(localRotation);
        }
    }

    public void UpdateControls()
    {
        useGyro = Settings.useGyro;
        useAccelerometer = Settings.useAccelerometer;
        useUnbiasedRotation = Settings.useUnbiasedRotation;
    }
}
