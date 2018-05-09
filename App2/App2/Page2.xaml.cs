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
        int idelem;

        public Page2(int  id)
        {
            InitializeComponent();

            this.Title = "Создаём категорию";
            this.idelem = id;


            var monkeyList = new List<string>();
            monkeyList.Add("Доход");
            monkeyList.Add("Расход");
            monkeyList.Add("Цель");


            picker.ItemsSource = monkeyList;


            if (id == 0)
            {//значит новый
              //  picker.SelectedIndex = 0;
            }
            else
            {
                string dbPath = DependencyService.Get<ISQLite>().GetDatabasePath("friends.db");
                var db = new SQLiteConnection(dbPath);


                an_dohod categoria = db.Query<an_dohod>("SELECT * " +
                "FROM [an_dohod] WHERE id=" + id).First();
                komment.Text = categoria.komment;
                summa.Text = categoria.summa_dohod;
                picker.SelectedIndex = categoria.name_dohod - 1;
            }
          







        }

        public async void OnButtonClicked(object sender, EventArgs args)
        {
            int namedohod = picker.SelectedIndex + 1;


            string dbPath = DependencyService.Get<ISQLite>().GetDatabasePath("friends.db");
            var db = new SQLiteConnection(dbPath);
            if (idelem == 0)
            {
                db.Insert(new an_dohod { komment = komment.Text, name_dohod = namedohod, summa_dohod = summa.Text, data_fakt = faktdata.Get(DateTime.Now) }); // after creating the newStock object
            }
            else
            {
                db.Query<vis_an_dohod>("UPDATE [an_dohod] SET [komment]='" + komment.Text + "', [name_dohod]=" + namedohod + ", [summa_dohod]=" + summa.Text +
                  " WHERE [id]=" + idelem.ToString());
            }

            MessagingCenter.Send<ContentPage>(this, "LabelChange");
            await Navigation.PopAsync();

        }
    }
}