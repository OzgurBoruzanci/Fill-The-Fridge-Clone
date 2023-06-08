using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{

    public static Action<float> Volume;
    public static Action<Vector3,Quaternion> TargetPosition;
    public static Action<float,bool> ProductHeight;
    public static Action<float> HamperVolume;
    public static Action<float> DidSettle;
    public static Action<bool> RevokeBool;
    public static Action<float> RevokeVolume;
}