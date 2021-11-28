using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace QQChannelFramework.Tools.TemplateHelper;

sealed partial class MessageTemplateHelper
{
    TextAndThumbnailTemplateHelper _textAndThumbnailTemplateHelper;

    /// <summary>
    /// 获取文本+略缩图消息模版构建器实例
    /// </summary>
    /// <returns></returns>
    public TextAndThumbnailTemplateHelper GetTextAndThumbnailTemplateHelper()
    {
        _textAndThumbnailTemplateHelper = new();

        return _textAndThumbnailTemplateHelper;
    }
}

/// <summary>
/// 文本+略缩图消息模版构建类
/// </summary>
public class TextAndThumbnailTemplateHelper
{
    const string DescribeType = "#DESC#";
    const string PromptType = "#PROMPT#";
    const string ListType = "#LIST#";
    const string TitleType = "#TITLE#";
    const string MetaDescribeType = "#METADESC#";
    const string ImageType = "#IMG#";
    const string JumpType = "#LINK#";
    const string SubTitleType = "#SUBTITLE#";

    Dictionary<string, object> _dic;

    JObject _template;

    List<Dictionary<string, object>> _kvs;

    public TextAndThumbnailTemplateHelper()
    {
        _dic = new Dictionary<string, object>()
            {
            {"msg_id", string.Empty},
                {"ark",new Dictionary<string,object>()
                {
                    {"template_id",24 },
                    {"kv",new List<Dictionary<string,object>>()
                    {
                        new()
                        {
                            {"key",DescribeType },
                            {"value",string.Empty }
                        },
                        new()
                        {
                            {"key",PromptType },
                            {"value",string.Empty }
                        },
                        new()
                        {
                            {"key",TitleType },
                            {"value",string.Empty }
                        },
                        new()
                        {
                            {"key",MetaDescribeType },
                            {"value",string.Empty }
                        },
                        new()
                        {
                            {"key",ImageType },
                            {"value",string.Empty }
                        },
                        new()
                        {
                            {"key", JumpType},
                            {"value",string.Empty }
                        },
                        new()
                        {
                            {"key",SubTitleType },
                            {"value",string.Empty }
                        }
                    }
                    }
                }
                }
            };

        _template = JObject.Parse(JsonConvert.SerializeObject(_dic));
    }

    /// <summary>
    /// 设置描述
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public TextAndThumbnailTemplateHelper SetDescribe(string message)
    {
        _template["ark"]["kv"][0]["value"] = message;

        return this;
    }

    /// <summary>
    /// 携带回复的消息ID
    /// </summary>
    /// <param name="messageId"></param>
    /// <returns></returns>
    public TextAndThumbnailTemplateHelper WithReplyMessageId(string messageId)
    {
        _template["msg_id"] = messageId;

        return this;
    }

    /// <summary>
    /// 设置提示消息
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns></returns>
    public TextAndThumbnailTemplateHelper SetPrompt(string prompt)
    {
        _template["ark"]["kv"][1]["value"] = prompt;

        return this;
    }

    /// <summary>
    /// 设置主标题
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    public TextAndThumbnailTemplateHelper SetTitle(string title)
    {
        _template["ark"]["kv"][2]["value"] = title;

        return this;
    }

    /// <summary>
    /// 设置详情描述
    /// </summary>
    /// <param name="metaDescribe"></param>
    /// <returns></returns>
    public TextAndThumbnailTemplateHelper SetMetaDescribe(string metaDescribe)
    {
        _template["ark"]["kv"][3]["value"] = metaDescribe;

        return this;
    }

    /// <summary>
    /// 设置略缩图
    /// </summary>
    /// <param name="imgUrl">图片URL</param>
    /// <returns></returns>
    public TextAndThumbnailTemplateHelper SetImg(string imgUrl)
    {
        _template["ark"]["kv"][4]["value"] = imgUrl;

        return this;
    }

    /// <summary>
    /// 设置跳转链接
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public TextAndThumbnailTemplateHelper SetLink(string url)
    {
        _template["ark"]["kv"][5]["value"] = url;

        return this;
    }

    /// <summary>
    /// 设置子标题
    /// </summary>
    /// <param name="subTitle"></param>
    /// <returns></returns>
    public TextAndThumbnailTemplateHelper SetSubTitle(string subTitle)
    {
        _template["ark"]["kv"][6]["value"] = subTitle;

        return this;
    }

    /// <summary>
    /// 构建模版
    /// </summary>
    /// <returns></returns>
    public JObject BuildTemplate()
    {
        return _template;
    }
}