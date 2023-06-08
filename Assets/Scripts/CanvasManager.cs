using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Button revokeBtn;
    bool revokeCliced = false;
    public void Revoke()
    {
        if (!revokeCliced)
        {
            revokeCliced = true;
            Debug.Log("***** REVOKE ACTIVE *****");
            revokeBtn.image.color = Color.red;
            EventManager.RevokeBool(revokeCliced);
        }
        else if (revokeCliced)
        {
            revokeCliced = false;
            Debug.Log("***** REVOKE NOT ACTIVE *****");
            revokeBtn.image.color = Color.grey;
            EventManager.RevokeBool(revokeCliced);
        }
    }
}
