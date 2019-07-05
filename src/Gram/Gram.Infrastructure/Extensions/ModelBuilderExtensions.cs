using Gram.Core.Interfaces;
using Gram.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Gram.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        internal static void ApplyBaseEntityConfigurations(this ModelBuilder modelBuilder)
        {
            var appyConfigurationMethodDefinition = typeof(ModelBuilder).GetTypeInfo().DeclaredMethods.Single(p =>
                p.Name == "ApplyConfiguration"
                && p.IsGenericMethodDefinition
                && p.GetParameters().Length == 1
                && p.GetParameters()[0].ParameterType.IsGenericType
                && p.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)
            );

            var entityTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(p => p.GetTypes())
                .Where(p => typeof(IEntity).IsAssignableFrom(p) && p != typeof(IEntity));

            foreach (var entityType in entityTypes)
            {
                var configurationType = typeof(BaseEntityConfiguration<>).MakeGenericType(entityType);
                var configuration = Activator.CreateInstance(configurationType);
                var applyConfigurationMethod = appyConfigurationMethodDefinition.MakeGenericMethod(entityType);
                applyConfigurationMethod.Invoke(modelBuilder, new object[] { configuration });
            }
        }
    }
}
