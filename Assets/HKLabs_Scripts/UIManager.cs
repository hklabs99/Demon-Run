using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HKLabs
{
    public class UIManager : MonoBehaviour
    {
        #region Variables

        /// <summary>
        /// Delay variable to allow the button animation to play
        /// </summary>
        private float _loadSequenceDelay = .1f;

        /// <summary>
        /// The URL of my play store page
        /// </summary>
        private string _playStoreURL = "https://youtube.com/playlist?list=PLB-L02dDmOw_LyK2MANrhp-sQekSYccOq";
        
        /// <summary>
        /// The URL of my Instagram handle
        /// </summary>
        private string _instagamURL = "https://www.instagram.com/hk__labs/";

        #endregion

        #region Custom Methods

        //-------------------------------------Public Methods--------------------------------------------------//

        /// <summary>
        /// To be called from the Play Game Button from the inspector
        /// </summary>
        public void PlayGame ()
        {
            StartCoroutine (PlayGameDelay ());
        }

        /// <summary>
        /// To be called from the Quit Game Button from the inspector
        /// </summary>
        public void QuitGame ()
        {
            StartCoroutine (QuitGameDelay ());
        }

        /// <summary>
        /// This opens my play store page
        /// </summary>
        public void OpenPlayStorePage ()
        {
            Application.OpenURL (_playStoreURL);
        }

        /// <summary>
        /// This opens my Instagram page
        /// </summary>
        public void OpenInstagramPage ()
        {
            Application.OpenURL (_instagamURL);
        }

        public void OnPauseButton ()
        {
            Time.timeScale = 0;
        }

        /// <summary>
        /// The time scale needs to be set back to 1 if it has been previously set to 0 
        /// </summary>
        public void SetTimeScaleTo1 ()
        {
            Time.timeScale = 1;
        }

        public void OnMainMenu ()
        {
            SceneManager.LoadScene (0);
        }

        //-------------------------------------Private Methods--------------------------------------------------//

        /// <summary>
        /// This delay allows the button's animation to play through
        /// </summary>
        private IEnumerator PlayGameDelay ()
        {
            yield return new WaitForSeconds (_loadSequenceDelay);
            SceneManager.LoadScene (1);
        }

        /// <summary>
        /// This delay allows the button's animation to play through
        /// </summary>
        private IEnumerator QuitGameDelay ()
        {
            yield return new WaitForSeconds(_loadSequenceDelay);
            print ("QUIT GAME!!");
            Application.Quit ();
        }

        #endregion
    }
}