using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GripCellManager : MonoBehaviour,IClickable
{
    Vector3 targetPosition;
    float height;
    public bool isDoubleSize=false;
    bool _cliced = false;
    bool _secondRow=false;
    bool _productCliced=false;
    public void OnClick()
    {
        _cliced = true;
        TargetPosition();
        EventManager.TargetPosition(targetPosition, transform.rotation);
    }

    private void OnEnable()
    {
        EventManager.ProductHeight += ProductHeight;
    }
    private void OnDisable()
    {
        EventManager.ProductHeight -= ProductHeight;
    }
    void ProductHeight(float productHeight,bool productCliced)
    {
        _productCliced=productCliced;
        height = productHeight;
        if (height>1.1f)
        {
            isDoubleSize = true;
        }
        else
        {
            isDoubleSize=false;
        }
    }

    void TargetPosition()
    {
        if (isDoubleSize && !_secondRow)
        {
            targetPosition = transform.GetChild(0).position;
        }
        else
        {
            targetPosition = transform.GetChild(1).position;
            _secondRow = true;
        }

        if (_cliced && !isDoubleSize && _productCliced)
        {
            GripCellPositionControl(1.001f);

            if (transform.localPosition.y > 2.001f)
            {
                GripCellPositionControl(-1.002f);
            }
        }
    }
    void Start()
    {
        targetPosition= transform.position;
    }

    void GripCellPositionControl(float distance)
    {
        transform.localPosition = new Vector3(transform.localPosition.x,
                    transform.localPosition.y +distance,
                    transform.localPosition.z);
    }
}
