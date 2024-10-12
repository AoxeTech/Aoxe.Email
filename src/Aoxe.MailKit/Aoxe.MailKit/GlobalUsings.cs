global using System;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using Aoxe.Email.Abstractions;
global using Aoxe.Email.Abstractions.Models;
global using Aoxe.Extensions;
global using MailKit;
global using MailKit.Net.Smtp;
global using Microsoft.Extensions.DependencyInjection;
global using MimeKit;
global using MimeKit.Text;
#if NET8_0_OR_GREATER
global using System.Diagnostics.Metrics;
#endif
