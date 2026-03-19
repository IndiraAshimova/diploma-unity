using UnityEngine;

public class DraggableSprite : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 offset;
    private bool isDragging = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset.z = 0;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (var hit in hits)
        {
            DropZoneSprite dropZone = hit.GetComponent<DropZoneSprite>();
            if (dropZone != null)
            {
                Debug.Log($"{name} отпущен на {dropZone.name}");
                if (dropZone.Accepts(this))
                {
                    dropZone.OnCorrectDrop(this);
                    return; // ✅ больше не продолжаем
                }
                else
                {
                    dropZone.OnWrongDrop();
                    transform.position = startPosition; // возвращаем на старт
                    return;
                }
            }
        }

        // Мимо всех DropZone
        transform.position = startPosition;
    }
}