using System.Collections.Generic;

namespace MasudaManager.Utility
{
    public static class SearchStringComparator
    {
        static string _leftOperand = null;
        static string _rightOperand = null;

        public static bool Compare(string targetString, string searchString, SearchOptionFlags searchOption = SearchOptionFlags.None)
        {
            return Compare(targetString, ExtractKeyWord(searchString), GetSearchMode(searchString), searchOption);
        }

        public static bool Compare(IEnumerable<string> rowValues, string searchString, SearchMode searchMode, SearchOptionFlags searchOption)
        {
            foreach(string value in rowValues)
            {
                if (Compare(value, searchString, searchMode, searchOption))
                    return true;
            }

            return false;
        }
          
        public static bool Compare(string targetString, string searchString, SearchMode searchMode, SearchOptionFlags searchOption)
        {
            if (searchOption.HasFlag(SearchOptionFlags.CaseSensitive))
            {
                _leftOperand = targetString;
                _rightOperand = searchString;
            }
            else
            {
                _leftOperand = targetString.ToStringOrEmpty().ToUpper();
                _rightOperand = searchString.ToStringOrEmpty().ToUpper();
            }

            switch (searchMode)
            {
                case SearchMode.Exact:
                    return _leftOperand == _rightOperand;
                case SearchMode.Partial:
                    return _leftOperand.Contains(_rightOperand);
                case SearchMode.Prefix:
                    return _leftOperand.StartsWith(_rightOperand);
                case SearchMode.Suffix:
                    return _leftOperand.EndsWith(_rightOperand);
            }

            return false;
        }

        static string ExtractKeyWord(string searchString)
        {
            if (searchString.StartsWith(Constants.StringAsterisk))
                return searchString.Substring(1, searchString.Length - 1);

            if (searchString.EndsWith(Constants.StringAsterisk))
                return searchString.Substring(0, searchString.Length - 1);

            return searchString;
        }

        static SearchMode GetSearchMode(string searchString)
        {
            if (searchString.StartsWith(Constants.StringAsterisk))
                return SearchMode.Suffix;

            if (searchString.EndsWith(Constants.StringAsterisk))
                return SearchMode.Prefix;

            return SearchMode.Partial;
        }
    }
}
