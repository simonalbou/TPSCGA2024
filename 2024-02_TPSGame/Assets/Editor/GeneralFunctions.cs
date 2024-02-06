using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class GeneralFunctions
{
    //[MenuItem("Tools/Simon/Browse Scene %w")]
    public static void BrowseScene()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        GameObject[] all = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        UnityEngine.Debug.Log($"In this Scene, there are {all.Length} GameObjects.");

        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");

        foreach(GameObject tree in trees)
        {
            Transform t = tree.transform;

            t.localScale = new Vector3(
                Random.Range(0.3f, 2f),
                Random.Range(1f, 10f),
                Random.Range(0.3f, 2f)
            );

            t.Translate(Vector3.right * Random.Range(-2f, 2f) + Vector3.forward * Random.Range(-2f, 2f));

            //EditorUtility.SetDirty(t); // sert à sauvegarder l'objet modifié. Inutile car à la fin de la boucle, on sauve carrément la scène (MarkAllScenesDirty)
        }

        UnityEngine.Debug.Log($"Successfully moved {trees.Length} trees.");

        EditorSceneManager.MarkAllScenesDirty();

        sw.Stop();
        UnityEngine.Debug.Log($"The whole operation took {sw.ElapsedTicks} ticks, which is {sw.ElapsedMilliseconds} milliseconds.");
    }
}
