using UnityEngine;
using System.Collections;
using System.IO;

namespace HKLabs
{
    public class ShareButton : MonoBehaviour
    {
		private string _setText = "Can you beat my high score? Download the game: ";
		private string _playStoreLink = "https://play.google.com/store/apps/details?id=com.HKLabs.DemonRun";


		public void ShareScreenShot ()
        {
			StartCoroutine (TakeScreenshotAndShare ());
        }

		private IEnumerator TakeScreenshotAndShare()
		{
			yield return new WaitForEndOfFrame();

			Texture2D ss = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
			ss.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0 );
			ss.Apply();

			string filePath = Path.Combine( Application.temporaryCachePath, "shared img.png" );
			File.WriteAllBytes( filePath, ss.EncodeToPNG() );

			// To avoid memory leaks
			Destroy( ss );

			new NativeShare().AddFile( filePath )
				.SetSubject( "Subject goes here" ).SetText( _setText + _playStoreLink)
				.SetCallback( ( result, shareTarget ) => Debug.Log( "Share result: " + result + ", selected app: " + shareTarget ) )
				.Share();
		}
    }
}