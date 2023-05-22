using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class HamperController : MonoBehaviour
{
    public float hamperVolume;
    public GameObject floor;
    public GameObject border;

    private void OnEnable()
    {
        EventManager.DidSettle += DidSettle;
        EventManager.DidNotSettle += DidNotSettle;
    }
    private void OnDisable()
    {
        EventManager.DidSettle -= DidSettle;
        EventManager.DidNotSettle -= DidNotSettle;
    }
    void DidSettle(float pVolume)
    {
        hamperVolume -= pVolume;
        EventManager.HamperVolume(hamperVolume);
    }
    void DidNotSettle(float pVolume)
    {
        hamperVolume += pVolume;
        EventManager.HamperVolume(hamperVolume);
    }

    void Start()
    {
        CalcuteHamperVolume();
    }
    

    void CalcuteHamperVolume()
    {
        hamperVolume = (floor.transform.localScale.x - 0.2f) *
            (floor.transform.localScale.z - 0.2f) *
            (border.transform.localScale.y);
        EventManager.HamperVolume(hamperVolume);
    }

}
