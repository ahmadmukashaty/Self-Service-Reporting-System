using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSRS.WebAPi.Models.Helper
{
    public class Constants
    {
        public static class Messages
        {
            public const string EMPTY_MESSAGE = "";
            public const string UNEXPECTED_ERROR_MESSAGE = "Unexpected Error";
        }

        public static class Relations
        {
            public const string INITIAL = "INITIAL";
            public const string ONE_TO_ONE = "ONE_TO_ONE";
            public const string ONE_TO_MANY = "ONE_TO_MANY";
            public const string MANY_TO_MANY = "MANY_TO_MANY";
        }

        public static class JoinType
        {
            public const string INNER_JOIN = " INNER JOIN ";
            public const string LEFT_OUTER_JOIN = " LEFT OUTER JOIN ";
        }

        public static class Types
        {
           
        }

        public const int SUCCESSED = 1;
        public const int FAILED = -1;
    }
}