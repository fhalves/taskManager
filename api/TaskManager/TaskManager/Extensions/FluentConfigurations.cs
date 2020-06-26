using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Domain.Events;
using TaskManager.Infra.Common;

namespace TaskManager.Extensions
{
    public static class FluentConfigurations
    {
        public static void AddFluentConfigurations(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    List<string> errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();

                    bool defaultResponse = false;
                    foreach (string item in errors)
                    {
                        if (string.IsNullOrEmpty(item))
                        {
                            defaultResponse = true;
                        }
                    }

                    if (defaultResponse)
                    {
                        errors.Clear();
                        foreach (Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateEntry item in context.ModelState.Values)
                        {
                            foreach (Microsoft.AspNetCore.Mvc.ModelBinding.ModelError i in item.Errors)
                            {
                                errors.Add(i.Exception.Message);
                            }
                        }
                    }


                    List<DomainNotification> errorList = new List<DomainNotification>();
                    foreach(var error in errors)
                    {
                        errorList.Add(new DomainNotification(error));
                    }

                    var result = new DefaultAPIResponse
                    {
                        Success = false,
                        Data = null,
                        Notifications = errorList
                    };

                    return new BadRequestObjectResult(result);
                };
            });
        }
    }
}
