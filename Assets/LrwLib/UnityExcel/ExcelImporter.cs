using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;
using LrwLib.ButtonAttribute;

namespace LrwLib.UnityExcel
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
            excel.testText = "";
            
            ctx.AddObjectToAsset("data", excel);
            ctx.SetMainObject(excel);
            
        }
    }
}