using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler {
	public Transform parentToReturn = null;


	public void OnBeginDrag (PointerEventData eventData){
		//Debug.Log ("Begin Drag");
		parentToReturn = transform.parent;
		transform.SetParent (transform.parent.parent);

		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}
	public void OnDrag (PointerEventData eventData){
		//Debug.Log ("Draggng");
		this.transform.position = eventData.position;
	}
	public void OnEndDrag (PointerEventData eventData){
		transform.SetParent (parentToReturn);
		//Debug.Log ("End Drag");
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}
}
