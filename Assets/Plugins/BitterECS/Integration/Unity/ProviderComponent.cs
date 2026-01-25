using UnityEngine;
using System;
using BitterECS.Core;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BitterECS.Integration
{
    [RequireComponent(typeof(ProviderEntity)), Serializable]
    public abstract class ProviderComponent<T> : MonoBehaviour, ITypedComponentProvider where T : struct
    {
        private EcsEntity _ecsEntity;

        [SerializeField] protected T _value;
        public ref T Value => ref _ecsEntity.Get<T>();

        private void OnValidate()
        {
            var entity = GetComponent<ProviderEntity>().Entity;
            if (entity != null)
                Sync(entity);
        }

        private void Update()
        {
            if (CompareComponents(ref Value, ref _value)) { }
        }

        public bool CompareComponents(ref T a, ref T b)
        {
            return Unsafe.As<T, byte>(ref a)
                == Unsafe.As<T, byte>(ref b);
        }

        public void Sync(EcsEntity entity)
        {
            _ecsEntity = entity;
            entity.AddOrReplace(_value);
        }
    }
}
