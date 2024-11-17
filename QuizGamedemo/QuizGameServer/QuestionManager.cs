using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class QuestionManager
{
    private List<Question> questions;
    private Random random;

    public QuestionManager()
    {
        questions = new List<Question>();
        random = new Random();
        InitializeQuestions();
    }

    private void InitializeQuestions()
    {
        questions.Add(new Question(1, "UDP là giao thức gì?",
            new List<string> {
                "Connection-oriented",
                "Connectionless",
                "Cả hai",
                "Không phải các đáp án trên"
            }, 1));

        questions.Add(new Question(2, "Port mặc định của HTTP là?",
            new List<string> {
                "80",
                "443",
                "8080",
                "21"
            }, 0));

        questions.Add(new Question(3, "Trong mô hình OSI, TCP hoạt động ở tầng nào?",
            new List<string> {
                "Application Layer",
                "Transport Layer",
                "Network Layer",
                "Data Link Layer"
            }, 1));

        questions.Add(new Question(4, "Giao thức nào được sử dụng để phân giải tên miền thành địa chỉ IP?",
            new List<string> {
                "HTTP",
                "FTP",
                "DNS",
                "DHCP"
            }, 2));

        questions.Add(new Question(5, "Socket là gì?",
            new List<string> {
                "Một phần mềm mạng",
                "Một kết nối giữa client và server",
                "Một điểm cuối của kết nối hai chiều giữa hai chương trình qua mạng",
                "Một giao thức mạng"
            }, 2));

        questions.Add(new Question(6, "Trong C#, class nào được sử dụng để tạo UDP client?",
            new List<string> {
                "TcpClient",
                "UdpClient",
                "NetworkClient",
                "SocketClient"
            }, 1));

        questions.Add(new Question(7, "Phương thức nào trong UdpClient được sử dụng để gửi dữ liệu?",
            new List<string> {
                "Send()",
                "SendAsync()",
                "Write()",
                "Tất cả đều sai"
            }, 1));

        questions.Add(new Question(8, "IPEndPoint trong C# dùng để làm gì?",
            new List<string> {
                "Để lưu trữ dữ liệu",
                "Để xác định địa chỉ IP và port của endpoint trong mạng",
                "Để kết nối database",
                "Để mã hóa dữ liệu"
            }, 1));

        questions.Add(new Question(9, "Phương thức Encoding.UTF8.GetBytes() trong C# dùng để làm gì?",
            new List<string> {
                "Mã hóa dữ liệu",
                "Chuyển đổi chuỗi thành mảng byte",
                "Giải mã dữ liệu",
                "Nén dữ liệu"
            }, 1));

        questions.Add(new Question(10, "Trong lập trình mạng, multicast là gì?",
            new List<string> {
                "Gửi dữ liệu từ một nguồn đến một đích",
                "Gửi dữ liệu từ một nguồn đến tất cả các đích",
                "Gửi dữ liệu từ một nguồn đến một nhóm các đích",
                "Không gửi dữ liệu"
            }, 2));

        questions.Add(new Question(11, "Giao thức TCP khác gì so với UDP?",
            new List<string> {
                "TCP nhanh hơn UDP",
                "TCP đảm bảo độ tin cậy và thứ tự gói tin",
                "TCP không cần kết nối",
                "TCP sử dụng ít tài nguyên hơn"
            }, 1));
        questions.Add(new Question(12, "Giao thức TCP khác gì so với UDP?",
            new List<string> {
                "TCP nhanh hơn UDP",
                "TCP đảm bảo độ tin cậy và thứ tự gói tin",
                "TCP không cần kết nối",
                "TCP sử dụng ít tài nguyên hơn"
            }, 1));

        questions.Add(new Question(13, "Protocol nào dùng để chuyển đổi địa chỉ IP sang địa chỉ MAC trên mạng cục bộ?",
        new List<string> {
        "TCP",
        "ICMP",
        "IPv4",
        "ARP"
        }, 3));

        questions.Add(new Question(14, "TTL trong gói tin IPv4 có chức năng gì?",
            new List<string> {
        "Ngăn chặn gói tin bị lặp vô hạn trong mạng",
        "Ngăn chặn gói tin bị trùng lặp",
        "Ngăn chặn gói tin bị mất",
        "Ngăn chặn gói tin bị hỏng"
            }, 0));

        questions.Add(new Question(15, "Khi switch không tìm thấy địa chỉ MAC đích, nó sẽ làm gì?",
            new List<string> {
        "Thay đổi địa chỉ MAC đích",
        "Bỏ gói tin",
        "Chuyển tiếp gói tin tới tất cả các cổng",
        "Chuyển tiếp gói tin tới cổng đúng"
            }, 2));

        questions.Add(new Question(16, "Địa chỉ mạng nào sau đây thuộc lớp C?",
            new List<string> {
        "192.168.0.0",
        "172.16.0.0",
        "10.0.0.0",
        "224.0.0.0"
            }, 0));

        questions.Add(new Question(17, "Giao thức ICMP thường dùng trong công cụ nào?",
            new List<string> {
        "Telnet",
        "Ping",
        "SSH",
        "FTP"
            }, 1));

        questions.Add(new Question(18, "Trong IPv4, địa chỉ nào thường dùng để broadcast?",
            new List<string> {
        "127.0.0.1",
        "0.0.0.0",
        "255.255.255.255",
        "224.0.0.0"
            }, 2));

        questions.Add(new Question(19, "Giao thức nào hoạt động ở tầng 3 của mô hình OSI?",
            new List<string> {
        "Ethernet",
        "IP",
        "TCP",
        "HTTP"
            }, 1));

        questions.Add(new Question(20, "Địa chỉ IP nào không hợp lệ trong IPv4?",
            new List<string> {
        "192.168.1.256",
        "10.0.0.1",
        "172.16.0.1",
        "192.0.2.1"
            }, 0));

        questions.Add(new Question(21, "Cổng mặc định của HTTPS là gì?",
            new List<string> {
        "21",
        "443",
        "80",
        "22"
            }, 1));

        questions.Add(new Question(22, "Mạng LAN là viết tắt của?",
            new List<string> {
        "Local Area Network",
        "Large Area Network",
        "Limited Area Network",
        "Link Area Network"
            }, 0));

        questions.Add(new Question(23, "Đâu là giao thức bảo mật ở tầng ứng dụng?",
            new List<string> {
        "TCP",
        "UDP",
        "HTTPS",
        "IP"
            }, 2));

        questions.Add(new Question(24, "Subnet mask của địa chỉ IP 192.168.1.0/24 là gì?",
            new List<string> {
        "255.255.255.0",
        "255.255.0.0",
        "255.0.0.0",
        "255.255.255.255"
            }, 0));

        questions.Add(new Question(25, "Giao thức nào thường được sử dụng để gửi email?",
            new List<string> {
        "HTTP",
        "FTP",
        "SMTP",
        "POP3"
            }, 2));

        questions.Add(new Question(26, "Đâu là dải địa chỉ IP private?",
            new List<string> {
        "192.0.2.1",
        "172.16.0.1",
        "8.8.8.8",
        "1.1.1.1"
            }, 1));

        questions.Add(new Question(27, "Giao thức nào được thiết kế để kiểm tra tính kết nối của một thiết bị?",
            new List<string> {
        "TCP",
        "UDP",
        "ICMP",
        "ARP"
            }, 2));

        questions.Add(new Question(28, "DHCP là viết tắt của gì?",
            new List<string> {
        "Dynamic Host Control Protocol",
        "Dynamic Host Configuration Protocol",
        "Domain Host Control Protocol",
        "Domain Host Configuration Protocol"
    }, 1));

        questions.Add(new Question(29, "Trong mô hình TCP/IP, tầng nào tương đương với tầng Network trong mô hình OSI?",
            new List<string> {
        "Transport Layer",
        "Internet Layer",
        "Network Access Layer",
        "Application Layer"
            }, 1));

        questions.Add(new Question(30, "Port nào thường được sử dụng cho giao thức FTP?",
            new List<string> {
        "20 và 21",
        "22 và 23",
        "80 và 443",
        "25 và 110"
            }, 0));

        questions.Add(new Question(31, "NAT được sử dụng để làm gì?",
            new List<string> {
        "Mã hóa dữ liệu",
        "Chuyển đổi địa chỉ IP private thành public",
        "Định tuyến gói tin",
        "Kiểm tra lỗi gói tin"
            }, 1));

        questions.Add(new Question(32, "Trong C#, lớp nào được sử dụng để lắng nghe kết nối TCP?",
            new List<string> {
        "TcpClient",
        "TcpListener",
        "NetworkStream",
        "Socket"
            }, 1));

        questions.Add(new Question(33, "Giao thức nào được sử dụng để nhận email?",
            new List<string> {
        "SMTP",
        "IMAP",
        "FTP",
        "HTTP"
            }, 1));

        questions.Add(new Question(34, "Trong C#, phương thức nào của TcpClient được sử dụng để lấy luồng dữ liệu?",
            new List<string> {
        "GetStream()",
        "GetData()",
        "GetConnection()",
        "GetBytes()"
            }, 0));

        questions.Add(new Question(35, "VPN là viết tắt của?",
            new List<string> {
        "Virtual Private Network",
        "Virtual Public Network",
        "Virtual Protocol Network",
        "Virtual Personal Network"
            }, 0));

        questions.Add(new Question(36, "Trong IPv6, địa chỉ loopback là gì?",
            new List<string> {
        "::1",
        "0000::0000",
        "FFFF::FFFF",
        "FE80::1"
            }, 0));

        questions.Add(new Question(37, "WebSocket khác gì so với HTTP thông thường?",
            new List<string> {
        "Không có gì khác biệt",
        "Hỗ trợ kết nối hai chiều liên tục",
        "Chỉ hỗ trợ truyền dữ liệu một chiều",
        "Không hỗ trợ bảo mật"
            }, 1));

        questions.Add(new Question(38, "SSL/TLS hoạt động ở tầng nào trong mô hình OSI?",
            new List<string> {
        "Application Layer",
        "Transport Layer",
        "Session Layer",
        "Presentation Layer"
            }, 3));

        questions.Add(new Question(39, "Trong C#, async/await thường được sử dụng trong lập trình mạng để làm gì?",
            new List<string> {
        "Tăng tốc độ mạng",
        "Giảm sử dụng bộ nhớ",
        "Xử lý các thao tác non-blocking I/O",
        "Mã hóa dữ liệu"
            }, 2));

        questions.Add(new Question(40, "CIDR là gì?",
            new List<string> {
        "Một giao thức mạng",
        "Một phương pháp định địa chỉ IP và chia subnet",
        "Một công cụ bảo mật",
        "Một loại định tuyến"
            }, 1));

        questions.Add(new Question(41, "Giao thức nào được sử dụng trong Remote Desktop?",
            new List<string> {
        "FTP",
        "RDP",
        "HTTP",
        "SMTP"
            }, 1));

        questions.Add(new Question(42, "MAC Address có độ dài bao nhiêu bit?",
            new List<string> {
        "32 bit",
        "48 bit",
        "64 bit",
        "128 bit"
            }, 1));

        questions.Add(new Question(43, "Trong C#, NetworkStream là gì?",
            new List<string> {
        "Một giao thức mạng",
        "Một lớp để đọc/ghi dữ liệu qua mạng",
        "Một công cụ mã hóa",
        "Một loại địa chỉ IP"
            }, 1));

        questions.Add(new Question(44, "Giao thức nào được sử dụng để đồng bộ thời gian trong mạng?",
            new List<string> {
        "SMTP",
        "NTP",
        "FTP",
        "HTTP"
            }, 1));

        questions.Add(new Question(45, "Trong IPv4, một octet có thể chứa giá trị từ?",
            new List<string> {
        "-128 đến 127",
        "0 đến 255",
        "0 đến 512",
        "-256 đến 255"
            }, 1));

        questions.Add(new Question(46, "Trong C#, lớp IPAddress thuộc namespace nào?",
            new List<string> {
        "System.Net",
        "System.IO",
        "System.Web",
        "System.Network"
            }, 0));

        questions.Add(new Question(47, "Load Balancing là gì?",
            new List<string> {
        "Một phương pháp mã hóa dữ liệu",
        "Một kỹ thuật phân phối tải trên nhiều server",
        "Một giao thức mạng",
        "Một phương pháp định tuyến"
            }, 1));

        questions.Add(new Question(48, "Trong C#, phương thức BeginAccept() của TcpListener được sử dụng để làm gì?",
            new List<string> {
        "Kết thúc kết nối",
        "Bắt đầu lắng nghe kết nối không đồng bộ",
        "Gửi dữ liệu",
        "Nhận dữ liệu"
            }, 1));

        questions.Add(new Question(49, "MTU là viết tắt của gì?",
            new List<string> {
        "Maximum Transfer Unit",
        "Maximum Transmission Unit",
        "Multiple Transfer Unit",
        "Multiple Transmission Unit"
            }, 1));

        questions.Add(new Question(50, "Trong C#, phương thức nào được sử dụng để chuyển đổi tên miền thành địa chỉ IP?",
            new List<string> {
        "Dns.GetHostEntry()",
        "Dns.GetAddress()",
        "Dns.Resolve()",
        "Dns.GetHostName()"
            }, 0));

        // Thêm các câu hỏi khác về networking ở đây
    }

    public Question GetRandomQuestion()
    {
        int index = random.Next(questions.Count);
        return questions[index];
    }

    // Thêm phương thức GetQuestionById vào đây
    public Question? GetQuestionById(int questionId)
    {
        return questions.FirstOrDefault(q => q.Id == questionId);
    }
}