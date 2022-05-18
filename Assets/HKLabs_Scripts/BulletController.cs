using UnityEngine;
using System.Collections;

namespace HKLabs
{
    public class BulletController : MonoBehaviour
    {
        #region Variables

        [SerializeField] private float _bulletSpeed = 5.0f;
        [SerializeField] private float _bulletDestroyTime = .5f;

        #endregion

        #region Builtin Methods

        // Start is called before the first frame update
        void Start ()
        {
            StartCoroutine (AutomaticallyDestroyBullet ());
        }

        // Update is called once per frame
        void Update ()
        {
            transform.Translate (Vector3.down * _bulletSpeed * Time.deltaTime);
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// This delays the destruction of <see cref="BulletController"/>
        /// <para> We must give the bullet some time to travel before destroying it </para>
        /// </summary>
        private IEnumerator AutomaticallyDestroyBullet ()
        {
            yield return new WaitForSeconds (_bulletDestroyTime);
            Destroy (gameObject);
        }

        #endregion
    }
}