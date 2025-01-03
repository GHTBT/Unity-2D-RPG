using Unity.Cinemachine;
using UnityEngine;

public class ScreenShakeManagement : Singleton<ScreenShakeManagement>
{
    private CinemachineImpulseSource source;

    protected override void Awake()
    {
        base.Awake();
        source = GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeScreen()
    {
        source.GenerateImpulse();
    }
}