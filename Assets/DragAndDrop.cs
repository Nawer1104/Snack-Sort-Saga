using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] GameObject particleVFX;

    private bool _dragging;

    private Vector2 _offset;

    public static bool mouseButtonReleased;

    private void OnMouseDown()
    {
        _dragging = true;

        _offset = GetMousePos() - (Vector2)transform.position;
    }

    private void OnMouseDrag()
    {
        if (!_dragging) return;

        var mousePosition = GetMousePos();

        transform.position = mousePosition - _offset;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _dragging = false;
        if (gameObject.tag == collision.gameObject.tag)
        {
            VendingGameManager.Instance.levels[VendingGameManager.Instance.GetCurrentIndex()].items.Remove(this);
            VendingGameManager.Instance.levels[VendingGameManager.Instance.GetCurrentIndex()].items.Remove(collision.gameObject.GetComponent<DragAndDrop>());
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GameObject explosion = Instantiate(particleVFX, transform.position, transform.rotation);
            Destroy(explosion, .75f);
        }
    }

    private void OnMouseUp()
    {
        mouseButtonReleased = true;
    }

    private Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}
