using System;
using GameLokal.Utility.Event;
using UnityEngine;
using UnityEngine.UI;

public class CoinsView : MonoBehaviour, IEventListener<GameEvent>
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
        if (eventType.eventName == "Coins Updated")
        {
            text.text = $"Coins : {inv.Coin}";
        }
    }
}
