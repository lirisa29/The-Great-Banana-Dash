using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset; // Offset position of camera relative to the player
    
    private KartController kartController;
    
    public Vector3 origCamPos;
    public Vector3 boostCamPos;

    void Start()
    {
        kartController = player.GetComponent<KartController>();
    }

    void LateUpdate()
    {
        // Follow the player's position while maintaining the offset
        transform.position = player.position + offset;
        // Smoothly rotate the camera to match the player's rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, 3 * Time.deltaTime);

        // Adjusts the camera's local position when boosting for a dynamic effect
        if (kartController.boostTime > 0)
        {
            transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, boostCamPos, 3 * Time.deltaTime);
        }
        else
        {
            transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, origCamPos, 3 * Time.deltaTime);
        }
    }
}
