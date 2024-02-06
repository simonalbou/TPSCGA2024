using System.IO;
using UnityEditor;

public class TSVToCSVConverter : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        // vérifier que des assets ont bien été importés
        if (importedAssets == null) return;
        if (importedAssets.Length == 0) return;

        for (int i = 0; i < importedAssets.Length; i++)
        {
            // vérifier que l'asset actuellement traité est un TSV
            string path = importedAssets[i];
            if (!path.EndsWith(".tsv")) continue;

            // à ce stade-là, on est surs qu'on traite un .tsv
            path = path.Substring(0, path.Length-4);
            path += ".csv";

            File.Move(importedAssets[i], path);
            File.Move(importedAssets[i]+".meta", path+".meta");

            // changer le séparateur à volonté si besoin
            /**
            char separator = '$';
            string content = File.ReadAllText(path);
            content = content.Replace('\t', separator);
            File.WriteAllText(path, content);
            /**/
        }
    }
}
