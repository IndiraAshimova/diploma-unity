using UnityEngine;

public class YurtPiece : MonoBehaviour
{
    public YurtPart partType;

    private Vector3 startPos;
    private bool dragging;

    private SpriteRenderer sr;

    private void Start()
    {
        startPos = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;

        if (PuzzleManager.Instance.IsCorrectPart(partType))
        {
            PuzzleManager.Instance.PlacePiece(this);
        }
        else
        {
            StartCoroutine(WrongPiece());
        }
    }

    private void Update()
    {
        if (dragging)
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0;
            transform.position = mouse;
        }
    }

    System.Collections.IEnumerator WrongPiece()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.5f);

        sr.color = Color.white;
        transform.position = startPos;
    }
}