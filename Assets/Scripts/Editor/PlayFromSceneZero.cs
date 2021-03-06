﻿using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class PlayFromSceneZero : Editor
{
    const string MENU_PATH = "Game/Play From Scene 0 &p";
    static bool playFromFirstScene
    {
        get { return EditorPrefs.HasKey(MENU_PATH) && EditorPrefs.GetBool(MENU_PATH); }
        set { EditorPrefs.SetBool(MENU_PATH, value); }
    }

    [MenuItem(MENU_PATH, false, 150)]
    static void PlayFromFirstSceneCheckMenu()
    {
        playFromFirstScene = !playFromFirstScene;
        Menu.SetChecked(MENU_PATH, playFromFirstScene);
        ShowNotifyOrLog(playFromFirstScene ? "Play from Scene 0" : "Play from Current Scene");
    }

    // The menu won't be gray out, we use this validate method for update check state
    [MenuItem(MENU_PATH, true)]
    static bool PlayFromFirstSceneCheckMenuValidate()
    {
        Menu.SetChecked(MENU_PATH, playFromFirstScene);
        return true;
    }

    // This method is called before any Awake. It's the perfect callback for this feature
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void LoadFirstSceneAtGameBegins()
    {
        if (!playFromFirstScene) return;

        if (EditorBuildSettings.scenes.Length == 0)
        {
            Debug.LogWarning("The scene build list is empty. Can't play from first scene.");
            return;
        }

        foreach (GameObject go in Object.FindObjectsOfType<GameObject>())
            go.SetActive(false);

        SceneManager.LoadScene(0);
    }

    static void ShowNotifyOrLog(string msg)
    {
        if (Resources.FindObjectsOfTypeAll<SceneView>().Length > 0)
            EditorWindow.GetWindow<SceneView>().ShowNotification(new GUIContent(msg));
        else
            Debug.Log(msg); // When there's no scene view opened, we just print a log
    }
}