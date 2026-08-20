using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace LrwLib.UnityExcel.Editor
{
    [ScriptedImporter(version: 13, ext: "xlsx")]
    public class ExcelImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            string assetPath = ctx.assetPath;
            
            string path = Path.GetFullPath(Path.Combine(Application.dataPath, assetPath));
            Debug.Log(path);
            
            Excel excel = ScriptableObject.CreateInstance<Excel>();
            excel.TestText = "";
            
            ctx.AddObjectToAsset("data", excel);
            ctx.SetMainObject(excel);
            
        }
    }
}