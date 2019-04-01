using System;
using System.Collections.Generic;
using System.Text;

namespace KnowPool.model
{
    public class InstructorName
    {
        public string c_name { get; set; }
        public string c_id { get; set; }
    }

    public class InstructorListClass
    {
        public string error_code { get; set; }
        public string message { get; set; }
        public List<InstructorName> records { get; set; }
    }
}
