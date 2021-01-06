using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;


namespace ChandlerLane.Scripts
{
    public class GameManager : MonoBehaviourPunCallbacks
    {

        #region Public Fields

        public static GameManager Instance;

        public GameObject playerPrefab;

        #endregion

        #region MonoBehavior Callbacks

        private void Start()
        {
            Instance = this;

            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> PlayerPrefab Reference");
            }
            else
            {
                Debug.LogFormat("We are Instantianting LocalPlayer from {0}", Application.loadedLevelName);
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
            }
        }

        #endregion
        
        #region Photon Callbacks

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
                
                LoadArena();
            }
        }

        #endregion

        #region Public Methods

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        #endregion

        #region Private Methods

        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to load a level but we are not the master Client");
            }


            if (PlayerManager.LocalPlayerInstance == null)
            {
                Debug.LogFormat("PhotonNetwork : Loading level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
                PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);    
            }
            else
            {
                Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            }

            
        }

        #endregion
        
    }
    
}
