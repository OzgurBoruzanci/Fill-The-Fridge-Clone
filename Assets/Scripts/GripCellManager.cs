using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripCellManager : MonoBehaviour,IClickable
{
    public void OnClick()
    {
        EventManager.TargetPosition(transform.position,transform.rotation);
    }
    //private void OnMouseDown()
    //{
    //    OnClick();
    //}
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
