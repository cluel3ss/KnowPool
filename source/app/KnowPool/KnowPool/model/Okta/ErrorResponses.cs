using System;
using System.Collections.Generic;

namespace KnowPool.model.Okta
{
    public class ErrorCause
    {
        public string errorSummary { get; set; }
    }

    public class ErrorResponses
    {
        public string errorCode { get; set; }
        public string errorSummary { get; set; }
        public string errorLink { get; set; }
        public string errorId { get; set; }
        public List<ErrorCause> errorCauses { get; set; }
    }
}
