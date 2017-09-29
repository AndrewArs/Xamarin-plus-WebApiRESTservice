using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;

namespace App2
{
    public partial class MainPage : ContentPage
    {

        const string uri = "http://10.0.2.2";

        public MainPage()
        {
            InitializeComponent();
            GetTimeButton.Clicked += GetTimeButton_Clicked;
        }

        private void GetTimeButton_Clicked(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString();
            DateTimeLabel.Text = date;
            SendDate(date);
        }

        private async void SendDate(string Date)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(uri);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("date", Date)
                });

                var result = await client.PostAsync(uri + "/api/values/", content);
            }
        }
    }
}
