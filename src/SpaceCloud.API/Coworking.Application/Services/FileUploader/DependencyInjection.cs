using System;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Uploader
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFileUploader(this IServiceCollection serviceCollection,
            Action<FileOptions> fileOptions)
        {
            serviceCollection.AddScoped<FileManager>();
            serviceCollection.Configure(fileOptions);
            serviceCollection.AddHttpContextAccessor();
            return serviceCollection;
        }
    }
}