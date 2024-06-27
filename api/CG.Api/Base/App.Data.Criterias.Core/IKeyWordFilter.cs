namespace App.Data.Criterias.Core {
    public interface IKeyWordFilter {
        public KeyWordFilter[]? KeyWords { get; set; }
    }
    
    public class KeyWordFilter {
        public string? KeyWord { get; set; }
        public string[]? KeyWordProperties { get; set; }
    }
}