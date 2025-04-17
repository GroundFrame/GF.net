using GF.Common;
using GF.Common.Translations;
using GF.SimSig.Gateway.Stomp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig.Gateway
{
    /// <summary>
    /// Class representing a SimSig Gateway Client
    /// </summary>
    public class GatewayClient : IDisposable
    {
        private readonly TcpClient _tcpClient;
        private IStompConnector _stompConnector = null!;
        private IStompConnection _stompConnection = null!;

        /// <summary>
        /// Gets the host name
        /// </summary>
        public string Host { get; private set; }

        /// <summary>
        /// Gets the port number
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// Gets a flag to indicate whether the client is connected to the SimSig Gateway
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// Gets a list of the subscriptions the client is currently connected to. Stored a <see cref="List{KeyValuePair{GatewayTopic,string}}"/> containing the topic subscribed to and the subscription Id
        /// </summary>
        public List<KeyValuePair<GatewayTopic, string>> SubscriptionKeys { get { return this._stompConnection == null ? new List<KeyValuePair<GatewayTopic, string>>() : this._stompConnection.Subscriptions; } } 

        public GatewayClient(string host, int port)
        {
            Host = host;
            Port = port;
            this._tcpClient = new TcpClient();
        }

        /// <summary>
        /// Starts this <see cref="GatewayClient"/> and subscriptions to the provided list of topics
        /// </summary>
        /// <param name="topics">A <see cref="List{GatewayTopic}"/> containing the list of topics to subscribe to</param>
        /// <param name="observer">An object which implements <see cref="IObserver{IStompMessage}"/> which handles messages as they are received</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        /// <returns></returns>
        public async Task StartAsync(List<GatewayTopic> topics, IObserver<IStompMessage> observer, CancellationToken cancellationToken = default, [CallerMemberName] string? callingMethod = null)
        {
            if (topics == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(topics), callingMethod), "You must supply list of topics to subscribe to");
            }

            if (observer == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(observer), callingMethod), "You an observer object to handle received messages");
            }

            try
            {
                //try and connect to the TCP client. SimSig must be running and the Information Gateway Server started on the correct port.
                try
                {
                    await _tcpClient.ConnectAsync(this.Host, this.Port);
                }
                catch (SocketException)
                {
                    throw GFException.Build($"Cannot connect to SimSig at Host {this.Host} | Port {this.Port}. Has the Information Gateway been started in the Server Configuration within SimSig and does the supplied hostname and port match the expected values?", "Gateway.TcpConnection.Error", this.Host, this.Port);
                }
                catch
                {
                    throw;
                }

                //instantiate the connector
                this._stompConnector = new Stomp12Connector(
                    _tcpClient.GetStream(),
                    this.Host,
                    "bluespider",
                    "KingLear12345"
                );

                //connector to the connector
                this._stompConnection = await _stompConnector.ConnectAsync(cancellationToken: cancellationToken);
                this.IsConnected = true;

                //loop around each topic and subscribe
                foreach (GatewayTopic topic in topics)
                {
                    await _stompConnection.SubscribeAsync(
                      observer,
                      topic,
                      "auto"
                      );
                }
            }
            catch (Exception ex)
            {
                throw GFException.Build(ex, $"An error has occurred trying to Start a connection to the SimSig Information Gateway at Host {this.Host} | Port {this.Port}", "Gateway.Connection.Error", this.Host, this.Port);
            }
        }

        /// <summary>
        /// Stops the connection to the client gateway
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        /// <returns></returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await this._stompConnection.DisconnectAsync(cancellationToken: cancellationToken);
        }

        public void Dispose()
        {
            //disconnect the connection if necessary
            if (this.IsConnected)
            {
                Task disconnectTask = this._stompConnection.DisconnectAsync();
                disconnectTask.Wait();
            }

            //dispose the connector
            this._stompConnector?.Dispose();

            //close and dispose of the tcp client
            if (this._tcpClient != null)
            {
                if (this._tcpClient.Connected)
                {
                    this._tcpClient.Close();
                }
                this._tcpClient.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
