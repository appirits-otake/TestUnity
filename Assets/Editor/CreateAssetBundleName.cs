using System.Collections;
using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

// バンドル名を管理する定数クラス作成
public class CreateAssetBundleName{
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

	private const string ITEM_NAME  = "Tools/Create/AssetBundleName";    // コマンド名
	private const string PATH       = "Assets/Scripts/AssetBundleName.cs";        // ファイルパス

	private static readonly string FILENAME                     = Path.GetFileName(PATH);                   // ファイル名(拡張子あり)
	private static readonly string FILENAME_WITHOUT_EXTENSION   = Path.GetFileNameWithoutExtension(PATH);   // ファイル名(拡張子なし)


	//アセットバンドル名定数クラス作成
	[MenuItem(ITEM_NAME)]
	public static void Create(){

		if (!CanCreate())
		{
			return;
		}

		CreateScript();

		EditorUtility.DisplayDialog(FILENAME, "作成が完了しました", "OK");
	}
		
	// アセットバンドル名定数クラスのスクリプト作成
	public static void CreateScript(){
		var builder = new StringBuilder();

		builder.AppendFormat("public static class {0}", FILENAME_WITHOUT_EXTENSION).AppendLine();
		builder.AppendLine("{");

		foreach (var name in AssetDatabase.GetAllAssetBundleNames()){

			Debug.Log (name);

			builder.Append("\t").AppendFormat("public const string {0} = \"{1}\";", RemoveInvalidChars(name), name).AppendLine();
		}

		builder.AppendLine("}");

		var directoryName = Path.GetDirectoryName(PATH);
		if (!Directory.Exists(directoryName)){
			
			Directory.CreateDirectory(directoryName);
		}

		File.WriteAllText(PATH, builder.ToString(), Encoding.UTF8);
	
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

	[MenuItem("Export/Test")]
	public static void Test(){

		foreach (var path in Directory.GetFiles("Assets/Prefabs")) {
			
		}

	}
}