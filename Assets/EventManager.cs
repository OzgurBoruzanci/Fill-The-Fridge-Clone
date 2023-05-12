using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{

    public static Action<float> Volume;
    public static Action<Vector3,Quaternion> TargetPosition;
}