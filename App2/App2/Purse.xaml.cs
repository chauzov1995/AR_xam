using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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




            var moreAction = new MenuItem { Text = "More" };
            moreAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            moreAction.Clicked += async (sender, e) => {
                var mi = ((MenuItem)sender);
                Debug.WriteLine("More Context Action clicked: " + mi.CommandParameter);
            };

            var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
            deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            deleteAction.Clicked += async (sender, e) => {
                var mi = ((MenuItem)sender);
                Debug.WriteLine("Delete Context Action clicked: " + mi.CommandParameter);
            };
            // add to the ViewCell's ContextActions property
            //ContextActions.Add(moreAction);
           // ContextActions.Add(deleteAction);


            listView.ItemsSource = stockList;
        }

        public async void OnButtonClicked(object sender, EventArgs args)
        {
            var detailPage = new Purse_add();
            await Navigation.PushAsync(detailPage);

        }

        public void OnMore(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }
    }
}