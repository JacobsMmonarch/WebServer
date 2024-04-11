using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    public class Server
    {
        static async Task Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("https://localhost:7191");

            try
            {
                listener.Start();
                Console.WriteLine("Сервер запущен...");
                while (true)
                {
                    HttpListenerContext context = await listener.GetContextAsync();
                    await ProcessRequest(context);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            finally
            {
                listener.Close();
            }
        }

        static async Task ProcessRequest(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            if (request.HttpMethod == "POST")
            {
                using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    string requestBody = await reader.ReadToEndAsync();
                    Console.WriteLine($"Получено сообщение от клиента: {requestBody}");

                    byte[] responseData = Encoding.UTF8.GetBytes("Сообщение получено успешно");
                    response.ContentType = "text/plain";
                    response.ContentEncoding = Encoding.UTF8;
                    response.ContentLength64 = responseData.Length;
                    await response.OutputStream.WriteAsync(responseData, 0, responseData.Length);
                }
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                response.Close();
            }
        }
    }
}