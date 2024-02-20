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

namespace kamalov_41razmer
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        string CountObjectAll;
        int curKol = 0;
        User user;
        public ServicePage(User _user)
        {
            InitializeComponent();
            user = _user;

            var currentProduct = KamalovEntities.GetContext().Product.ToList();
            curKol = currentProduct.Count;
            ProductListView.ItemsSource = currentProduct;
            CountObjectAll = Convert.ToString(currentProduct.Count);
            CountObject.Text = "Кол-во " + currentProduct.Count.ToString() + " из " + CountObjectAll;

            DiscountFilter.SelectedIndex = 0;
            TBoxSearch.Text = "";

            if (user == null)
            {
                authuser.Text = "Вы авторизованы как гость";
                authuserrole.Text = "Роль: Гость";
            }
            else
            {
                authuser.Text = "Вы авторизованы как " + user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
                switch (user.UserRole)
                {
                    case 2:
                        authuserrole.Text = "Роль: " + "Клиент"; break;
                    case 3:
                        authuserrole.Text = "Роль: " + "Менеджер"; break;
                    case 1:
                        authuserrole.Text = "Роль: " + "Администратор"; break;
                    default:
                        break;
                }
            }
        }

        private void UpdateProducts()
        {
            var currentProducts = KamalovEntities.GetContext().Product.ToList();

            if (UpCost.IsChecked.Value)
            {
                currentProducts = currentProducts.OrderBy(p => p.ProductCost).ToList();
            }
            if (DownCost.IsChecked.Value)
            {
                currentProducts = currentProducts.OrderByDescending(p => p.ProductCost).ToList();
            }
            if (DiscountFilter.SelectedIndex == 1)
            {
                currentProducts = currentProducts.Where(p => (p.ProductDiscountAmount > 0 && p.ProductDiscountAmount <= 9.99)).ToList();
            }
            if (DiscountFilter.SelectedIndex == 2)
            {
                currentProducts = currentProducts.Where(p => (p.ProductDiscountAmount >= 10 && p.ProductDiscountAmount < 14.99)).ToList();
            }
            if (DiscountFilter.SelectedIndex == 3)
            {
                currentProducts = currentProducts.Where(p => (p.ProductDiscountAmount >= 15 && p.ProductDiscountAmount <= 100)).ToList();
            }
            currentProducts = currentProducts.Where(p => p.ProductName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            CountObject.Text = "Кол-во " + currentProducts.Count.ToString() + " из " + CountObjectAll;
            //.Text = currentProducts.Count.ToString();
            ProductListView.ItemsSource = currentProducts;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }

        private void TBSearch_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void TBSearch_DataContextChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void SortCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DiscountFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UpCost_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProducts();
        }

        private void DownCost_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProducts();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void DiscountFilter_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow();
            orderWindow.client.Text = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
        }
    }
}
