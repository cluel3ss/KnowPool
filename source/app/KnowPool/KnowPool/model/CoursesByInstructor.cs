using System;
using System.Collections.Generic;
using System.Text;

namespace KnowPool.model
{
    public class CourseData
    {
        public string cd_id { get; set; }
        public string cd_title { get; set; }
    }

    public class CoursesByInstructor
    {
        public string error_code { get; set; }
        public string message { get; set; }
        public List<CourseData> records { get; set; }
    }
}
