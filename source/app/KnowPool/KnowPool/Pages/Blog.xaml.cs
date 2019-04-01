using KnowPool.interfaces;
using KnowPool.model;
using Refit;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KnowPool.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Blog : ContentPage
	{
        IApiInterface apiInterface;
        string BlogId;
        BlogDetails blogDetails;

        public Blog (string BlogId)
		{
			InitializeComponent ();
            this.BlogId = BlogId;
            apiInterface = RestService.For<IApiInterface>("https://xonshiz.heliohost.org");
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            myActivityIndicator.IsVisible = true;
            //courseList = await apiInterface.GetCourses();
            var data = new Dictionary<string, object> {
                {"coursedetail_id", BlogId}
            };
            blogDetails = await apiInterface.GetBlogDetails(data);
            
            this.Title = blogDetails.cd_title;
            Label_BlogDescription.Text = blogDetails.cd_desc;
            Label_BlogKeywords.Text = blogDetails.cd_keywords;
            myActivityIndicator.IsVisible = false;
        }


    }
}