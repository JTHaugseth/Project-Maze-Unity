using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class cameraSwitcher : MonoBehaviour
{
    
    public  CinemachineVirtualCamera currentCamera;
    public CinemachineVirtualCamera menuCam;
   
    // adds prority to menucam so this cam is 1 higher the the others 
    public void Start()
    {
        menuCam = currentCamera;
        currentCamera.Priority++;
    }
    // switch camera from current to target
    public void UpdateCamera(CinemachineVirtualCamera target)
    {
        
        currentCamera.Priority--;

        currentCamera = target;

        currentCamera.Priority++;
        Debug.Log("Button clicked");

       
    
    }
}