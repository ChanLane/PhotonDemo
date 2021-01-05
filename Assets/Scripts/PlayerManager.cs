using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChandlerLane.Scripts
{
    public class PlayerManager : MonoBehaviour
    {
        #region Private Fields

        [Tooltip("The Beams GameObject to control")] [SerializeField]
        private GameObject beams;

        private bool IsFiring;
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
            
            // trigger Beams active state
            if (beams != null && IsFiring != beams.activeInHierarchy)
            {
                beams.SetActive(IsFiring);
            }
        }

        #endregion
        // Start is called before the first frame update

        #region Custom

        void ProcessInputs()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!IsFiring)
                {
                    IsFiring = true;
                }
            }

            if (Input.GetButtonUp("Fire1"))
            {
                if (IsFiring)
                {
                    IsFiring = false;
                }
            }
        }
        
        #endregion

    }    
}

