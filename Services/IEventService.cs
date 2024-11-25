﻿using System;

namespace Framework.Yggdrasil.Services
{
    public abstract class EventType<T> where T : EventArgs
    {
        public EventHandler<T> handler;

        public virtual void OnAdd()
        {
        }

        public virtual void OnRemove()
        {
        }
    }

    public interface IEventService : IService
    {
        void Raise<T>(object sender, T args) where T : EventArgs;
        void AddHandler<T>(EventType<T> eventType) where T : EventArgs;
        void RemoveHandler<T>(EventType<T> eventType) where T : EventArgs;
    }

    public class FakeEventService : IEventService
    {
        public void Raise<T>(object sender, T args) where T : EventArgs => throw new NotImplementedException();

        public void AddHandler<T>(EventType<T> eventType) where T : EventArgs => throw new NotImplementedException();

        public void RemoveHandler<T>(EventType<T> eventType) where T : EventArgs => throw new NotImplementedException();

        public void OnStart()
        {
        }

        public void OnDestroy()
        {
        }
    }
}