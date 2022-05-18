using UnityEngine;

namespace HKLabs
{
    public class CameraFollow : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Vector3 _targetOffset;

        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start ()
        {
            _targetOffset = transform.position;
        }

        void LateUpdate ()
        {
            //This only makes the camera follow the player's x-axis.
            transform.position = new Vector3 (_playerTransform.position.x + _targetOffset.x, _targetOffset.y, _targetOffset.z);
        }

        #endregion
    }
}