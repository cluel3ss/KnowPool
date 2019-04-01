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
	public partial class CoursesByInstructor : ContentPage
	{
        IApiInterface apiInterface;
        string courseId, instructorId;
        model.CoursesByInstructor coursesByInstructor; //Same name of class and the XAML page. So, need to be declarative.

        public CoursesByInstructor (string courseId, string instructorId)
		{
			InitializeComponent ();
            this.courseId = courseId;
            this.instructorId = instructorId;
            apiInterface = RestService.For<IApiInterface>("https://xonshiz.heliohost.org");
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //courseList = await apiInterface.GetCourses();
            await GetCoursesByInstructorListAsync();
        }

        async Task GetCoursesByInstructorListAsync()
        {
            myActivityIndicator.IsVisible = true;
            var data = new Dictionary<string, object> {
                {"course_id", courseId},
                {"course_a_id", instructorId}
            };

            coursesByInstructor = await apiInterface.GetCoursesByInstructor(data);
            ListView_CoursesByInstructors.ItemsSource = coursesByInstructor.records;
            myActivityIndicator.IsVisible = false;
        }

        private async void ListView_CoursesByInstructors_Refreshing(object sender, EventArgs e)
        {
            await GetCoursesByInstructorListAsync();
            ListView_CoursesByInstructors.EndRefresh();
        }

        private async void ListView_CoursesByInstructors_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var entrySelected = e.SelectedItem as CourseData;
            await Navigation.PushAsync(new Blog(entrySelected.cd_id));
            //await DisplayAlert("Item Selected", entrySelected.cd_title, "ok");
            ListView_CoursesByInstructors.SelectedItem = null;

        }
    }
}