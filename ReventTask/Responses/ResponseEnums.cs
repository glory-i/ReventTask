using System.ComponentModel;

namespace ReventTask.Responses
{
    public enum ApiResponseEnum
    {
        [Description("failure")] failure = 1,
        [Description("success")] success,

    }
}
