using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace QuizGameClient
{
    public partial class Form1 : Form
    {
        private ClientLogic client;
        private Question currentQuestion;
        private List<RadioButton> radioButtons;
        private Label lblWelcome;
        private Label lblQuestion;
        private Label lblTimer;
        private Label lblQuestionCount;
        private Button btnStart;
        private Button btnSubmit;
        private Button btnPlayAgain;
        private Button btnExit;
        private Label lblScore;
        private Panel welcomePanel;
        private Panel gamePanel;
        private Panel resultPanel;
        private System.Windows.Forms.Timer gameTimer;
        private const int TOTAL_TIME = 150; // 2 minutes 30 seconds
        private const int TOTAL_QUESTIONS = 15;
        private int timeRemaining;
        private int totalScore = 0;
        private int currentQuestionIndex = 0;
        private HashSet<int> answeredQuestions = new HashSet<int>();

        public Form1()
        {
            InitializeComponents();
            client = new ClientLogic();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000; // 1 second
            gameTimer.Tick += Timer_Tick;
        }

        private void InitializeComponents()
        {
            // Form settings
            this.Text = "Quiz Game";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            InitializeWelcomePanel();
            InitializeGamePanel();
            InitializeResultPanel();

            // Hiển thị màn hình chào mừng đầu tiên
            ShowWelcomeScreen();
        }

        private void InitializeWelcomePanel()
        {
            welcomePanel = new Panel
            {
                Size = new Size(800, 600),
                Location = new Point(0, 0)
            };

            // Welcome Label
            lblWelcome = new Label
            {
                Location = new Point(100, 150),
                Size = new Size(600, 100),
                Text = "Chào mừng các bạn đến với\ntrò chơi đố vui kiến thức!!!",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 24, FontStyle.Bold),
                ForeColor = Color.DarkBlue
            };

            // Start Button
            btnStart = new Button
            {
                Text = "Chơi Ngay",
                Location = new Point(300, 300),
                Size = new Size(200, 50),
                Font = new Font("Arial", 14, FontStyle.Bold),
                BackColor = Color.LightGreen,
                FlatStyle = FlatStyle.Flat
            };
            btnStart.Click += (s, e) => StartGame();

            welcomePanel.Controls.Add(lblWelcome);
            welcomePanel.Controls.Add(btnStart);
            this.Controls.Add(welcomePanel);
        }

        private void InitializeGamePanel()
        {
            gamePanel = new Panel
            {
                Size = new Size(800, 600),
                Location = new Point(0, 0),
                Visible = false
            };

            // Timer Label
            lblTimer = new Label
            {
                Location = new Point(600, 20),
                Size = new Size(150, 30),
                Text = "Thời gian: 2:30",
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Red
            };

            // Question Counter Label
            lblQuestionCount = new Label
            {
                Location = new Point(50, 20),
                Size = new Size(150, 30),
                Text = "Câu hỏi: 1/15",
                Font = new Font("Arial", 12, FontStyle.Bold)
            };

            // Question Label
            lblQuestion = new Label
            {
                Location = new Point(50, 70),
                Size = new Size(700, 80),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 14)
            };

            // Radio Buttons for answers
            radioButtons = new List<RadioButton>();
            for (int i = 0; i < 4; i++)
            {
                RadioButton rb = new RadioButton
                {
                    Location = new Point(100, 180 + i * 50),
                    Size = new Size(600, 30),
                    Font = new Font("Arial", 12)
                };
                radioButtons.Add(rb);
                gamePanel.Controls.Add(rb);
            }

            // Submit Button
            btnSubmit = new Button
            {
                Text = "Trả lời",
                Location = new Point(300, 400),
                Size = new Size(200, 40),
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.LightBlue,
                FlatStyle = FlatStyle.Flat
            };
            btnSubmit.Click += async (s, e) => await SubmitAnswer();

            gamePanel.Controls.Add(lblTimer);
            gamePanel.Controls.Add(lblQuestionCount);
            gamePanel.Controls.Add(lblQuestion);
            gamePanel.Controls.Add(btnSubmit);
            this.Controls.Add(gamePanel);
        }

        private void InitializeResultPanel()
        {
            resultPanel = new Panel
            {
                Size = new Size(800, 600),
                Location = new Point(0, 0),
                Visible = false
            };

            // Score Label
            lblScore = new Label
            {
                Location = new Point(200, 150),
                Size = new Size(400, 100),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 20, FontStyle.Bold)
            };

            // Play Again Button
            btnPlayAgain = new Button
            {
                Text = "Chơi Lại",
                Location = new Point(200, 300),
                Size = new Size(150, 50),
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.LightGreen,
                FlatStyle = FlatStyle.Flat
            };
            btnPlayAgain.Click += (s, e) => RestartGame();

            // Exit Button
            btnExit = new Button
            {
                Text = "Thoát",
                Location = new Point(450, 300),
                Size = new Size(150, 50),
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.LightCoral,
                FlatStyle = FlatStyle.Flat
            };
            btnExit.Click += (s, e) => Application.Exit();

            resultPanel.Controls.Add(lblScore);
            resultPanel.Controls.Add(btnPlayAgain);
            resultPanel.Controls.Add(btnExit);
            this.Controls.Add(resultPanel);
        }

        private void ShowWelcomeScreen()
        {
            welcomePanel.Visible = true;
            gamePanel.Visible = false;
            resultPanel.Visible = false;
        }

        private void ShowGameScreen()
        {
            welcomePanel.Visible = false;
            gamePanel.Visible = true;
            resultPanel.Visible = false;
        }

        private void ShowResultScreen()
        {
            welcomePanel.Visible = false;
            gamePanel.Visible = false;
            resultPanel.Visible = true;

            // Đảm bảo rằng totalScore không vượt quá TOTAL_QUESTIONS
            if (totalScore > TOTAL_QUESTIONS)
            {
                totalScore = TOTAL_QUESTIONS;
            }

            float percentage = (float)totalScore / TOTAL_QUESTIONS * 100;
            lblScore.Text = $"Điểm của bạn: {totalScore}/{TOTAL_QUESTIONS} ({percentage:F2}%) trả lời đúng";
        }


        private async void StartGame()
        {
            totalScore = 0;
            currentQuestionIndex = 0;
            answeredQuestions.Clear();
            ShowGameScreen();
            await GetNextQuestion();
            StartTimer();
        }

        private void RestartGame()
        {
            gameTimer.Stop();
            StartGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeRemaining--;
            UpdateTimerDisplay();

            if (timeRemaining <= 10)
            {
                lblTimer.ForeColor = lblTimer.ForeColor == Color.Red ? Color.Black : Color.Red;
            }

            if (timeRemaining <= 0)
            {
                TimeUp();
            }
        }

        private void UpdateTimerDisplay()
        {
            int minutes = timeRemaining / 60;
            int seconds = timeRemaining % 60;
            lblTimer.Text = $"Thời gian: {minutes}:{seconds:D2}";
        }

        private void TimeUp()
        {
            gameTimer.Stop();
            MessageBox.Show("Hoàn thành!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ShowResultScreen();
        }

        private void End()
        {
            gameTimer.Stop();
            MessageBox.Show("Hoàn thành!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ShowResultScreen();
        }

        private void StartTimer()
        {
            timeRemaining = TOTAL_TIME;
            UpdateTimerDisplay();
            lblTimer.ForeColor = Color.Black;
            gameTimer.Start();
        }

        private async Task GetNextQuestion()
        {
            try
            {
                if (currentQuestionIndex >= TOTAL_QUESTIONS)
                {
                    End();
                    return;
                }

                currentQuestion = await client.RequestQuestion();
                while (answeredQuestions.Contains(currentQuestion.Id))
                {
                    currentQuestion = await client.RequestQuestion();
                }

                answeredQuestions.Add(currentQuestion.Id);
                lblQuestion.Text = currentQuestion.Content;
                lblQuestionCount.Text = $"Câu hỏi: {currentQuestionIndex + 1}/{TOTAL_QUESTIONS}";

                for (int i = 0; i < radioButtons.Count; i++)
                {
                    radioButtons[i].Text = currentQuestion.Options[i];
                    radioButtons[i].Checked = false;
                    radioButtons[i].Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task SubmitAnswer()
        {
            int selectedAnswer = -1;
            for (int i = 0; i < radioButtons.Count; i++)
            {
                if (radioButtons[i].Checked)
                {
                    selectedAnswer = i;
                    break;
                }
            }

            if (selectedAnswer == -1)
            {
                MessageBox.Show("Vui lòng chọn một đáp án!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int score = await client.SubmitAnswer(selectedAnswer);
                if (selectedAnswer == currentQuestion.CorrectAnswer)
                {
                    totalScore += 1;
                }

                currentQuestionIndex++;

                // Kiểm tra nếu đã trả lời hết câu hỏi
                if (currentQuestionIndex >= TOTAL_QUESTIONS)
                {
                    End(); // Gọi End() để hiển thị thông báo hoàn thành
                    return;
                }

                // Nếu chưa hết câu hỏi và còn thời gian thì tiếp tục
                if (timeRemaining > 0)
                {
                    await GetNextQuestion();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            gameTimer?.Stop();
            gameTimer?.Dispose();
        }
    }
}