using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public delegate void MyEvent(EventInfo eventInfo);

    Dictionary<System.Type, List<MyEvent>> myEventListener;


    private static EventSystem _Instance;
    public static EventSystem Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<EventSystem>();
            }
            return _Instance;
        }

    }

    public void RegisterEvent<T>(System.Action<T> action)  where T : EventInfo
    {
        System.Type key = typeof(T);
       
        
        if (myEventListener == null)
        {
            myEventListener = new Dictionary<System.Type, List<MyEvent>>();
        }
        if (!myEventListener.ContainsKey(key) || myEventListener[key] == null)
        {
            myEventListener[key] = new List<MyEvent>();
        }

        MyEvent wrapper = (info) => { action((T)info); };
        myEventListener[key].Add(wrapper);

         
    }

    public void UnRegisterEvent<T>(System.Action<T> action) where T: EventInfo
    {
        System.Type key = typeof(T);
        if (myEventListener == null || !myEventListener.ContainsKey(key)   || myEventListener[key] == null)
        {
            Debug.Log("already unregistered");
            return;
        }
        myEventListener[key].Clear();
        
    }

    public void FireEvent(EventInfo eventInfo)
    {
        System.Type key = eventInfo.GetType();

       
        if (myEventListener== null || !myEventListener.ContainsKey(key) || myEventListener[key] == null)
        {
            Debug.Log("listening to " + key.Name + " failed");
            Debug.Log("no listeners");
            return;
        }
        for (int i = 0; i < myEventListener[key].Count; i++)
        {
            myEventListener[key][i](eventInfo);
        }
    }
}

public interface EventInfo
{
    
}

