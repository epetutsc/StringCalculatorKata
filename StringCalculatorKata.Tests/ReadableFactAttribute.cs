using System.Runtime.CompilerServices;

namespace StringCalculatorKata.Tests
{

    public class ReadableFactAttribute : Xunit.FactAttribute
    {
        public ReadableFactAttribute([CallerMemberName] string testMethodName = "")
        {
            base.DisplayName = testMethodName
                ?.Replace("__", " - ")
                ?.Replace("_", " ");
        }
    }
}
