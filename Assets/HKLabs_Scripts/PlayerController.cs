using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace HKLabs
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        public static float playerSpeed = 5.0f;

        //---------------------------------------------Private Variables---------------------------------------------/

        [Header ("Player Properties")]
        
        [SerializeField] private float _jumpForce = 5.0f;
        [SerializeField] private float _destroyDelay = .5f, _jumpDelay;
        [SerializeField] private bool _isJumping;

        [Tooltip ("The offset for the bullet position")]
        [SerializeField] private Vector3 _offset;

        [Space]
        [Header ("Required References to Unity Components")]
        [SerializeField] private Rigidbody _playerRb;
        [SerializeField] private GameObject _bulletPrefab, _gunPrefab;
        [SerializeField] private Animator _playerAnim;
        [SerializeField] private Button[] _playerButton;

        [Space]
        [Header ("SFX Properties")]
        [SerializeField] private AudioSource _playerSFX;
        [SerializeField] private AudioClip _gunShotSound, _jumpSound;

        [Tooltip ("To be played if the player dies by fire")]
        [SerializeField] private AudioClip _explosionSound;

        [Tooltip ("To be played if the player dies by the enemy")]
        [SerializeField] private AudioClip _enemyLaughSound;

        [Space]
        [Header ("Particle FX")]
        [Tooltip ("Particle effect to be played when the player collides with the fire")]
        [SerializeField] private ParticleSystem _fireParticles;

        [Tooltip ("Particle effect to be played when the player collides with the enemy")]
        [SerializeField] private ParticleSystem _enemyParticles;

        private bool _isPlayerGrounded = true;

        /// <summary>
        /// This allows other classes to only check whether the player is dead.
        /// </summary>
        public static bool IsPlayerDead
        {
            get;
            private set;
        }

        #endregion

        #region Builtin Methods

        // Start is called before the first frame update
        void Start ()
        {
            IsPlayerDead = false;
        }

        // Update is called once per frame
        void Update ()
        {
            _enemyParticles.gameObject.transform.position = transform.position;
            _fireParticles.gameObject.transform.position = transform.position;

            if (!IsPlayerDead)
                transform.Translate (Vector3.forward * playerSpeed * Time.deltaTime);

            CheckingForJumpAnimation ();

            if (Input.GetKey (KeyCode.Escape))
                Debug.Break ();

            if (IsPlayerDead)
            {
                for (int i = 0; i < _playerButton.Length; i++)
                    _playerButton[i].gameObject.SetActive (false);
            }
        }

        private void OnCollisionEnter (Collision collision)
        {
            if (collision.gameObject.CompareTag ("Ground"))
            {
                _isPlayerGrounded = true;
                _isJumping = false;
            }

            //For when the player collides with the fire
            else if (collision.gameObject.CompareTag ("Fire"))
            {
                IsPlayerDead = true;
                Handheld.Vibrate ();

                _playerSFX.PlayOneShot (_explosionSound);

                if (!_fireParticles.isPlaying)
                    _fireParticles.Play ();

                StartCoroutine (DestroyPlayer ());
            }

            //For when the player collides with the enemy
            else if (collision.gameObject.CompareTag ("Enemy"))
            {
                IsPlayerDead = true;
                Handheld.Vibrate ();

                if (!_enemyParticles.isPlaying)
                    _enemyParticles.Play ();

                _playerSFX.PlayOneShot (_enemyLaughSound);

                StartCoroutine (DestroyPlayer ());
            }
        }

        #endregion

        #region Custom Methods

        //---------------------------------------------Public Methods---------------------------------------------//

        /// <summary>
        /// To be called from the Jump button
        /// </summary>
        public void OnJumpButton ()
        {
            if (_isPlayerGrounded && !IsPlayerDead)
            {
                _isJumping = true;
                StartCoroutine (JumpDelay (_jumpDelay));

                _playerSFX.PlayOneShot (_jumpSound);
            }
        }

        /// <summary>
        /// To be called from the Shoot button
        /// </summary>
        public void OnShootButton ()
        {
            Vector3 bulletOffset = _gunPrefab.transform.position + _offset;

            Instantiate (_bulletPrefab, bulletOffset, _bulletPrefab.transform.rotation);

            _playerSFX.PlayOneShot (_gunShotSound);
        }

        //---------------------------------------------Private Methods---------------------------------------------//

        /// <summary>
        /// This method checks whether the player is jumping 
        /// and based on that it sets the <see cref="Animation"/> state
        /// </summary>
        private void CheckingForJumpAnimation ()
        {
            if (_isJumping)
            {
                _playerAnim.SetBool ("isJumping", true);

                for (int i = 0; i < _playerButton.Length; i++)
                    _playerButton[i].gameObject.SetActive (false);
            }

            else
            {
                _playerAnim.SetBool ("isJumping", false);

                for (int i = 0; i < _playerButton.Length; i++)
                    _playerButton[i].gameObject.SetActive (true);
            } 
        }

        /// <summary>
        /// This method introduces a delay in jump force, to sync the motion with the animation
        /// </summary>
        /// <param name="jumpDelay">Amount of time to delay the jump force</param>
        private IEnumerator JumpDelay (float jumpDelay)
        {
            yield return new WaitForSeconds(jumpDelay);

            Vector3 jump = new Vector3 (1, _jumpForce, 0);

            _playerRb.AddForce(jump, ForceMode.Impulse);
            _isPlayerGrounded = false;
        }

        private IEnumerator DestroyPlayer ()
        {
            yield return new WaitForSeconds (_destroyDelay);
            Destroy (gameObject);
        }

        #endregion
    }
}
