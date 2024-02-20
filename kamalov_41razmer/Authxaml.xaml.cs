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
using System.Windows.Threading;

namespace kamalov_41razmer
{
    /// <summary>
    /// Логика взаимодействия для Authxaml.xaml
    /// </summary>
    public partial class Authxaml : Page
    {
        public Authxaml()
        {
            DispatcherTimer timer = new DispatcherTimer();

            InitializeComponent();
            {
                InitializeComponent();
                timer.Interval = TimeSpan.FromSeconds(10);
                //timer.Tick += Timer_Tick;
            }



            //private void Timer_Tick(object sender, EventArgs e)
            //{
            //    Sign.IsEnabled = true; // Включить TextBox
            //    timer.Stop(); // Остановить таймер
            //}



        }

        private void SignGhost_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ServicePage(null));
            Login.Text = "";
            Password.Text = "";
        }

        private async void Sign_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Text;

            if (login == "" || password == "")
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка");
            }
            else
            {
                User user = KamalovEntities.GetContext().User.ToList().Find(p => p.UserLogin == login && p.UserPassword == password);
                if (user != null)
                {
                    Manager.MainFrame.Navigate(new ServicePage(user));
                    Login.Text = "";
                    Password.Text = "";
                }
                else
                {
                    MessageBox.Show("Введены неверные данные!", "Ошибка");
                    Sign.IsEnabled = false;
                    await Task.Delay(10000);
                    Sign.IsEnabled = true;
                    /*timer.Start();*/ // Запустить таймер
                }
            }
        }
    }
}
