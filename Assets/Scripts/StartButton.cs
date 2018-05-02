using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	public StackAssetObjects _stackObjects;

	public void GameStart () {

		_stackObjects.BundlesUnload ();

		SceneManager.LoadScene(SceneName.MAIN);
	}

}
