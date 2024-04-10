using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

class Server
{
    static async Task Main(string[] args)
    {
        //string directoryPath = "C:\\Users\\Practika\\source\\repos\\WebServer\\file_sys";
        //Directory.CreateDirectory(directoryPath);

        //// Создание файла
        //string filePath = "C:\\Users\\Practika\\source\\repos\\WebServer\\file_sys\\file.txt";
        //File.WriteAllText(filePath, "This is a new file content.");

        //Console.WriteLine("File system created successfully.");




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


    //  Main(string[] args):
    //    Это точка входа в приложение.
    //    Создает новый экземпляр HttpListener и добавляет к нему префикс "https://localhost:7191", указывая, что сервер должен прослушивать входящие запросы по этому URL.
    //    Запускает сервер методом Start().
    //    В бесконечном цикле ожидает входящие запросы с помощью метода GetContextAsync() и передает их на обработку методу ProcessRequest().
    //    Обрабатывает любые исключения, которые могут возникнуть в процессе выполнения сервера.
    //    В блоке finally закрывает экземпляр HttpListener.

    //  ProcessRequest(HttpListenerContext context):
    //    Получает объекты запроса и ответа из контекста HTTP-запроса.
    //    Проверяет метод запроса. Если он равен "POST", то:
    //        Считывает тело запроса из потока ввода с помощью StreamReader.
    //        Выводит полученное сообщение от клиента в консоль.
    //        Формирует ответное сообщение.
    //        Устанавливает заголовки ответа для отправки сообщения с типом контента "text/plain".
    //        Записывает ответное сообщение в поток ответа.

    //  Если метод запроса не равен "POST", устанавливает статус ответа "Method Not Allowed" и закрывает поток ответа.


    // HttpListener... - Предоставляет доступ к объектам запроса и ответа, используемым классом HttpListener . Этот класс не может быть унаследован. 














namespace WebServer
{
    public class RecAndSend_message
    {

    }
}
