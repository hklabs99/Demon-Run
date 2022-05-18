using UnityEngine;

namespace HKLabs
{
    public class RepeatBackground : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Camera _camera;
        [SerializeField] private float _parallaxEffect;

        private float _startPos, _length;

        #endregion

        #region Builtin Methods

        // Start is called before the first frame update
        void Start ()
        {
            _startPos = transform.position.x;
            _length = GetComponent<SpriteRenderer> ().bounds.size.x;
        }

        void Update ()
        {
            float temp = (_camera.gameObject.transform.position.x * (1 - _parallaxEffect));

            float distance = (_camera.gameObject.transform.position.x * _parallaxEffect);

            transform.position = new Vector3 (_startPos + distance, transform.position.y, transform.position.z);

            if (temp > _startPos + _length)
                _startPos += _length;

            else if (temp < _startPos - _length)
                _startPos -= _length;
        }

        #endregion
    }
}