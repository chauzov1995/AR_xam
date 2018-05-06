using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{



    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {

        public Page1()
        {
            InitializeComponent();
            string dbPath = DependencyService.Get<ISQLite>().GetDatabasePath("friends.db");
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<an_dohod>();
            db.CreateTable<an_dkr_hist>();
            db.CreateTable<an_purse>();
            //     db.Insert(new an_dohod { komment = "Работа", id = "2", summa_dohod = "25000" }); // after creating the newStock object


            var stockList = db.Query<vis_an_dohod>("SELECT a.[id], a.[name_dohod], b.summa, a.[summa_dohod], a.[komment] " +
                "FROM [an_dohod] a " +
                "LEFT JOIN ( SELECT SUM([summa]) summa,[kuda] FROM [an_dkr_hist] GROUP BY [kuda]) b ON  a.[id]=b.[kuda]");

            var newspis = new List<vis_an_dohod>();
            foreach (vis_an_dohod elem in stockList)
            {

               // DisplayAlert("Item Selected", elem.name_dohod.ToString(), "Ok");
                switch (elem.name_dohod)
                {
                    case 1:
                        elem.bg = "#3cd528";
                        break;
                    case 2:
                        elem.bg = "#df812a";
                        break;
                    case 3:
                        elem.bg = "#2abadf";
                        break;
                    default:
                        elem.bg = "#eee";
                        break;
                }

                newspis.Add(elem);
            }


            listView.ItemsSource = newspis;






        }

        async void OnSelection(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            //  DisplayAlert("Item Selected", (e.SelectedItem as an_dohod).komment.ToString(), "Ok");


            vis_an_dohod element = (e.Item as vis_an_dohod);
            var detailPage = new sozdkr(element);
            await Navigation.PushAsync(detailPage);



            //((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
        }
        public async void OnButtonClicked(object sender, EventArgs args)
        {
            var detailPage = new Page2();
            await Navigation.PushAsync(detailPage);

        }





    }
    public static class faktdata
    {

        public static string Get(DateTime date)
        {

            DateTime thisDay = DateTime.Today;
            string timestr = thisDay.ToString("yyyy.MM.dd");
            return (timestr);
        }
      

    }


  public  class vis_an_dohod
    {

        public int id { get; set; }
        //   public string id_clienta { get; set; }
        public int name_dohod { get; set; }
        public string summa_dohod { get; set; }
        public string summa_fakt1 { get; set; }
        public string komment { get; set; }
        public int visible { get; set; }
        public int postoyan { get; set; }
        public int nompp1 { get; set; }
        public string summa { get; set; }
        public string bg { get; set; }

    }


    public class an_dkr_hist
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        //    public string id_clienta { get; set; }
        public int kuda { get; set; }
        public int purse { get; set; }
        public string summa { get; set; }
        public string komment { get; set; }
        public string data_fakt { get; set; }
        public string data { get; set; }
        public int visible { get; set; }
    }

    public class an_dohod
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        //   public string id_clienta { get; set; }
        public int name_dohod { get; set; }
        public string summa_dohod { get; set; }
        public string summa_fakt1 { get; set; }
        public string komment { get; set; }
        public int visible { get; set; }
        public int postoyan { get; set; }
        public int nompp1 { get; set; }
        public string data_fakt { get; set; }
    }
    public class an_purse
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        // public string id_clienta { get; set; }
        public string summa { get; set; }
        public string komment { get; set; }
        public int visible { get; set; }
        public int deafault { get; set; }
        public string data_fakt { get; set; }
    }



}