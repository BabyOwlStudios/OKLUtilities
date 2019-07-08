using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler
{
    public Canvas canvas;
    public RectTransform panel, canvasPanel;

    public void OnDrag(PointerEventData eventData)
    {
        float position = eventData.position.y;
        UpdatePanel(position);
    }

    private void UpdatePanel(float yPosition)
    {
        Rect panel = this.panel.rect;
        this.panel.sizeDelta = new Vector2(0f, Mathf.Clamp(yPosition, 100f, canvasPanel.sizeDelta.y));
    }
}
