using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class InputManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public float x=0, y=0;
    
    private void Update()
    {
        x = Input.GetAxis("Horizontal") * 10f * Time.deltaTime;
        y = Input.GetAxis("Vertical") * 10f * Time.deltaTime;
    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(x);
            stream.SendNext(y);
        }
        else
        {
            this.x = (float) stream.ReceiveNext();
            this.y = (float) stream.ReceiveNext();
        }
    }
}
