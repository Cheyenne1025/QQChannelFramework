using System.Collections.Generic;
using System.Threading.Tasks;
using MyBot.Api.Base;
using MyBot.Api.Raws;
using MyBot.Api.Types;
using MyBot.Models.MessageModels;
namespace MyBot.Api;

sealed partial class QQChannelApi {
    public MessageSettingApi GetMessageSettingApi() {
        return new(apiBase);
    }
}

/// <summary>
/// 频道消息频率Api
/// </summary>
public class MessageSettingApi
{
    readonly ApiBase _apiBase;

    public MessageSettingApi(ApiBase apiBase) {
        _apiBase = apiBase;
    }

    /// <summary>
    /// 获取频道消息频率设置
    /// </summary>
    /// <param name="guildID">主频道ID</param>
    /// <returns></returns>
    public async ValueTask<MessageSetting> GetSetting(string guildID)
    {
        RawGetMessageSettingInfoApi rawGetMessageSettingInfoApi;

        var processedInfo = ApiFactory.Process(rawGetMessageSettingInfoApi, new Dictionary<ParamType, string>()
        {
            [ParamType.guild_id] = guildID
        });

        var requestData =  await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);

        return requestData.ToObject<MessageSetting>();
    }
}
