using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ServerLogic
{
    private UdpClient server;
    private QuestionManager questionManager;
    private Dictionary<string, int> playerScores;
    private const int PORT = 11000;

    public ServerLogic()
    {
        server = new UdpClient(PORT);
        questionManager = new QuestionManager();
        playerScores = new Dictionary<string, int>();
    }

    public async Task StartServer()
    {
        Console.WriteLine("Server started on port " + PORT);

        while (true)
        {
            try
            {
                UdpReceiveResult result = await server.ReceiveAsync();
                string message = Encoding.UTF8.GetString(result.Buffer);
                IPEndPoint clientEndPoint = result.RemoteEndPoint;

                if (message == "REQUEST_QUESTION")
                {
                    Question question = questionManager.GetRandomQuestion();
                    string questionJson = JsonSerializer.Serialize(question);
                    byte[] responseData = Encoding.UTF8.GetBytes(questionJson);
                    await server.SendAsync(responseData, responseData.Length, clientEndPoint);
                }
                else if (message.StartsWith("ANSWER:"))
                {
                    // Process answer and send score back
                    string[] parts = message.Split(':');

                    if (parts.Length < 2 || !int.TryParse(parts[1], out int answerId))
                    {
                        Console.WriteLine("Invalid answer format.");
                        return; // Hoặc xử lý theo cách khác
                    }

                    //int answerId = int.Parse(parts[1]);
                    string clientKey = clientEndPoint.ToString();

                    if (!playerScores.ContainsKey(clientKey))
                        playerScores[clientKey] = 0;

                    playerScores[clientKey]++;

                    string scoreMessage = $"SCORE:{playerScores[clientKey]}";
                    byte[] scoreData = Encoding.UTF8.GetBytes(scoreMessage);
                    await server.SendAsync(scoreData, scoreData.Length, clientEndPoint);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}