using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ChandlerLane.Scripts
{
    public class PlayerAnimatorManager : MonoBehaviour
    {

        #region PrivateFields

        private Animator _animator;
        [SerializeField] private float directionDampTime = 0.25f;

        #endregion
        
        
        #region MonoBehaviour Callbacks

       
        private void Start()
        {
            _animator = GetComponent<Animator>();
            if (!_animator)
            {
                Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
            }
        }

        private void Update()
        {
            if (!_animator)
            {
                return;
            }
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (v < 0)
            {
                v = 0;
            }
            _animator.SetFloat("Speed", h * h + v * v);
            _animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);
        }

        #endregion
    }
}
