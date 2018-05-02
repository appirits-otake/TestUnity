using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class StackObjects : MonoBehaviour {

	public Dictionary<string, AssetBundle> _assetBundles = new Dictionary<string, AssetBundle> ();

	public static Dictionary<string, GameObject> _stackObjects = new Dictionary<string, GameObject> ();


	void Start(){

		if (_stackObjects.ContainsKey ("D")) return;

		StartCoroutine ("AllLoadAssetBundle");

	}

	public static Dictionary<string, GameObject> GetStackObjects(){

		return _stackObjects;
	}
		
	IEnumerator  AllLoadAssetBundle(){
		string abPrefabName = "ab_prefab";
		string cPrefabName = "c_prefab";
		string dPrefabName = "d_prefab";

		StartCoroutine("LoadAssetBundle", abPrefabName);

		while (!_assetBundles.ContainsKey(abPrefabName)) {
			yield return null;
		}

		StartCoroutine("LoadAssetBundle", cPrefabName);

		while (!_assetBundles.ContainsKey(cPrefabName)) {
			yield return null;
		}

		StartCoroutine("LoadAssetBundle", dPrefabName);

		while (!_assetBundles.ContainsKey(dPrefabName)) {
			yield return null;
		}

		StackObjectSet ();

		BundlesUnload ();
	}


	IEnumerator LoadAssetBundle(string bundleName){
		string bundlePath = Application.streamingAssetsPath + "/" + bundleName;
		var request = AssetBundle.LoadFromFileAsync (bundlePath);

		while (!request.isDone) {
			yield return null;
		}

		AssetBundle bundle = request.assetBundle;
		_assetBundles.Add (bundle.name, bundle);
	}



	GameObject GetAssetBundleObject(string bundleName, string objectName){

		AssetBundle assetBundle = _assetBundles[bundleName];

		if (assetBundle == null) return null;

		return assetBundle.LoadAsset<GameObject> (objectName);

	}

	void StackObjectSet(){
		
		_stackObjects.Add ("A", GetAssetBundleObject ("ab_prefab", "A"));
		_stackObjects.Add ("B", GetAssetBundleObject ("ab_prefab", "B"));
		_stackObjects.Add ("C", GetAssetBundleObject ("c_prefab", "C"));
		_stackObjects.Add ("D", GetAssetBundleObject ("d_prefab", "D"));

	}

	public void BundlesUnload(){

		foreach(KeyValuePair<string, AssetBundle> pair in _assetBundles){
			_assetBundles [pair.Key].Unload (false);
		}

		_assetBundles.Clear ();
	}

	public GameObject GetObject(string name){

		return _stackObjects [name];
	}
		
}
