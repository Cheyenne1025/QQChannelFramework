using System.Collections.Generic;
using System.Threading.Tasks;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api;

sealed partial class QQChannelApi {
    public ForumApi GetForumApi() {
        return new(apiBase);
    }
}

public class ForumApi
{
    readonly ApiBase _apiBase;

    public ForumApi(ApiBase apiBase) {
        _apiBase = apiBase;
    }
    
    /// <summary>
    /// 获取论坛帖子详细
    /// </summary>
    /// <param name="childId">论坛子频道Id</param>
    /// <param name="threadId">论坛帖子Id</param>
    /// <returns></returns>
    public async ValueTask<Models.Forum.ThreadInfo> GetThreadDetailAsync(string childId,string threadId)
    {
        Raws.RawGetForumThreadsDetailApi rawGetForumThreadsDetailApi;

        var processedInfo = ApiFactory.Process(rawGetForumThreadsDetailApi, new Dictionary<ParamType, string>()
        {
            {
                ParamType.channel_id, childId
            }
            ,
            {
                ParamType.thread_id, threadId
            }
        });

        var requestData = await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);

        return requestData["thread"]?.ToObject<Models.Forum.ThreadInfo>();
    }

    /// <summary>
    /// 删除论坛帖子
    /// </summary>
    /// <param name="childId">论坛子频道Id</param>
    /// <param name="threadId">论坛帖子Id</param>
    public async ValueTask DeleteThreadAsync(string childId, string threadId)
    {
        Raws.RawDeleteForumThreadsApi rawDeleteForumThreadsApi;

        var processedInfo = ApiFactory.Process(rawDeleteForumThreadsApi, new Dictionary<ParamType, string>()
        {
            {
                ParamType.channel_id, childId
            }
            ,
            {
                ParamType.thread_id, threadId
            }
        });

        await _apiBase.RequestAsync(rawDeleteForumThreadsApi).ConfigureAwait(false);
    }
}