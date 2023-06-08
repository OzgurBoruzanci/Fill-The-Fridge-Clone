using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Products : MonoBehaviour,IClickable
{
    public float volume;
    bool _revokebool;
    public bool cliced = false;
    public bool didSettle = false;
    public bool didNotSettle=false;
    public bool _collided=true;
    public bool _clickable = true;
    Vector3 firstPosition;
    Quaternion firstRotation;
    private void OnEnable()
    {
        EventManager.RevokeBool += RevokeBool;
        EventManager.TargetPosition += TargetPosition;
        //EventManager.DidNotSettle += DidNotSettle;
        //EventManager.DidSettle += DidSettle;
    }
    private void OnDisable()
    {
        EventManager.RevokeBool -= RevokeBool;
        EventManager.TargetPosition -= TargetPosition;
        //EventManager.DidNotSettle -= DidNotSettle;
        //EventManager.DidSettle -= DidSettle;
    }
    void RevokeBool(bool revoke)
    {
        _revokebool = revoke;
    }
    void TargetPosition(Vector3 targetPosition,Quaternion rotation)
    {
        if (cliced)
        {
            transform.rotation = rotation;
            transform.position = targetPosition;
            EventManager.DidSettle(Mathf.Floor(volume));
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
            DidNotSettle();
        }
        if (!didNotSettle && !other.GetComponent<Products>() && !other.GetComponent<BorderManager>())
        {
            DidSettle();
        }
    }
    void DidSettle()
    {
        didSettle = true;
        _clickable = false;
    }
    void DidNotSettle()
    {
        didNotSettle = true;
        if (didNotSettle && !didSettle)
        {
            transform.position = firstPosition;
            transform.rotation = firstRotation;
        }
    }

    void Start()
    {
        CalculateVolume();
        firstPosition=transform.position;
        firstRotation = transform.rotation;
    }
    private void Update()
    {
        if (transform.position==firstPosition && didNotSettle)
        {
            InitialValues();
            EventManager.RevokeVolume(Mathf.Floor(volume));
        }
    }

    void CalculateVolume()
    {
        volume = transform.GetComponent<BoxCollider>().size.x * 
            transform.GetComponent<BoxCollider>().size.y * 
            transform.GetComponent<BoxCollider>().size.z;

    }

    public void OnClick()
    {
        if (_clickable && !_revokebool)
        {
            cliced = true;
            EventManager.ProductHeight(transform.GetComponent<BoxCollider>().size.y,cliced);
        }
        else if (_revokebool)
        {
            transform.position = firstPosition;
            transform.rotation=firstRotation;
            //InitialValues();
            EventManager.RevokeVolume(Mathf.Floor(volume));
        }
    }
 

    void InitialValues()
    {
        cliced = false;
        didSettle = false;
        didNotSettle = false;
        _collided = true;
        _clickable = true;
    }
}
