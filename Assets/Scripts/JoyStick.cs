using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IEndDragHandler
{

    public static JoyStick instance = null;

    public float dragSpeed = 6f;

    private float screen;

    private Vector2 centerPos;

    private float horizon;
    private float vertical;
    public Vector2 CenterPos
    {
        set { centerPos = value; }
    }

    public float Horizon
    {
        get { return horizon; }
    }
    public float Vertical
    {
        get { return vertical; }
    }

    void Awake()
    {

        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

    }
    void Start()
    {
        screen = 1f * Screen.width / 40; 
        centerPos = GetComponent<RectTransform>().position;
    }
    void GetAxis(Vector2 pos)
    { 
        Vector2 diff = pos - centerPos;
        float distance = diff.magnitude;
        if (distance > screen)
        {
            pos = centerPos + diff / distance * screen;

        }
        GetComponent<RectTransform>().position = pos;
        Vector2 move = pos - centerPos;
        horizon = move.x;
        vertical = move.y;
    }

    public void OnDrag(PointerEventData data)
    {
        Vector2 newPos = new Vector2(data.position.x - 20f, data.position.y - 20f);
        GetAxis(newPos);
    }

    public void OnEndDrag(PointerEventData data)
    {
        GetComponent<RectTransform>().position = centerPos;
        GetAxis(centerPos);
    }

}