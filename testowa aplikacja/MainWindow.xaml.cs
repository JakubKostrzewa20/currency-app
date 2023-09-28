using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
namespace testowa_aplikacja
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
             
        }
        public async Task<string> Konwersja(string from, string to)
        {
            using (var client = new HttpClient())
            {
                    client.BaseAddress = new Uri("https://api.currencyapi.com/");
                    string linkacz = string.Format("v3/latest?apikey=cur_live_Msk5Fomfl2UlwnYh0S8Bxeumh4tqLR3l1cuOq6LZ&currencies={0}&base_currency={1}", to, from);
                    var response = await client.GetAsync(linkacz);

                    if (response.IsSuccessStatusCode)
                    {
                        var stringResult = await response.Content.ReadAsStringAsync();
                        var data = JObject.Parse(stringResult);
                        var wynik = data["data"][$"{to}"]["value"];
                        return (string)wynik;
                    }
                    else
                    {
                        string message = "Brak połączenia z API. Spróbuj ponownie później";
                        string caption = "Błąd";
                        MessageBox.Show(message, caption);
                        return null;
                    }
                  
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string From = waluta1.Text;
            decimal suma;
            string To = waluta2.Text;
            decimal f;
            if(decimal.TryParse(wpisane.Text, out f)&& From !=null && To!=null && From!=To)
            {
                string cos = (await Konwersja(From, To));
                if(cos!=null)
                {
                    suma = Convert.ToDecimal(cos, CultureInfo.InvariantCulture);
                    suma = suma * f;
                
                    wynik.Text = suma.ToString();
                }  
            }
            else
            {
                string message = "Źle uzupełnione dane";
                string caption = "Błąd";
                MessageBox.Show(message, caption);
            } 
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void Waluta1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Waluta2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
