using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfAppShopping
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<ShoppingCartitem> ShoppingCart { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            InitializeProducts();
            ShoppingCart = new ObservableCollection<ShoppingCartitem>();
            DataContext = this;
        }

        private void InitializeProducts()
        {
            Products = new ObservableCollection<Product>
            {
                new Product { Name = "Яблоко", Price = 15, Quantity = 100, ImagePath = "apple.png" },
                new Product { Name = "Вишня", Price = 210, Quantity = 20, ImagePath = "cherry.png" },
                new Product { Name = "Виноград", Price = 75, Quantity = 60, ImagePath = "grape.png" },
                new Product { Name = "Апельсин", Price = 60, Quantity = 200, ImagePath = "orange.png" },
                new Product { Name = "Персик", Price = 200, Quantity = 10, ImagePath = "peach.png" },
            };
        }
        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Product selectedProduct = button.Tag as Product;
                if (selectedProduct != null && selectedProduct.Quantity > 0)
                {
                    
                    selectedProduct.Quantity--;

                    ShoppingCartitem cartItem = ShoppingCart.FirstOrDefault(item => item.ProductName == selectedProduct.Name);
                    if (cartItem != null)
                    {
                        cartItem.Quantity++;
                    }
                    else
                    {
                        ShoppingCart.Add(new ShoppingCartitem
                        {
                            ProductName = selectedProduct.Name,
                            Price = selectedProduct.Price,
                            Quantity = 1
                        });
                    }
                }
            }
        }
        private void ShowCartButton_Click(object sender, RoutedEventArgs e)
        {
           
            StringBuilder sb = new StringBuilder();
            decimal totalAmount = 0;

            foreach (var item in ShoppingCart)
            {
                sb.AppendLine($"{item.ProductName} - {item.Quantity} шт. - {item.Total:F2}грн");
                totalAmount += item.Total;
            }

            sb.AppendLine($"Общая сумма: {totalAmount:F2}грн");

            MessageBox.Show(sb.ToString(), "Корзина");
        }
    }

}

