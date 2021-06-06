using Shouldly;
using StringCalculatorKata.Exceptions;

namespace StringCalculatorKata.Tests
{
    public class StringCalculatorTests
    {
        private readonly StringCalculator _sut;

        public StringCalculatorTests()
        {
            _sut = new StringCalculator();
        }

        [ReadableFact]
        public void An_empty_string_should_return_a_sum_of_0()
        {
            var result = _sut.Add("");
            result.ShouldBe(0);
        }

        [ReadableFact]
        public void Sum_of_one_number_is_the_number_itself()
        {
            var result = _sut.Add("1");
            result.ShouldBe(1);
        }

        [ReadableFact]
        public void Sum_of_two_numbers()
        {
            var result = _sut.Add("1,2");
            result.ShouldBe(3);
        }

        [ReadableFact]
        public void Sum_of_x_numbers()
        {
            var result = _sut.Add("1,2,3,4,5");
            result.ShouldBe(15);
        }

        [ReadableFact]
        public void NewLine_is_allowed()
        {
            var result = _sut.Add("1\n2,3");
            result.ShouldBe(6);
        }

        [ReadableFact]
        public void Use_custom_delimiter()
        {
            var result = _sut.Add("//;\n1;2");
            result.ShouldBe(3);
        }

        [ReadableFact]
        public void Negative_numbers_are_not_allowed()
        {
            var ex = Should.Throw<NegativesNotAllowedException>(() =>
            {
                _sut.Add("-1,2");
            });

            ex.Message.ShouldBe("Negatives not allowed: -1");

            ex = Should.Throw<NegativesNotAllowedException>(() =>
            {
                _sut.Add("2,-4,3,-5");
            });

            ex.Message.ShouldBe("Negatives not allowed: -4,-5");
        }

        [ReadableFact]
        public void Numbers_greater_than_1000_are_ignored()
        {
            var result = _sut.Add("1001,2");
            result.ShouldBe(2);
        }

        [ReadableFact]
        public void Delimiters_can_be_any_length()
        {
            var result = _sut.Add("//[|||]\n1|||2|||3");
            result.ShouldBe(6);
        }

        [ReadableFact]
        public void Allow_multiple_delimiters()
        {
            var result = _sut.Add("//[|][%]\n1|2%3");
            result.ShouldBe(6);
        }

        [ReadableFact]
        public void Allow_multiple_delimiters_of_any_length()
        {
            var result = _sut.Add("//[||][%%%]\n1||2%%%3");
            result.ShouldBe(6);
        }
    }
}
