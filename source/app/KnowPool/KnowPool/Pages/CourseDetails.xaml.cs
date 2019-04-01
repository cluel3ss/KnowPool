using KnowPool.interfaces;
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
	public partial class CourseDetails : ContentPage
	{
        IApiInterface apiInterface;
        InstructorListClass instructorListClass;

        string courseId;

        public CourseDetails (string courseId, string courseTitle, string courseDesc, string courseUse, string coursePhoto)
		{
			InitializeComponent ();
            this.Title = courseTitle + " " + "Courses";
            Label_CourseDescription.Text = courseDesc;
            Label_CourseUse.Text = courseUse;
            Image_CourseImage.Source = coursePhoto;
            this.courseId = courseId;
            apiInterface = RestService.For<IApiInterface>("https://xonshiz.heliohost.org");
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await GetCourseInstructorListAsync();
        }

        async Task GetCourseInstructorListAsync()
        {
            myActivityIndicator.IsVisible = true;
            var data = new Dictionary<string, object> {
                {"course_id", courseId}
            };

            instructorListClass = await apiInterface.GetListOfInstructors(data);
            ListView_CourseInstructors.ItemsSource = instructorListClass.records;
            myActivityIndicator.IsVisible = false;
        }

        private async void ListView_CourseInstructors_Refreshing(object sender, EventArgs e)
        {
            await GetCourseInstructorListAsync();
            ListView_CourseInstructors.EndRefresh();
        }

        private async void ListView_CourseInstructors_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var entrySelected = e.SelectedItem as InstructorName;
            await Navigation.PushAsync(new CoursesByInstructor(this.courseId, entrySelected.c_id));
            ListView_CourseInstructors.SelectedItem = null;

        }
    }
}