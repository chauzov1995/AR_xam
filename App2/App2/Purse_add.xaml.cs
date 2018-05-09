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
        int idelem;
        public Purse_add(int id)
        {
            InitializeComponent();


            this.Title = "Создаём кошелёк";
            this.idelem = id;
            if (id == 0)
            {//значит новый

            }
            else
            {
                string dbPath = DependencyService.Get<ISQLite>().GetDatabasePath("friends.db");
                var db = new SQLiteConnection(dbPath);


                an_purse element = db.Query<an_purse>("SELECT * " +
                "FROM [an_purse] WHERE id="+id).First();
                komment.Text = element.komment;
                    summa.Text = element.summa;
            }


        }

        public async void OnButtonClicked(object sender, EventArgs args)
        {
            
            string dbPath = DependencyService.Get<ISQLite>().GetDatabasePath("friends.db");
            var db = new SQLiteConnection(dbPath);
            if (idelem == 0)
            {
                db.Insert(new an_purse { komment = komment.Text, summa = summa.Text, data_fakt = faktdata.Get(DateTime.Now) }); // after creating the newStock object
            }
            else
            {
                db.Query<vis_an_dohod>("UPDATE [an_purse] SET [komment]='"+ komment.Text + "', [summa]="+ summa.Text + " WHERE [id]=" + idelem);
            }

            MessagingCenter.Send<ContentPage>(this, "LabelChange");
            await Navigation.PopAsync();

        }
    }
}