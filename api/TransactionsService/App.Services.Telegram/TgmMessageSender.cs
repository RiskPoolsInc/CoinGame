using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using App.Services.Telegram.Interfaces;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace App.Services.Telegram; 

public class TgmMessageSender {
    private readonly string _token;

    public TgmMessageSender(string token) {
        _token = token;
    }

    public async Task<Message> Send(ITgmMessage payloadObj) {
        var client = new TelegramBotClient(_token);
        var chatId = new ChatId(payloadObj.ChatId);
        var response = await client.SendTextMessageAsync(chatId, payloadObj.Text, null, payloadObj.Entities);
        return response;
    }

    public async Task<string> Send(ITelegramPayload payloadObj, string url) {
        try {
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";

            var postData = JsonSerializer.Serialize((object)payloadObj);

            await using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream())) {
                await streamWriter.WriteAsync(postData);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = await streamReader.ReadToEndAsync();
            return result;
        }
        catch (WebException webExcp) {
            var message = $"A WebException has been caught: {webExcp}";
            var status = webExcp.Status;

            if (status == WebExceptionStatus.ProtocolError) {
                message += "____ The REST API server returned a protocol error:";
                var httpResponse = webExcp.Response as HttpWebResponse;
                var stream = httpResponse.GetResponseStream();
                var reader = new StreamReader(stream);
                var body = reader.ReadToEnd();
                message += (int)httpResponse.StatusCode + " - " + body;
            }

            throw new WebException(message);
        }
        catch (Exception e) {
            throw new Exception($"A general exception has been caught: {e}");
        }
    }
}