using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace TestSocket
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Socket sk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //IPEndPoint direccion = new IPEndPoint(IPAddress.Any, 7004);
            //sk.Bind(direccion);
            //sk.Listen(1);
            //while (true)
            //{
                //try
                //{
                //    Console.WriteLine("escuchando");
                //    Socket escuchar = sk.Accept();
                    
                //    Console.WriteLine("Conectado con exito ip{0}, bufersize{1}", escuchar.RemoteEndPoint.AddressFamily, escuchar.ReceiveBufferSize);
                //    sk.Close();
                //    Console.WriteLine("Presione cualquier tecla para continuar");
                //    Console.ReadLine();
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine("Error:{0}", ex.Message);
                    
                //}
            //}

            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = 9000;
                IPAddress localAddr = IPAddress.Any;

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[128];
                String data = null;

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);

                        // Process the data sent by the client.
                        data = data.ToUpper();

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        // Send back a response.
                        //stream.Write(msg, 0, msg.Length);
                        //Console.WriteLine("Sent: {0}", data);
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }


            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
            

        }
    }
}
