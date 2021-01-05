
using UnityEngine;
using Sirenix.OdinInspector;

namespace ChandlerLane.Scripts
{
    public class CameraWork : MonoBehaviour
    {

        #region Private Fields
        
        [BoxGroup("Camera Settings")]
        [Tooltip("The distance in the local x-z plane to the target")] 
        [SerializeField]
        private float distance = 7.0f;

       
        [BoxGroup("Camera Settings")]
        [Tooltip("The height we want the camera to be above the target")] 
        [SerializeField]
        private float height = 3.0f;

        
        [BoxGroup("Camera Settings")]
        [Tooltip("Allow the camera to be offset vertically from the target, for example giving more view of the scenery and less ground")]
        [SerializeField]
        private Vector3 centerOffset = Vector3.zero;
        
        [BoxGroup("Camera Settings")]
        [Tooltip("The Smoothing for the camera to follow the target")] 
        [SerializeField]
        private float SmoothSpeed = 0.125f;

        [BoxGroup("Camera Settings")]
        [Tooltip("Set this as false if a component of a prefab being instantiated by Photon Network, and manually call OnStartFollowing() when and if needed.")]
        [SerializeField]
        private bool followOnStart = false;
        
        [SerializeField]
        private Transform cameraTransform;

        private bool isFollowing;

        private Vector3 CameraOffset = Vector3.zero;
       
        #endregion

        #region MonoBehavior Callbacks

        // Start is called before the first frame update
        void Start()
        {
            if (followOnStart)
            {
                OnStartFollowing();
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (cameraTransform == null && isFollowing)
            {
                OnStartFollowing();
            }


            if (isFollowing)
            {
                Follow();
            }
        } 

        #endregion

        #region Public Methods

        public void OnStartFollowing()
        {
            cameraTransform = Camera.main.transform;
            isFollowing = true;
           
            // we dont smooth anything, we go straight to the right camera shot
            Cut();
        }

        #endregion

        #region Private Methods

        void Follow()
        {
            CameraOffset.z = -distance;
            CameraOffset.y = height;

            cameraTransform.position = Vector3.Lerp(cameraTransform.position, this.transform.position +
                this.transform.TransformVector(CameraOffset),SmoothSpeed * Time.deltaTime);
            
            cameraTransform.LookAt(this.transform.position + centerOffset);
        }

        void Cut()
        {
            CameraOffset.z = -distance;
            CameraOffset.y = height;

            cameraTransform.position = this.transform.position + this.transform.TransformVector(CameraOffset);
            
            cameraTransform.LookAt(this.transform.position + centerOffset);
        }

        #endregion
    }    
}

