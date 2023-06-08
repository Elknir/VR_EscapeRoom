using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.Oculus;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.XR;


public class OculusDetection : MonoBehaviour
{

    public InputDevice hmd;
    // Start is called before the first frame update
    void Start()
    {
        InitializeInputDevice(InputDeviceCharacteristics.HeadMounted, ref hmd);
            
            
        Debug.Log($"HEADSET {hmd}" );
    }
    
    void InitializeInputDevices(){}

    void InitializeInputDevice(InputDeviceCharacteristics inputCharacteristics, ref InputDevice inputDevice)
    {
        List<InputDevice> devices = new List<InputDevice>();
        
        InputDevices.GetDevicesWithCharacteristics(inputCharacteristics,devices);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}


