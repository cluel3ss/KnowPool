using KnowPool.Pages;
using Okta.Sdk;
using Okta.Sdk.Configuration;
using System;
using Xamarin.Forms;

namespace KnowPool
{
	public partial class MainPage : ContentPage
	{
        public MainPage()
		{
			InitializeComponent();
            Button_Go.Text = @"< START />";
            //Tagline_Label.Text = "Made With 💗 By Xonshiz";
        }

        private async void Button_Go_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new LoginPage());
            //var client = new OktaClient(new OktaClientConfiguration
            //{
            //    OktaDomain = "https://dev-484942.okta.com",
            //    Token = "00NYEz1hk8-m-Epy3E1y8MkFkx9LzeZznQmi8_hbUZ"
            //});

            //var vader = await client.Users.GetUserAsync("login");
        }
    }
}
