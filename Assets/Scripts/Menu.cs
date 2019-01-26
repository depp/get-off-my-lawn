using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject cursor;
    private RectTransform cursorRect;
    public MenuItem[] items;
    private RectTransform[] itemRects;
    private int selection = -1;
    private BinaryInputAxis verticalAxis;

    private void InitCursor()
    {
        if (cursor == null)
        {
            Debug.LogError("Cursor is null");
            return;
        }
        cursorRect = cursor.GetComponent<RectTransform>();
        if (cursorRect == null)
        {
            Debug.LogError("Cursor has no RectTransform");
        }
    }

    private void InitItems()
    {
        itemRects = new RectTransform[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            MenuItem item = items[i];
            if (item == null)
            {
                Debug.LogErrorFormat("Menu item {0} is null", i);
                continue;
            }
            RectTransform rect = item.GetComponent<RectTransform>();
            if (rect == null)
            {
                Debug.LogErrorFormat("Menu item {0} ({1}) has no RectTransform", i, item.gameObject.name);
                continue;
            }
            itemRects[i] = rect;
        }
    }

    private void Awake()
    {
        InitCursor();
        InitItems();
        verticalAxis = new BinaryInputAxis("Vertical");
    }

    private void Start()
    {
        SelectItem(0);
    }

    private void SelectItem(int index)
    {
        if (selection == index)
            return;
        RectTransform ixform = itemRects[index];
        if (ixform != null && cursorRect != null)
        {
            Rect irect = ixform.rect;
            cursorRect.anchoredPosition = new Vector2(irect.xMin, 0.5f * (irect.yMin + irect.yMax)) + ixform.anchoredPosition;
        }
        selection = index;
    }

    void Update()
    {
        verticalAxis.Update();
        if (Input.GetButtonDown("Submit"))
        {
            Submit();
            return;
        }
        int move = verticalAxis.Value;
        if (move < 0)
        {
            int index = selection + 1;
            if (index >= items.Length)
                index = 0;
            SelectItem(index);
        }
        else if (move > 0)
        {
            int index = selection - 1;
            if (index < 0)
                index = items.Length - 1;
            SelectItem(index);
        }
    }

    private void Submit()
    {
        MenuItem item = this.items[selection];
        if (item == null)
        {
            Debug.LogError("Menu item is null");
            return;
        }
        item.ActivateMenuItem();
    }
}
