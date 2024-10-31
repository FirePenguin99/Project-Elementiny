using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartAsClassBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameStateHandler.classes thisComponentsClass;

    public void OnPointerDown(PointerEventData eventData)
    {
        // spawn player as class thisComponentsClass
        GameStateHandler.instance.SpawnPlayerAsClass(thisComponentsClass);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
    }
}
