using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;

namespace Domain.Infrastructure
{
    class InjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEFRepository>().To<EFRepository>();
        }
    }

    public class NinjectFactory 
    {
        public IKernel NInjectKernel {
            get { return this._ninjectKernel; }
            set { this._ninjectKernel = value; }
        }

        IKernel _ninjectKernel;

        public NinjectFactory()
        {
            this._ninjectKernel = new StandardKernel(new InjectModule());
        }
    }

    public interface INinjectManager
    {
        T GetInjectedInstance<T>();
    }

    public class NinjectManager : INinjectManager
    {
        static INinjectManager _instance;
        NinjectFactory _factory;

        private NinjectManager()
        {
            this._factory = new NinjectFactory();
        }

        public static INinjectManager GetInstance()
        {
            if (_instance == null)
                _instance = new NinjectManager();

            return _instance;
        }

        public T GetInjectedInstance<T>()
        {
            T injectedInstance = this._factory.NInjectKernel.Get<T>();

            return injectedInstance;
        }
    }
}
