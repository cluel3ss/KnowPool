using KnowPool.interfaces;
using KnowPool.internalData;
using KnowPool.model;
using Refit;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KnowPool.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Dashboard : ContentPage
	{
        IApiInterface apiInterface;
        CourseList courseList;

        public Dashboard ()
		{
			InitializeComponent ();
            apiInterface = RestService.For<IApiInterface>("https://xonshiz.heliohost.org");
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (LoggedInUserData.u_type == "Instructor")
                Button_NewPost.IsVisible = true; // Instructor
            else
                Button_NewPost.IsVisible = false; // Learner
            //courseList = await apiInterface.GetCourses();
            await GetCourseListAsync();
        }

        async Task GetCourseListAsync()
        {
            myActivityIndicator.IsVisible = true;
            courseList = await apiInterface.GetCourses();
            ListView_CourseList.ItemsSource = courseList.records;
            myActivityIndicator.IsVisible = false;
        }

        private async void ListView_CourseList_Refreshing(object sender, EventArgs e)
        {
            //ListView_CourseList.ItemsSource = courseList.records;
            await GetCourseListAsync();
            ListView_CourseList.EndRefresh();
        }

        private async void ListView_CourseList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var entrySelected = e.SelectedItem as Record;
            await Navigation.PushAsync(new CourseDetails(entrySelected.c_id, entrySelected.c_name, entrySelected.c_desc, entrySelected.c_use, entrySelected.c_photo));
            //await DisplayAlert("Item Selected", entrySelected.c_id, "ok");
            ListView_CourseList.SelectedItem = null;

        }

        private async void Button_NewPost_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddNewBlogPage());
        }
    }
}