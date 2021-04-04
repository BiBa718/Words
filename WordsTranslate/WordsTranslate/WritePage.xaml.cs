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
	public partial class WritePage : ContentPage
	{
        private int countWords = 0; //правильные ответы
        private int countLine = 0; //кол-во линий в файле
        Random random = new Random();
        private int[,] masWords = null; //массив для устранения повторений
        private string[] words = null; //для разбития строки
        int i = 0;

        public WritePage ()
		{
			InitializeComponent ();


            var assembly = typeof(WritePage).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("WordsTranslate.Files.words.txt");
            using (var reader = new StreamReader(stream))
            {
                while (reader.ReadLine() != null)
                {
                    countLine++;
                }
            }
            masWords = new int[countLine,2];
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
            for (int j = 0; j < masWords.GetUpperBound(0); j++)
            {
                for (int k = 0; k < masWords.GetUpperBound(1); k++)
                {
                    masWords[j,k] = 0;
                }
            }
            Entry_Write.Text = "";
            Entry_Write.TextColor = Color.Black;
            Button_Next.IsEnabled = true;
            NextWord();
        }

        private void Button_Check_Click(object sender, EventArgs e)
        {
            if (Entry_Write.Text == words[0])
            {
                countWords++;
                Label_Stat.Text = countWords.ToString() + " / " + countLine.ToString();
                Entry_Write.TextColor = Color.Green;
                masWords[i,0] = 1;
                Button_Check.IsEnabled = false;
            }
            else
            {
                Entry_Write.TextColor = Color.Red;
                masWords[i,1]++;
                if (masWords[i,1] >= 3)
                {
                    Label_Hint.Text = words[0];
                    Label_Hint.IsVisible = true;
                }
            }
        }

        private void Button_Next_Click(object sender, EventArgs e)
        {
            if (countWords != countLine)
            {
                Entry_Write.TextColor = Color.Black;
                Entry_Write.Text = "";
                Button_Check.IsEnabled = true;
                NextWord();
            }
            else
            {
                Button_Next.IsEnabled = false;
                Label_Hint.IsVisible = false;
            }
        }
        
        private void NextWord()
        {
            string text = "";
            do
            {
                i = random.Next(countLine);
            } while (masWords[i,0] != 0);

            var assembly = typeof(WritePage).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("WordsTranslate.Files.words.txt");
            using (var reader = new StreamReader(stream))
            {
                for (int j = 0; j <= i; j++)
                {
                    text = reader.ReadLine();
                }
            }
            words = text.Split('|');
            Label_RU.Text = words[1];
        }
    }
}