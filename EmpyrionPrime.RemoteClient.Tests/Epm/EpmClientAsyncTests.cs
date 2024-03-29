﻿using Eleon.Modding;
using EmpyrionPrime.RemoteClient.Epm;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace EmpyrionPrime.RemoteClient.Tests.Epm;

public class EpmClientAsyncTests
{
    private readonly ITestOutputHelper _outputHelper;

    public EpmClientAsyncTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public void Test_ClientConnects()
    {
        var trigger = new ManualResetEventSlim(false);
        var client = CreateClient();

        // Set trigger after connecting
        client.OnConnected += () =>
        {
            _outputHelper.WriteLine($"Client Id {client.ClientId} connected");
            trigger.Set();
        };

        // Delay starting until after we're waiting
        _ = Task.Run(async () =>
        {
            await Task.Delay(500);
            client.Start();
        });

        var connected = trigger.Wait(5000);
        Assert.True(connected);
    }

    [Theory]
    [InlineData(CmdId.Request_Dedi_Stats, 42000, null)]
    public void Test_RequestEvent(CmdId id, ushort seqNum, object? payload)
    {
        var trigger = new ManualResetEventSlim(false);
        var client = CreateClient();

        // Send request after connecting
        client.OnConnected += () =>
        {
            _outputHelper.WriteLine($"Client Id {client.ClientId} connected");
            _ = Task.Run(async () =>
            {
                await Task.Delay(500);
                client.SendRequest(id, seqNum, payload);
            });
        };

        // Read response and trigger
        client.GameEventHandler += gameEvent =>
        {
            if (gameEvent.SequenceNumber != seqNum)
                return;

            _outputHelper.WriteLine(gameEvent.ToString());
            trigger.Set();
        };

        client.Start();

        var recieved = trigger.Wait(5000);
        Assert.True(recieved);
    }

    private EpmClientAsync CreateClient()
    {
        var random = new Random();
        var clientId = random.Next();

        var logger = MockLogger<EpmClient>();

        return new EpmClientAsync(logger.Object, "127.0.0.1", 12345, clientId);
    }

    private Mock<ILogger<T>> MockLogger<T>()
    {
        // From: https://stackoverflow.com/questions/52707702/how-do-you-mock-ilogger-loginformation/63701968#63701968
        var logger = new Mock<ILogger<T>>();

        logger.Setup(x => x.Log(
            It.IsAny<LogLevel>(),
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception>(),
            (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()))
            .Callback(new InvocationAction(invocation =>
            {
                var logLevel = (LogLevel)invocation.Arguments[0];
                var eventId = (EventId)invocation.Arguments[1];
                var state = invocation.Arguments[2];
                var exception = (Exception)invocation.Arguments[3];
                var formatter = invocation.Arguments[4];

                var invokeMethod = formatter.GetType().GetMethod("Invoke");
                var logMessage = (string)invokeMethod?.Invoke(formatter, new[] { state, exception })!;

                var output = $"{logLevel} - {logMessage}";

                _outputHelper.WriteLine(output);
                Trace.WriteLine(output);
            }));

        return logger;
    }
}
