using KnowPool.interfaces;
using KnowPool.internalData;
using KnowPool.model;
using KnowPool.model.Okta;
using Newtonsoft.Json;
using Refit;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KnowPool.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        IApiInterface apiInterface;
        IOktaInterface oktaInterface;
        SignUpResponse signUpResponse;
        ErrorResponses errorResponses;

        string generatedOTP;

        public LoginPage ()
		{
			InitializeComponent ();
            signUpResponse = new SignUpResponse();
            errorResponses = new ErrorResponses();
            generatedOTP = Functionalities.OTPGenerator();
            AccountTypeSelect.SelectedIndex = 1;
            apiInterface = RestService.For<IApiInterface>("https://xonshiz.heliohost.org");
            oktaInterface = RestService.For<IOktaInterface>("https://dev-484942.okta.com");

            //Entry_SignUp_userName.Text = "Dhruv Kanojia";
            //Entry_SignUp_userPassword.Text = "GuessMe24";
            //Entry_SignUp_userPhone.Text = "9717155636";
            //Entry_SignUp_userEmail.Text = "kanojia25.11@gmail.com";

            //Entry_userEmail.Text = "kanojia25.11@gmail.com";
            //Entry_userPassword.Text = "GuessMe24!";
        }

        private async void Button_Login_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Entry_userEmail.Text))
            {
                await DisplayAlert("Empty Fields", "Please Fill The Empty Fields", "Ok");
                return;
            }

            //string generatedOTP = null;

            if (string.IsNullOrEmpty(Entry_userOTP.Text))
            {
                myActivityIndicator.IsVisible = true;
                GetUserWithLogin getUserWithLogin = new GetUserWithLogin();
                ErrorResponses errorResponses = new ErrorResponses();

                try
                {
                    getUserWithLogin = await oktaInterface.CheckUser(Entry_userEmail.Text.Trim());
                }
                catch (ApiException APIException)
                {
                    errorResponses = APIException.GetContentAs<ErrorResponses>();
                }
                if (string.IsNullOrEmpty(errorResponses.errorCode))
                {
                    // When the correct JSON is received.
                    //generatedOTP = Functionalities.OTPGenerator();
                    bool isEmailSent = await Functionalities.EmailSender(this.generatedOTP, getUserWithLogin.profile.email, getUserWithLogin.profile.firstName, Functionalities.EventType.login);
                    bool isSMSSent = Functionalities.SMSSender(getUserWithLogin.profile.mobilePhone, this.generatedOTP, Functionalities.EventType.login);
                    if (isEmailSent || isSMSSent)
                    {
                        if (isEmailSent && !isSMSSent)
                            await DisplayAlert("Success!", "Sent OTP to Email.", "ok");
                        else if (isSMSSent && !isEmailSent)
                            await DisplayAlert("Success!", "Sent OTP to your mobile.", "ok");
                        else if (isSMSSent && isEmailSent)
                            await DisplayAlert("Success!", "Sent OTP to Email and Mobile.", "ok");
                        Entry_userOTP.IsEnabled = true;
                        Entry_userOTP.IsVisible = true;
                        myActivityIndicator.IsVisible = false;
                        LoggedInUserData.u_id = getUserWithLogin.id;
                        LoggedInUserData.u_name = getUserWithLogin.profile.firstName + " " + getUserWithLogin.profile.lastName;
                        LoggedInUserData.u_email = getUserWithLogin.profile.email;
                        LoggedInUserData.u_status = getUserWithLogin.status;
                        LoggedInUserData.u_type = getUserWithLogin.profile.user_type;
                    }
                    else
                    {
                        await DisplayAlert("Failed!", "Couldn't send the verification email. Contact admin.", "ok");
                        myActivityIndicator.IsVisible = false;
                    }


                }
                else
                {
                    await DisplayAlert("Failed!", errorResponses.errorSummary, "ok");
                    myActivityIndicator.IsVisible = false;
                }

            }
            else
            {
                // if OTP is entered.
                myActivityIndicator.IsVisible = true;
                if (this.generatedOTP.Equals(Entry_userOTP.Text.Trim()))
                {
                    myActivityIndicator.IsVisible = false;
                    if (LoggedInUserData.u_status == "ACTIVE" || LoggedInUserData.u_status == "PROVISIONED")
                        await this.Navigation.PushAsync(new Dashboard());
                    else
                        await DisplayAlert("Failed!", "Your Account Is Not Verified Yet or Blocked.", "ok");

                    Entry_userEmail.Text = "";
                    Entry_userOTP.Text = "";
                    Entry_userOTP.IsVisible = false;
                }
                else
                {
                    await DisplayAlert("Wrong OTP", "The OTP you entered doesn't match the one we sent.", "ok");
                }
            }
        }

        private async void Button_SignUp_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Entry_SignUp_userOTP.Text) || !Entry_SignUp_userOTP.IsVisible)
            {
                if (string.IsNullOrEmpty(Entry_SignUp_userName.Text) ||
                string.IsNullOrEmpty(Entry_SignUp_userEmail.Text) ||
                string.IsNullOrEmpty(Entry_SignUp_userPhone.Text))
                {
                    await DisplayAlert("Empty Fields", "Please Fill The Empty Fields", "Ok");
                    return;
                }
                myActivityIndicator.IsVisible = true;



                //Password password = new Password();
                //Credentials credentials = new Credentials();
                Profile profile = new Profile();
                SignUpUser signUpUser = new SignUpUser();

                //password.value = Entry_SignUp_userPassword.Text.Trim();

                profile.firstName = Entry_SignUp_userName.Text.Split()[0].Trim();
                profile.lastName = Entry_SignUp_userName.Text.Split()[1].Trim();
                profile.mobilePhone = Entry_SignUp_userPhone.Text.Trim();
                profile.email = Entry_SignUp_userEmail.Text.Trim();
                profile.login = Entry_SignUp_userEmail.Text.Trim();
                profile.user_type = AccountTypeSelect.SelectedItem.ToString();
                //credentials.password = password;

                signUpUser.profile = profile;
                //signUpUser.credentials = credentials;

                // string output = JsonConvert.SerializeObject(signUpUser);

                //signUpResponse = new SignUpResponse();
                

                try
                {
                    signUpResponse = await oktaInterface.CreateUser(signUpUser);

                    try
                    {
                        SignUp signUp = new SignUp();
                        var data = new Dictionary<string, object> {
                        {"user_email", Entry_userEmail.Text},
                        {"user_name", Entry_SignUp_userName.Text},
                        {"u_id", signUpResponse.id},
                        {"user_password", "Hello"}
                    };
                        signUp = await apiInterface.UserSignUp(data);
                    }
                    catch (Exception)
                    {
                        //  Move On
                    }
                }
                catch (ApiException APIException)
                {
                    errorResponses = APIException.GetContentAs<ErrorResponses>();
                }

                if (string.IsNullOrEmpty(errorResponses.errorCode))
                {
                    bool isEmailSent = await Functionalities.EmailSender(generatedOTP, signUpResponse.profile.email, signUpResponse.profile.firstName, Functionalities.EventType.login);
                    bool isSMSSent = Functionalities.SMSSender(signUpResponse.profile.mobilePhone, generatedOTP, Functionalities.EventType.login);
                    if (isEmailSent || isSMSSent)
                    {
                        if (isEmailSent && !isSMSSent)
                            await DisplayAlert("Success!", "Sent OTP to Email.", "ok");
                        else if (isSMSSent && !isEmailSent)
                            await DisplayAlert("Success!", "Sent OTP SMS to your mobile.", "ok");
                        else if (isSMSSent && isEmailSent)
                            await DisplayAlert("Success!", "Sent OTP to Email and Mobile.", "ok");

                        Entry_SignUp_userOTP.IsVisible = true;
                        Entry_SignUp_userName.IsVisible = false;
                        Entry_SignUp_userPhone.IsVisible = false;
                        Entry_SignUp_userEmail.IsVisible = false;
                        AccountTypeSelect.IsVisible = false;
                        myActivityIndicator.IsVisible = false;

                        Entry_SignUp_userName.Text = null;
                        Entry_SignUp_userPhone.Text = null;
                        Entry_SignUp_userEmail.Text = null;

                    }
                    else
                    {
                        await DisplayAlert("Failed!", "Couldn't send the verification email. Contact admin.", "ok");
                        myActivityIndicator.IsVisible = false;
                    }
                }
                else
                {
                    await DisplayAlert("Failed!", errorResponses.errorSummary, "ok");
                    myActivityIndicator.IsVisible = false;
                }
            }
            else
            {
                if (generatedOTP.Equals(Entry_SignUp_userOTP.Text.Trim()))
                {
                    myActivityIndicator.IsVisible = true;
                    try
                    {
                        UserActivation userActivation = await oktaInterface.ActivateUser(signUpResponse.id);
                    }
                    catch (ApiException APIException)
                    {
                        errorResponses = APIException.GetContentAs<ErrorResponses>();
                    }
                    myActivityIndicator.IsVisible = false;
                    if (string.IsNullOrEmpty(errorResponses.errorCode))
                    {
                        await DisplayAlert("Congratulations", "You have been Verified", "Ok");
                        BoardLogin.IsVisible = true;
                        BoardSignUp.IsVisible = false;
                        Entry_userEmail.Text = LoggedInUserData.u_email;
                        Entry_userOTP.Text = "";
                        Entry_SignUp_userName.Text = "";
                        Entry_SignUp_userEmail.Text = "";
                        myActivityIndicator.IsVisible = false;
                    }
                    else
                    {
                        await DisplayAlert("Error", errorResponses.errorSummary, "Ok");
                    }
                    
                }
                else
                {
                    await DisplayAlert("Wrong OTP", "The OTP you have entered is not the one we sent", "ok");
                }
            }
        }

        private void ButtonLoginFrame_Clicked(object sender, EventArgs e)
        {
            BoardLogin.IsVisible = true;
            BoardSignUp.IsVisible = false;
        }

        private void ButtonRegisterFrame_Clicked(object sender, EventArgs e)
        {
            BoardLogin.IsVisible = false;
            BoardSignUp.IsVisible = true;
        }

    }
}