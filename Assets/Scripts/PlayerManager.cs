using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace ChandlerLane.Scripts
{
    public class PlayerManager : MonoBehaviourPun, IPunObservable
    {

        #region Public Fields

        [Tooltip("The local player instance. Use this to know if the local player is represented in the scene.")]
        public static GameObject LocalPlayerInstance;

        #endregion
        
        #region Private Fields

        [Tooltip("The Beams GameObject to control")] [SerializeField]
        private GameObject beams;

        private bool _isFiring;
        
        [Tooltip("The current Health of our Player")]
        public float health = 1f;

        #endregion

        #region IPunObservable implementation

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                //we own this player: send the others our data
                stream.SendNext(_isFiring);
                stream.SendNext(health);
            }
            else
            {
                // Network player, receive data
                this._isFiring = (bool) stream.ReceiveNext();
                this.health = (float) stream.ReceiveNext();
            }
        }

        #endregion
        
        #region MonoBehaviour Callbacks

#if UNITY_5_4_OR_NEWER
        void OnlevelWasLoaded(int Level)
        {
            this.CalledOnLevelWasLoaded(Level);
        }
#endif
        void CalledOnLevelWasLoaded(int level)
        {
            if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
            {
                transform.position = new Vector3(0f, 5f, 0f);
            }
        }
        
#if UNITY_5_4_OR_NEWER
        private void OnDisable()
        {
            OnDisable();
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }
#endif        
        private void Awake()
        {
            if (photonView.IsMine)
            {
                PlayerManager.LocalPlayerInstance = this.gameObject;
            }
            
            DontDestroyOnLoad(this.gameObject);
            
            if (beams == null)
            {
                Debug.LogError("Missing Beams Reference.", this);
            }
            else
            {
                beams.SetActive(false);
            }
            
        }

        private void Start()
        {
            CameraWork cameraWork = this.gameObject.GetComponent<CameraWork>();

            if (cameraWork != null)
            {
                if (photonView.IsMine)
                {
                    cameraWork.OnStartFollowing();
                }
            }
            else
            {
                Debug.LogError("<color=Red><a>Missing</a></Color> Camera component on Player prefab", this);
            }
            
#if UNITY_5_4_OR_NEWER
            
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;

#endif

        }

        private void Update()
        {
            if (photonView.IsMine)
            {
                ProcessInputs();    
            }

            

            if (health < 0f)
            {
                GameManager.Instance.LeaveRoom();
            }
            
            // trigger Beams active state
            if (beams != null && _isFiring != beams.activeInHierarchy)
            {
                beams.SetActive(_isFiring);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (photonView.IsMine)
            {
                return;
            }

            if (!other.name.Contains("Beam"))
            {
               return;    
            }

            health -= 0.1f;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }

            if (other.name.Contains("Beam"))
            {
                return;
            }

            health -= 0.1f * Time.deltaTime;
        }

        #endregion

        #region Custom

        void ProcessInputs()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!_isFiring)
                {
                    _isFiring = true;
                }
            }

            if (Input.GetButtonUp("Fire1"))
            {
                if (_isFiring)
                {
                    _isFiring = false;
                }
            }
        }
        
        #endregion

        #region Private Methods
        
        #if UNITY_5_4_OR_NEWER

        void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene,
            UnityEngine.SceneManagement.LoadSceneMode loadingMode)
        {
            this.CalledOnLevelWasLoaded(scene.buildIndex);
        }
        
        #endif
        #endregion
    }    
}

