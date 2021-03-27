using MasudaManager.Utility.Preference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager
{
    public static class StringUtil
    {
        static readonly string _escapeSequence = "\"\"";
        static readonly string _encloseFormat = "\"{0}\"";

        public static string GetDoubleQuotedText(string text)
        {
            return Constants.StringDoubleQuotation + text + Constants.StringDoubleQuotation;
        }

        public static string ReplaceCrLf(string text, string replaceValue)
        {
            return text.Replace(Constants.StringNewLine, replaceValue).Replace(Constants.StringCarriageReturn, replaceValue);
        }

        public static string RemoveCrLf(string text)
        {
            return text.Replace(Constants.StringNewLine, string.Empty).Replace(Constants.StringCarriageReturn, string.Empty);

        }

        public static string GetEnclosedCsvFiledValue(string field)
        {
            if (UserPreference.Setting.File.EncloseCsvFields)
                return EncloseCsvFiledValue(field);
            else
                return EncloseCsvFieldValueIfSpeicalCharExists(field);
        }

        static string EncloseCsvFiledValue(string field)
        {
            if (field == null)
                return field;

            return String.Format(_encloseFormat, EncloseCsvFieldValueIfSpeicalCharExists(field));
        }

        static IEnumerable<string> EncloseCsvFiledValues(IEnumerable<string> fields)
        {
            return fields.Select(s => EncloseCsvFiledValue(s));
        }

        public static IEnumerable<string> GetCsvFiledValues(IEnumerable<string> fields)
        {
            return EncloseCsvFieldValuesIfSpeicalCharExists(fields);
        }

        static string EncloseCsvFieldValueIfSpeicalCharExists(string field)
        {
            if (field == null)
                return field;

            if (Constants.CsvSpecialCharacters.Any(field.Contains))
                return Constants.StringDoubleQuotation + GetCsvEscapedString(field) + Constants.StringDoubleQuotation;
            else
                return GetCsvEscapedString(field);
        }

        static IEnumerable<string> EncloseCsvFieldValuesIfSpeicalCharExists(IEnumerable<string> fields)
        {
            return fields.Select(s => EncloseCsvFieldValueIfSpeicalCharExists(s));
        }

        static string GetCsvEscapedString(string value)
        {
            return value.Replace(Constants.StringDoubleQuotation, _escapeSequence);
        }

        public static void PrintTime(string msg)
        {
            System.Diagnostics.Trace.WriteLine(msg + "=" + GetTime());
        }

        static string GetTime()
        {
            return DateTime.Now + "." + DateTime.Now.Millisecond;
        }
    }
}
