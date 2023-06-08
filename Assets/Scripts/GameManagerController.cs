using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    public float hamperVolume;
    bool revokeCliced=false;
    private void OnEnable()
    {
        EventManager.HamperVolume += HamperVolume;
        EventManager.DidSettle += DidSettle;
    }
    private void OnDisable()
    {
        EventManager.HamperVolume -= HamperVolume;
        EventManager.DidSettle -= DidSettle;
    }
    void HamperVolume(float hVolume)
    {
        hamperVolume=hVolume;
    }
    void DidSettle(float didSettle) 
    {
        hamperVolume -= didSettle;
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
        if (hamperVolume>=0)
        {
            MouseDown();
        }
        else
        {
            Debug.Log("****GAME OVER****");
        }
    }
    public void Revoke()
    {
        if (!revokeCliced)
        {
            revokeCliced = true;
            EventManager.RevokeBool(revokeCliced);
        }
        else if (revokeCliced)
        {
            revokeCliced = false;
            EventManager.RevokeBool(revokeCliced);
        }
    }
}
