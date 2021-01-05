
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
        private float height = 7.0f;

        [BoxGroup("Camera Settings")]
        [Tooltip("Allow the camera to be offseted vertically from the target, for example giving more view of the scenery and less ground")]
        [SerializeField]
        private Vector3 centerOffset = Vector3.zero;

        [BoxGroup("Camera Settings")]
        [Tooltip("The Smoothing for the camera to follow the target")] 
        [SerializeField]
        private float SmoothSpeed = 0.125f;
       
        #endregion
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }    
}

