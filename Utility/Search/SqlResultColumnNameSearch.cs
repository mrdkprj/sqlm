using System;
using System.Collections.Generic;
using System.Linq;

namespace MasudaManager.Utility
{
    public class SqlResultColumnNameSearch : IDataSearch
    {
        SearchBaseInfo _request = null;
        List<string> _columnNameList = null;
        Cell _startCell = Cell.Empty;
        readonly int _searchForwardTakeCount = 3;

        public void SetSearchTarget(IEnumerable<SqlResult> sqlResults)
        {
            if (sqlResults.First() == null)
                _columnNameList = new List<string>();
            else
                _columnNameList = sqlResults.First().ColumnNames;
        }

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
            _columnNameList = _request.Context.SqlResults.First().ColumnNames;
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

            if (searchRequest.Context.SqlResults.First().ColumnNames == null)
                return false;

            if (searchRequest.Context.SqlResults.First().ColumnNames.Count <= 0)
                return false;
            
            return true;
        }

        Cell SearchForward()
        {
            foreach (Cell cell in GetMatchedCells(_searchForwardTakeCount))
            {
                if (cell.ColumnIndex > _startCell.ColumnIndex)
                    return cell;
            }

            return _startCell;
        }

        Cell SearchBackward()
        {
            foreach (Cell cell in GetMatchedCells(_startCell.ColumnIndex).Reverse())
            {
                if (cell.ColumnIndex < _startCell.ColumnIndex)
                    return cell;
            }

            return _startCell;
        }

        IEnumerable<Cell> GetMatchedCells(int takeCount)
        {
            var searchedData = _columnNameList
                .Select((value, index) => new { index, value })
                .SkipWhile(c => CanSkip(c.index, _startCell.ColumnIndex, _request.Direction))
                .Where(s => SearchStringComparator.Compare(s.value, _request.SearchKey, _request.Mode, _request.Options))
                .Take(takeCount)
                .Select(s => new Cell(s.index, 0));

            return searchedData;
        }

        bool CanSkip(int currentIndex, int targetIndex, SearchDirection direction)
        {
            if (direction == SearchDirection.Forward)
                return CanSkipForward(currentIndex, targetIndex);
            else
                return CanSkipBackward(currentIndex, targetIndex);
        }

        bool CanSkipForward(int currentIndex, int targetIndex)
        {
            if (currentIndex < targetIndex)
                return true;

            return false;
        }

        bool CanSkipBackward(int currentIndex, int targetIndex)
        {
            if (currentIndex > targetIndex)
                return true;

            return false;
        }
    }
}
