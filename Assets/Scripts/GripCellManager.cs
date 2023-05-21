using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class GripCellManager : MonoBehaviour,IClickable
{
    Vector3 targetPosition;
    float height;
    public bool isDoubleSize=false;
    public void OnClick()
    {
        EventManager.TargetPosition(targetPosition, transform.rotation);
    }

    private void OnEnable()
    {
        EventManager.ProductHeight += TargetPosition;
    }
    private void OnDisable()
    {
        EventManager.ProductHeight -= TargetPosition;
    }
    void IsDoubleSize()
    {
        //IClickable clickable = hit.collider.GetComponent<IClickable>();
        if (height>1.1)
        {
            isDoubleSize = true;
        }
    }

    void TargetPosition(float productHeight)
    {
        height = productHeight;
        IsDoubleSize();
        if (isDoubleSize)
        {
            targetPosition = transform.GetChild(0).position;
        }
        else
        {
            targetPosition = transform.GetChild(1).position;
            isDoubleSize = true;
        }
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
