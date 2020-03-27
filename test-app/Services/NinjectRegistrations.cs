using Ninject.Modules;

namespace test_app.Services
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<JsonValidationSchema>().To<JsonValidationSchema>();
        }
    }
}