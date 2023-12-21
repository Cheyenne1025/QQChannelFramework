using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBot.Api.Base;
using MyBot.Api.Types;
using MyBot.Models.Forum;
using MyBot.Models.Forum.Contents;
using MyBot.Tools;
namespace MyBot.Api;

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
    /// 获取指定子频道帖子列表
    /// </summary>
    /// <param name="childId">论坛子频道Id</param>
    /// <returns>元组(帖子列表，是否拉取完毕)</returns>
    public async ValueTask<(List<Thread>, bool)> GetThreadsAsync(string childId)
    {
        Raws.RawGetForumThreadsApi rawGetForumThreadsApi;

        var processedInfo = ApiFactory.Process(rawGetForumThreadsApi, new Dictionary<ParamType, string>()
        {
            {ParamType.channel_id, childId}
        });

        var requestData = await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);

        var arr = requestData["threads"].ToArray();
        var finish = requestData["is_finish"].ToString();

        List<Thread> threads = new List<Thread>();

        foreach (var threadObj in arr)
        {
            threads.Add(threadObj.ToObject<Thread>());
        }

        return (threads, finish is "1");
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

        await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);
    }

    /// <summary>
    /// 发布帖子
    /// </summary>
    /// <param name="title">帖子标题</param>
    /// <param name="childId">论坛子频道Id</param>
    /// <param name="contentData">帖子内容</param>
    /// <returns>元组 (帖子任务ID,发帖时间)</returns>
    public async ValueTask<(string, DateTime)> Publish(string title, string childId, ThreadContent contentData)
    {
        Raws.RawPublishForumThreads rawPublishForumThreads;

        var processedInfo = ApiFactory.Process(rawPublishForumThreads, new Dictionary<ParamType, string>()
        {
            {
                ParamType.channel_id, childId
            }
        });

        var requestData = await _apiBase.WithJsonContentData(new Dictionary<string, object>()
        {
            ["title"] = title, ["content"] = contentData.Content, ["format"] = contentData.Type
        }).RequestAsync(processedInfo).ConfigureAwait(false);

        return (requestData["task_id"].ToString(), ConvertHelper.GetDateTime(long.Parse(requestData["create_time"].ToString())));
    }
}