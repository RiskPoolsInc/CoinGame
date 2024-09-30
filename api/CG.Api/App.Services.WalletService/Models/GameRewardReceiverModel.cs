using System.Text.Json.Serialization;

namespace App.Services.WalletService.Models;

public class GameRewardReceiverModel {
    public string Address { get; set; }
    public decimal Sum { get; set; }
    public Guid GameId { get; set; }

    public GameRewardReceiverModel() {
    }

    public GameRewardReceiverModel(string address, decimal sum) {
        Address = address;
        Sum = sum;
    }
}