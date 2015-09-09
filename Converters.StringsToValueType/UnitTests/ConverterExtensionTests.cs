using NUnit.Framework;
using System;
using Converters.StringsToValueType.CustomConverters;

namespace Converters.StringsToValueType
{
    [TestFixture]
    internal class ConverterExtensionTests
    {
        [Test]
        public void NumericString_ConvertToInt_Valid()
        {
            var numericTestValue = 123;

            var result = numericTestValue.ToString().To <Int32>();
            Console.Write(result.GetType());

            Assert.AreEqual(numericTestValue, result);
        }

        [Test]
        public void NumericString_ConvertValueNullableInteger_Valid()
        {
            int? nullableTestValue = 12323;

            var result = nullableTestValue.Value.ToString().To<Nullable<Int32>>();

            Assert.AreEqual(nullableTestValue, result);
            Assert.IsInstanceOf<Nullable<Int32>>(result);
        }

        [Test]
        public void GuidString_ConvertToGuidType_Valid()
        {
            var guid = Guid.NewGuid();

            var result = guid.ToString().To<Guid>();

            Assert.AreEqual(guid, result);
        }

        [Test]
        public void EmptyString_ConvertToGuidType_ReturnEmptyGuid()
        {
            var guid = Guid.Empty;

            var result = string.Empty.To<Guid>();

            Assert.AreEqual(guid, result);
        }

        [Test]
        public void BooleanString_ConvertToBool_Valid()
        {
            bool expectedValue = false;
            string testInput = "false";

            var result = testInput.To<Boolean>();

            Assert.AreEqual(expectedValue, result);
        } 
        
        [Test]
        public void InvalidBase64ByteRepresentedByString_ConvertToBool_ThrowsInvalidCastException()
        {
            bool expectedValue = false;
            string testInput = "\x00af";

            Assert.Throws<InvalidCastException>(() => testInput.To<byte[]>(new Base64StringConverter()));
        }

        [Test]
        public void EnumReresentedByString_ConvertToEnum_Valid()
        {
            TestState expectedValue = TestState.Success;
            string testInput = "success";

            var result = testInput.To<TestState>();

            Assert.AreEqual(expectedValue, result);
        }

        [Test]
        public void DateReresentedByString_ConvertToDateTimeObject_Valid()
        {
            string testInput = "08/01/2015";

            var result = testInput.To<DateTime>();

            Assert.AreEqual(testInput, result.ToString("MM/dd/yyyy"));
        }

        [Test]
        public void EmptyString_ConvertToDateTimeMinValue_Valid()
        {
            string expectedOutput = DateTime.MinValue.ToString("MM/dd/yyyy");

            var result = string.Empty.To<DateTime>();

            Assert.AreEqual(expectedOutput, result.ToString("MM/dd/yyyy"));
        }

        [Test]
        public void DecimalString_ConvertToDouble_Valid()
        {
            double decimalExpectedValue = 12121.001d;
            string input = "12121.001";

            var result = input.To<double>(new FractionalNumericValueConverter<double>());

            Assert.AreEqual(decimalExpectedValue, result);
        }
    }
}
