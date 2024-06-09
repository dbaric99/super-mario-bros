using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    public float height = 7.04f;
    public float undergroundHeight = -7.01f;

    private Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag(PLAYER_TAG).transform;
    }

    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
        transform.position = cameraPosition;
    }

    public void SetUndergroud(bool isUnderground)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = isUnderground ? undergroundHeight : height;
        transform.position = cameraPosition;
    }
}
