using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EvolutionManager : MonoBehaviour
{
    public EssenceCollected essence_scriptable;
    [SerializeField] private TextMeshProUGUI essence_text;
    [SerializeField] GameObject evolution_panel;
    private int essence_collected = 0;
    [SerializeField] private GameObject wings;

    private void Awake()
    {
        essence_collected = essence_scriptable.essence_collected;
        essence_text.text = essence_collected.ToString();

    }

    public void add_essence()
    {
        essence_collected += 1;
        essence_scriptable.essence_collected = essence_collected;
        Debug.Log("current essence: " + essence_collected);
        essence_text.text = essence_collected.ToString();
    }

    public void consume_essence()
    {
        essence_collected -= 3;
        essence_scriptable.essence_collected = essence_collected;
        essence_text.text = essence_collected.ToString();
    }
    private void Update()
    {
        if(essence_collected >= 3)
        {
            evolution_panel.SetActive(true);
        }

        if(essence_collected <= 0)
        {
            essence_collected = 0;
            evolution_panel.SetActive(false);
        }

        if(evolution_panel.active == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void evolve_wings()
    {
        wings.SetActive(true);
    }
}
