using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    public float hamperVolume;
    
    private void OnEnable()
    {
        EventManager.HamperVolume += HamperVolume;
        EventManager.DidSettle += DidSettle;
        EventManager.RevokeVolume += RevokeVolume;
    }
    private void OnDisable()
    {
        EventManager.HamperVolume -= HamperVolume;
        EventManager.DidSettle -= DidSettle;
        EventManager.RevokeVolume -= RevokeVolume;
    }
    void HamperVolume(float hVolume)
    {
        hamperVolume=hVolume;
        Debug.Log(hamperVolume + " gelen= " + hVolume);
    }
    void DidSettle(float didSettle) 
    {
        hamperVolume -= didSettle;
        Debug.Log(didSettle);
    }
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
        if (hamperVolume>=0)
        {
            MouseDown();
        }
        else
        {
            Debug.Log("****GAME OVER****");
        }
    }
    
}
