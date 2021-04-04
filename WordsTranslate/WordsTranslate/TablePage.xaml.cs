using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	public partial class TablePage : ContentPage
	{
        public ObservableCollection<StringFile> stringFileColl { get; set; }
        private StringFile stringFile = null;
        private string[] words = null;

		public TablePage ()
		{
			InitializeComponent ();

            var text = "";
            stringFileColl = new ObservableCollection<StringFile>();
            var assembly = typeof(TablePage).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("WordsTranslate.Files.words.txt");
            using (var reader = new StreamReader(stream))
            {
                while ((text = reader.ReadLine()) != null)
                {
                    words = text.Split('|');
                    stringFile = new StringFile
                    {
                        Word = words[0],
                        Translate = words[1]
                    };
                    stringFileColl.Add(stringFile);
                }
            }
            this.BindingContext = this;
        }

        private async void Button_Back_Click(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage());
        }
	}

    public class StringFile
    {
        public string Word { get; set; }
        public  string Translate { get; set; }
    }
}