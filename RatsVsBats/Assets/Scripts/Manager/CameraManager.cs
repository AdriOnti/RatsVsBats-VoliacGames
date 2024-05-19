using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Cameras
{
    MainCamera,
    StartKeyCamera,
    JailCamera,
    LabCamera,
    BossCamera,
    BossCamera2,
    ExtraCamera
}

public class CameraManager : MonoBehaviour
{
    public List<Camera> cameras;
    //public Camera m2_bossCamera;
    //public Camera m3_jailCamera;
    //public Camera m4_doorCamera;

    private Camera currentCamera;
    //public CamerasContainer camerasDatabase;

    private readonly Dictionary<Cameras, Camera> cameraDictionary = new Dictionary<Cameras, Camera>();
    public static CameraManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        currentCamera = Camera.main;

        bool sucess;
        foreach (Camera cam in cameras)
        {
            if (sucess = Enum.TryParse(cam.name, out Cameras value))
                cameraDictionary.Add(value, cam);
            else Debug.Log(cam.name);
        }
        cameraDictionary.Add(Cameras.MainCamera, Camera.main);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCamera(Cameras camera)
    {
        if (cameraDictionary.TryGetValue(camera, out Camera desiredCamera))
        {
            currentCamera.enabled = false;
            desiredCamera.enabled = true;
            currentCamera = desiredCamera;
        }
    }
}
