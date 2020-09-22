using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using ToDos.Models.Dtos;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ToDos.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        string[] scopes = new[] { "https://ahmadmozaffar.onmicrosoft.com/540729f1-17aa-4bf9-a2db-4837442426fd/Events.Manage" };
        private static string clientId = "b8e80483-cf1f-49bc-85e9-a02aba505123";
        private static string authority = "https://ahmadmozaffar.b2clogin.com/tfp/ahmadmozaffar.onmicrosoft.com/B2C_1_sign_in_up";
        private static string domain = "ahmadmozaffar.onmicrosoft.com";

        private static IPublicClientApplication publicApplication;
        private static AuthenticationResult authResult;
        private string accessToken = "";

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            accessToken = await SignInUser().ConfigureAwait(false);


            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var result = await client.PostAsJsonAsync("http://localhost:65281", new ToDoItemDetail
                {
                    Category = "UWP",
                    Descrption = "The first item from the UWP App",
                    IsDone = false,
                    Points = 652,
                    Quality = 10,
                    TaskDate = DateTime.Now
                });

                if (result.IsSuccessStatusCode)
                    await new Windows.UI.Popups.MessageDialog("Item has been created successfully!", "Alert").ShowAsync();
                else
                    await new Windows.UI.Popups.MessageDialog("Something went wrong", "Alert").ShowAsync();

            }

        }

        private async Task<string> SignInUser()
        {
            publicApplication = PublicClientApplicationBuilder.Create(clientId)
                .WithB2CAuthority(authority)
                .WithUseCorporateNetwork(false)
                .WithRedirectUri("https://ahmadmozaffar.b2clogin.com/oauth2/nativeclient")
                .Build();

            IEnumerable<IAccount> accounts = await publicApplication.GetAccountsAsync();
            IAccount firstAccount = accounts.FirstOrDefault();

            try
            {
                authResult = await publicApplication.AcquireTokenSilent(scopes, firstAccount).ExecuteAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                authResult = await publicApplication.AcquireTokenInteractive(scopes).ExecuteAsync();
            }

            return authResult.AccessToken;
        }
    }
}
