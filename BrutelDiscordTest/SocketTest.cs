﻿using BrutelDiscord.Clients;
using BrutelDiscord.Interfaces;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BrutelDiscordTest
{
    public class Class1
    {
        [Fact]
        public async Task ConnectTest()
        {
            var mock = new Mock<ISocket>();
            mock.Setup(x => x.Connect(It.IsAny<string>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>())).Returns(Task.Run(() => true));

            var client = new SocketClient("test", mock.Object);
            await client.StartAsync();
            
            mock.Verify(x => x.Connect(It.Is<string>(y => y == "test"), It.Is<TimeSpan>(y => y == TimeSpan.FromMilliseconds(300)), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ConnectFailTest()
        {
            var mock = new Mock<ISocket>();
            mock.Setup(x => x.Connect(It.IsAny<string>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>())).Returns(Task.Run(() => false));

            var client = new SocketClient("test", mock.Object);
            await Assert.ThrowsAsync<Exception>(() => client.StartAsync());

            mock.Verify(x => x.Connect(It.Is<string>(y => y == "test"), It.Is<TimeSpan>(y => y == TimeSpan.FromMilliseconds(300)), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
