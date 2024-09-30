using System.Text.Json.Serialization;

namespace App.Services.WalletService.Models;

public class GameRewardSentCoinsModel {
    public string Address { get; set; }
    public decimal Sum { get; set; }
    public Guid GameId { get; set; }

    public GameRewardSentCoinsModel() {
    }

    public GameRewardSentCoinsModel(string address, decimal sum) {
        Address = address;
        Sum = sum;
    }
}