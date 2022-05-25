using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Clicker : MonoBehaviour
{
    public UnityEvent upEvent;
    public UnityEvent downEvent;

    private void OnMouseDown()
    {
        Debug.Log("Down");
        downEvent?.Invoke();
    }
    private void OnMouseUp()
    {
        Debug.Log("Up");
        upEvent?.Invoke();
    }
}
