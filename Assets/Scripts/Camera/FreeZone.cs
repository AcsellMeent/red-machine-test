using Connection;
using UnityEngine;
using Utils.Scenes;
using Utils.Singleton;

public class FreeZone : DontDestroyMonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();

        ScenesChanger.SceneLoadedEvent += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        ScenesChanger.SceneLoadedEvent -= OnSceneLoaded;
    }

    public void OnSceneLoaded()
    {
        Debug.Log(FindObjectOfType<ColorConnectionManager>());
    }
}
