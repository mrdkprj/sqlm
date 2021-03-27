using MasudaManager.Utility;
using MasudaManager.Views;
using System;
using System.Linq;
using WinFormsMvp;
using WinFormsMvp.Binder;
using WinFormsMvp.Messaging;

namespace MasudaManager.Presenters
{
    public class SearchGridPresenter : Presenter<ISearchGridView>
    {
        IDataSearch _resultSearch = new SqlResultSearch();
        IDataSearch _headerSearch = new SqlResultColumnNameSearch();
        SearchViewRequestData _searchViewRequestData = new SearchViewRequestData();
        SearchBaseInfo _searchRequestInfo = new SearchBaseInfo();
        Cell _matchedCell = Cell.Empty;

        public SearchGridPresenter(ISearchGridView view)
            : base(view)
        {
            View.Model = new SearchGridModel();

            RegisterHandlers();
            RegisterMessages();
        }

        #region EventHandlers
        void RegisterHandlers()
        {
            View.Initiated += View_Initiated;
            View.DisposeDialogRequested += View_DisposeDialogRequested;
            View.SearchForwardButtonClicked += View_SearchForwardButtonClicked;
            View.SearchBackwardButtonClicked += View_SearchBackwardButtonClicked;
        }

        void View_Initiated(object sender, ShowSearchGridEventArgs e)
        {
            _searchViewRequestData = e.SearchViewRequestData;
            View.ShowModeless();
        }

        void View_DisposeDialogRequested(object sender, EventArgs e)
        {
            View.Model.ReleaseModel();
        }

        void View_SearchBackwardButtonClicked(object sender, EventArgs e)
        {
            ExecuteSearch(SearchDirection.Backward);
        }

        void View_SearchForwardButtonClicked(object sender, EventArgs e)
        {
            ExecuteSearch(SearchDirection.Forward);
        }
        #endregion

        #region Messages
        void RegisterMessages()
        {
            PresenterBinder.MessageBus.Register(this, PresenterTokens.ExecuteSearchToken
                , new Action<GenericMessage<SearchViewRequestData>>(OnExecuteSearchRequested)); 
            
            PresenterBinder.MessageBus.Register(this, PresenterTokens.SearchContextPreparedToken
                , new Action<GenericMessage<SearchContext>>(OnSearchContextPrepared));
        }

        void RequestSearchContext()
        {
            _searchViewRequestData.CallBackActions.OnSearchContextRequest.Invoke();
        }

        void NotifySearchComplete(Cell matchedCell)
        {
            _searchViewRequestData.CallBackActions.OnSearchComplete.Invoke(matchedCell);
        }

        void OnExecuteSearchRequested(GenericMessage<SearchViewRequestData> message)
        {
            _searchViewRequestData = message.Content;
            ExecuteSearch(_searchViewRequestData.SearchDirection);
        }
        
        void OnSearchContextPrepared(GenericMessage<SearchContext> message)
        {
            View.Model.SearchContext = message.Content;
        }
        #endregion
        
        #region Methods
        void ExecuteSearch(SearchDirection direction)
        {
            RequestSearchContext();

            if (View.Model.SearchContext == null)
                return;

            PrepareSearchRequestInfo(direction);

            if (_searchRequestInfo.Options.HasFlag(SearchOptionFlags.SearchHeader))
                SearchColumnNames();
            else
                SearchSqlResults();
        }

        void SearchSqlResults()
        {
            _matchedCell = _resultSearch.Search(_searchRequestInfo);

            OnSearchComplete();
        }

        void SearchColumnNames()
        {
            _matchedCell = _headerSearch.Search(_searchRequestInfo);

            OnSearchComplete();
        }

        void PrepareSearchRequestInfo(SearchDirection direction)
        {
            _searchRequestInfo = new SearchBaseInfo();
            _searchRequestInfo.SearchKey = View.GetSearchString();
            _searchRequestInfo.Context = View.Model.SearchContext;
            _searchRequestInfo.Direction = direction;
            _searchRequestInfo.Mode = View.SearchMode;
            _searchRequestInfo.Options = View.GetSearchOption();
        }

        void OnSearchComplete()
        {
            View.AddSearchedString(_searchRequestInfo.SearchKey);

            if (_searchRequestInfo.Options.HasFlag(SearchOptionFlags.CloseDialog))
                View.HideView();

            if (_matchedCell == Cell.Empty)
                return;

            NotifySearchComplete(_matchedCell);
        }
        #endregion
    }
}
