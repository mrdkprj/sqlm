using System;
using System.Collections.Generic;
using System.Linq;

namespace MasudaManager.Utility
{
    public class SqlResultSearch : IDataSearch
    {
        SearchBaseInfo _request = new SearchBaseInfo();
        IEnumerable<IEnumerable<string>> _rowValues = Enumerable.Empty<IEnumerable<string>>();
        Cell _startCell = Cell.Empty;
        readonly int _searchForwardTakeCount = 3;

        public Cell Search(string searchKey, SearchContext context, SearchDirection direction)
        {
            SearchBaseInfo request = new SearchBaseInfo();
            request.SearchKey = searchKey;
            request.Context = context;
            request.Direction = direction;
            request.Mode = SearchMode.Partial;
            request.Options = SearchOptionFlags.None;

            return Search(request);
        }

        public Cell Search(string searchKey, SearchContext context, SearchDirection direction, SearchMode mode, SearchOptionFlags option = SearchOptionFlags.None)
        {
            SearchBaseInfo request = new SearchBaseInfo();
            request.SearchKey = searchKey;
            request.Context = context;
            request.Direction = direction;
            request.Mode = mode;
            request.Options = option;

            return Search(request);
        }

        public Cell Search(SearchBaseInfo searchRequest)
        {
            if (!CanSearchData(searchRequest))
                return searchRequest.Context.CurrentCell; 
            
            _request = searchRequest;
            _rowValues = _request.Context.SqlResults.Select(s => s.RowValues);            
            _startCell = _request.Context.CurrentCell;
            
            if (_request.Direction == SearchDirection.Forward)
                return SearchForward();
            else
                return SearchBackward();
        }

        bool CanSearchData(SearchBaseInfo searchRequest)
        {
            if (String.IsNullOrEmpty(searchRequest.SearchKey))
                return false;

            if (searchRequest.Context.CurrentCell == Cell.Empty)
                return false;

            if (searchRequest.Context.SqlResults == null)
                return false;

            if (!searchRequest.Context.SqlResults.Any())
                return false;

            if (searchRequest.Context.SqlResults.First().RowValues == null)
                return false; 
            
            return true;
        }

        Cell SearchForward()
        {
            foreach (var rowValues in GetRowValuesHavingKeyWord(_searchForwardTakeCount))
            {
                foreach (Cell cell in GetMatchedCells(rowValues))
                {
                    if (IsValidMatchedCell(SearchDirection.Forward, cell))
                        return cell;
                }
            }

            return _startCell;
        }

        Cell SearchBackward()
        {
            int takeCount = _startCell.RowIndex + 1;

            foreach (var rowValues in GetRowValuesHavingKeyWord(takeCount).Reverse())
            {
                foreach (Cell cell in GetMatchedCells(rowValues).Reverse())
                {
                    if (IsValidMatchedCell(SearchDirection.Backward, cell))
                        return cell;
                }
            }

            return _startCell;
        }

        IDictionary<int, IEnumerable<string>> GetRowValuesHavingKeyWord(int takeCount)
        {
            var rowIndexValuePairs = _rowValues
                .Select((values, index) => new { index, values })
                .SkipWhile(s => CanSkip(s.index, _startCell.RowIndex - 1, _request.Direction))
                .Where(s => SearchStringComparator.Compare(s.values, _request.SearchKey, _request.Mode, _request.Options))
                .Take(takeCount)
                .ToDictionary(s => s.index, s => s.values);

            return rowIndexValuePairs;
        }

        IEnumerable<Cell> GetMatchedCells(KeyValuePair<int, IEnumerable<string>> indexRowValuesPair)
        {
            var searchedData = indexRowValuesPair.Value
               .Select((value, index) => new { index, value })
               .Where(s => SearchStringComparator.Compare(s.value, _request.SearchKey, _request.Mode, _request.Options))
               .Select(s => new Cell(s.index, indexRowValuesPair.Key));

            return searchedData;
        }

        bool CanSkip(int currentIndex, int targetIndex, SearchDirection direction)
        {
            if (direction == SearchDirection.Forward && currentIndex < targetIndex)
                    return true;

            return false;
        }

        bool IsValidMatchedCell(SearchDirection direction, Cell cell)
        {
            if (direction == SearchDirection.Forward)
                return IsValidMatchedCellInForwardSearch(cell);
            else
                return IsValidMatchedCellInBackwardSearch(cell);
        }

        bool IsValidMatchedCellInForwardSearch(Cell cell)
        {
            if (cell.RowIndex == _startCell.RowIndex && cell.ColumnIndex > _startCell.ColumnIndex)
                return true;

            if (cell.RowIndex > _startCell.RowIndex)
                return true;

            return false;
        }

        bool IsValidMatchedCellInBackwardSearch(Cell cell)
        {
            if (cell.RowIndex == _startCell.RowIndex && cell.ColumnIndex < _startCell.ColumnIndex)
                return true;

            if (cell.RowIndex < _startCell.RowIndex)
                return true;

            return false;
        }
    }
}
