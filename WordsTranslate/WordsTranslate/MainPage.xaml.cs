using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WordsTranslate
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Words_Click(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new WordsPage());
        }

        private async void Button_Table_Click(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TablePage());
        }

        private async void Button_Write_Click(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new WritePage());
        }
    }
}
