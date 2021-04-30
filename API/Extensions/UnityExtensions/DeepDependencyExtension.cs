using Mono.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using System.Reflection;
using System.Threading.Tasks;
using Unity;
using Unity.Builder;
using Unity.Extension;
using Unity.Strategies;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace API.Extensions.UnityExtensions
{
    #region Extension
    internal class DeepDependencyExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Context.Strategies.Add(new DeepDependencyStrategy(),
                UnityBuildStage.Initialization);
        }
    }
    #endregion

    #region Attributes
    /// <summary>
    /// Automatically resolves a property on initialization.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DeepDependency : Attribute { }
    #endregion

    #region Strategy
    class DeepDependencyStrategy : BuilderStrategy
    {

        public static Logger Log { get; } = LogManager.GetCurrentClassLogger();


        public override void PostBuildUp(ref BuilderContext context)
        {
            if (context.Type == typeof(object)) return;
            var container = context.Container;

            var properties = context.Existing.GetType().GetProperties(
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.GetProperty | BindingFlags.SetProperty |
                BindingFlags.Instance)
                .Where(x => x.GetCustomAttributes(typeof(DeepDependency), false)
                .Any());

            foreach (var prop in properties)
            {
                object value = null;

                // Resolve ILogger by simply creating it based on the current class
                if (!prop.PropertyType.IsGenericType && prop.PropertyType.IsAssignableTo<ILogger>())
                    value = container.TryResolve<ILoggerFactory>().CreateLogger(context.Type);

                else value = context.Container.TryResolve(prop.PropertyType);

                try
                {
                    if (prop.CanWrite) prop.SetValue(context.Existing, value);
                    else prop.GetBackingField().SetValue(context.Existing, value);
                }
                catch (Exception ex)
                {
                    Log.Error($"An error occured during deep property resolutiion\n{ex}");
                }
            }
        }
    }
    #endregion
}
