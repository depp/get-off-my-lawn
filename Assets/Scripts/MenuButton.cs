using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButton : MenuItem
{
    Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public override void ActivateMenuItem()
    {
        button.onClick.Invoke();
    }
}
