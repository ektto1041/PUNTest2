using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Scenes
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields

        [SerializeField] private GameObject playerPrefab;

        #endregion
        
        #region MonoBehaviourPunCallbacks Callbacks

        // 플레이어가 방에 들어왔을 때, 방 내의 모두에게 알려줌
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", newPlayer.NickName);
        }

        #endregion

        #region MonoBehaviour Callbacks

        private void Start()
        {
            // if (PhotonNetwork.IsMasterClient)
            // {
            //     // PhotonNetwork.InstantiateRoomObject(playerPrefab.name, Vector3.zero, Quaternion.identity);
            //     PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity, 0);    
            // }
            PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity, 0);
        }

        #endregion
    }
}