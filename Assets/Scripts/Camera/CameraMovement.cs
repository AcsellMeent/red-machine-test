using Player.ActionHandlers;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private ClickHandler _clickHandler;

    private void Awake()
    {
        _clickHandler = ClickHandler.Instance;

        _clickHandler.DragStartEvent += OnDragStart;
    }

    private void OnDestroy()
    {
        _clickHandler.DragStartEvent -= OnDragStart;
    }

    private void OnDragStart(Vector3 position)
    {
        Debug.Log(position);
    }

}
