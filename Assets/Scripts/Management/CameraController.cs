using UnityEngine;
using Unity.Cinemachine;
using System.ComponentModel;

public class CameraController : Singleton<CameraController>
{
    private CinemachineCamera cinemachineCamera;
    public void SetPlayerCameraFollow()
    {
        cinemachineCamera = FindFirstObjectByType<CinemachineCamera>();
        cinemachineCamera.Follow= Player_Controller.Instance.transform;
    }   
}
