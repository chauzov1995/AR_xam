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
			InitializeComponent ();

            this.id_dohoda = element.id;
            categoria = element;

            string dbPath = DependencyService.Get<ISQLite>().GetDatabasePath("friends.db");
            var db = new SQLiteConnection(dbPath);
            var stockList = db.Query<an_dkr_hist>("SELECT * " +
           "FROM [an_dkr_hist] " +
           "WHERE [kuda]=" + id_dohoda+" and [visible]=0");

            this.Title = element.komment;

            listView.ItemsSource = stockList;



            var an_categ_list = db.Query<an_dohod>("SELECT * " +
"FROM [an_dohod] ");

           int ind_categ= an_categ_list.FindIndex(x => x.id == element.id);
            picker_categ.ItemsSource = an_categ_list;
            picker_categ.SelectedIndex = ind_categ;

            var an_purse_list = db.Query<an_purse>("SELECT * " +
          "FROM [an_purse] ");

           // int ind_purse = an_purse_list.FindIndex(x => x.id == element.ku);
            picker_purse.ItemsSource = an_purse_list;
            picker_purse.SelectedIndex = 0;

      

        }

        public async void OnButtonClicked(object sender, EventArgs args)
        {
            //создадим дкр

            DateTime date = mDatePicker.Date;//установленная дата
          
            string dbPath = DependencyService.Get<ISQLite>().GetDatabasePath("friends.db");
            var db = new SQLiteConnection(dbPath);
   
            db.Insert(new an_dkr_hist { komment = komment.Text, summa = summa.Text, kuda= id_dohoda, data_fakt = faktdata.Get(date) }); // after creating the newStock object



          
            await Navigation.PopAsync();

        }

        

                public async void del_activated(object sender, EventArgs args)
        {
            //удалим кошель

       
            string dbPath = DependencyService.Get<ISQLite>().GetDatabasePath("friends.db");
            var db = new SQLiteConnection(dbPath);

            db.Query<an_dohod>("UPDATE [an_dohod] SET [visible]=1 " +
           "WHERE [id]="+ id_dohoda.ToString());



            await Navigation.PopAsync();

        }

        public async void red_activated(object sender, EventArgs args)
        {
            //редактировать кошель


            var detailPage = new Page2(categoria);
            await Navigation.PushAsync(detailPage);

        }

        
    }
}