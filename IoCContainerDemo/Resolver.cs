using System;
using System.Collections.Generic;
using System.Linq;

namespace IoCContainerDemo
{
    class Resolver
    {
        private readonly Dictionary<Type, Type> dependencyMap = new Dictionary<Type, Type>(); 
        public T Resolve<T>()
        {
           return (T)Resolve(typeof(T));
        }

        private object Resolve(Type typeToResolve)
        {
            Type resolveType = null;
            try
            {
                resolveType = dependencyMap[typeToResolve];
            }
            catch
            {
                throw new Exception(string.Format("could not resolve type {0}", typeToResolve.FullName));
            }

            var firstConstructor = resolveType.GetConstructors().First();
            var constructorParamaters = firstConstructor.GetParameters();

            if (!constructorParamaters.Any())
            {
                return Activator.CreateInstance(resolveType);
            }

            IList<object> paramaters = new List<object>();
            foreach (var paramaterToResolve in constructorParamaters)
            {
                //recursion
                paramaters.Add(Resolve(paramaterToResolve.ParameterType));
            }

            return firstConstructor.Invoke(paramaters.ToArray());
        }

        public void Register<T, T1>()
        {
            dependencyMap.Add(typeof(T), typeof(T1));
        }
    }
}
