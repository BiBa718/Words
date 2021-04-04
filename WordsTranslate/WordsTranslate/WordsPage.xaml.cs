using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordsTranslate
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WordsPage : ContentPage
    {
        private int countWords = 0; //правильные ответы
        private int countLine = 0; //кол-во линий в файле
        Random random = new Random();
        private int[] masWords = null; //массив для устранения повторений
        private string[] words = null; //для разбития строки
        int i = 0;

        public WordsPage ()
		{
			InitializeComponent ();

            var assembly = typeof(WordsPage).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("WordsTranslate.Files.words.txt");
            using (var reader = new StreamReader(stream))
            {
                while (reader.ReadLine() != null)
                {
                    countLine++;
                }
            }
            masWords = new int[countLine];
            Label_Stat.Text = "0 / " + countLine.ToString();
            NextWord();
        }

        private async void Button_Back_Click(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage());
        }

        private void Button_Restart_Click(object sender, EventArgs e)
        {
            countWords = 0;
            Label_Stat.Text = "0 / " + countLine.ToString();
            for (int j = 0; j < masWords.Length; j++)
            {
                masWords[j] = 0;
            }
            Button_Right.IsEnabled = true;
            Button_Wrong.IsEnabled = true;
            Label_RU.IsVisible = false;
            NextWord();
        }

        private void Button_Check_Click(object sender, EventArgs e)
        {
            Label_RU.IsVisible = true;
        }

        private void Button_Wrong_Click(object sender, EventArgs e)
        {
            Label_RU.IsVisible = false;
            NextWord();
        }

        private void Button_Right_Click(object sender, EventArgs e)
        {
            countWords++;
            Label_Stat.Text = countWords.ToString() + " / " + countLine.ToString();
            masWords[i] = 1;
            if (countWords != countLine)
            {
                Label_RU.IsVisible = false;
                NextWord();
            }
            else
            {
                Button_Wrong.IsEnabled = false;
                Button_Right.IsEnabled = false;
            }
        }

        private void NextWord()
        {
            string text = "";
            do
            {
                i = random.Next(countLine);
            } while (masWords[i] != 0);

            var assembly = typeof(WordsPage).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("WordsTranslate.Files.words.txt");
            using (var reader = new StreamReader(stream))
            {
                for (int j = 0; j <= i; j++)
                {
                    text = reader.ReadLine();
                }
            }
            words = text.Split('|');
            Label_EN.Text = words[0];
            Label_RU.Text = words[1];
        }
    }
}