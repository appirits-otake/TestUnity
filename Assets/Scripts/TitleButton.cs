using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour {

	public StackAssetObjects _stackObjects;

	// Use this for initialization
	public void ChangeTitle () {

		_stackObjects.BundlesUnload ();

		SceneManager.LoadScene(SceneName.TITLE);
	}
}
