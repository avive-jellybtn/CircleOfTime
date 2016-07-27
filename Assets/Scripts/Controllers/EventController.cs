using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace JellyJam.Events
{
    public enum JellyEventType
    {
        CollectPowerup,
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
            foreach (var item in _eventsDict)
            {
               if (item.Key == eventType)
                {
                    foreach (var e in item.Value)
                    {
                        e();
                    }
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
            Action eventToRemove = null;

            foreach (var item in _eventsDict)
            {
                if (_eventsDict.ContainsKey(eventType))
                {
                    foreach (var e in item.Value)
                    {
                        if (e == callback)
                        {
                            eventToRemove = e;
                        }
                    }
                }
            }

            if (eventToRemove != null)
            {
                Action a = () =>
                {
                    var events = _eventsDict[eventType];
                    events.Remove(eventToRemove);
                    _eventsDict[eventType] = events;
                };

                CoroutineController.instance.ExecuteOnEndOfFrame(a);
            }
        }
    }
}

