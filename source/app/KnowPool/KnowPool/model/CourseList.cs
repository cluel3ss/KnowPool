using System;
using System.Collections.Generic;
using System.Text;

namespace KnowPool.model
{
    public class Record
    {
        public string c_id { get; set; }
        public string c_name { get; set; }
        public string c_desc { get; set; }
        public string c_use { get; set; }
        public string c_photo { get; set; }
    }

    public class CourseList
    {
        public string error_code { get; set; }
        public string message { get; set; }
        public List<Record> records { get; set; }
    }
}
