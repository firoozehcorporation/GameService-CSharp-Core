using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using FiroozehGameService.Handlers;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Socket.ClientHelper
{
    /// <summary>
    ///     TcpClientWithTimeout is used to open a TcpClient connection, with a
    ///     user definable connection timeout in milliseconds (1000=1second)
    ///     Use it like this:
    ///     TcpClient connection = new TcpClientWithTimeout('127.0.0.1',80,1000).Connect();
    /// </summary>
    internal class TcpClientWithTimeout
    {
        private readonly string _hostname;
        private readonly int _port;
        private readonly int _timeoutWaitMilliseconds;
        private bool _connected;
        private TcpClient _connection;
        private Exception _exception;
        private readonly CancellationTokenSource _cancellationToken;


        internal TcpClientWithTimeout(string hostname, int port,int timeoutWaitMilliseconds)
        {
            _hostname = hostname;
            _port = port;
            _timeoutWaitMilliseconds = timeoutWaitMilliseconds;
            _cancellationToken = new CancellationTokenSource();
        }

        internal async Task Connect(GSLiveType type)
        {
            // kick off the thread that tries to connect
            _connected = false;
            _exception = null;


            await Task.Run(async () => { await BeginConnect(); },_cancellationToken.Token);

            if (_connected)
            {
                CoreEventHandlers.OnTcpClientConnected?.Invoke(type,_connection);
                return;
            }

            if (_exception != null)
            {
                CoreEventHandlers.OnGsTcpClientError?.Invoke(type, new GameServiceException(_exception.Message));
                return;
            }
            
            CoreEventHandlers.OnGsTcpClientError?.Invoke(type,new GameServiceException( $"TcpClient connection to {_hostname}:{_port} timed out"));
        }

        private async Task BeginConnect()
        {
            try
            {
                LogUtil.Log(this, "Wait " + _timeoutWaitMilliseconds + " Before Connect");
                await Task.Delay(_timeoutWaitMilliseconds);
                LogUtil.Log(this, "Connect To " + _hostname);
                
                _connection = new TcpClient(_hostname,_port);
                /*_connection = new TcpClient();
                var result = _connection.BeginConnect(_hostname, _port, null, null);

                var success = result.AsyncWaitHandle.WaitOne(_timeoutWaitMilliseconds);
                if (!success) throw new GameServiceException("Connection Main Timeout");
                
                _connection.EndConnect(result);*/
                _connected = true;
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
            finally
            {
                _cancellationToken?.Cancel(false);
            }
        }
    }
}