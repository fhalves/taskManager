using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TaskManager.Domain.Events;
using TaskManager.Infra.Common;

namespace TaskManager.Extensions
{
    public static class ExceptionHandler
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app,
                    ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionHandlerFeature != null)
                    {
                        var logger = loggerFactory.CreateLogger("GlobalExceptionHandler");
                        logger.LogError($"Failed to execute: {exceptionHandlerFeature.Error}");

                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";
                        string path = Path.Combine($"{Directory.GetCurrentDirectory()}\\log\\{ DateTime.Now.ToString("MM-dd-yyyy") }-errors.txt");
                        string file = $@"Error: {DateTime.Now.ToString("dd/MM/yyyy")}{Environment.NewLine
                            }========================================================================================================================================={
                            Environment.NewLine}{exceptionHandlerFeature.Error}{Environment.NewLine
                            }_________________________________________________________________________________________________________________________________________{
                            Environment.NewLine}";

                        string currentContent = String.Empty;
                        if (File.Exists(path))
                        {
                            currentContent = File.ReadAllText(path);
                        }
                        File.WriteAllText(path, file + currentContent);

                        var json = new DefaultAPIResponse
                        {
                            Success = false,
                            Data = null,
                            Notifications = new List<DomainNotification> { new DomainNotification("An error ocurred! See details.") },
                        };

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(json));
                    }
                });
            });
        }
    }
}