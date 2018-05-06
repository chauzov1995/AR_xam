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
    public partial class Purse_add : ContentPage
    {
        public Purse_add()
        {
            InitializeComponent();


            this.Title = "Создаём кошелёк";



        }

        public async void OnButtonClicked(object sender, EventArgs args)
        {
            
            string dbPath = DependencyService.Get<ISQLite>().GetDatabasePath("friends.db");
            var db = new SQLiteConnection(dbPath);
            db.Insert(new an_purse { komment = komment.Text,  summa = summa.Text, data_fakt = faktdata.Get(DateTime.Now) }); // after creating the newStock object



            await Navigation.PopAsync();

        }
    }
}