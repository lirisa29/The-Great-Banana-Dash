using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    public float RotationPerSecond = 1;
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotationPerSecond);
    }
}
