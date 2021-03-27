using System;
using System.Collections.Generic;
using System.Linq;

namespace MasudaManager.Utility
{
    public class EditResultPasteDataConverter
    {
        List<List<string>> _clipboardTextList = new List<List<string>>();
        CellRange _range;
        GridPasteData _pasteData = new GridPasteData();
        List<GridPasteData> _pasteDataList = new List<GridPasteData>();
        readonly int _defaultLoopCount = 1;

        public void Prepare(string clipboardText)
        {
            if (String.IsNullOrEmpty(clipboardText))
                _clipboardTextList.Clear();
            else
                PrepareClipboardTextList(clipboardText.Replace(Constants.CharCarriageReturn, Constants.CharNewLine));
        }

        public void Release()
        {
            _clipboardTextList.Clear();
            _pasteDataList.Clear();
        }

        void PrepareClipboardTextList(string clipboardText)
        {
            _clipboardTextList.Clear();

            IEnumerable<string> clipboardRowValus = clipboardText.Split(Constants.CharNewLine);

            foreach (var values in clipboardRowValus)
            {
                _clipboardTextList.Add(values.Split(Constants.CharTab).ToList());
            }
        }
        
        public IEnumerable<GridPasteData> Convert(CellRange range)
        {
            _pasteDataList = new List<GridPasteData>();

            if (_clipboardTextList.Count <= 0)
                return _pasteDataList;

            _range = range;

            int pasteEndRow = GetRowLoopCount();

            for (int rowCount = 0; rowCount < pasteEndRow; rowCount++)
            {
                _pasteDataList.AddRange(GetPasteDataList());
            }

            return _pasteDataList;
        }

        IEnumerable<GridPasteData> GetPasteDataList()
        {
            for (int recordIndex = 0; recordIndex < _clipboardTextList.Count; recordIndex++)
            {
                yield return GetPasteData(recordIndex, GetCurrentRowIndex(recordIndex));
            }
        }
        
        GridPasteData GetPasteData(int recordIndex, int currentRowIndex)
        {
            _pasteData = new GridPasteData();

            int columnLoopCount = GetColumnLoopCount();

            for (int loop = 0; loop < columnLoopCount; loop++)
            {
                AddRowValues(recordIndex, currentRowIndex, GetCurrentColumnIndex());
            }

            return _pasteData;
        }

        void AddRowValues(int recordIndex, int currentRowIndex, int currentColumnIndex)
        {
            for (int valueIndex = 0; valueIndex < _clipboardTextList[recordIndex].Count; valueIndex++)
            {
                if (CanPaste(valueIndex, recordIndex))
                {
                    _pasteData.RowValues.Add(_clipboardTextList[recordIndex][valueIndex].ToStringOrNull());
                    _pasteData.CellPositions.Add(new Cell(currentColumnIndex + valueIndex, currentRowIndex));
                }
            }
        }

        bool CanPaste(int valueIndex, int recordIndex)
        {
            if (valueIndex >= GetPastableColumnCount() || recordIndex >= GetPastableRowCount())
                return false;

            return true;
        }
        
        int GetCurrentRowIndex(int currentRecordIndex)
        {
            return _range.StartCell.RowIndex + _pasteDataList.Count;
        }

        int GetCurrentColumnIndex()
        {
            return _range.StartCell.ColumnIndex + _pasteData.RowValues.Count;
        }
        
        int GetPastableColumnCount()
        {
            return _range.ColumnCount - _range.StartCell.ColumnIndex;
        }

        int GetPastableRowCount()
        {
            return _range.RowCount - _range.StartCell.RowIndex;
        }

        int GetRowLoopCount()
        {
            int selectedRowCount = _range.EndCell.RowIndex - _range.StartCell.RowIndex + 1;

            if (selectedRowCount % _clipboardTextList.Count == 0)
                return selectedRowCount / _clipboardTextList.Count;
            else
                return _defaultLoopCount;
        }

        int GetColumnLoopCount()
        {
            int selectedColumnCount = _range.EndCell.ColumnIndex - _range.StartCell.ColumnIndex + 1;

            if (selectedColumnCount % _clipboardTextList[0].Count == 0)
                return selectedColumnCount / _clipboardTextList[0].Count;
            else
                return _defaultLoopCount;
        }
    }
}
