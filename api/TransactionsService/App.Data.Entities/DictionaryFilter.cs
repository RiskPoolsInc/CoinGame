using Newtonsoft.Json;

namespace App.Data.Entities {

public class DictionaryFilter
{
    [JsonProperty("selectedIds")]
    public int[] SelectedIds { get; set; }

    [JsonProperty("excludeMode")]
    public bool ExcludeMode { get; set; }
}
}