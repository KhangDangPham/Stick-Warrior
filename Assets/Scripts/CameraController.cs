using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float followSpeed = 2.0f;
    public float yOffset = 1.5f;
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = new Vector3(player.position.x, player.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, playerPosition, followSpeed * Time.deltaTime);
    }
}
