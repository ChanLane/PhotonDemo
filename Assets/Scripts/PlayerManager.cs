using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace ChandlerLane.Scripts
{
    public class PlayerManager : MonoBehaviour
    {
        #region Private Fields

        [Tooltip("The Beams GameObject to control")] [SerializeField]
        private GameObject beams;

        private bool _isFiring;
        #endregion

        private PhotonView View;

        #region Public Fields
        
        [Tooltip("The current Health of our Player")]
        public float health = 1f;

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            if (beams == null)
            {
                Debug.LogError("Missing Beams Reference.", this);
            }
            else
            {
                beams.SetActive(false);
            }
            
        }
        private void Update()
        {
            ProcessInputs();

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
            if (!View.IsMine)
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
            if (!View.IsMine)
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
        // Start is called before the first frame update

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

    }    
}

