using UnityEngine;
using TMPro;
using Michsky.UI.ModernUIPack;

namespace HKLabs
{
    public class GameManager : MonoBehaviour
    {
        #region Variables

        [Header ("Gameplay Information")]
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;

        [Space]
        [Header ("Properties")]
        [Tooltip ("To be turned on only when the player is dead")]
        [SerializeField] private WindowManager _windowManagerOnPlayerDeath;

        private static int _score;
        private float _highScore;
        private float _maxSpeed = 10.0f;

        #endregion

        #region Builtin Methods

        // Start is called before the first frame update
        void Start ()
        {
            _scoreText.text = "Shots: ";

            _highScore = PlayerPrefs.GetFloat ("Highscore", 0);
            DisplayHighScore ();
        }

        // Update is called once per frame
        void Update ()
        {
            DisplayScore ();
            CheckForHighScore ();
            DisplayHighScore ();

            if (PlayerController.IsPlayerDead)
                _windowManagerOnPlayerDeath.gameObject.SetActive (true);

            else
                _windowManagerOnPlayerDeath.gameObject.SetActive (false);

            if (_score == 5)
                PlayerController.playerSpeed = 6.0f;

            else if (_score == 10)
                PlayerController.playerSpeed = 7.0f;

            else if (_score == 20)
                PlayerController.playerSpeed = 8.0f;
        }

        #endregion

        #region Custom Methods

        public static void IncreaseScore ()
        {
            if (EnemyController.HasEnemyBeenShot)
            {
                _score++;
                print ("Score: " + _score);
            }

            return;
        }

        //-------------------------------------------Private Methods------------------------------------//

        private void DisplayScore ()
        {
            _scoreText.text = "Shots: " + _score.ToString ();
        }

        private void CheckForHighScore ()
        {
            if (_score > _highScore)
            {
                _highScore = _score;
                PlayerPrefs.SetFloat ("Highscore", _highScore);
            }
        }

        private void DisplayHighScore ()
        {
            _highScoreText.text = "Highscore: " + _highScore.ToString ();
        }

        #endregion
    }
}