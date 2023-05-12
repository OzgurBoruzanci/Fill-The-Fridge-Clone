using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Products : MonoBehaviour,IClickable
{
    public float volume;
    public bool cliced = false;
    private void OnEnable()
    {
        EventManager.TargetPosition += TargetPosition;
    }
    private void OnDisable()
    {
        EventManager.TargetPosition -= TargetPosition;
    }
    void TargetPosition(Vector3 targetPosition,Quaternion rotation)
    {
        if (cliced)
        {
            transform.DOMove(targetPosition, 0.5f);
            transform.DORotateQuaternion(rotation, 0.5f);
            cliced = false;
        }
    }

    void Start()
    {
        CalculateVolume();
    }

    
    void Update()
    {
        
    }
    //private void OnMouseDown()
    //{
    //    OnClick();
    //}
    void CalculateVolume()
    {
        volume = transform.localScale.x*transform.localScale.y*transform.localScale.z;
    }

    public void OnClick()
    {
        cliced= true;
    }
}
