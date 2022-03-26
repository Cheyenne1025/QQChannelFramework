using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Raws;
using QQChannelFramework.Api.Types;
using QQChannelFramework.Models;

namespace QQChannelFramework.Api;

sealed partial class QQChannelApi
{ 
    public ScheduleApi GetScheduleApi()
    { 
        return new(apiBase);
    }
}

/// <summary>
/// 日程Api
/// </summary>
public class ScheduleApi
{
    readonly ApiBase _apiBase;

    public ScheduleApi(ApiBase apiBase)
    {
        _apiBase = apiBase;
    }


    /// <summary>
    /// 获取指定 日程子频道 中指定结束时间后的当日活动日程
    /// </summary>
    /// <param name="channel">日程子频道Id</param>
    /// <param name="since">结束时间</param>
    /// <returns>元组 (日程集合，数量)</returns>
    public async Task<List<Schedule>> GetSchedulesAsync(string channel, DateTime since)
    {
        RawGetSchedulesApi rawGetSchedulesApi;

        var processedInfo = ApiFactory.Process(rawGetSchedulesApi, new Dictionary<ParamType, string>()
        {
            {ParamType.channel_id,channel }
        });

        JToken requestData;

        if (since != default)
        {
            requestData = await _apiBase
                .WithQueryParam(new Dictionary<string, object>()
                {
                    {"since", new DateTimeOffset(since).ToUnixTimeMilliseconds() }
                })
                .RequestAsync(processedInfo);
        }
        else
        {
            requestData = await _apiBase
                .WithQueryParam(new Dictionary<string, object>()
                {
                    {"since","" }
                })
                .RequestAsync(processedInfo);
        }

        List<Schedule> schedules = new();

        if(requestData is null)
        {
            return schedules;
        }

        var array = requestData as JArray;

        foreach (var info in array)
        {
            schedules.Add(info.ToObject<Schedule>());
        }

        return schedules;
    }

    /// <summary>
    /// 获取当日所有日程
    /// </summary>
    /// <param name="channel">日程子频道Id</param>
    /// <returns>元组 (日程集合，数量)</returns>
    public async Task<List<Schedule>> GetToDaySchedulesAsync(string channel)
    {
        return await GetSchedulesAsync(channel, default(DateTime));
    }

    /// <summary>
    /// 获取指定的日程信息
    /// </summary>
    /// <param name="channelId">子频道Id</param>
    /// <param name="scheduleId">日程ID</param>
    /// <returns></returns>
    public async Task<Schedule> GetScheduleInfoAsync(string channelId, string scheduleId)
    {
        RawGetScheduleApi rawGetScheduleApi;

        var processedInfo = ApiFactory.Process(rawGetScheduleApi, new Dictionary<ParamType, string>()
        {
            {ParamType.channel_id,channelId },
            {ParamType.schedule_id,scheduleId }
        });

        var requestData = await _apiBase.RequestAsync(processedInfo);

        return requestData.ToObject<Schedule>();
    }

    /// <summary>
    /// 创建日程
    /// </summary>
    /// /// <param name="channelId">日程子频道ID</param>
    /// <param name="newSchedule">不需要ID的新日程信息</param>
    /// <returns>创建的日程信息</returns>
    public async Task<Schedule> CreateAsync(string channelId,Schedule newSchedule)
    {
        RawCreateScheduleApi rawCreateScheduleApi;

        var processedInfo = ApiFactory.Process(rawCreateScheduleApi, new Dictionary<ParamType, string>()
        {
            {ParamType.channel_id,channelId }
        });

        if(newSchedule is not null)
        {
            newSchedule.Id = null;
        }

        var requestData = await _apiBase
            .WithContentData(new Dictionary<string, object>()
            {
                {"schedule",newSchedule }
            })
            .RequestAsync(processedInfo);

        return requestData.ToObject<Schedule>();
    }

    /// <summary>
    /// 更新日程
    /// </summary>
    /// <param name="channelId">日程所在子频道ID</param>
    /// <param name="scheduleId">日程ID</param>
    /// <param name="newSchedule"></param>
    /// <returns>修改后的日程信息</returns>
    public async Task<Schedule> UpdateAsync(string channelId, string scheduleId, Schedule newSchedule)
    {
        RawAddMemberToRoleApi rawAddMemberToRoleApi;

        var processedInfo = ApiFactory.Process(rawAddMemberToRoleApi, new Dictionary<ParamType, string>()
        {
            {ParamType.channel_id,channelId },
            {ParamType.schedule_id,scheduleId }
        });

        if(newSchedule.Id is not null)
        {
            newSchedule.Id = null;
        }

        var requestData = await _apiBase
            .WithContentData(new Dictionary<string, object>()
            {
                {"schedule",newSchedule }
            })
            .RequestAsync(processedInfo);


        return requestData.ToObject<Schedule>();
    }

    /// <summary>
    /// 删除日程
    /// </summary>
    /// <param name="channelId">日程所在子频道ID</param>
    /// <param name="scheduleId">日程ID</param>
    /// <returns></returns>
    public async ValueTask DeleteAsync(string channelId,string scheduleId)
    {
        RawDeleteScheduleApi rawDeleteScheduleApi;

        var processedInfo = ApiFactory.Process(rawDeleteScheduleApi, new Dictionary<ParamType, string>()
        {
            {ParamType.channel_id,channelId },
            {ParamType.schedule_id,scheduleId }
        });

        await _apiBase.RequestAsync(processedInfo);
    }
}