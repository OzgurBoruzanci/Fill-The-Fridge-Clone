using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Products : MonoBehaviour,IClickable
{
    public float volume;
    public bool cliced = false;
    public bool doubleSize = false;
    public bool didSettle = false;
    public bool didNotSettle=false;
    public bool _collided=true;
    public bool _clickable = true;
    Vector3 firstPosition;
    Quaternion firstRotation;
    private void OnEnable()
    {
        EventManager.TargetPosition += TargetPosition;
        //EventManager.DidNotSettle += DidNotSettle;
        //EventManager.DidSettle += DidSettle;
    }
    private void OnDisable()
    {
        EventManager.TargetPosition -= TargetPosition;
        //EventManager.DidNotSettle -= DidNotSettle;
        //EventManager.DidSettle -= DidSettle;
    }
    void TargetPosition(Vector3 targetPosition,Quaternion rotation)
    {
        if (cliced)
        {
            transform.rotation = rotation;
            transform.position = targetPosition;
            //transform.DORotateQuaternion(rotation, 0.01f);
            //transform.DOMove(targetPosition, 0.5f);
            cliced = false;
            //transform.GetComponent<BoxCollider>().enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Products>() || other.GetComponent<BorderManager>()) 
        {
            didNotSettle = true;
            DidNotSettle();
        }

        if (other.tag != "Product" || other.tag != "Border")
        {
            if (_collided && !didNotSettle)
            {
                didSettle= true;
                DidSettle();
                _collided = false;
            }
        }
    }
    void DidSettle()
    {
        if (transform.position!=firstPosition)
        {
            _clickable = false;
            EventManager.DidSettle(Mathf.Floor(volume));
        }
    }
    void DidNotSettle()
    {
        if (didNotSettle && !didSettle)
        {
            transform.position = firstPosition;
            transform.rotation = firstRotation;
            _clickable = true;
        }
        if (transform.position==firstPosition)
        {
            cliced = false; 
            doubleSize = false; 
            didSettle = false; 
            didNotSettle = false;
            _collided = true;
            _clickable = true;
            Debug.Log("2 "+transform.name);
        }
    }

    void Start()
    {
        CalculateVolume();
        IsDoubleSize();
        firstPosition=transform.position;
        firstRotation = transform.rotation;
    }

    
    void Update()
    {
        
    }

    void CalculateVolume()
    {
        volume = transform.GetComponent<BoxCollider>().size.x * 
            transform.GetComponent<BoxCollider>().size.y * 
            transform.GetComponent<BoxCollider>().size.z;

    }

    public void OnClick()
    {
        if (_clickable)
        {
            cliced = true;
            EventManager.ProductHeight(transform.GetComponent<BoxCollider>().size.y);
        }
    }
    void IsDoubleSize()
    {
        if (transform.GetComponent<BoxCollider>().size.y>1)
        {
            doubleSize= true;
        }
    }
}
