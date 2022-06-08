using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public float FoVAttenRate = 3.0f; // FoVの減衰比率
    public float MovedFoV = 65.0f; // プレイヤーが移動している時のFoV
    public float FoV = 50.0f; // プレイヤーが立ち止まっている時のFoV

    public CinemachineVirtualCamera vCamera;

    void Update ()
    {
        var fov = Grapple.grapple ? MovedFoV : FoV;
        vCamera.m_Lens.FieldOfView = Mathf.Lerp(vCamera.m_Lens.FieldOfView, fov, Time.deltaTime * FoVAttenRate);
    }
}