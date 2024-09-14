using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public void OnPointerEnter(PointerEventData _)
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    public void OnPointerExit(PointerEventData _)
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
