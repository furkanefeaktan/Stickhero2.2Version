using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
    public bool DraggingAllowed = true;
    public bool IsDragging = false;
    private Vector3 _offset;
    private Rigidbody2D _rb;
    
    public bool IsPlayerInRoom = false;
    private Vector3 _dragRoomPosition;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        if(!DraggingAllowed)
        return;

        _startPosition = transform.position;
        _rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        IsDragging = true;
        _offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _rb.bodyType = RigidbodyType2D.Kinematic;
    }


    private void OnMouseUp()
    {
        _rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        IsDragging = false;
        _rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void FixedUpdate()
    {
        if (IsDragging)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
            _rb.MovePosition(curPosition);
        }
        else
        {

           if(IsPlayerInRoom)
           {
             transform.position = new Vector3(_dragRoomPosition.x, _dragRoomPosition.y, 0);
           }
           else
           {
             transform.position = _startPosition;
           }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Room"))
        {
            IsPlayerInRoom = true;
            _dragRoomPosition = other.transform.position;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Room"))
        {
            IsPlayerInRoom = false;
        }
    }
}