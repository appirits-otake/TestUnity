using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;


public class ExportAssetbundle  {

	[MenuItem("Export/Assetbundle")]
	static void Export()
	{
		Directory.CreateDirectory (Application.streamingAssetsPath);
		BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath, 
			BuildAssetBundleOptions.None, BuildTarget.StandaloneOSX);
	}
}
