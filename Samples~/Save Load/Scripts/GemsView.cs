using GameLokal.Utility.Event;
using UnityEngine;
using UnityEngine.UI;

public class GemsView : MonoBehaviour, IEventListener<GameEvent>
{

    private Text text;
    private InventoryDemo inv;

    private void Awake()
    {
        text = GetComponent<Text>();
    }
    
    private void OnEnable()
    {
        EventManager.AddListener(this);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(this);
    }

    private void Start()
    {
        inv = InventoryDemo.Instance;
    }

    public void OnEvent(GameEvent eventType)
    {
        if (eventType.eventName == "Gems Updated")
        {
            text.text = $"Gems : {inv.Gems}";
        }
    }
}