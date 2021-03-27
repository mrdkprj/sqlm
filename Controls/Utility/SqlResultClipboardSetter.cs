using MasudaManager.Utility.Preference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MasudaManager.Utility
{
    static class SqlResultClipboardSetter
    {
        static IEnumerable<SqlResult> _sqlResults;
        static int _startColumnIndex = 0;
        static int _copyColumnCount = 0;
        static int _startRowIndex = 0;
        static int _copyRowCount = 0;

        public static void Copy(CopyTargetType targetType, CellRange range, IEnumerable<SqlResult> sqlResults)
        {
            if (!sqlResults.Any())
                return;

            _startRowIndex = range.StartCell.RowIndex;
            _copyRowCount = range.RowCount;
            _startColumnIndex = range.StartCell.ColumnIndex;
            _copyColumnCount = range.ColumnCount;
            _sqlResults = sqlResults;

            switch (targetType)
            {
                case CopyTargetType.Text:
                    CopyText();
                    break;
                case CopyTargetType.Header:
                    CopyHeader();
                    break;
                case CopyTargetType.TextWithHeader:
                    CopyTextWithHeader();
                    break;
            }
        }

        public static void Copy(CopyTargetType targetType, IEnumerable<Cell> selectedCells, IEnumerable<SqlResult> sqlResults)
        {
            if (!sqlResults.Any())
                return;

            _startRowIndex = selectedCells.Min(cell => cell.RowIndex);
            _copyRowCount = (selectedCells.Max(cell => cell.RowIndex) - _startRowIndex) + 1;
            _startColumnIndex = selectedCells.Min(cell => cell.ColumnIndex);
            _copyColumnCount = (selectedCells.Max(cell => cell.ColumnIndex) - _startColumnIndex) + 1;
            _sqlResults = sqlResults;

            switch (targetType)
            {
                case CopyTargetType.Text:
                    CopyText();
                    break;
                case CopyTargetType.Header:
                    CopyHeader();
                    break;
                case CopyTargetType.TextWithHeader:
                    CopyTextWithHeader();
                    break;
            }
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

        static void CopyHeader()
        {
            try
            {
                Clipboard.SetText(String.Join(Constants.StringCarriageReturn, GetTsvHeaderList()));
            }
            catch
            {
            }
        }

        static void CopyTextWithHeader()
        {
            try
            {
                var combinedDataList = GetTsvHeaderList().Concat(GetTsvDataList());
                Clipboard.SetText(String.Join(Constants.StringCarriageReturn, combinedDataList));
            }
            catch
            {
            }
        }

        static IEnumerable<string> GetTsvDataList()
        {
            var tsvDataList = GetCopyData().Select(s => s.RowValues)
                                           .Select(s => s.GetRange(_startColumnIndex, _copyColumnCount))
                                           .Select(s => (String.Join(UserPreference.Reflector.CopyDataSeparator, StringUtil.GetCsvFiledValues(s))));

            return tsvDataList;
        }

        static IEnumerable<string> GetTsvHeaderList()
        {
            var tsvHeaderList = GetCopyData().Select(s => s.ColumnNames)
                                             .Select(s => s.GetRange(_startColumnIndex, _copyColumnCount))
                                             .Select(s => (String.Join(UserPreference.Reflector.CopyDataSeparator, StringUtil.GetCsvFiledValues(s))))
                                             .Take(1);

            return tsvHeaderList;
        }

        static IEnumerable<SqlResult> GetCopyData()
        {
            var copyData = _sqlResults.SkipWhile((set, index) => index < _startRowIndex)
                                      .TakeWhile((set, index) => index < _copyRowCount);

            return copyData;
        }
    }
}
