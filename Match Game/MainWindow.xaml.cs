using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Match_Game
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // 欄位
        TextBlock lastTextblockClicked;
        bool findingMatch = false; // 標示玩家按的是第幾個emoji false狀態下按的就是第一個
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " 再玩一次?";
            }
        }

        private void SetUpGame()    // 初始化遊戲，將八對emoji隨機分給textblock
        {
            List<string> animalEmoji = new List<string> // 建立一個List用來存放emoji
            {
                "🦐", "🐬", "🐤", "🦔", "🐧", "🦈", "🦑", "🐈",
                "🦐", "🐬", "🐤", "🦔", "🐧", "🦈", "🦑", "🐈"
            };

            Random rd = new Random(); // 實例化一個Random物件叫"rd"

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) //從mainGrid存取每一個TextBlock類型物件，取名叫textblock
            {
                if (textBlock.Name.Contains("time") == false)   // 跳過timeTextBlock
                {
                    int index = rd.Next(animalEmoji.Count); // 將list長度隨機一個數字(0-16之間)傳入index
                    string nextEmoji = animalEmoji[index];  // 用隨機出來的index從list裡抓出一個emoji存入nextEmoji
                    textBlock.Text = nextEmoji;             // 把nextEmoji裡的emoji存入textBlock
                    animalEmoji.RemoveAt(index);            // 將list裡index位置的emoji移除（以免重複）
                }
            }

            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e) // 按下emoji的事件常式
        {
            TextBlock textBlock = sender as TextBlock;

            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextblockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextblockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextblockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
