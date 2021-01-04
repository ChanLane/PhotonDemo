using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;



namespace chandler.scripts
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields

        #endregion

        
        #region Private Fields
        
        /// <summary>
        /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
        /// </summary>
        
        /// This is the clients version number. Users are seperated from each other by gameVersion( which allows you to make breaking changes)
         
        private string gameVersion = "1";
        
        #endregion


        #region MonoBehavior Callbacks

        // Start is called before the first frame update

        private void Awake()
        {
            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all the clinets in the same room sync their level automatically
            
             PhotonNetwork.AutomaticallySyncScene = true;
        }

        void Start()
        { 
            Connect();  
        }
        
        #endregion

        #region PublicMethods

        // start the connection process.
        // - if already connected, we attempt joining a random room
        // - if not yet connected, Connect this application instance to Photon Cloud Network

        public void Connect()
        {
            
            // we check if we are connected or not, we join if we are, else we initiate connection to the server
            if (PhotonNetwork.IsConnected)
            {
                // #Critical 
                // we need at this point to attempt joining a random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
            
        }
        
      #endregion



      #region MonobehaviorPUNCallbacks Callbacks

      public override void OnConnectedToMaster()
      {
          Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
      }

      public override void OnDisconnected(DisconnectCause cause)
      {
          Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause); 
      }

      #endregion
      
      
    }    
}

