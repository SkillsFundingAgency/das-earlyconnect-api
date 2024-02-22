using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests
{
    public class StudentOnboardDataPostRequest
    {
        [RegularExpressionList(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email in your list")]
        public IList<string> Emails { get; set; }
    }
}

public class RegularExpressionListAttribute : RegularExpressionAttribute
{
    public RegularExpressionListAttribute(string pattern)
        : base(pattern) { }

    public override bool IsValid(object value)
    {
        if (value is not IEnumerable<string>)
            return false;

        foreach (var val in value as IEnumerable<string>)
        {
            if (!Regex.IsMatch(val, Pattern))
                return false;
        }

        return true;
    }
}
