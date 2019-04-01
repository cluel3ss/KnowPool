using KnowPool.interfaces;
using KnowPool.internalData;
using KnowPool.model;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KnowPool.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNewBlogPage : ContentPage
	{
        IApiInterface apiInterface;
        CourseList courseList;
        NewBlog newBlog;

        public AddNewBlogPage ()
		{
			InitializeComponent ();
            apiInterface = RestService.For<IApiInterface>("https://xonshiz.heliohost.org");
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //courseList = await apiInterface.GetCourses();
            await GetCourseListAsync();
        }

        async Task GetCourseListAsync()
        {
            myActivityIndicator.IsVisible = true;
            courseList = await apiInterface.GetCourses();

            foreach (var courseTitle in courseList.records)
            {
                CoursePicker.Items.Add(courseTitle.c_name);
            }
            CoursePicker.SelectedIndex = 0;
            myActivityIndicator.IsVisible = false;
        }

        private async void Button_SubmitBlog_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Entry_BlogTitle.Text) || string.IsNullOrEmpty(Entry_BlogKeywords.Text) || string.IsNullOrEmpty(Entry_BlogContent.Text))
            {
                await DisplayAlert("Empty Fields", "Please Fill All The Fields Before Submitting", "ok");
                return;
            }

            myActivityIndicator.IsVisible = true;
            var data = new Dictionary<string, object> {
                {"cd_a_id", LoggedInUserData.u_id},
                {"cd_c_id", CoursePicker.SelectedIndex + 1},
                {"cd_title", Entry_BlogTitle.Text},
                {"cd_keywords", Entry_BlogKeywords.Text},
                {"cd_desc", Entry_BlogContent.Text},
            };

            newBlog = await apiInterface.AddNewBlog(data);
            myActivityIndicator.IsVisible = false;

            if (newBlog.error_code == "0")
            {
                await DisplayAlert("Successfuly Added", "Your New Blog Was Added", "ok");
                Entry_BlogTitle.Text = "";
                Entry_BlogKeywords.Text = "";
                Entry_BlogContent.Text = "";
                //await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Failed To Add", "Your New Blog Was Not Added", "ok");
            }
        }
    }
}