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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SetUpGame();
        }

        private void SetUpGame() //存放8對emoji，隨機分給textblock
        {
            List<string> animalEmoji = new List<string> // 建立一個List用來存放emoji
            {
                "🦐", "🐬", "🐤", "🦔", "🐧", "🦈", "🦑", "🐈",
                "🦐", "🐬", "🐤", "🦔", "🐧", "🦈", "🦑", "🐈"
            };

            Random rd = new Random(); // 實例化一個Random物件叫"rd"

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) //從mainGrid存取每一個TextBlock類型物件，取名叫textblock
            {
                int index = rd.Next(animalEmoji.Count); // 將list長度隨機一個數字(0-16之間)傳入index
                string nextEmoji = animalEmoji[index];  // 用隨機出來的index從list裡抓出一個emoji存入nextEmoji
                textBlock.Text = nextEmoji;             // 把nextEmoji裡的emoji存入textBlock
                animalEmoji.RemoveAt(index);            // 將list裡index位置的emoji移除（以免重複）
            }

        }
    }
}
