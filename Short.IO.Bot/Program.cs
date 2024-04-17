using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Runtime.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(Serilog.Log.Logger, true);

await builder.Build().RunAsync();
