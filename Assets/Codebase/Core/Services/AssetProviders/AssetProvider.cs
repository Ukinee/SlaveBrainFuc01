using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ApplicationCode.Core.Services.AssetProviders
{
    public class AssetProvider
    {
        private Dictionary<string, MonoBehaviour> _resources = new Dictionary<string, MonoBehaviour>();
        private Dictionary<string, Object> _objects = new Dictionary<string, Object>();

        public T Instantiate<T>(string path) where T : MonoBehaviour
        {
            if (_resources.TryGetValue(path, out MonoBehaviour resource) == false)
            {
                resource = Resources.Load<T>(path);
                _resources[path] = resource;
            }

            try
            {
                return (T)Object.Instantiate(resource);
            }
            catch (Exception)
            {
                throw new Exception("Resource not found: " + path + " with type: " + typeof(T));
            }
        }

        public T Get<T>(string path) where T : Object
        {
            if( _objects.TryGetValue(path, out Object resource) == false)
            {
                resource = Resources.Load<T>(path);
                
                if(resource == null)
                    throw new Exception("Resource not found: " + path + " with type: " + typeof(T));
                
                _objects[path] = resource;
            }
            
            return (T)resource;
        }
    }
}