using System;
using System.Collections.Generic;
using System.Text;
using TrelloApp.Services.API;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace TrelloApp.ViewModels.Base
{
    public class ViewModelLocator
    {
        private readonly IUnityContainer _container;

        public static ViewModelLocator Instance { get; } = new ViewModelLocator();

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            // remote services
            _container.RegisterType<ITrelloService, TrelloService>();

            // viewmodels
            _container.RegisterType<TrelloViewModel>(new ContainerControlledLifetimeManager());



            var unityServiceLocator = new UnityServiceLocator(_container);
            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

    }
}
