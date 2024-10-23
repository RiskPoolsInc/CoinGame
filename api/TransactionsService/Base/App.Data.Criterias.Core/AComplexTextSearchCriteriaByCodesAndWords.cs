namespace App.Data.Criterias.Core;

public abstract class AComplexTextSearchCriteriaByCodesAndWords<TEntity> : ACriteria<TEntity> where TEntity : class {
    public AComplexTextSearchCriteriaByCodesAndWords(string searchPhrase) {
        ParseSearchPhrase(searchPhrase);
    }

    protected string[] Codes { get; private set; }
    protected string[] Words { get; private set; }

    public void ParseSearchPhrase(string searchPhrase) {
        var phrase = searchPhrase?.Trim() ?? throw new ArgumentNullException(nameof(searchPhrase));

        var parts = phrase.Split(' ', ',', '.')
                          .Distinct()
                          .Where(w => !string.IsNullOrWhiteSpace(w))
                          .Select(w => new { isDigit = w.All(char.IsDigit), word = w });

        Codes = parts.Where(w => w.isDigit).Select(w => w.word).ToArray();
        Words = parts.Where(w => !w.isDigit).Select(w => w.word).ToArray();
    }
}