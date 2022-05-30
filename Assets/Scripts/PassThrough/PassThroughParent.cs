using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughParent : MonoBehaviour {
    [SerializeField] Material _transparent;

    public void SendTransparent()
    {
        BroadcastMessage("SetTransparent", _transparent, SendMessageOptions.DontRequireReceiver);
    }

    public void ResetMaterials()
    {
        BroadcastMessage("ResetMaterial", SendMessageOptions.DontRequireReceiver);
    }
}
