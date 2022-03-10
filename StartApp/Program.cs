using System;
using System.Collections.Generic;
using DependencyInjection;

namespace StartApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = DependencyContainerFactory.MakeLiteContainer();
            container.Bind<IRepository, Repository>().ToSingleton();
            container.Bind<IDataContext, DataContext>();

            var dc = container.Resolve<IDataContext>();
            var repo = container.Resolve<IRepository>();
        }

        interface IDataContext
        {
            IRepository Repository { get; }
        }

        class DataContext : IDataContext
        {
            public IRepository Repository { get; }

            public DataContext(IRepository repository)
            {
                Repository = repository;
            }
        }

        interface IRepository
        {
            IEnumerable<User> Users { get; }
        }

        class Repository : IRepository
        {
            public IEnumerable<User> Users { get; }

            public Repository()
            {
                Users = new List<User>();
            }
        }

        class User
        {
            public string Name { get; set; }
        }
    }
}
