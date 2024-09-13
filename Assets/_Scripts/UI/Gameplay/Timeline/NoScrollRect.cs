using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoScrollRect : ScrollRect
{
    public override void OnBeginDrag(PointerEventData _) { }
    public override void OnDrag(PointerEventData _) { }
    public override void OnEndDrag(PointerEventData _) { }
}
