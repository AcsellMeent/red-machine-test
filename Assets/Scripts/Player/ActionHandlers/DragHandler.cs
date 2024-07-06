using System;
using Camera;
using Connection;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils.Scenes;
using Utils.Singleton;


namespace Player.ActionHandlers
{
    public class DragHandler : DontDestroyMonoBehaviourSingleton<DragHandler>
    {
        public event Action<Vector3> DragStartEvent;
        public event Action<Vector3> DragEndEvent;

        private ColorConnectionManager _colorConnectionManager;

        private void Start()
        {
            _colorConnectionManager = FindObjectOfType<ColorConnectionManager>();

            ScenesChanger.SceneLoadedEvent += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            ScenesChanger.SceneLoadedEvent -= OnSceneLoaded;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 position = CameraHolder.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                //Проверка на пустое пространство
                if (!EventSystem.current.IsPointerOverGameObject() && !_colorConnectionManager.TryGetColorNodeInPosition(position, out var colorNode))
                {
                    DragStartEvent?.Invoke(position);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                DragEndEvent?.Invoke(CameraHolder.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition));
            }
        }

        private void OnSceneLoaded()
        {
            _colorConnectionManager = FindObjectOfType<ColorConnectionManager>();
        }
    }
}
