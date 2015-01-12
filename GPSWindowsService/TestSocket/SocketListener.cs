using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using log4net;
using PT.Util;


public class StateObject
{
    public Socket workSocket = null;

    public const int BufferSize = 1024;

    public byte[] buffer = new byte[BufferSize];

    public StringBuilder sb = new StringBuilder();
}

public class SocketListener
{
   

    delegate void ProcessCompletedMessageDelegate(byte[] _messages);

    public static string data = null;

    private static int _codigoProcesamiento;

    private static int CodigoProcesamiento
    {
        get { return _codigoProcesamiento != 0 ? (_codigoProcesamiento += 1) : 1; }
        set { _codigoProcesamiento = value; }
    }

    public static ManualResetEvent allDone = new ManualResetEvent(false);

    public SocketListener()
    {
    }

    public static void StartListening()
    {
        var appSettings = ConfigurationManager.AppSettings;
        byte[] bytes = new Byte[1024];
        int addList = int.Parse(appSettings["AddressList"]);
        /*
        IPAddress ipAddress = Dns.GetHostEntry(appSettings["IP"]).AddressList[addList];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, int.Parse(appSettings["Puerto"]));
         */

        IPEndPoint localEndPoint = new IPEndPoint( IPAddress.Any, 9000);



        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(100);

            while (true)
            {
                allDone.Reset();

                Console.WriteLine("Waiting for a connection...");
                listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                allDone.WaitOne();
            }

        }
        catch (Exception e)
        {
            log.Error(e);
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("nPress ENTER to continue...");
        Console.Read();

    }

    public static void AcceptCallback(IAsyncResult ar)
    {
        allDone.Set();

        Socket listener = (Socket)ar.AsyncState;
        Socket handler = listener.EndAccept(ar);

        StateObject state = new StateObject();
        state.workSocket = handler;
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
            new AsyncCallback(ReadCallback), state);
    }

    public static void ReadCallback(IAsyncResult ar)
    {
        String content = String.Empty;
        var appSettings = ConfigurationManager.AppSettings;
        StateObject state = (StateObject)ar.AsyncState;
        Socket handler = state.workSocket;

        int bytesRead = handler.EndReceive(ar);

        if (bytesRead > 0)
        {

            state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

            content = state.sb.ToString();

            Thread.Sleep(new TimeSpan(0, 0, int.Parse(appSettings["TimeSpanSleep"])));
            //Console.WriteLine(HexEncoding.HexEncode(state.buffer, 0, state.buffer.Length));

            byte[] byteData = Messaging.GetMensajeBytes(state.buffer, "BPT_0200", CodigoProcesamiento);

            CodigoProcesamiento += 1;

            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
        }
    }

    private static void Send(Socket handler, String data)
    {
        byte[] byteData = Encoding.ASCII.GetBytes(data);

        handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
    }

    private static void SendCallback(IAsyncResult ar)
    {
        try
        {

            Socket handler = (Socket)ar.AsyncState;

            int bytesSent = handler.EndSend(ar);
            //Console.WriteLine("Sent {0} bytes to client.", bytesSent);

            handler.Shutdown(SocketShutdown.Both);
            handler.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static int Main(String[] args)
    {
        StartListening();
        return 0;
    }
}