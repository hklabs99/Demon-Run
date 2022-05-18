using UnityEngine;
using System.Collections;

namespace HKLabs
{
    public class EnemyController : MonoBehaviour
    {
        #region Variables

        [Header ("Required References to Unity Components")]
        [SerializeField] private Animator _enemyAnims;
        [SerializeField] private ParticleSystem _enemyDeathParticles;

        public static bool HasEnemyBeenShot
        {
            get;
            private set;
        }

        #endregion

        #region Builtin Methods

        // Update is called once per frame
        void Update ()
        {
            CheckPlayerDeadStatus ();

            _enemyDeathParticles.gameObject.transform.position = transform.position;
        }

        private void OnCollisionEnter (Collision collision)
        {
            if (collision.gameObject.CompareTag ("Bullet"))
            {
                //increase score
                HasEnemyBeenShot = true;
                Destroy (collision.gameObject);
                GameManager.IncreaseScore ();
                Destroy (gameObject);

                if (!_enemyDeathParticles.isPlaying)
                    _enemyDeathParticles.Play ();
            }
        }

        #endregion

        #region Custom Method

        /// <summary>
        /// This method checks whether <see cref="PlayerController"/> is dead 
        /// and based on that it sets the animation state
        /// </summary>
        private void CheckPlayerDeadStatus ()
        {
            if (PlayerController.IsPlayerDead)
                _enemyAnims.SetBool ("isLaughing", true);

            else
                _enemyAnims.SetBool ("isLaughing", false);
        }

        #endregion
    }
}