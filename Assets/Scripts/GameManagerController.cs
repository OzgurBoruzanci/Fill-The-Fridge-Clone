using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    public float hamperVolume;
    bool _revokeBool;
    private void OnEnable()
    {
        EventManager.HamperVolume += HamperVolume;
        EventManager.DidSettle += DidSettle;
        EventManager.RevokeVolume += RevokeVolume;
        EventManager.RevokeBool += RevokeBool;
    }
    private void OnDisable()
    {
        EventManager.HamperVolume -= HamperVolume;
        EventManager.DidSettle -= DidSettle;
        EventManager.RevokeVolume -= RevokeVolume;
        EventManager.RevokeBool -= RevokeBool;
    }
    void RevokeBool(bool revokeBool)
    {
        _revokeBool = revokeBool;
    }
    void HamperVolume(float hVolume)
    {
        hamperVolume=hVolume;    }
    void DidSettle(float didSettle) 
    {
        hamperVolume -= didSettle;    }
    void RevokeVolume(float revokeVolume)
    {
        hamperVolume += revokeVolume;
    }

    void Update()
    {
        IsItHealthy();
    }

    private void MouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                IClickable clickable = hit.collider.GetComponent<IClickable>();
                if (clickable != null)
                {
                    clickable.OnClick();
                    
                }
            }
        }
    }

    void IsItHealthy()
    {
        if (!_revokeBool)
        {
            if (hamperVolume > 0)
            {
                MouseDown();
            }
            else
            {
                Debug.Log("****GAME END****");
            }
        }
        else
        {
            MouseDown();
        }
    }
    
}
