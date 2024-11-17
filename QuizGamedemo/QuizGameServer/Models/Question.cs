using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class Question
{
    public int Id { get; set; }
    public string Content { get; set; }
    public List<string> Options { get; set; }
    public int CorrectAnswer { get; set; }

    public Question(int id, string content, List<string> options, int correctAnswer)
    {
        Id = id;
        Content = content;
        Options = options;
        CorrectAnswer = correctAnswer;
    }
}