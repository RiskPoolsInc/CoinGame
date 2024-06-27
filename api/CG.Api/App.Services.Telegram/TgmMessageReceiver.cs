using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace App.Services.Telegram; 

public class TgmMessageReceiver {
    private readonly string _token;

    public TgmMessageReceiver(string token) {
        _token = token;
    }

    public async Task<Update[]> GetUpdates(int?                     offset            = default,
                                           int?                     limit             = default,
                                           int?                     timeout           = default,
                                           IEnumerable<UpdateType>? allowedUpdates    = default,
                                           CancellationToken        cancellationToken = default) {
        var client = new TelegramBotClient(_token);

        var response = await client.GetUpdatesAsync(offset, limit, timeout, allowedUpdates,
                                                    cancellationToken);
        return response;
    }
}