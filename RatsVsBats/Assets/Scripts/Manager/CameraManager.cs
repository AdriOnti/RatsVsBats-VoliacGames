using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform m2_bossCamera;
    public Transform m3_jailCamera;
    public Transform m4_doorCamera;

    public enum Cameras
    {
        m2_bossCamera,
        m3_jailCamera,
        m4_doorCamera
    }

    private Dictionary<CameraType, Transform> cameraDictionary;

    // Start is called before the first frame update
    void Start()
    {
        //cameraDictionary = new Dictionary<Cameras, Transform>
        //{
        //    { CameraType.M2_bossCamera, m2_bossCamera },
        //    { CameraType.M3_JailCamera, m3_jailCamera },
        //    { CameraType.M4_DoorCamera, m4_doorCamera }
        //};
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCamera(string camName)
    {
        
    }
}
