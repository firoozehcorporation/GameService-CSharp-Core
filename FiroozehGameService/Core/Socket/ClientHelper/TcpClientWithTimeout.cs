using System;
using System.Net.Sockets;
using System.Threading;

namespace FiroozehGameService.Core.Socket.ClientHelper
{
    /// <summary>
    ///     TcpClientWithTimeout is used to open a TcpClient connection, with a
    ///     user definable connection timeout in milliseconds (1000=1second)
    ///     Use it like this:
    ///     TcpClient connection = new TcpClientWithTimeout('127.0.0.1',80,1000).Connect();
    /// </summary>
    public class TcpClientWithTimeout
    {
        private readonly string _hostname;
        private readonly int _port;
        private readonly int _timeoutMilliseconds;
        private bool _connected;
        private TcpClient _connection;
        private Exception _exception;

        public TcpClientWithTimeout(string hostname, int port, int timeoutMilliseconds)
        {
            _hostname = hostname;
            _port = port;
            _timeoutMilliseconds = timeoutMilliseconds;
        }

        public TcpClient Connect()
        {
            // kick off the thread that tries to connect
            _connected = false;
            _exception = null;
            var thread = new Thread(BeginConnect) {IsBackground = true};
            // So that a failed connection attempt 
            // wont prevent the process from terminating while it does the long timeout
            thread.Start();

            // wait for either the timeout or the thread to finish
            thread.Join(_timeoutMilliseconds);

            if (_connected)
            {
                // it succeeded, so return the connection
                thread.Abort();
                return _connection;
            }

            if (_exception != null)
            {
                // it crashed, so return the exception to the caller
                thread.Abort();
                throw _exception;
            }

            // if it gets here, it timed out, so abort the thread and throw an exception
            thread.Abort();
            var message = $"TcpClient connection to {_hostname}:{_port} timed out";
            throw new TimeoutException(message);
        }

        private void BeginConnect()
        {
            try
            {
                _connection = new TcpClient(_hostname, _port);
                // record that it succeeded, for the main thread to return to the caller
                _connected = true;
            }
            catch (Exception ex)
            {
                // record the exception for the main thread to re-throw back to the calling code
                _exception = ex;
            }
        }
    }
}