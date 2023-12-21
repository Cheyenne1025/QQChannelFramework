using System.Collections.Generic;
using System.Threading.Tasks;
using MyBot.Api.Base;
using MyBot.Api.Raws;
using MyBot.Api.Types;
using MyBot.Models.MessageModels;
namespace MyBot.Api;

sealed partial class QQChannelApi {
    public PinsApi GetPinsApi() {
        return new(apiBase);
    }
}

/// <summary>
/// 精华消息Api
/// </summary>
public class PinsApi {
    readonly ApiBase _apiBase;

    public PinsApi(ApiBase apiBase) {
        _apiBase = apiBase;
    }

    /// <summary>
    /// 添加精华消息
    /// </summary> 
    /// <returns></returns>
    public async Task<PinsMessage> PinMessageAsync(string channelId, string messageId) {
        RawPinMessageApi raw;

        var processedInfo = ApiFactory.Process(raw, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId},
            {ParamType.message_id, messageId}
        });

        var requestData = await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);

        return requestData.ToObject<PinsMessage>();
    }

    /// <summary>
    /// 删除精华消息
    /// </summary> 
    /// <returns></returns>
    public async Task UnPinMessageAsync(string channelId, string messageId) {
        RawUnPinMessageApi raw;

        var processedInfo = ApiFactory.Process(raw, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId},
            {ParamType.message_id, messageId}
        });

        await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false); 
    }

    /// <summary>
    /// 删除所有精华消息
    /// </summary> 
    /// <returns></returns>
    public async Task UnPinAllMessagesAsync(string channelId) {
        await UnPinMessageAsync(channelId, "all").ConfigureAwait(false);
    }

    /// <summary>
    /// 获取精华消息
    /// </summary> 
    /// <returns></returns>
    public async Task<PinsMessage> GetPinMessagesAsync(string channelId) {
        RawGetPinMessagesApi raw;

        var processedInfo = ApiFactory.Process(raw, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId}
        });

        var requestData = await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);

        return requestData.ToObject<PinsMessage>();
    }
}