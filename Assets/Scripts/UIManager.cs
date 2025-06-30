using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Camera _camera;
    public Transform healthBarParent;
    public Vector3 healthBarOffset;
}
