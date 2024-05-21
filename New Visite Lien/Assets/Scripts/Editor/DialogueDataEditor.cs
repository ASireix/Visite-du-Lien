using UnityEngine;
using UnityEditor;
using System.IO;

public class DialogueDataEditor : EditorWindow
{
    [MenuItem("Assets/Create Dialogue Text Assets")]
    static void CreateDialogueTextAssets()
    {
        // Récupérer le dossier sélectionné dans le Project view
        string selectedFolderPath = AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID());
        if (!AssetDatabase.IsValidFolder(selectedFolderPath))
        {
            Debug.LogError("Please select a valid folder in the Project view.");
            return;
        }

        // Recherche tous les ScriptableObject de type DialogueData dans le dossier sélectionné
        string[] guids = AssetDatabase.FindAssets("t:DialogueData", new[] { selectedFolderPath });
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            DialogueData dialogueData = AssetDatabase.LoadAssetAtPath<DialogueData>(assetPath);

            // Créer un fichier TextAsset pour chaque DialogueData
            if (!dialogueData.textFile)
            {
                TextAsset textAsset = new TextAsset();
                textAsset.name = dialogueData.name + "_TEXTFILE";
                string textAssetPath = Path.Combine(selectedFolderPath, textAsset.name + ".txt");
                AssetDatabase.CreateAsset(textAsset, textAssetPath);

                // Assigner le fichier TextAsset au DialogueData
                dialogueData.textFile = textAsset;

                // Sauvegarder les changements
                EditorUtility.SetDirty(dialogueData);
                AssetDatabase.SaveAssets();
            }
        }

        // Déplacer tous les fichiers TextAsset à la racine du dossier
        string[] textAssetPaths = AssetDatabase.FindAssets("t:TextAsset", new[] { selectedFolderPath });
        foreach (string textAssetPath in textAssetPaths)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(textAssetPath);
            string fileName = Path.GetFileName(assetPath);
            string newPath = Path.Combine(selectedFolderPath, fileName);
            AssetDatabase.MoveAsset(assetPath, newPath);
        }

        Debug.Log("Dialogue Text Assets created and moved successfully.");
    }
}