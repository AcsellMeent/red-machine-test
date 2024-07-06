using System;
using Connection;
using UnityEngine;
using Utils.Scenes;
using Utils.Singleton;

public class LevelBounds : DontDestroyMonoBehaviourSingleton<LevelBounds>
{
    public event Action<Vector2, Vector2> ChangeLevelBounds;

    private Vector2 min;
    private Vector2 max;

    private void Start()
    {
        ScenesChanger.SceneLoadedEvent += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        ScenesChanger.SceneLoadedEvent -= OnSceneLoaded;
    }

    private void OnSceneLoaded()
    {
        ColorNode[] nodes = FindObjectsOfType<ColorNode>();

        //Определение границ уровня
        for (int i = 0; i < nodes.Length; i++)
        {
            min.x = nodes[i].transform.position.x < min.x ? nodes[i].transform.position.x : min.x;
            min.y = nodes[i].transform.position.y < min.y ? nodes[i].transform.position.y : min.y;
            max.x = nodes[i].transform.position.x > max.x ? nodes[i].transform.position.x : max.x;
            max.y = nodes[i].transform.position.y > max.y ? nodes[i].transform.position.y : max.y;
        }
        ChangeLevelBounds?.Invoke(min, max);
    }
}
