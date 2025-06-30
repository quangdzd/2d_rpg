using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject _player;

    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (_player != null)
        {
            Vector3 newPos = _player.transform.position;
            newPos.z = transform.position.z; // Giữ nguyên Z của camera (tránh áp sát player nếu 2D)
            transform.position = newPos;
        }
    }
}
