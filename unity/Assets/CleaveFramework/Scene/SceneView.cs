﻿using System.Collections;
using CleaveFramework.Core;
using UnityEngine;

namespace CleaveFramework.Scene
{
    abstract public class SceneView : View
    {
        public SceneObjectData SceneObjects { get; set; }

        /// <summary>
        /// construct your scene here
        /// </summary>
        public abstract void Initialize();

        virtual public void Update()
        {
            if(SceneObjects != null)
                SceneObjects.Update(Time.deltaTime);
        }

        void OnDestroy()
        {
            if(SceneObjects != null)
                SceneObjects.Destroy();
        }

        IEnumerator ValidateSceneObjects()
        {
            while (!SceneObjects.IsObjectDataInitialized)
            {
                yield return null;
            }
            SceneManager.ValidateSceneObjects();
        }

    }
}
