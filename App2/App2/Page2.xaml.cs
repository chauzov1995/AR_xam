using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page2 : ContentPage
    {
        vis_an_dohod categoria;

        public Page2(vis_an_dohod categoria)
        {
            InitializeComponent();

            this.Title = "Создаём категорию";
            this.categoria = categoria;


            var monkeyList = new List<string>();
            monkeyList.Add("Доход");
            monkeyList.Add("Расход");
            monkeyList.Add("Цель");


            picker.ItemsSource = monkeyList;

            if (categoria != null)
            {
                komment.Text = categoria.komment;
                summa.Text = categoria.summa;
                picker.SelectedIndex = categoria.name_dohod - 1;
            }
            else
            {
                picker.SelectedIndex = 0;
            }







        }

        public async void OnButtonClicked(object sender, EventArgs args)
        {
            int namedohod = picker.SelectedIndex + 1;


            string dbPath = DependencyService.Get<ISQLite>().GetDatabasePath("friends.db");
            var db = new SQLiteConnection(dbPath);
            if (categoria == null)
            {
                db.Insert(new an_dohod { komment = komment.Text, name_dohod = namedohod, summa_dohod = summa.Text, data_fakt = faktdata.Get(DateTime.Now) }); // after creating the newStock object
            }
            else
            {
                db.Query<vis_an_dohod>("UPDATE [an_dohod] SET [komment]='" + komment.Text + "', [name_dohod]=" + namedohod + ", [summa_dohod]=" + summa.Text +
                  " WHERE [id]=" + categoria.id);
            }


            await Navigation.PopAsync();

        }
    }
}