using GameLokal.Utility.Event;
using UnityEngine;
using UnityEngine.UI;

public class ItemsView : MonoBehaviour, IEventListener<GameEvent>
{
    public GameObject itemContent;
    private InventoryDemo inv;

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
        if (eventType.eventName == "Items Updated")
        {
            RemoveAllChildren();
            SpawnContent();
        }
    }

    private void RemoveAllChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void SpawnContent()
    {
        foreach (var items in inv.Items)
        {
            Instantiate(itemContent, transform);
        }
    }
}