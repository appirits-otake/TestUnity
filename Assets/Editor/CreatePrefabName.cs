using System.Collections;
using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

// バンドル名を管理する定数クラス作成
public static class CreatePrefabName{
	// 無効な文字を管理する配列
	private static readonly string[] INVALUD_CHARS =
	{
		" ", "!", "\"", "#", "$",
		"%", "&", "\'", "(", ")",
		"-", "=", "^",  "~", "\\",
		"|", "[", "{",  "@", "`",
		"]", "}", ":",  "*", ";",
		"+", "/", "?",  ".", ">",
		",", "<"
	};

	private const string ITEM_NAME  = "Tools/Create/PrefabName";    // コマンド名
	private const string PATH       = "Assets/Scripts/PrefabName.cs";        // ファイルパス

	private static readonly string FILENAME                     = Path.GetFileName(PATH);                   // ファイル名(拡張子あり)
	private static readonly string FILENAME_WITHOUT_EXTENSION   = Path.GetFileNameWithoutExtension(PATH);   // ファイル名(拡張子なし)


	//定数クラス作成
	[MenuItem(ITEM_NAME)]
	public static void Create(){

		if (!CanCreate())
		{
			return;
		}

		CreateScript();

		EditorUtility.DisplayDialog(FILENAME, "作成が完了しました", "OK");
	}

	// スクリプト作成
	public static void CreateScript(){
		var builder = new StringBuilder();

		builder.AppendFormat("public static class {0}", FILENAME_WITHOUT_EXTENSION).AppendLine();
		builder.AppendLine("{");

		foreach (var name in AssetDatabase.GetAllAssetBundleNames()){

			LoadAssetBundle(name,  builder);
		}

		builder.AppendLine("}");

		var directoryName = Path.GetDirectoryName(PATH);
		if (!Directory.Exists(directoryName)){

			Directory.CreateDirectory(directoryName);
		}

		File.WriteAllText(PATH, builder.ToString(), Encoding.UTF8);
		AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
	}

	//定数で管理するクラスを作成できるかどうかを取得します
	[MenuItem(ITEM_NAME, true)]
	public static bool CanCreate(){

		return !EditorApplication.isPlaying && !Application.isPlaying && !EditorApplication.isCompiling;
	}

	//無効な文字を削除します
	public static string RemoveInvalidChars(string str){

		Array.ForEach(INVALUD_CHARS, c => str = str.Replace(c, string.Empty));

		string strUp = str.ToUpper().Replace("/", "_");
		return strUp;
	}

	public static void LoadAssetBundle(string bundleName, StringBuilder builder){
		string bundlePath = Application.streamingAssetsPath + "/" + bundleName;


		Debug.Log (bundlePath);

		var request = AssetBundle.LoadFromFileAsync (bundlePath);


		AssetBundle bundle = request.assetBundle;

		foreach (var assetName in bundle.GetAllAssetNames()){

			Debug.Log (assetName);

			string name = Path.GetFileNameWithoutExtension(assetName);

			builder.Append("\t").AppendFormat("public const string {0} = \"{1}\";", RemoveInvalidChars(name), name).AppendLine();
		}

		bundle.Unload (true);
	}

}