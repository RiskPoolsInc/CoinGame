namespace CG.WebApi
{
    public static class RouteNames
    {
        public static class Users
        {
            public const string GetById = "GetUser";

            public static class Image
            {
                public const string GetById = "GetUserImage";
            }
        }


        public static class Attachments
        {
            public const string GetById = "GetAttachment";
            public const string GetByObjectId = "GetObjectAttachment";
        }

        public static class Profile
        {
            public static class Image
            {
                public const string GetById = "GetProfileImageById";
            }
        }


        public static class Tasks
        {
            public const string GetById = "GetTask";
            public const string List = "list";
            public const string CustomerTasksList = "customer-list";
            public const string ListInfo = "list-info";
            public const string Canceled = "Canceled";
            public const string Executions = "Executions";
            public const string ExecutionsPaged = "ExecutionsPaged";
            public const string SendDeposit = "SendDeposit";
            public const string ConfirmExecution = "ConfirmExecution";
            public const string CancelExecution = "CancelExecution";
            public const string Complete = "Completed";
            public const string Cancel = "Cancel";

            public static class Notes
            {
                public const string GetById = "GetTaskNote";
                public const string AddNote = "AddTaskNote";
            }
        }

        public static class TaskExecutions
        {
            public const string GetById = "GetTaskExecution";
            public const string Ready = "TaskExecutionReady";

            public static class Notes
            {
                public const string Add = "TaskExectuionAddNote";
                public const string Get = "GetTaskExectuionNotes";
            }
        }
    }
}