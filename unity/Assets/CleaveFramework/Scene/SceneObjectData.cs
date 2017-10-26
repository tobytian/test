﻿using System;
using System.Collections.Generic;
using CleaveFramework.Interfaces;
using CleaveFramework.Tools;
using CleaveFramework.Binding;

namespace CleaveFramework.Scene
{
    /// <summary>
    /// container for scene objects
    /// </summary>
    public sealed class SceneObjectData
    {
        /// <summary>
        /// singleton library
        /// </summary>
        //private Dictionary<Type, object> _singletons;
        private Binding<Type, object> _singletons = new Binding<Type,object>();

        /// <summary>
        /// transient objects library
        /// </summary>
        //private Dictionary<Type, Dictionary<string, object>> _transients;
        private Binding<Type, Binding<string, object>> _transients = new Binding<Type, Binding<string, object>>();

        /// <summary>
        /// IUpdateable objects library
        /// </summary>
        private List<IUpdateable> _updateables = new List<IUpdateable>();

        /// <summary>
        /// IInitializeable objects library
        /// </summary>
        private List<IInitializeable> _initializeables = new List<IInitializeable>();

        /// <summary>
        /// IConfigureable objects library
        /// </summary>
        private List<IConfigureable> _configureables = new List<IConfigureable>();

        /// <summary>
        /// IConfigureable objects library
        /// </summary>
        private List<IDestroyable> _destroyables = new List<IDestroyable>();

        public bool IsObjectDataInitialized { get; private set; }

        public SceneObjectData()
        {
            IsObjectDataInitialized = false;
        }

        public void Destroy()
        {
            foreach (var obj in _destroyables)
            {
                obj.Destroy();
            }

            _destroyables.Clear();
            _singletons.Clear();
            _transients.Clear();
            _updateables.Clear();
            _initializeables.Clear();
            _configureables.Clear();

            IsObjectDataInitialized = false;
        }

        public void Update(float deltaTime)
        {
            // ensure IInitializeables before we update
            if (!IsObjectDataInitialized) return;

            // process IUpdateables
            foreach (var obj in _updateables)
            {
                obj.Update(deltaTime);
            }
        }

        public void InitializeSceneObjects()
        {
            // ensure scene is only able to be initialized once
            if (IsObjectDataInitialized) return;

            foreach (var obj in _initializeables)
            {
                obj.Initialize();
            }

            // configure the objects after everything has been initialized
            foreach (var obj in _configureables)
            {
                obj.Configure();
            }

            IsObjectDataInitialized = true;

            _initializeables.Clear();
            _configureables.Clear();
        }

        /// <summary>
        /// reflect the object's interfaces and insert it into the appropriate library
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        private void ReflectInterfaces<T>(T obj)
        {
            if (typeof(IUpdateable).IsAssignableFrom(typeof(T)))
            {
                _updateables.Add((IUpdateable)obj);
            }
            if (typeof(IInitializeable).IsAssignableFrom(typeof(T)))
            {
                // if the scene was already initialized we need to call initialize on this object which has been added dynamically after construction
                if (IsObjectDataInitialized)
                {
                    ((IInitializeable)obj).Initialize();
                }
                else
                {
                    _initializeables.Add((IInitializeable)obj);
                }
               
            }
            if (typeof (IConfigureable).IsAssignableFrom(typeof (T)))
            {
                if (IsObjectDataInitialized)
                {
                    ((IConfigureable)obj).Configure();
                }
                else
                {
                    _configureables.Add((IConfigureable)obj);
                }
            }
            if (typeof (IDestroyable).IsAssignableFrom(typeof (T)))
            {
                _destroyables.Add((IDestroyable)obj);
            }
        }

        /// <summary>
        /// Injects an object instance into the framework singleton library
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="obj">object instance</param>
        public T PushSingleton<T>(T obj)
            where T: class
        {
            return PushObjectAsSingleton(obj);
        }

        /// <summary>
        /// Injects an object instance into the framework singleton library
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="obj">object instance</param>
        public T PushObjectAsSingleton<T>(T obj)
            where T: class
        {
            // reflect on the implemented interfaces
            ReflectInterfaces(obj);

            // destroy the original object if we're overwriting
            var originalObj = (T) _singletons[typeof (T)];
            if (originalObj != null)
            {
                PopUpdateable(originalObj);
                PopDestroyable(originalObj);
            }

            _singletons[typeof (T)] = obj;
            return obj;
        }

        /// <summary>
        /// resolve a type into an instance
        /// </summary>
        /// <typeparam name="T">object type to retrieve</typeparam>
        /// <returns>object instance if injected, otherwise null</returns>
        public T ResolveSingleton<T>()
            where T: class
        {
            return (T)_singletons[typeof (T)];
        }

        /// <summary>
        /// Inject an object instance into the framework transients library
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="name">object instance name, required for lookup and must be unique per type</param>
        /// <param name="obj">object instance</param>
        public T PushTransient<T>(string name, T obj)
            where T: class
        {
            return PushObjectAsTransient(name, obj);
        }

        /// <summary>
        /// Inject an object instance into the framework transients library
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="name">object instance name, required for lookup and must be unique per type</param>
        /// <param name="obj">object instance</param>
        private T PushObjectAsTransient<T>(string name, T obj)
            where T: class
        {
            ReflectInterfaces(obj);

            var objType = typeof (T);
            if (!_transients.IsBound(objType))
            {
                _transients.Bind(objType, new Binding<string, object>());
            }

            CDebug.Assert(_transients[objType].IsBound(name),
                "Object instance type/name combination is not unique: " + objType + "/" + name);

            _transients[objType][name] = obj;
            return obj;
        }

        public T PopTransient<T>(string name)
            where T: class
        {
            var objType = typeof (T);
            CDebug.Assert(!_transients.IsBound(objType));

            var obj = (T) _transients[objType].Clear(name);
            PopUpdateable(obj);
            PopDestroyable(obj);

            return obj;
        }

        private void PopDestroyable<T>(T obj)
        {
            if (typeof (IDestroyable).IsAssignableFrom(typeof (T)))
            {
                ((IDestroyable)obj).Destroy();
                _destroyables.Remove((IDestroyable) obj);
            }
        }

        private void PopUpdateable<T>(T obj)
        {
            if (typeof(IUpdateable).IsAssignableFrom(typeof(T)))
            {
                _updateables.Remove((IUpdateable)obj);
            }
        }

        /// <summary>
        /// Resolve a name and type into an object from the transients library
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="name">instance name</param>
        /// <returns>object instance</returns>
        public T ResolveTransient<T>(string name)
            where T: class
        {
            var objType = typeof (T);
            return !_transients.IsBound(objType) ? null : (T)_transients[objType][name];
        }

        /// <summary>
        /// Retrieve a KeyValuePair (name, object) tuple of every transient matching that type
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <returns>KeyValuePair(name, object)</returns>
        public KeyValuePair<string, object> [] ResolveTransient<T>()
            where T: class
        {
            var objType = typeof (T);
            KeyValuePair<string, object> [] objList;
            if (_transients.IsBound(objType))
            {
                var objs = _transients[objType];
                objList = objs.ToArray();
            }
            else
            {
                objList = new KeyValuePair<string, object>[0];
            }
            return objList;
        }
    }
}
