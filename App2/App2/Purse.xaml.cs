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
	public partial class Purse : ContentPage
	{
		public Purse ()
		{
			InitializeComponent ();


            string dbPath = DependencyService.Get<ISQLite>().GetDatabasePath("friends.db");
            var db = new SQLiteConnection(dbPath);


            var stockList = db.Query<an_purse>("SELECT * " +
            "FROM [an_purse] ");


            listView.ItemsSource = stockList;
        }

        public async void OnButtonClicked(object sender, EventArgs args)
        {
            var detailPage = new Purse_add();
            await Navigation.PushAsync(detailPage);

        }
    }
}