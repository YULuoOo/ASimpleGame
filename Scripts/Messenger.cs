using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

internal static class Messenger  {

    public static Dictionary<GameEvent, Delegate> mEventTable = new Dictionary<GameEvent, Delegate>();

    private static bool OnAddListener(GameEvent eventType, Delegate handler)
    {
        if(!mEventTable.ContainsKey(eventType))
        {
            mEventTable.Add(eventType, null);
        }
        Delegate d = mEventTable[eventType];
        if(d != null && d.GetType() != handler.GetType())
        {
            Debug.LogError(eventType+ "加入监听回调与当前监听类型不符合,当前类型为" + d.GetType().Name + "加入类型为" + handler.GetType().Name);
            return false;
        }
        return true;
    }

    private static bool OnRemoveListener(GameEvent eventType, Delegate handler)
    {
        if(mEventTable.ContainsKey(eventType))
        {
            Delegate d = mEventTable[eventType];
            if(d == null)
            {
                Debug.LogError("试图移除" + eventType + ",但当前监听为空");
                return false;
            }
            else if(d.GetType() != handler.GetType())
            {
                Debug.LogError("试图移除" + eventType + ",与当前类型不符合，当前类型" + d.GetType().Name);
                return false;
            }
        }
        else
        {
            Debug.LogError("Messenger不包含要移除的对象" + eventType);
            return false;
        }
        return true;
    }

    //添加监听
    public static void AddListener(GameEvent eventType, CallBack handler)
    {
        if(!OnAddListener(eventType,handler))
        {
            return;
        }
        mEventTable[eventType] = (CallBack)mEventTable[eventType] + handler;
    }

    public static void AddListener<T>(GameEvent eventType, CallBack<T> handler)
    {
        if(!OnAddListener(eventType,handler))
        {
            return;
        }
        mEventTable[eventType] = (CallBack<T>)mEventTable[eventType] + handler;
    }


    //移除监听
    public static void RemoveListener(GameEvent eventType, CallBack handler)
    {
        if(!OnRemoveListener(eventType,handler))
        {
            return;
        }
        mEventTable[eventType] = (CallBack)mEventTable[eventType] - handler;
        if(mEventTable[eventType] == null)
        {
            mEventTable.Remove(eventType);
        }
    }

    public static void RemoveListener<T>(GameEvent eventType, CallBack<T> handler)
    {
        if (!OnRemoveListener(eventType, handler))
        {
            return;
        }
        mEventTable[eventType] = (CallBack<T>)mEventTable[eventType] - handler;
        if (mEventTable[eventType] == null)
        {
            mEventTable.Remove(eventType);
        }
    }


    //广播监听

    public static void Broadcast(GameEvent eventType)
    {
        if(!mEventTable.ContainsKey(eventType))
        {
            return;
        }
        Delegate d;
        if(mEventTable.TryGetValue(eventType,out d))
        {
            CallBack callback = d as CallBack;
            if(callback != null)
            {
                callback();
            }
            else
            {
                Debug.LogError("广播" + eventType + "为空");
            }
        }

    }

    public static void Broadcast<T>(GameEvent eventType, T arg1)
    {
        if (!mEventTable.ContainsKey(eventType))
        {
            return;
        }
        Delegate d;
        if (mEventTable.TryGetValue(eventType, out d))
        {
            CallBack<T> callback = d as CallBack<T>;
            if (callback != null)
            {
                callback(arg1);
            }
            else
            {
                Debug.LogError("广播" + eventType + "为空");
            }
        }
    }
}
