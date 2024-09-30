using System.Text.Json.Serialization;

namespace App.Services.WalletService.Models;

public class ReceiverCoinsModel {
    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("sum")]
    public decimal Sum { get; set; }

    public ReceiverCoinsModel() {
        
    }

    public ReceiverCoinsModel(string address, decimal sum) {
        Address = address;
        Sum = sum;
    }
}