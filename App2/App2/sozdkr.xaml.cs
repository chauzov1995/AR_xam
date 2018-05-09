using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class sozdkr : ContentPage
    {
        int id_dohoda;
        vis_an_dohod categoria;
        public sozdkr(vis_an_dohod element)
        {
            InitializeComponent();

            this.id_dohoda = element.id;
            categoria = element;

            string dbPath = DependencyService.Get<ISQLite>().GetDatabasePath("friends.db");
            var db = new SQLiteConnection(dbPath);
          



            var an_categ_list = db.Query<an_dohod>("SELECT * " +
"FROM [an_dohod] ");

            int ind_categ = an_categ_list.FindIndex(x => x.id == element.id);
            picker_categ.ItemsSource = an_categ_list;
            picker_categ.SelectedIndex = ind_categ;

            var an_purse_list = db.Query<an_purse>("SELECT * " +
          "FROM [an_purse] ");

            // int ind_purse = an_purse_list.FindIndex(x => x.id == element.ku);
            picker_purse.ItemsSource = an_purse_list;
            picker_purse.SelectedIndex = 0;



        }

        public async void MenuItem1_Activated(object sender, EventArgs args)
        {
            //создадим дкр

            DateTime date = mDatePicker.Date;//установленная дата

            string dbPath = DependencyService.Get<ISQLite>().GetDatabasePath("friends.db");
            var db = new SQLiteConnection(dbPath);

            db.Insert(new an_dkr_hist { komment = komment.Text, summa = summa.Text, kuda = id_dohoda, data_fakt = faktdata.Get(date) }); // after creating the newStock object



            MessagingCenter.Send<ContentPage>(this, "LabelChange");
            await Navigation.PopAsync();

        }






    }
}