using Connection;
using Player.ActionHandlers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Utils.Scenes;
using Utils.Singleton;

namespace Camera
{
    public class CameraMovement : DontDestroyMonoBehaviour
    {
        private DragHandler _dragHandler;
        private Transform _cameraTransform;
        private Vector3 _startDragPosition;

        private Vector2 minBound;
        private Vector2 maxBound;

        [SerializeField]
        private Vector3 _startCameraPosition;

        [SerializeField]
        private float _horizontalPadding;

        [SerializeField]
        private float _verticalPadding;

        private bool mouseDrag;

        protected override void Awake()
        {
            base.Awake();
            _dragHandler = DragHandler.Instance;

            _dragHandler.DragStartEvent += OnDragStart;
            _dragHandler.DragEndEvent += OnDragEnd;

            LevelBounds.Instance.ChangeLevelBounds += OnLevelBoundsChanged;

            ScenesChanger.SceneLoadedEvent += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            _dragHandler.DragStartEvent -= OnDragStart;
            _dragHandler.DragEndEvent -= OnDragEnd;

            ScenesChanger.SceneLoadedEvent -= OnSceneLoaded;
        }

        private void LateUpdate()
        {
            if (mouseDrag)
            {
                //Перемещение камеры при помощи перетаскивания
                Vector3 delta = CameraHolder.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition) - _cameraTransform.position;
                Vector3 cameraPosition = new Vector3(Mathf.Clamp((_startDragPosition - delta).x, minBound.x, maxBound.x), Mathf.Clamp((_startDragPosition - delta).y, minBound.y, maxBound.y), (_startDragPosition - delta).z);
                _cameraTransform.position = cameraPosition;
            }
        }

        private void OnDragStart(Vector3 position)
        {
            _startDragPosition = position;
            mouseDrag = true;
        }

        private void OnDragEnd(Vector3 position)
        {
            mouseDrag = false;
        }

        private void OnLevelBoundsChanged(Vector2 min, Vector2 max)
        {
            minBound = min + new Vector2(-_horizontalPadding, -_verticalPadding);
            maxBound = max + new Vector2(_horizontalPadding, _verticalPadding);
        }

        private void OnSceneLoaded()
        {
            //Обновление камеры
            _cameraTransform ??= CameraHolder.Instance.MainCamera.transform;
            _cameraTransform.position = _startCameraPosition;
        }

    }
}