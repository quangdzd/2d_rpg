using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;


    void LateUpdate()
    {
        Vector3 worldPos = target.position + offset;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        transform.position = screenPos;
    }
}
