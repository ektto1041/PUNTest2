using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NicknameInput : MonoBehaviour
{
    public void SetNickname(string str)
    {
        PhotonNetwork.NickName = str;
    }
}
