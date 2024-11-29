using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BaseUI : MonoBehaviour
{
    private Dictionary<string, GameObject> _objDic;
    private Dictionary<(string, System.Type), Component> _componentDic;

    protected void Bind()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>(true);
        _objDic = new Dictionary<string, GameObject>(transforms.Length << 2);

        foreach (Transform child in transforms)
        {
            _objDic.TryAdd(child.gameObject.name, child.gameObject);
        }

        _componentDic = new Dictionary<(string, System.Type), Component>();
    }

    protected void BindAll()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>(true);
        _objDic = new Dictionary<string, GameObject>(transforms.Length << 2);

        foreach (Transform child in transforms)
        {
            _objDic.TryAdd(child.gameObject.name, child.gameObject);
        }

        Component[] components = GetComponentsInChildren<Component>(true);
        _componentDic = new Dictionary<(string, System.Type), Component>(components.Length << 4);

        foreach (Component child in components)
        {
            _componentDic.TryAdd((child.gameObject.name, components.GetType()), child);
        }
    }

    public GameObject GetUI(in string name)
    {
        _objDic.TryGetValue(name, out GameObject obj);
        return obj;
    }

    public T GetUI<T>(in string name) where T : Component
    {
        (string, System.Type) key = (name, typeof(T));

        _componentDic.TryGetValue(key, out Component component);
        if (component != null) { return component as T; }

        _objDic.TryGetValue(name, out GameObject obj);
        if (obj == null) {  return null; }

        component = obj.GetComponent<T>();
        if (component == null) { return null; }

        _componentDic.TryAdd(key, component);
        return component as T;
    }

    public enum EventType { Click, Enter, Exit, Up, Down, Move, BeginDrag, EndDrag, Drag, Drop }

    public void AddEvent(in string name, EventType eventType, UnityAction<PointerEventData> callback)
    {
        _objDic.TryGetValue(name, out GameObject obj);
        if (obj == null) { return; }

        EventReceiver receiver = gameObject.GetOrAddComponent<EventReceiver>();

        switch (eventType)
        {
            case EventType.Click:
                receiver.OnClicked += callback;
                break;

            case EventType.Enter:
                receiver.OnEntered += callback;
                break;

            case EventType.Exit:
                receiver.OnExited += callback;
                break;

            case EventType.Up:
                receiver.OnUped += callback;
                break;

            case EventType.Down:
                receiver.OnDowned += callback;
                break;

            case EventType.Move:
                receiver.OnMoved += callback;
                break;

            case EventType.BeginDrag:
                receiver.OnBeginDraged += callback;
                break;

            case EventType.EndDrag:
                receiver.OnEndDraged += callback;
                break;

            case EventType.Drag:
                receiver.OnDraged += callback;
                break;

            case EventType.Drop:
                receiver.OnDroped += callback;
                break;
        }
    }

    public void RemoveEvent(in string name, EventType eventType, UnityAction<PointerEventData> callback)
    {
        _objDic.TryGetValue(name, out GameObject obj);
        if (obj == null) { return; }

        EventReceiver receiver = gameObject.GetOrAddComponent<EventReceiver>();

        switch (eventType)
        {
            case EventType.Click:
                receiver.OnClicked -= callback;
                break;

            case EventType.Enter:
                receiver.OnEntered -= callback;
                break;

            case EventType.Exit:
                receiver.OnExited -= callback;
                break;

            case EventType.Up:
                receiver.OnUped -= callback;
                break;

            case EventType.Down:
                receiver.OnDowned -= callback;
                break;

            case EventType.Move:
                receiver.OnMoved -= callback;
                break;

            case EventType.BeginDrag:
                receiver.OnBeginDraged -= callback;
                break;

            case EventType.EndDrag:
                receiver.OnEndDraged -= callback;
                break;

            case EventType.Drag:
                receiver.OnDraged -= callback;
                break;

            case EventType.Drop:
                receiver.OnDroped -= callback;
                break;
        }
    }
}