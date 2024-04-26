using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Cameras
{
    m1_mainCamera,
    m2_bossCamera,
    m3_jailCamera,
    m4_doorCamera
}

public class CameraManager : MonoBehaviour
{
    public Camera m1_mainCamera;
    public Camera m2_bossCamera;
    public Camera m3_jailCamera;
    public Camera m4_doorCamera;

    private Camera currentCamera;

    private readonly Dictionary<Cameras, Camera> cameraDictionary = new Dictionary<Cameras, Camera>();
    public static CameraManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        currentCamera = Camera.main;

        cameraDictionary.Add(Cameras.m1_mainCamera, m1_mainCamera);
        cameraDictionary.Add(Cameras.m2_bossCamera, m2_bossCamera);
        cameraDictionary.Add(Cameras.m3_jailCamera, m3_jailCamera);
        cameraDictionary.Add(Cameras.m4_doorCamera, m4_doorCamera);
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
