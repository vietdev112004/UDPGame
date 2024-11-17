using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ClientLogic
{
    private UdpClient client;
    private IPEndPoint serverEndPoint;
    private const int SERVER_PORT = 11000;

    public ClientLogic()
    {
        client = new UdpClient();
        serverEndPoint = new IPEndPoint(IPAddress.Loopback, SERVER_PORT);
    }

    public async Task<Question> RequestQuestion()
    {
        byte[] requestData = Encoding.UTF8.GetBytes("REQUEST_QUESTION");
        await client.SendAsync(requestData, requestData.Length, serverEndPoint);

        UdpReceiveResult result = await client.ReceiveAsync();
        string json = Encoding.UTF8.GetString(result.Buffer);
        return JsonSerializer.Deserialize<Question>(json);
    }

    public async Task<int> SubmitAnswer(int answerId)
    {
        string message = $"ANSWER:{answerId}";
        byte[] answerData = Encoding.UTF8.GetBytes(message);
        await client.SendAsync(answerData, answerData.Length, serverEndPoint);

        UdpReceiveResult result = await client.ReceiveAsync();
        string response = Encoding.UTF8.GetString(result.Buffer);
        string[] parts = response.Split(':');
        return int.Parse(parts[1]);
    }

}