#if ENABLE_REFLEX_DI
namespace PubSub.Runtime.PubSub.Runtime
{
    using ReflexDI;
    using UnityEngine;

    public class PubSubInstaller : Installer<PubSubInstaller>
    {
        public override void Installing(IBuilder builder, IResolver resolver, Transform parent)
        {
            builder.Register<PubSubService>().AsInterfaces();
        }
    }
}
#endif