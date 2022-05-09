    using System;
    using System.Collections;
using System.Collections.Generic;
    using Photon.Pun;
    using Photon.Realtime;
    using UnityEngine;
    using UnityEngine.UI;

    public class Launcher : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields

    [SerializeField] private byte roomSize = 4;
    [SerializeField] private Text serverStateText;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject connectingText;
    [SerializeField] private Button connectButton;

    #endregion

    #region Private Fields

    private string gameVersion = "1";

    #endregion

    #region Private Methods

    private void ChangeUIWhenConnected()
    {
        serverStateText.text = "Server is Connected!";

        // 닉네임 입력하는 input, 버튼 표시
        connectingText.SetActive(false);
        panel.SetActive(true);
        
        // Connect Button 비활성화
        connectButton.enabled = false;
    }

    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    
        if (PhotonNetwork.IsConnected)
        {
            ChangeUIWhenConnected();
        }
        else
        {
            // Launcher Scene 이 실행될 때 서버 연결이 되어 있지 않으면 자동으로 연결 시도
            Connect();
        }
    }
    
    #endregion

    #region MonoBehaviourPunCallbacks Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("Server is connected!");

        ChangeUIWhenConnected();
    }

    // 서버와의 연결이 끊겼을 때 호출됨
    public override void OnDisconnected(DisconnectCause cause)
    { 
        Debug.LogError("Server is disconnected... [" + cause.ToString() + "]");

        // ServerStateText 변경
        // Connect Button 활성화
        serverStateText.text = "Server is disconnected.";
        connectButton.enabled = true;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " is in room now");

        // 왜 플레이어 수가 1명일 때 LoadLevel() 을 호출하는 지 모르겠음
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LoadLevel("Map");
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Join Room Fail");
        
        PhotonNetwork.CreateRoom(null, new RoomOptions
        {
            MaxPlayers = roomSize
        });
    }

    #endregion

    #region Public Methods

    // Connect Button OnClick
    public void Connect()
    {
        // ConnectingText 표시
        connectingText.SetActive(true);
        panel.SetActive(false);

        // Photon Master Server 접속 -> OnConnectedToMaster()
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    // Play Button OnClick
    public void Play()
    {
        Debug.Log("Play Button is pushed " + PhotonNetwork.IsConnected + " " + PhotonNetwork.NickName);
        
        Debug.Log(PhotonNetwork.CurrentRoom);
        // 랜덤한 방에 입장 시도, 방이 없으면 만들어서 입장
        if(PhotonNetwork.IsConnected) PhotonNetwork.JoinRandomRoom();
    }

    #endregion
}
