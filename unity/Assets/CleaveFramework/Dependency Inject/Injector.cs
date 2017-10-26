﻿/* Copyright 2014 Glen/CleaveTV

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License. */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CleaveFramework.Binding;
using CleaveFramework.Tools;
using UnityEngine;


namespace CleaveFramework.DependencyInjection
{
    /// <summary>
    /// Injector handles mapping types and interfaces to implementations
    /// 
    /// 
    /// for ex:
    /// <code>
    /// // Map IMySystem interface to an implementation of MySystem in the Injector
    /// // Create MySystem using ConstructMySystem as its secondary constructor
    /// Injector.AddSingleton<IMySystem>(Factory.Create<MySystem>(ConstructMySystem));
    /// // Or have the Injector create a new instance for you instead:
    /// Injector.AddTransient<IMySystem>(typeof(MySystem))
    /// 
    /// // Inject MySystem into an object that requires an IMySystem interface:
    /// class SomeObject : IInitializable {
    ///     [Inject] public IMySystem MySystemService {get; set;}
    ///     public Initialize() {
    ///         // MySystemService is valid here and instance is either the singleton given or a new instance
    ///         // depending on what was given to the Injector
    ///         MySystemService.Method(); 
    ///     }
    /// }
    /// 
    /// </code>
    /// </summary>
    static public class Injector
    {

        private enum InjectTypes
        {
            Singleton,
            Transient,
        }

        private static Binding<Type, InjectTypes> _injectionTypes = new Binding<Type, InjectTypes>();
        private static Binding<Type, object> _singletons = new Binding<Type, object>();
        private static Binding<Type, Type> _transients = new Binding<Type, Type>();
        private static Binding<string, object> _templates = new Binding<string, object>(); 

        private static Binding<Type, IEnumerable<FieldInfo>>  _fieldsCache = new Binding<Type, IEnumerable<FieldInfo>>();
        private static Binding<Type, IEnumerable<PropertyInfo>> _propertyCache = new Binding<Type, IEnumerable<PropertyInfo>>();
        private static Binding<Type, IEnumerable<MemberInfo>> _memberCache = new Binding<Type, IEnumerable<MemberInfo>>();

        /// <summary>
        /// Bind a template to an object to be assigned at injection time
        /// for ex:
        /// define a template:
        /// class Foo { ... }
        /// var foo = new Foo();
        /// Injector.BindTemplate("fooTemplate", foo);
        /// [Inject("fooTemplate)] Foo MyFoo; // injector will inject fooTemplate's instance into this object
        /// </summary>
        /// <param name="name">name of the template</param>
        /// <param name="obj">object to inject</param>
        public static void BindTemplate(string name, object obj)
        {
            _templates[name] = obj;
        }

        /// <summary>
        /// Add a Transient type to the Injector and map it to a specific implementation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="impl"></param>
        public static void BindTransient<T>(Type impl)
        {
            _injectionTypes.Bind(typeof(T), InjectTypes.Transient);
            _transients.Bind(typeof(T), impl);
        }

        /// <summary>
        /// Add a Transient type to the Injector and map it to a specific implementation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="I"></typeparam>
        public static void BindTransient<T, I>()
        {
            _injectionTypes.Bind(typeof(T), InjectTypes.Transient);
            _transients.Bind(typeof(T), typeof(I));
        }

        /// <summary>
        /// Add a transient type to the Injector and use itself as the implementation type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void BindTransient<T>()
        {
            _injectionTypes.Bind(typeof(T), InjectTypes.Transient);
            _transients.Bind(typeof(T), typeof(T));
        }

        /// <summary>
        /// Add a singleton type to the injector and map it to the instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public static void BindSingleton<T>(object instance)
        {
            _injectionTypes.Bind(typeof(T), InjectTypes.Singleton);
            _singletons.Bind(typeof (T), instance);
        }

        private static object Resolve(Type t)
        {
            if(_singletons.IsBound(t))
            {
                return _singletons.Resolve(t);
            }
            if(_injectionTypes.Resolve(t) == InjectTypes.Transient)
            {
                var impl = _transients.Resolve(t);
                var instance = Factory.Factory.Create(impl);

                return instance;

            }
            return null;
        }

