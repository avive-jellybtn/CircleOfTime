using System.Collections;
using System.Collections.Generic;
using System;

namespace JellyJam.Events
{
    public enum JellyEventType
    {
        CollectGun,
        GameEnd,
        GameOver,
        EnemyDead,
        PlayerCollision
    }

    public static class JellyEventController
    {
        private static Dictionary<JellyEventType, List<Action>> _eventsDict;

        static JellyEventController()
        {
            _eventsDict = new Dictionary<JellyEventType, List<Action>>();
            foreach (JellyEventType jellyEvent in Enum.GetValues(typeof(JellyEventType)))
            {
                _eventsDict.Add(jellyEvent, new List<Action>());
            }
        }

        public static void FireEvent(JellyEventType eventType)
        {
            if (_eventsDict.ContainsKey(eventType))
            {
                for (int i = _eventsDict[eventType].Count - 1; i >= 0; i--)
                {
                    _eventsDict[eventType][i]();
                }
            }
        }

        public static void SubscribeEvent(JellyEventType eventType, Action callback)
        {
            if (_eventsDict.ContainsKey(eventType))
            {
                _eventsDict[eventType].Add(callback);
            }
        }

        public static void UnsubscribeEvent(JellyEventType eventType, Action callback)
        {
            if (!_eventsDict.ContainsKey(eventType))
            {
                return;
            }
            _eventsDict[eventType].Remove(callback);
            //for (int i = _eventsDict[eventType].Count - 1; i >= 0; i--)
            //{
            //    if (_eventsDict[eventType][i] == callback)
            //    {
            //        _eventsDict[eventType].RemoveAt(i);
            //    }
            //}
        }
    }
}
