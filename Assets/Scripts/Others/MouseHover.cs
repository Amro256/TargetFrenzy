using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject[] arrows;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        foreach (GameObject arrow in arrows)
        {
            arrow.SetActive(false);
        }
    }



    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Debug.Log("Cursor Hovering Over " + gameObject);
        foreach (GameObject arrow in arrows)
        {
            arrow.SetActive(true);
        }
    }
    
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        foreach (GameObject arrow in arrows)
        {
            arrow.SetActive(false);
        }
    }
}