        private static void InjectProperties<T>(T obj)
        {
            var injectingType = typeof (T);

            IEnumerable<PropertyInfo> props;
            if (_propertyCache.IsBound(injectingType))
            {
                props = _propertyCache.Resolve(injectingType);
            }
            else
            {
                props = injectingType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(Inject)));
                _propertyCache.Bind(injectingType, props);
            }
            CDebug.Assert(props == null, "Injector.InjectProperties()");

            foreach (var cProp in props)
            {
                if (!cProp.CanWrite) continue;

                object[] attribs = cProp.GetCustomAttributes(true);
                foreach (var attrib in attribs)
                {
                    var inject = attrib as Inject;
                    if (inject != null)
                    {
                        object instance = null;
                        if (inject.TemplateName != "")
                        {
                            instance = _templates.IsBound(inject.TemplateName) ? _templates[inject.TemplateName] : Resolve(cProp.PropertyType);
                        }
                        else
                        {
                            instance = Resolve(cProp.PropertyType);
                        }
                        CDebug.Log("Injector.InjectProperties<" + cProp.PropertyType + "> on " + obj);
                        cProp.SetValue(obj, instance, null);
                    }
                }
            }
        }

        private static void InjectFields<T>(T obj)
        {

            var injectingType = typeof(T);

            IEnumerable<FieldInfo> fields;
            if (_fieldsCache.IsBound(injectingType))
            {
                fields = _fieldsCache.Resolve(injectingType);
            }
            else
            {
                fields = injectingType.GetFields().Where(prop => Attribute.IsDefined(prop, typeof(Inject)));
                _fieldsCache.Bind(injectingType, fields);
            }
            CDebug.Assert(fields == null, "Injector.InjectFields()");

            foreach (var cField in fields)
            {
                object[] attribs = cField.GetCustomAttributes(true);
                foreach (var attrib in attribs)
                {
                    var inject = attrib as Inject;
                    if (inject != null)
                    {
                        object instance = null;
                        if (inject.TemplateName != "")
                        {
                            instance = _templates.IsBound(inject.TemplateName) ? _templates[inject.TemplateName] : Resolve(cField.FieldType);
                        }
                        else
                        {
                            instance = Resolve(cField.FieldType);
                        }
                        CDebug.Log("Injector.InjectFields<" + cField.FieldType + "> on " + obj);
                        cField.SetValue(obj, instance);
                    }
                }
            }
        }

        private static void InjectMonoBehaviour<T>(T obj)
        {
            var monoObj = obj as MonoBehaviour;

            var injectingType = monoObj.GetType();

            IEnumerable<MemberInfo> members;
            if (_memberCache.IsBound(injectingType))
            {
                members = _memberCache.Resolve(injectingType);
            }
            else
            {
                members = injectingType.GetMembers().Where(prop => Attribute.IsDefined(prop, typeof(Inject)));
                _memberCache.Bind(injectingType, members);
            }
            CDebug.Assert(members == null, "Injector.InjectMonoBehaviour()");

            foreach (FieldInfo cField in members)
            {
                object[] attribs = cField.GetCustomAttributes(true);
                foreach (var attrib in attribs)
                {
                    var inject = attrib as Inject;
                    if (inject != null)
                    {
                        object instance = null;
                        if (inject.TemplateName != "")
                        {
                            instance = _templates.IsBound(inject.TemplateName) ? _templates[inject.TemplateName] : Resolve(cField.FieldType);
                        }
                        else
                        {
                            instance = Resolve(cField.FieldType);
                        }
                        CDebug.Log("Injector.InjectFields<" + cField.FieldType + "> on " + obj);
                        cField.SetValue(monoObj, instance);
                    }
                }
            }
        }

        /// <summary>
        /// Reflect on the Object and inject its fields and properties marked with [Inject] tag
        /// Note: although this is public it should generally only need to be called by Factory
        /// except in the case of nested transient injections
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T PerformInjections<T>(T obj)
        {
            if (typeof (MonoBehaviour).IsAssignableFrom(typeof (T)))
            {
                InjectMonoBehaviour(obj);
            }
            else
            {
                InjectProperties(obj);
                InjectFields(obj);
            }

            return obj;
        }
    }

}
