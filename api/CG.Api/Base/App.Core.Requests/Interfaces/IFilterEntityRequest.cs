namespace App.Core.Requests.Interfaces {
    public interface IFilterEntityRequest {
        public string FilterName { get; set; }
        public string SearchExpr { get; set; }
        public bool IncludeEmptyIfExists { get; set; }
    }
}