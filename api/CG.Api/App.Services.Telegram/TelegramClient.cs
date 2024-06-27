using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using App.Services.Telegram.Interfaces;
using App.Services.Telegram.Options;
using App.Services.Telegram.User;

using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace App.Services.Telegram; 

public class TelegramClient {
    private readonly string _baseApiEndpoint = "https://api.telegram.org/bot";
    private readonly TgmMessageSender _messageSender;
    private readonly TelegramBotData _telegramBotData;
    private readonly TelegramOptions _telegramOptions;
    private readonly TgmMessageReceiver _tgmMessageReceiver;

    public TelegramClient(TelegramOptions telegramOptions) {
        _telegramOptions = telegramOptions;
        _telegramBotData = new TelegramBotData(telegramOptions.TelegramBotOptions.ClientSecret);
        _tgmMessageReceiver = new TgmMessageReceiver(_telegramBotData.ClientSecret);
        _messageSender = new TgmMessageSender(_telegramBotData.ClientSecret);
        _baseApiEndpoint += _telegramBotData.ClientSecret + "/";
    }

    private string SendMessageEndpoint => _baseApiEndpoint + "sendMessage";

    public async Task<Message> SendMessage(ITgmMessage payload) {
        var response = await _messageSender.Send(payload);
        return response;
    }

    public async Task<Update[]> GetMessages(int?                     offset            = default,
                                            int?                     limit             = default,
                                            int?                     timeout           = default,
                                            IEnumerable<UpdateType>? allowedUpdates    = default,
                                            CancellationToken        cancellationToken = default) {
        var response = await _tgmMessageReceiver.GetUpdates(offset, limit, timeout, allowedUpdates,
                                                            cancellationToken);
        return response;
    }
}