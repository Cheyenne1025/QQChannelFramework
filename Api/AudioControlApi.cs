using System.Collections.Generic;
using System.Threading.Tasks;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Types;
using QQChannelFramework.Models.AudioModels;

namespace QQChannelFramework.Api;

sealed partial class QQChannelApi {
    public AudioControlApi GetAudioControlApi() {
        return new(apiBase);
    }
}

public class AudioControlApi
{
    readonly ApiBase _apiBase;

    public AudioControlApi(ApiBase apiBase) {
        _apiBase = apiBase;
    }
    
    /// <summary>
    /// 音频控制
    /// </summary>
    /// <param name="childId">子频道Id</param>
    /// <param name="controlData">控制数据</param>
    public async ValueTask Control(string childId,AudioControl controlData)
    {
        Raws.RawAudioControlApi rawAudioControlApi;

        var processedInfo = ApiFactory.Process(rawAudioControlApi, new Dictionary<ParamType, string>()
        {
            {
                ParamType.channel_id, childId
            }
        });

        await _apiBase.WithJsonContentData(controlData).RequestAsync(processedInfo).ConfigureAwait(false);
    }
}
