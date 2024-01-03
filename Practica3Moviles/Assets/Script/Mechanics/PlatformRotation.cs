using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlatformRotation : MonoBehaviour
{
    //[SerializeField] TextMeshPro text1;
    //[SerializeField] TextMeshPro text2;
    //[SerializeField] TextMeshPro text3;
    //[SerializeField] TextMeshPro text4;
    [SerializeField] bool useGyro;
    [SerializeField] bool useUnbiasedRotation;
    [SerializeField] bool useAccelerometer;
    [SerializeField] float AccelerationThreshold = 0.05f;

    [SerializeField] float speed=5;
    Vector3 localRotation;
    private float yRotation;
    private float xRotation;

    void Start()
    {
        Input.gyro.enabled = true;
        Debug.Log(DeviceRotation.hasGyroscope);
        //text1.text = $"Gyro: {DeviceRotation.hasGyroscope}";
        //text2.text = $"Accelerometer: {DeviceAccelerometer.hasAcceletometer}";
        localRotation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
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
                //text3.text = deviceRotation.ToString();
            }
        }
        if (useAccelerometer) 
        {
            //Quaternion deviceAcceleration = DeviceAccelerometer.Get();
            //transform.rotation = deviceAcceleration;

            float curSpeed = Time.deltaTime * speed;

            //text3.text = $"{Input.acceleration}";
            //text4.text = $"{transform.rotation}";

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

            localRotation.z = Mathf.Clamp(localRotation.z,-1f,1f);
            localRotation.x = Mathf.Clamp(localRotation.x,-1f,1f);

            // then rotate this object accordingly to the new angle
            transform.Rotate(localRotation);
        }
    }

    public void ChangeMaterial(PhysicMaterial physicMaterial, Material material) 
    {
        Collider collider = GetComponent<Collider>();
        collider.material = physicMaterial;

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = material;
    }
}
