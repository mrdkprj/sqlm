using MasudaManager.Utility.Preference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MasudaManager.Utility
{
    static class QueryResultClipboardSetter
    {
        static readonly string _separator = Constants.StringTab;
        static IEnumerable<List<string>> _sqlResults;
        static int _startColumnIndex = 0;
        static int _copyColumnCount = 0;
        static int _startRowIndex = 0;
        static int _copyRowCount = 0;

        public static void Copy(CellRange range, IEnumerable<QueryResult> queryResults)
        {
            if (!queryResults.Any())
                return;

            _startRowIndex = range.StartCell.RowIndex;
            _copyRowCount = range.RowCount;
            _startColumnIndex = range.StartCell.ColumnIndex;
            _copyColumnCount = range.ColumnCount;
            _sqlResults = queryResults.Select(s => s.Values.ToList());

            CopyText();
        }
        
        public static void Copy(IEnumerable<Cell> selectedCells, IEnumerable<QueryResult> queryResults)
        {
            if (!queryResults.Any())
                return;

            _startRowIndex = selectedCells.Min(cell => cell.RowIndex);
            _copyRowCount = (selectedCells.Max(cell => cell.RowIndex) - _startRowIndex) + 1;
            _startColumnIndex = selectedCells.Min(cell => cell.ColumnIndex);
            _copyColumnCount = (selectedCells.Max(cell => cell.ColumnIndex) - _startColumnIndex) + 1;
            _sqlResults = queryResults.Select(s => s.Values.ToList());

            CopyText();
        }

        static void CopyText()
        {
            try
            {
                Clipboard.SetText(String.Join(Constants.StringCarriageReturn, GetTsvDataList()));
            }
            catch
            {
            }
        }

        static IEnumerable<string> GetTsvDataList()
        {
            var tsvDataList = GetCopyData().Select(s => s.GetRange(_startColumnIndex, _copyColumnCount))
                                           //.Select(s => (String.Join(_separator, s)));
                                           .Select(s => (String.Join(_separator, StringUtil.GetCsvFiledValues(s))));

            return tsvDataList;
        }

        static IEnumerable<List<string>> GetCopyData()
        {
            var copyData = _sqlResults.SkipWhile((str, index) => index < _startRowIndex)
                                      .TakeWhile((str, index) => index < _copyRowCount);

            return copyData;
        }


    }
}
