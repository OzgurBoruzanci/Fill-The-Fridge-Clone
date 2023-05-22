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
    bool collided=true;
    public bool clickable = true;
    Vector3 firstPosition;
    Quaternion firstRotation;
    private void OnEnable()
    {
        EventManager.TargetPosition += TargetPosition;
        EventManager.DidNotSettle += DidNotSettle;
        //EventManager.DidSettle += DidSettle;
    }
    private void OnDisable()
    {
        EventManager.TargetPosition -= TargetPosition;
        EventManager.DidNotSettle -= DidNotSettle;
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
            clickable = false;
            cliced = false;
            //transform.GetComponent<BoxCollider>().enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (/*cliced &&*/!didSettle && other.GetComponent<Products>() || other.GetComponent<BorderManager>())
        {
            EventManager.DidNotSettle(Mathf.Floor(volume));
            clickable = true;
            Debug.Log("girdi"+other.name);
        }
        else if (!didSettle && other.transform.parent==null && !other.GetComponent<Products>() || !other.GetComponent<BorderManager>())
        {
            DidSettle();
            didSettle=true;
            Debug.Log(didSettle+other.name);
        }


        //if (other.tag != "Product" || other.tag != "Border")
        //{
        //    if (collided)
        //    {
        //        DidSettle();
        //        collided = false;
        //    }
        //}
    }
    void DidSettle()
    {
        clickable = false;
        EventManager.DidSettle(Mathf.Floor(volume));
    }
    void DidNotSettle(float pVolume)
    {
        if (clickable)
        {
            transform.position = firstPosition;
            transform.rotation = firstRotation;
            //clickable = true;
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
        if (clickable)
        {
            cliced = true;
            EventManager.ProductHeight(transform.GetComponent<BoxCollider>().size.y);
            //DidSettle();
            EventManager.DidSettle(Mathf.Floor(volume));
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
