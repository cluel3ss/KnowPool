using KnowPool.model;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KnowPool.interfaces
{
    interface IApiInterface
    {
        [Post("/KnowPool/API/login.php")]
        Task<LoginClass> UserLogin([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);

        [Post("/KnowPool/API/signup.php")]
        Task<SignUp> UserSignUp([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);

        [Get("/KnowPool/API/course_list.php")]
        Task<CourseList> GetCourses();

        [Post("/KnowPool/API/instructor_list.php")]
        Task<InstructorListClass> GetListOfInstructors([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);

        [Post("/KnowPool/API/coursesByInstructor.php")]
        Task<CoursesByInstructor> GetCoursesByInstructor([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);

        [Post("/KnowPool/API/courseDetails.php")]
        Task<BlogDetails> GetBlogDetails([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);

        [Post("/KnowPool/API/addCourseDetails.php")]
        Task<NewBlog> AddNewBlog([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);
    }
}
