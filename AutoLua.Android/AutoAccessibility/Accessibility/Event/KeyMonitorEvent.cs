﻿using System;
using System.Collections.Generic;
using Android.Views;
using Xamarin.Forms;

namespace AutoLua.Droid.AutoAccessibility.Accessibility.Event
{
    [Android.Runtime.Preserve(AllMembers = true)]
    public class KeyMonitorEvent : IKeyMonitorEvent
    {
        /// <summary>
        /// 监听按键的缓存
        /// </summary>
        private readonly IList<IKeyMonitorEvent> keyMonitorEvents = new List<IKeyMonitorEvent>();

        /// <summary>
        /// 添加按键监听事件。
        /// </summary>
        /// <param name="event"></param>
        public void Add(IKeyMonitorEvent @event)
        {
            if (@event == null)
                return;

            keyMonitorEvents.Add(@event);
        }

        /// <summary>
        /// 移除按键监听事件。
        /// </summary>
        /// <param name="event"></param>
        public void Remove(IKeyMonitorEvent @event)
        {
            if (@event == null)
                return;

            keyMonitorEvents.Remove(@event);
        }

        public void OnKeyEvent(Keycode keyCode, KeyEvent @event)
        {
            foreach (var item in keyMonitorEvents)
            {
                try
                {
                    item.OnKeyEvent(keyCode, @event);
                }
                catch (Exception)
                {
                    AppApplication.OnLog("异常", $"Error OnKeyEvent: {@event.ToString()} Listener: {item.ToString()}", Color.Red);
                }
            }
        }
    }
}