using System;
using Connection;
using UnityEngine;
using Utils.Scenes;
using Utils.Singleton;

namespace Player.ActionHandlers
{
    public class NearBounds : DontDestroyMonoBehaviourSingleton<NearBounds>
    {
        private ColorConnectionManager _colorConnectionManager;

        public event Action<Vector2> ReachBounds;

        private bool _nodeDrag;

        [SerializeField]
        [Range(0, 100)]
        private float threshold = 50;

        private void Start()
        {
            _colorConnectionManager = FindObjectOfType<ColorConnectionManager>();

            ScenesChanger.SceneLoadedEvent += OnSceneLoaded;

            DragHandler.Instance.NodeDragStartEvent += OnNodeDragStart;
            DragHandler.Instance.NodeDragEndEvent += OnNodeDragEnd;
        }

        private void OnDestroy()
        {
            ScenesChanger.SceneLoadedEvent -= OnSceneLoaded;

            DragHandler.Instance.NodeDragStartEvent -= OnNodeDragStart;
            DragHandler.Instance.NodeDragEndEvent -= OnNodeDragEnd;
        }

        private void Update()
        {
            if (_nodeDrag)
            {
                Vector2 direction = new Vector2();
                if (Input.mousePosition.x > Screen.width - threshold || Input.mousePosition.x < threshold)
                {
                    direction.x = Input.mousePosition.x > Screen.width / 2 ? 1 : -1;
                }
                if (Input.mousePosition.y > Screen.height - threshold || Input.mousePosition.y < threshold)
                {
                    direction.y = Input.mousePosition.y > Screen.height / 2 ? 1 : -1;
                }
                ReachBounds?.Invoke(direction);
            }
        }

        private void OnNodeDragStart(Vector3 position)
        {
            _nodeDrag = true;
        }
        private void OnNodeDragEnd(Vector3 position)
        {
            _nodeDrag = false;
        }

        private void OnSceneLoaded()
        {
            _colorConnectionManager = FindObjectOfType<ColorConnectionManager>();
        }
    }
}